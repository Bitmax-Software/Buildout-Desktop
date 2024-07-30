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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            comboBox1 = new ComboBox();
            button2 = new Button();
            groupBox1 = new GroupBox();
            button3 = new Button();
            groupBox3 = new GroupBox();
            button6 = new Button();
            dataGridView1 = new DataGridView();
            tabControl1 = new TabControl();
            Nombre = new DataGridViewTextBoxColumn();
            Id = new DataGridViewTextBoxColumn();
            Href = new DataGridViewTextBoxColumn();
            groupBox5 = new GroupBox();
            button5 = new Button();
            button4 = new Button();
            dataGridView2 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 54);
            button1.Margin = new Padding(6, 7, 6, 7);
            button1.Name = "button1";
            button1.Size = new Size(989, 65);
            button1.TabIndex = 0;
            button1.Text = "Import call lists";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(1084, 65);
            comboBox1.Margin = new Padding(6, 7, 6, 7);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(805, 45);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Location = new Point(26, 127);
            button2.Margin = new Padding(6, 7, 6, 7);
            button2.Name = "button2";
            button2.Size = new Size(562, 63);
            button2.TabIndex = 2;
            button2.Text = "Import primary contacts";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Location = new Point(6, 7);
            groupBox1.Margin = new Padding(6, 7, 6, 7);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 7, 6, 7);
            groupBox1.Size = new Size(1901, 130);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tool bar";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.Location = new Point(1561, 123);
            button3.Margin = new Padding(6, 7, 6, 7);
            button3.Name = "button3";
            button3.Size = new Size(328, 63);
            button3.TabIndex = 3;
            button3.Text = "Export all";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(button6);
            groupBox3.Controls.Add(button2);
            groupBox3.Controls.Add(dataGridView1);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(tabControl1);
            groupBox3.Location = new Point(6, 151);
            groupBox3.Margin = new Padding(6, 7, 6, 7);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(6, 7, 6, 7);
            groupBox3.Size = new Size(1901, 514);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Call list";
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.Location = new Point(1234, 123);
            button6.Margin = new Padding(6, 7, 6, 7);
            button6.Name = "button6";
            button6.Size = new Size(315, 63);
            button6.TabIndex = 4;
            button6.Text = "Import all";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.GridColor = SystemColors.Window;
            dataGridView1.Location = new Point(26, 204);
            dataGridView1.Margin = new Padding(6, 7, 6, 7);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 92;
            dataGridView1.Size = new Size(1863, 296);
            dataGridView1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Enabled = false;
            tabControl1.Location = new Point(26, 50);
            tabControl1.Margin = new Padding(6, 7, 6, 7);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1863, 59);
            tabControl1.TabIndex = 0;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 11;
            Nombre.Name = "Nombre";
            Nombre.Width = 225;
            // 
            // Id
            // 
            Id.HeaderText = "Id";
            Id.MinimumWidth = 11;
            Id.Name = "Id";
            Id.Width = 225;
            // 
            // Href
            // 
            Href.HeaderText = "Href";
            Href.MinimumWidth = 11;
            Href.Name = "Href";
            Href.Width = 225;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(button5);
            groupBox5.Controls.Add(button4);
            groupBox5.Controls.Add(dataGridView2);
            groupBox5.Location = new Point(6, 679);
            groupBox5.Margin = new Padding(6, 7, 6, 7);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(6, 7, 6, 7);
            groupBox5.Size = new Size(1901, 516);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Contact list";
            // 
            // button5
            // 
            button5.Location = new Point(301, 54);
            button5.Margin = new Padding(6, 7, 6, 7);
            button5.Name = "button5";
            button5.Size = new Size(287, 63);
            button5.TabIndex = 2;
            button5.Text = "Export";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(26, 54);
            button4.Margin = new Padding(6, 7, 6, 7);
            button4.Name = "button4";
            button4.Size = new Size(263, 63);
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
            dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(26, 145);
            dataGridView2.Margin = new Padding(6, 7, 6, 7);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersWidth = 92;
            dataGridView2.Size = new Size(1866, 348);
            dataGridView2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(groupBox5, 0, 2);
            tableLayoutPanel1.Controls.Add(groupBox3, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.Size = new Size(1913, 1202);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1913, 1202);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 7, 6, 7);
            Name = "Form1";
            Text = "Buildout Desktop";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private Button button3;
        private GroupBox groupBox5;
        private DataGridView dataGridView2;
        private Button button4;
        private Button button5;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button6;
    }
}
