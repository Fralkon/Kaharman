namespace Kaharman
{
    partial class TournamentGridForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            dateTimePicker1 = new DateTimePicker();
            menuStrip1 = new MenuStrip();
            label1 = new Label();
            label3 = new Label();
            allParticipant = new DataGridView();
            gridParticipant = new DataGridView();
            label4 = new Label();
            label5 = new Label();
            button3 = new Button();
            button4 = new Button();
            panel1 = new Panel();
            programComboBox = new ComboBox();
            label8 = new Label();
            weight = new TextBox();
            numberProtocol = new TextBox();
            programmText = new Label();
            label2 = new Label();
            label6 = new Label();
            genderFilterText = new Label();
            ageMaxTextBox = new TextBox();
            ageRange = new Label();
            ageMinTextBox = new TextBox();
            panel2 = new Panel();
            qualMaxComboBox = new ComboBox();
            label7 = new Label();
            qualMinComboBox = new ComboBox();
            genderComboBox = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)allParticipant).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridParticipant).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(425, 519);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Создать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(326, 519);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dateTimePicker1.Location = new Point(126, 40);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(375, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1107, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 72);
            label1.Name = "label1";
            label1.Size = new Size(111, 15);
            label1.TabIndex = 5;
            label1.Text = "Тех. квалификация";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 43);
            label3.Name = "label3";
            label3.Size = new Size(100, 15);
            label3.TabIndex = 8;
            label3.Text = "Дата проведения";
            // 
            // allParticipant
            // 
            allParticipant.AllowUserToAddRows = false;
            allParticipant.AllowUserToDeleteRows = false;
            allParticipant.AllowUserToResizeColumns = false;
            allParticipant.AllowUserToResizeRows = false;
            allParticipant.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            allParticipant.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            allParticipant.BackgroundColor = SystemColors.Window;
            allParticipant.BorderStyle = BorderStyle.Fixed3D;
            allParticipant.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            allParticipant.Location = new Point(17, 152);
            allParticipant.Name = "allParticipant";
            allParticipant.ReadOnly = true;
            allParticipant.RowHeadersVisible = false;
            allParticipant.RowTemplate.Height = 25;
            allParticipant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            allParticipant.Size = new Size(504, 336);
            allParticipant.TabIndex = 15;
            allParticipant.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // gridParticipant
            // 
            gridParticipant.AllowDrop = true;
            gridParticipant.AllowUserToAddRows = false;
            gridParticipant.AllowUserToDeleteRows = false;
            gridParticipant.AllowUserToResizeColumns = false;
            gridParticipant.AllowUserToResizeRows = false;
            gridParticipant.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridParticipant.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridParticipant.BackgroundColor = SystemColors.Window;
            gridParticipant.BorderStyle = BorderStyle.Fixed3D;
            gridParticipant.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridParticipant.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridParticipant.Location = new Point(15, 152);
            gridParticipant.Name = "gridParticipant";
            gridParticipant.ReadOnly = true;
            gridParticipant.RowHeadersVisible = false;
            gridParticipant.RowTemplate.Height = 25;
            gridParticipant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridParticipant.Size = new Size(484, 336);
            gridParticipant.TabIndex = 19;
            gridParticipant.CellDoubleClick += dataGridView2_CellDoubleClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 132);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 20;
            label4.Text = "Участники сетки";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 132);
            label5.Name = "label5";
            label5.Size = new Size(86, 15);
            label5.TabIndex = 21;
            label5.Text = "Все участники";
            // 
            // button3
            // 
            button3.Location = new Point(540, 275);
            button3.Name = "button3";
            button3.Size = new Size(46, 25);
            button3.TabIndex = 22;
            button3.Text = ">";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(540, 304);
            button4.Name = "button4";
            button4.Size = new Size(46, 25);
            button4.TabIndex = 23;
            button4.Text = "<";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(programComboBox);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(weight);
            panel1.Controls.Add(numberProtocol);
            panel1.Controls.Add(programmText);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(allParticipant);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(534, 557);
            panel1.TabIndex = 24;
            // 
            // programComboBox
            // 
            programComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            programComboBox.AutoCompleteCustomSource.AddRange(new string[] { "М", "Ж" });
            programComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            programComboBox.FormattingEnabled = true;
            programComboBox.Location = new Point(126, 69);
            programComboBox.Name = "programComboBox";
            programComboBox.Size = new Size(375, 23);
            programComboBox.TabIndex = 34;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(17, 101);
            label8.Name = "label8";
            label8.Size = new Size(109, 15);
            label8.TabIndex = 29;
            label8.Text = "Весовая категория";
            // 
            // weight
            // 
            weight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            weight.Location = new Point(126, 98);
            weight.Name = "weight";
            weight.Size = new Size(375, 23);
            weight.TabIndex = 28;
            // 
            // numberProtocol
            // 
            numberProtocol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numberProtocol.Location = new Point(126, 11);
            numberProtocol.Name = "numberProtocol";
            numberProtocol.Size = new Size(375, 23);
            numberProtocol.TabIndex = 27;
            // 
            // programmText
            // 
            programmText.AutoSize = true;
            programmText.Location = new Point(17, 72);
            programmText.Name = "programmText";
            programmText.Size = new Size(72, 15);
            programmText.TabIndex = 25;
            programmText.Text = "Программа";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 14);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 26;
            label2.Text = "Номер протокола";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(321, 43);
            label6.Name = "label6";
            label6.Size = new Size(12, 15);
            label6.TabIndex = 29;
            label6.Text = "-";
            // 
            // genderFilterText
            // 
            genderFilterText.AutoSize = true;
            genderFilterText.Location = new Point(15, 14);
            genderFilterText.Name = "genderFilterText";
            genderFilterText.Size = new Size(30, 15);
            genderFilterText.TabIndex = 21;
            genderFilterText.Text = "Пол";
            // 
            // ageMaxTextBox
            // 
            ageMaxTextBox.Location = new Point(336, 40);
            ageMaxTextBox.Name = "ageMaxTextBox";
            ageMaxTextBox.Size = new Size(176, 23);
            ageMaxTextBox.TabIndex = 28;
            ageMaxTextBox.TextChanged += ageMaxTextBox_TextChanged;
            // 
            // ageRange
            // 
            ageRange.AutoSize = true;
            ageRange.Location = new Point(15, 43);
            ageRange.Name = "ageRange";
            ageRange.Size = new Size(127, 15);
            ageRange.TabIndex = 27;
            ageRange.Text = "Возрастная категория";
            // 
            // ageMinTextBox
            // 
            ageMinTextBox.Location = new Point(143, 40);
            ageMinTextBox.Name = "ageMinTextBox";
            ageMinTextBox.Size = new Size(176, 23);
            ageMinTextBox.TabIndex = 26;
            ageMinTextBox.TextChanged += ageMinTextBox_TextChanged;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(qualMaxComboBox);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(qualMinComboBox);
            panel2.Controls.Add(genderComboBox);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(gridParticipant);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(genderFilterText);
            panel2.Controls.Add(ageMinTextBox);
            panel2.Controls.Add(ageRange);
            panel2.Controls.Add(ageMaxTextBox);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(592, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(515, 557);
            panel2.TabIndex = 25;
            // 
            // qualMaxComboBox
            // 
            qualMaxComboBox.AutoCompleteCustomSource.AddRange(new string[] { "М", "Ж" });
            qualMaxComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            qualMaxComboBox.FormattingEnabled = true;
            qualMaxComboBox.Location = new Point(336, 69);
            qualMaxComboBox.Name = "qualMaxComboBox";
            qualMaxComboBox.Size = new Size(176, 23);
            qualMaxComboBox.TabIndex = 33;
            qualMaxComboBox.SelectedIndexChanged += qualMaxComboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(321, 72);
            label7.Name = "label7";
            label7.Size = new Size(12, 15);
            label7.TabIndex = 32;
            label7.Text = "-";
            // 
            // qualMinComboBox
            // 
            qualMinComboBox.AutoCompleteCustomSource.AddRange(new string[] { "М", "Ж" });
            qualMinComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            qualMinComboBox.FormattingEnabled = true;
            qualMinComboBox.Location = new Point(143, 69);
            qualMinComboBox.Name = "qualMinComboBox";
            qualMinComboBox.Size = new Size(176, 23);
            qualMinComboBox.TabIndex = 31;
            qualMinComboBox.SelectedIndexChanged += qualMinComboBox_SelectedIndexChanged;
            // 
            // genderComboBox
            // 
            genderComboBox.AutoCompleteCustomSource.AddRange(new string[] { "М", "Ж" });
            genderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            genderComboBox.FormattingEnabled = true;
            genderComboBox.Location = new Point(143, 11);
            genderComboBox.Name = "genderComboBox";
            genderComboBox.Size = new Size(176, 23);
            genderComboBox.TabIndex = 30;
            genderComboBox.SelectedValueChanged += genderComboBox_SelectedValueChanged;
            // 
            // TournamentGridForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1107, 581);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "TournamentGridForm";
            Text = "Создание сетки";
            Load += TournamentGridForm_Load;
            Resize += TournamentGridForm_Resize;
            ((System.ComponentModel.ISupportInitialize)allParticipant).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridParticipant).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private DateTimePicker dateTimePicker1;
        private MenuStrip menuStrip1;
        private Label label1;
        private Label label3;
        private DataGridView allParticipant;
        private DataGridView gridParticipant;
        private Label label4;
        private Label label5;
        private Button button3;
        private Button button4;
        private Panel panel1;
        private Panel panel2;
        private Label genderFilterText;
        private Label programmText;
        private Label ageRange;
        private TextBox ageMinTextBox;
        private Label label6;
        private TextBox ageMaxTextBox;
        private TextBox numberProtocol;
        private Label label2;
        private ComboBox qualMaxComboBox;
        private Label label7;
        private ComboBox qualMinComboBox;
        private ComboBox genderComboBox;
        private Label label8;
        private TextBox weight;
        private ComboBox programComboBox;
    }
}