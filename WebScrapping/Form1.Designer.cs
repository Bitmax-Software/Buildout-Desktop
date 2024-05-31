namespace WebScrapping
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            comboBox1 = new ComboBox();
            button2 = new Button();
            groupBox1 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox2 = new GroupBox();
            button3 = new Button();
            groupBox3 = new GroupBox();
            dataGridView1 = new DataGridView();
            tabControl1 = new TabControl();
            Nombre = new DataGridViewTextBoxColumn();
            Id = new DataGridViewTextBoxColumn();
            Href = new DataGridViewTextBoxColumn();
            groupBox5 = new GroupBox();
            button5 = new Button();
            button4 = new Button();
            dataGridView2 = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(16, 22);
            button1.Name = "button1";
            button1.Size = new Size(402, 35);
            button1.TabIndex = 0;
            button1.Text = "Import";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 29);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(240, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(12, 63);
            button2.Name = "button2";
            button2.Size = new Size(147, 35);
            button2.TabIndex = 2;
            button2.Text = "Import";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1367, 112);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tool bar";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(comboBox1);
            groupBox4.Location = new Point(451, 22);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(289, 73);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "Select a call list";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button1);
            groupBox2.Location = new Point(12, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(433, 73);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "All Call Lists";
            // 
            // button3
            // 
            button3.Location = new Point(165, 63);
            button3.Name = "button3";
            button3.Size = new Size(153, 35);
            button3.TabIndex = 3;
            button3.Text = "Export";
            button3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(dataGridView1);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(tabControl1);
            groupBox3.Location = new Point(12, 141);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1367, 371);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Call list detail";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.Window;
            dataGridView1.Location = new Point(12, 113);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1335, 241);
            dataGridView1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Enabled = false;
            tabControl1.Location = new Point(12, 22);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1349, 24);
            tabControl1.TabIndex = 0;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.Name = "Id";
            // 
            // Href
            // 
            Href.HeaderText = "Href";
            Href.Name = "Href";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(button5);
            groupBox5.Controls.Add(button4);
            groupBox5.Controls.Add(dataGridView2);
            groupBox5.Location = new Point(12, 533);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(1367, 344);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Contact list detail";
            // 
            // button5
            // 
            button5.Location = new Point(165, 22);
            button5.Name = "button5";
            button5.Size = new Size(153, 35);
            button5.TabIndex = 2;
            button5.Text = "Export";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 22);
            button4.Name = "button4";
            button4.Size = new Size(147, 35);
            button4.TabIndex = 1;
            button4.Text = "Import";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToOrderColumns = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(12, 63);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.Size = new Size(1335, 247);
            dataGridView2.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1409, 889);
            Controls.Add(groupBox5);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Buildout Desktop";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private TabControl tabControl1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn Href;
        private GroupBox groupBox2;
        private Button button3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private DataGridView dataGridView2;
        private Button button4;
        private Button button5;
    }
}
