using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebScrapping.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using static ClosedXML.Excel.XLPredefinedFormat;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualBasic;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace WebScrapping
{
    public partial class Form1 : Form
    {
        private readonly string _callListItemsPath = "callListItems.json";
        private readonly string _contactListItemsPath = "contactListItems_{0}.json";
        private readonly string _callListItemsDetailPath = "callListItemsDetail_{0}_{1}.json";

        private ContactListItem contactListItemSelected = null;

        public Form1()
        {
            InitializeComponent();
        }

        #region Form Events

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCallListItemsToComboBox(_callListItemsPath);

            if (comboBox1.SelectedIndex >= 0)
            {
                CallListItem selectedItem = (CallListItem)comboBox1.SelectedItem;
                var path = string.Format(_contactListItemsPath, selectedItem.Id);
                LoadCallListItemsToDataGrid(path);

                //Tab Behaviour
                tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<CallListItem> callListItems = ExtractCallListItems();
            SaveCallListItemsToJson(callListItems, _callListItemsPath);
            LoadCallListItemsToComboBox(_callListItemsPath);
            CreateTabsForCallListItems(callListItems);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Reset Second DataGridView
            contactListItemSelected = null;
            CreateDetailDataTable(items: new List<DetailListItem>());


            CallListItem selectedItem = (CallListItem)comboBox1.SelectedItem;

            //Reset First DataGridView
            var path = string.Format(_contactListItemsPath, selectedItem.Id);
            LoadCallListItemsToDataGrid(path);

            // Seleccionar la pestaña correspondiente
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text == selectedItem.Text)
                {
                    tabControl1.SelectedTab = tabPage;
                    break;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar una lista de llamadas para importar los contactos primarios", "Importar de Buildout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Obtener el elemento seleccionado en el ComboBox
            CallListItem selectedItem = (CallListItem)comboBox1.SelectedItem;
            var path = string.Format(_contactListItemsPath, selectedItem.Id);

            List<ContactListItem> contactListItems = ExtractCallListContacts(selectedItem);
            SaveContactListItemsToJson(contactListItems, path);
            LoadCallListItemsToDataGrid(path);
            CreateDataTable(contactListItems);
        }
        private void button6_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                var result = MessageBox.Show($"¿Está seguro que desea importar todos los contactos de cada contacto primario? ({dataGridView1.Rows.Count} contactos primarios)", "Importar de Buildout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var item = new ContactListItem
                        {
                            Id = (string)row.Cells["Id"].Value,
                            Text = (string)row.Cells["Primary Contact"].Value,
                            ParentId = (string)row.Cells["ParentId"].Value,
                            Href = (string)row.Cells["Href"].Value
                        };

                        var path = string.Format(_callListItemsDetailPath, item.ParentId, item.Id);
                        List<DetailListItem> detailListItems = ExtractCallListContactsDetail(item);
                        SaveDetailListItemsToJson(detailListItems, path);
                    }
                    MessageBox.Show("Datos importados exitosamente", "Importar de Buildout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else { MessageBox.Show("Debe existir minimo un contacto primario para importar todos los datos", "Importar de Buildout", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (contactListItemSelected != null)
            {
                var path = string.Format(_callListItemsDetailPath, contactListItemSelected.ParentId, contactListItemSelected.Id);
                List<DetailListItem> detailListItems = ExtractCallListContactsDetail(contactListItemSelected);
                SaveDetailListItemsToJson(detailListItems, path);
                LoadDetailsInSecondDataGridView(path);
                CreateDetailDataTable(detailListItems);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un contacto primario para exportar los datos", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private List<CallListItem> ExtractCallListItems()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            List<CallListItem> callListItems = new List<CallListItem>();

            try
            {
                // Navega a la página de login y realiza el login
                PerformLogin(driver);

                // Navegar a la vista de Call Lists
                NavigateToCallListsView(driver);

                // Cambiar el contexto al iframe de la lista de llamadas
                SwitchToCallListsFrame(driver, "call_lists_paginated_list");

                // Scroll hasta el final de la página para cargar todos los elementos
                ScrollToLoadAllItems(driver, callListItems);

                // Cambiar de nuevo el contexto al contenido principal
                driver.SwitchTo().DefaultContent();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Cierra el navegador
                driver.Quit();
            }

            return callListItems.OrderBy(item => item.Text).Distinct().ToList();
        }

        private void PerformLogin(IWebDriver driver)
        {
            string loginUrl = "https://buildout.com/connect/login";
            driver.Navigate().GoToUrl(loginUrl);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("login")));

            var emailField = driver.FindElement(By.Id("login"));
            emailField.SendKeys("renzo@solarunion.com");

            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys("SUMiraflores01#!");

            var loginButton = driver.FindElement(By.Id("login_button"));
            loginButton.Click();

            wait.Until(d => d.FindElement(By.CssSelector("a[href='/connect/call_lists']")));
        }

        private void NavigateToCallListsView(IWebDriver driver)
        {
            var callListsLink = driver.FindElement(By.CssSelector("a[href='/connect/call_lists']"));
            callListsLink.Click();
            System.Threading.Thread.Sleep(5000);
        }

        private void SwitchToCallListsFrame(IWebDriver driver, string waitElementId, string id = "")
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector($"iframe[src='https://prospect.buildout.com/call_lists{id}']")));

            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector($"iframe[src='https://prospect.buildout.com/call_lists{id}']")));

            wait.Until(d => d.FindElement(By.Id(waitElementId)));
        }

        private void ScrollToLoadAllItems(IWebDriver driver, List<CallListItem>? callListItems)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var turboFrames = new List<IWebElement>();
            int previousCount;
            do
            {
                previousCount = turboFrames.Count;
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                System.Threading.Thread.Sleep(2000);

                turboFrames = new List<IWebElement>(driver.FindElements(By.CssSelector("#call_lists_paginated_list turbo-frame")));

            } while (turboFrames.Count > previousCount);

            var currentDateTime = System.DateTime.Now;

            if (callListItems != null)
            {
                foreach (var frame in turboFrames)
                {
                    var link = frame.FindElement(By.CssSelector("a.card-title-link"));
                    string text = link.Text;
                    string href = link.GetAttribute("href");

                    // Convertir el URL absoluto a relativo si es necesario
                    Uri baseUri = new Uri("https://prospect.buildout.com");
                    Uri hrefUri = new Uri(href);
                    string relativeHref = baseUri.MakeRelativeUri(hrefUri).ToString();

                    string id = relativeHref.Replace("call_lists/", string.Empty);

                    callListItems.Add(new CallListItem { Id = id.Trim(), Text = text, Href = relativeHref, ExtractDateTime = currentDateTime });
                }
            }
        }

        private void SaveCallListItemsToJson(List<CallListItem> callListItems, string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(callListItems, Formatting.Indented));
        }

        private void LoadCallListItemsToComboBox(string filePath)
        {
            List<CallListItem> callListItems;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                callListItems = JsonConvert.DeserializeObject<List<CallListItem>>(json);
            }
            else
            {
                callListItems = new List<CallListItem>();
                SaveCallListItemsToJson(callListItems, filePath); // Crear un archivo vacío
            }

            comboBox1.DisplayMember = "Text";
            comboBox1.DataSource = callListItems;

            CreateTabsForCallListItems(callListItems);
        }

        private void CreateTabsForCallListItems(List<CallListItem> callListItems)
        {
            tabControl1.TabPages.Clear();
            if (callListItems != null)
            {
                foreach (var item in callListItems)
                {
                    TabPage tabPage = new TabPage(item.Text);
                    tabControl1.TabPages.Add(tabPage);
                }
            }
        }

        private void ScrollToLoadAllContactItems(IWebDriver driver, List<ContactListItem>? contactListItem, string callListId)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var turboFrames = new List<IWebElement>();
            int previousCount;
            do
            {
                previousCount = turboFrames.Count;
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                System.Threading.Thread.Sleep(3000);

                turboFrames = new List<IWebElement>(driver.FindElements(By.CssSelector($"#table_call_list_{callListId} tr")));
            } while (turboFrames.Count > previousCount);

            var currentDateTime = System.DateTime.Now;

            if (contactListItem != null)
            {
                foreach (var frame in turboFrames)
                {
                    try
                    {

                        string paragraphText = "";
                        try
                        {
                            var aText = frame.FindElement(By.CssSelector("a.clickable"));
                            paragraphText = aText.Text;
                        }
                        catch
                        {
                        }


                        var id = frame.GetAttribute("id");
                        var idFormated = id.Replace("call_list_item_", string.Empty);

                        var a = frame.FindElement(By.CssSelector("a.btn-icon"));
                        var href = a.GetAttribute("href");
                        // Convertir el URL absoluto a relativo si es necesario
                        Uri baseUri = new Uri("https://prospect.buildout.com");
                        Uri hrefUri = new Uri(href);
                        string relativeHref = baseUri.MakeRelativeUri(hrefUri).ToString();

                        contactListItem.Add(new ContactListItem
                        {
                            Text = paragraphText,
                            Id = idFormated,
                            Href = relativeHref,
                            ExtractDateTime = currentDateTime,
                            ParentId = callListId
                        });
                    }
                    catch
                    {
                        Console.WriteLine("No se encontró el elemento");
                    }
                }
            }
        }

        private List<ContactListItem> ExtractCallListContacts(CallListItem selectedItem)
        {
            List<ContactListItem> contactListItems = new List<ContactListItem>();
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                // Navega a la página de login y realiza el login
                PerformLogin(driver);

                // Navegar a la vista de Call Lists
                NavigateToHrefAfterLogin(driver, $"https://buildout.com/connect/{selectedItem.Href}", $"#main_content");

                // Cambiar el contexto al iframe de la lista de contactos
                SwitchToCallListsFrame(driver, $"table_call_list_{selectedItem.Id}", $"/{selectedItem.Id}");

                // Scroll hasta el final de la página para cargar todos los elementos
                ScrollToLoadAllContactItems(driver, contactListItems, selectedItem.Id);

                // Cambiar de nuevo el contexto al contenido principal
                driver.SwitchTo().DefaultContent();

            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Cierra el navegador
                driver.Quit();
            }

            return contactListItems.ToList();
        }

        private void SaveContactListItemsToJson(List<ContactListItem> contactListItems, string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(contactListItems, Formatting.Indented));
        }

        private void LoadCallListItemsToDataGrid(string filePath)
        {
            List<ContactListItem> contactListItems;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                contactListItems = JsonConvert.DeserializeObject<List<ContactListItem>>(json);
            }
            else
            {
                contactListItems = new List<ContactListItem>();
                SaveContactListItemsToJson(contactListItems, filePath); // Crear un archivo vacío
            }

            CreateDataTable(contactListItems);
        }

        private void CreateDataTable(List<ContactListItem> items)
        {
            // Obtener el objeto ContactListItem correspondiente al tab seleccionado
            int tabIndex = tabControl1.SelectedIndex;

            DataTable dataTable = new DataTable();
            if (tabIndex >= 0)
            {
                dataTable.Columns.Add("Id", typeof(string));
                dataTable.Columns.Add("Primary Contact", typeof(string));
                dataTable.Columns.Add("Href", typeof(string));
                dataTable.Columns.Add("ParentId", typeof(string));

                // Agregar la fila con los datos del objeto
                foreach (var item in items)
                {
                    dataTable.Rows.Add(item.Id, item.Text, item.Href, item.ParentId);
                }

                // Asignar el DataTable a un control DataGridView para mostrarlo
                dataGridView1.DataSource = dataTable;

                // Hacer que las columnas "Id" y "Href" no sean visibles
                dataGridView1.Columns["ParentId"].Visible = false;
                dataGridView1.Columns["Href"].Visible = false;


                dataGridView1.CellClick += dataGridView1_CellClick;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que la columna sea la de "Acciones" y que no sea el encabezado
            if (e.RowIndex >= 0)
            {
                // Obtén los datos de la fila seleccionada
                string id = (string)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                string nombre = (string)dataGridView1.Rows[e.RowIndex].Cells["Primary Contact"].Value;
                string parentId = (string)dataGridView1.Rows[e.RowIndex].Cells["ParentId"].Value;
                string href = (string)dataGridView1.Rows[e.RowIndex].Cells["Href"].Value;

                contactListItemSelected = new ContactListItem
                {
                    Id = id,
                    Text = nombre,
                    ParentId = parentId,
                    Href = href
                };

                // Aquí puedes cargar los datos en el segundo DataGridView
                LoadDetailsInSecondDataGridView(string.Format(_callListItemsDetailPath, parentId, id));
            }
        }

        private void SaveDetailListItemsToJson(List<DetailListItem> list, string filePath)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(list, Formatting.Indented));
        }

        private void LoadDetailsInSecondDataGridView(string filePath)
        {
            List<DetailListItem> detailListItems;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                detailListItems = JsonConvert.DeserializeObject<List<DetailListItem>>(json);
            }
            else
            {
                detailListItems = new List<DetailListItem>();
                SaveDetailListItemsToJson(detailListItems, filePath); // Crear un archivo vacío
            }

            CreateDetailDataTable(detailListItems);
        }

        private void CreateDetailDataTable(List<DetailListItem> items)
        {
            // Obtener el objeto ContactListItem correspondiente al tab seleccionado
            int tabIndex = tabControl1.SelectedIndex;

            DataTable dataTable = new DataTable();
            if (tabIndex >= 0)
            {
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Email", typeof(string));
                dataTable.Columns.Add("Phone", typeof(string));
                dataTable.Columns.Add("Address", typeof(string));

                // Agregar la fila con los datos del objeto
                foreach (var item in items)
                {
                    dataTable.Rows.Add(item.Name, item.Email, item.Phone, item.Address);
                }

                dataGridView2.DataSource = dataTable;
            }
        }

        public void NavigateToHrefAfterLogin(IWebDriver driver, string href, string waitElement)
        {
            // Navega a la URL almacenada en href
            driver.Navigate().GoToUrl(href);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector($"{waitElement}")));
        }
        private void SwitchToDetailFrame(IWebDriver driver, string waitElementId, string href)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector($"iframe[src='https://prospect.buildout.com/{href}']")));

            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector($"iframe[src='https://prospect.buildout.com/{href}']")));

            wait.Until(d => d.FindElement(By.Id(waitElementId)));
            System.Threading.Thread.Sleep(1000); // Espera 1 segundo

        }

        private void ScrollToLoadAllContactDetailItems(IWebDriver driver, List<DetailListItem>? detailListItem, int page)
        {
            var actionsButtons = driver.FindElements(By.CssSelector("a[href*='ownerships?page=']"));
            var hasMorePages = (actionsButtons.Count == 1 && page == 1) || actionsButtons.Count > 1;
            var nextButton = actionsButtons.Reverse().FirstOrDefault();

            var turboFrames = new List<IWebElement>(driver.FindElements(By.CssSelector($"#console_ownership_list > turbo-frame")));

            var currentDateTime = System.DateTime.Now;

            if (detailListItem != null)
            {
                foreach (var frame in turboFrames)
                {
                    var collapsedElements = frame.FindElements(By.CssSelector("div.collapsed[data-bs-toggle='collapse']"));

                    foreach (var element in collapsedElements)
                    {
                        var elementText = string.Empty;
                        try
                        {
                            elementText = element.FindElement(By.CssSelector("strong")).Text;
                        }
                        catch
                        {
                            elementText = "NO NAME";
                        }

                        if (!elementText.Contains("Relationships"))
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", element);
                            System.Threading.Thread.Sleep(1000); // Espera 1 segundo entre clics
                        }
                    }

                    // Encuentra todos los elementos <a> que tienen la clase 'fw-bold collapsable-toggle'
                    var showMoreButtons = frame.FindElements(By.CssSelector("a.fw-bold.collapsable-toggle"));

                    foreach (var button in showMoreButtons)
                    {
                        // Verifica si el texto "Show More" está presente
                        var showMoreText = button.FindElement(By.CssSelector("div.show-collapsed")).Text;
                        if (showMoreText.Contains("Show More"))
                        {
                            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            executor.ExecuteScript("arguments[0].click();", button);
                            System.Threading.Thread.Sleep(1000); // Espera 1 segundo entre clics
                        }
                    }

                    string name = "";
                    try
                    {
                        // Encuentra el elemento <div> que tiene las clases 'fs-large fw-bold'
                        var nameElement = frame.FindElement(By.CssSelector("div.fs-large.fw-bold"));
                        name = nameElement.Text;
                    }
                    catch (NoSuchElementException)
                    {
                        // Manejar la excepción si el elemento no se encuentra
                        Console.WriteLine("El elemento con el nombre no fue encontrado.");
                    }

                    var phones = new List<string>();
                    try
                    {
                        var htmlPhones = frame.FindElements(By.CssSelector("a[href^='tel:']"));
                        phones = htmlPhones.Select(p => p.Text).ToList();
                    }
                    catch (NoSuchElementException)
                    {

                    }
                    phones = phones.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

                    var emails = new List<string>();
                    try
                    {
                        // Encuentra todos los elementos <a> con la clase 'text-truncate'
                        var htmlEmailElements = frame.FindElements(By.CssSelector("a.text-truncate"));

                        // Extrae el texto del <div> dentro de cada <turbo-frame> encontrado
                        emails = htmlEmailElements.Select(p => p.FindElement(By.CssSelector("turbo-frame div.fw-normal")).Text).ToList();
                    }
                    catch (NoSuchElementException)
                    {
                        // Manejar la excepción si el elemento no se encuentra
                        Console.WriteLine("No se encontraron elementos de correo electrónico.");
                    }
                    emails = emails.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

                    var addresses = new List<string>();
                    try
                    {
                        // Encuentra todos los elementos <turbo-frame> cuyo id empieza con 'info_address'
                        var htmlAddressElements = frame.FindElements(By.CssSelector("turbo-frame[id^='info_address'] div"));

                        // Extrae el texto del <div> dentro de cada <turbo-frame> encontrado
                        addresses = htmlAddressElements.Select(p => p.Text).ToList();
                    }
                    catch (NoSuchElementException)
                    {
                        // Manejar la excepción si el elemento no se encuentra
                        Console.WriteLine("No se encontraron elementos de dirección.");
                    }
                    addresses = addresses.Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

                    var phonescount = phones.Count;
                    var emailscount = emails.Count;
                    var addressescount = addresses.Count;

                    // Get the max count of phones, emails and addresses
                    var maxCount = Math.Max(phonescount, Math.Max(emailscount, addressescount));

                    // Fill the lists of emails, phones and addresses with empty strings to have the same count
                    var phonesToAdd = maxCount - phonescount;
                    var emailsToAdd = maxCount - emailscount;
                    var addressesToAdd = maxCount - addressescount;

                    // Add empty strings to the lists
                    phones.AddRange(Enumerable.Repeat("", phonesToAdd));
                    emails.AddRange(Enumerable.Repeat("", emailsToAdd));
                    addresses.AddRange(Enumerable.Repeat("", addressesToAdd));

                    // Add DetailListItem objects to the list
                    for (int i = 0; i < maxCount; i++)
                    {
                        detailListItem.Add(new DetailListItem
                        {
                            Name = name,
                            Phone = phones[i],
                            Email = emails[i],
                            Address = addresses[i]
                        });
                    }


                }
            }

            if (hasMorePages)
            {
                IJavaScriptExecutor ex = (IJavaScriptExecutor)driver;
                ex.ExecuteScript("arguments[0].click();", nextButton);
                System.Threading.Thread.Sleep(5000);
                page += 1;
                ScrollToLoadAllContactDetailItems(driver, detailListItem, page);
            }
        }
        private List<DetailListItem> ExtractCallListContactsDetail(ContactListItem selectedItem)
        {
            List<DetailListItem> contactListItems = new List<DetailListItem>();
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            try
            {
                // Navega a la página de login y realiza el login
                PerformLogin(driver);

                NavigateToHrefAfterLogin(driver, $"https://buildout.com/connect/{selectedItem.Href}", "a[href='/connect/call_lists']");

                // Cambiar el contexto al iframe de la lista de contactos
                SwitchToDetailFrame(driver, $"console_ownership_list_pagination", selectedItem.Href);

                System.Threading.Thread.Sleep(3000);

                // Scroll hasta el final de la página para cargar todos los elementos
                ScrollToLoadAllContactDetailItems(driver, contactListItems, 1);

                // Cambiar de nuevo el contexto al contenido principal
                driver.SwitchTo().DefaultContent();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Cierra el navegador
                driver.Quit();
            }

            //If Phone or Email is empty not consider it in the neww list
            contactListItems = contactListItems.Where(l => !string.IsNullOrWhiteSpace(l.Phone) || !string.IsNullOrWhiteSpace(l.Email)).ToList();

            return contactListItems.ToList();
        }
        private void NavigateToCallListById(IWebDriver driver, string href)
        {
            var callListsLink = driver.FindElement(By.CssSelector($"a[href='/{href}']"));

            IJavaScriptExecutor ex = (IJavaScriptExecutor)driver;
            ex.ExecuteScript("arguments[0].click();", callListsLink);

            System.Threading.Thread.Sleep(5000);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (contactListItemSelected != null)
            {
                // Obtener la ruta de la carpeta de descargas
                string carpetaDescargas = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                // Nombre del archivo
                string nombreArchivo = $"{contactListItemSelected.Id}_{contactListItemSelected.Text}.xlsx";
                // Ruta completa del archivo
                string rutaArchivo = Path.Combine(carpetaDescargas, nombreArchivo);

                ExportarDataGridViewAExcel(dataGridView2, rutaArchivo);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un contacto primario para exportar los datos", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        
        // Función para verificar si el archivo está en uso
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }

        // Guardar el archivo Excel con verificación y reintento
        private void SaveExcelFile(string rutaArchivo, XLWorkbook? workbook)
        {
            int maxRetries = 3;
            int delay = 1000; // milisegundos

            for (int retry = 0; retry < maxRetries; retry++)
            {
                if (!IsFileLocked(rutaArchivo))
                {
                    try
                    {
                        workbook.SaveAs(rutaArchivo);
                        MessageBox.Show($"Datos exportados exitosamente a Excel ({rutaArchivo})", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error al guardar el archivo: {ex.Message}");
                        return;
                    }
                }
                else
                {
                    Thread.Sleep(delay); // Esperar antes de reintentar
                }
            }

            MessageBox.Show("No se pudo guardar el archivo. Está siendo utilizado por otro proceso.");
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private List<DetailListItem> GetAllContactsItems(string filePath)
        {
            List<DetailListItem> detailListItems;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                detailListItems = JsonConvert.DeserializeObject<List<DetailListItem>>(json);
            }
            else
            {
                detailListItems = new List<DetailListItem>();
            }
            return detailListItems;
        }

        public void ExportarDataGridViewAExcel(DataGridView dataGridView, string rutaArchivo)
        {
            var dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            using (var workbook = new XLWorkbook())
            {
                var sheetName = $"All Data {dateTime}";

                if (sheetName.Length > 31)
                    sheetName = sheetName.Substring(0, 31);

                var worksheet = workbook.Worksheets.Add(sheetName);
                var onlyEmailworksheet = workbook.Worksheets.Add("Only Emails");
                var completeDataWorksheet = workbook.Worksheets.Add("Complete Data");

                // Agregar encabezados de columna
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    worksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    worksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;

                    onlyEmailworksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    onlyEmailworksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    onlyEmailworksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;

                    completeDataWorksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    completeDataWorksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    completeDataWorksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                }

                int emailRowCounter = 2; // Contador para las filas de la hoja de emails
                int phoneRowCounter = 2; // Contador para las filas de la hoja de teléfonos

                // Agregar filas de datos
                // 0 - Name
                // 1 - Email
                // 2 - Phone
                // 3 - Address

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    var row = dataGridView.Rows[i];
                    var rowHasEmail = row.Cells[1].Value != null && !string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString());
                    var rowHasPhone = row.Cells[2].Value != null && !string.IsNullOrWhiteSpace(row.Cells[2].Value.ToString());

                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        var cellValue = row.Cells[j].Value?.ToString();

                        // Insert data to the complete data sheet
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            worksheet.Cell(i + 2, j + 1).Value = cellValue;

                            // Insertar datos en la hoja solo de email si tiene email
                            if (rowHasEmail)
                            {
                                onlyEmailworksheet.Cell(emailRowCounter, j + 1).Value = cellValue;
                            }

                            // Insertar datos en la hoja solo de teléfono si tiene teléfono
                            if (rowHasPhone)
                            {
                                completeDataWorksheet.Cell(phoneRowCounter, j + 1).Value = cellValue;
                            }
                        }
                    }

                    // Incrementar los contadores de filas sólo si la fila tiene email o teléfono
                    if (rowHasEmail)
                    {
                        emailRowCounter++;
                    }
                    if (rowHasPhone)
                    {
                        phoneRowCounter++;
                    }
                }

                // Guardar el archivo Excel
                SaveExcelFile(rutaArchivo, workbook);
            }
        }

        public void ExportarAllDataAExcel(DataGridView dataGridView, string rutaArchivo)
        {
            var dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

            using (var workbook = new XLWorkbook())
            {
                var sheetName = $"All Data {dateTime}".Substring(0, Math.Min(31, $"All Data {dateTime}".Length));

                var worksheet = workbook.Worksheets.Add(sheetName);
                var onlyEmailWorksheet = workbook.Worksheets.Add("Only Emails");
                var completeDataWorksheet = workbook.Worksheets.Add("Complete Data");

                // Encabezados de columna
                string[] headers = { "Name", "Email", "Phone", "Address" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cell(1, i + 1).Value = headers[i];
                    onlyEmailWorksheet.Cell(1, i + 1).Value = headers[i];
                    completeDataWorksheet.Cell(1, i + 1).Value = headers[i];
                }

                var data = new List<DetailListItem>();
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    var row = dataGridView.Rows[i];
                    var contactList = new ContactListItem
                    {
                        Id = row.Cells["Id"].Value.ToString(),
                        Text = row.Cells["Primary Contact"].Value.ToString(),
                        ParentId = row.Cells["ParentId"].Value.ToString(),
                        Href = row.Cells["Href"].Value.ToString()
                    };

                    var path = string.Format(_callListItemsDetailPath, contactList.ParentId, contactList.Id);
                    data.AddRange(GetAllContactsItems(path));
                }

                int emailRowCounter = 2;
                int phoneRowCounter = 2;

                for (int j = 0; j < data.Count; j++)
                {
                    var item = data[j];
                    worksheet.Cell(j + 2, 1).Value = item.Name;
                    worksheet.Cell(j + 2, 2).Value = item.Email;
                    worksheet.Cell(j + 2, 3).Value = item.Phone;
                    worksheet.Cell(j + 2, 4).Value = item.Address;

                    if (!string.IsNullOrWhiteSpace(item.Email))
                    {
                        onlyEmailWorksheet.Cell(emailRowCounter, 1).Value = item.Name;
                        onlyEmailWorksheet.Cell(emailRowCounter, 2).Value = item.Email;
                        onlyEmailWorksheet.Cell(emailRowCounter, 3).Value = item.Phone;
                        onlyEmailWorksheet.Cell(emailRowCounter, 4).Value = item.Address;
                        emailRowCounter++;
                    }

                    if (!string.IsNullOrWhiteSpace(item.Phone))
                    {
                        completeDataWorksheet.Cell(phoneRowCounter, 1).Value = item.Name;
                        completeDataWorksheet.Cell(phoneRowCounter, 2).Value = item.Email;
                        completeDataWorksheet.Cell(phoneRowCounter, 3).Value = item.Phone;
                        completeDataWorksheet.Cell(phoneRowCounter, 4).Value = item.Address;
                        phoneRowCounter++;
                    }
                }

                SaveExcelFile(rutaArchivo, workbook);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                var result = MessageBox.Show($"¿Está seguro que desea exportar todos los contactos de cada contacto primario? ({dataGridView1.Rows.Count} contactos primarios)", "Exportar a Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    CallListItem selectedItem = (CallListItem)comboBox1.SelectedItem;

                    // Obtener la ruta de la carpeta de descargas
                    string carpetaDescargas = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                    // Nombre del archivo
                    string nombreArchivo = $"{selectedItem.Text}.xlsx";
                    // Ruta completa del archivo
                    string rutaArchivo = Path.Combine(carpetaDescargas, nombreArchivo);

                    ExportarAllDataAExcel(dataGridView1, rutaArchivo);
                }
            }
            else
            {
                MessageBox.Show("Debe existir minimo un contacto primario para exportar los datos", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
