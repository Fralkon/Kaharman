﻿namespace Kaharman
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
            nameTextBox = new TextBox();
            label1 = new Label();
            label3 = new Label();
            allParticipant = new DataGridView();
            gridParticipant = new DataGridView();
            label4 = new Label();
            label5 = new Label();
            button3 = new Button();
            button4 = new Button();
            panel1 = new Panel();
            programmText = new Label();
            programm = new TextBox();
            gender = new TextBox();
            genderFilterText = new Label();
            panel2 = new Panel();
            label7 = new Label();
            label6 = new Label();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            ageRange = new Label();
            textBox3 = new TextBox();
            numberProtocol = new TextBox();
            label2 = new Label();
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
            dateTimePicker1.Location = new Point(144, 39);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(339, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1104, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // nameTextBox
            // 
            nameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            nameTextBox.Location = new Point(129, 39);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(375, 23);
            nameTextBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 42);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 5;
            label1.Text = "Наименование";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 42);
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
            allParticipant.Location = new Point(17, 167);
            allParticipant.Name = "allParticipant";
            allParticipant.ReadOnly = true;
            allParticipant.RowHeadersVisible = false;
            allParticipant.RowTemplate.Height = 25;
            allParticipant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            allParticipant.Size = new Size(504, 321);
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
            gridParticipant.Location = new Point(15, 167);
            gridParticipant.Name = "gridParticipant";
            gridParticipant.ReadOnly = true;
            gridParticipant.RowHeadersVisible = false;
            gridParticipant.RowTemplate.Height = 25;
            gridParticipant.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridParticipant.Size = new Size(484, 321);
            gridParticipant.TabIndex = 19;
            gridParticipant.CellDoubleClick += dataGridView2_CellDoubleClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 149);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 20;
            label4.Text = "Участники сетки";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 149);
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
            panel1.Controls.Add(numberProtocol);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(programmText);
            panel1.Controls.Add(programm);
            panel1.Controls.Add(gender);
            panel1.Controls.Add(genderFilterText);
            panel1.Controls.Add(nameTextBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(allParticipant);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(534, 557);
            panel1.TabIndex = 24;
            // 
            // programmText
            // 
            programmText.AutoSize = true;
            programmText.Location = new Point(17, 100);
            programmText.Name = "programmText";
            programmText.Size = new Size(72, 15);
            programmText.TabIndex = 25;
            programmText.Text = "Программа";
            // 
            // programm
            // 
            programm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            programm.Location = new Point(129, 97);
            programm.Name = "programm";
            programm.Size = new Size(375, 23);
            programm.TabIndex = 24;
            // 
            // gender
            // 
            gender.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gender.Location = new Point(129, 68);
            gender.Name = "gender";
            gender.Size = new Size(375, 23);
            gender.TabIndex = 23;
            // 
            // genderFilterText
            // 
            genderFilterText.AutoSize = true;
            genderFilterText.Location = new Point(17, 71);
            genderFilterText.Name = "genderFilterText";
            genderFilterText.Size = new Size(30, 15);
            genderFilterText.TabIndex = 21;
            genderFilterText.Text = "Пол";
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(textBox5);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(ageRange);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(gridParticipant);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(589, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(515, 557);
            panel2.TabIndex = 25;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 100);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 27;
            label7.Text = "Квалификация";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(267, 71);
            label6.Name = "label6";
            label6.Size = new Size(12, 15);
            label6.TabIndex = 29;
            label6.Text = "-";
            // 
            // textBox5
            // 
            textBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox5.Location = new Point(144, 97);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(339, 23);
            textBox5.TabIndex = 26;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(298, 68);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(101, 23);
            textBox4.TabIndex = 28;
            // 
            // ageRange
            // 
            ageRange.AutoSize = true;
            ageRange.Location = new Point(25, 71);
            ageRange.Name = "ageRange";
            ageRange.Size = new Size(50, 15);
            ageRange.TabIndex = 27;
            ageRange.Text = "Возраст";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(144, 68);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(101, 23);
            textBox3.TabIndex = 26;
            // 
            // numberProtocol
            // 
            numberProtocol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numberProtocol.Location = new Point(129, 10);
            numberProtocol.Name = "numberProtocol";
            numberProtocol.Size = new Size(375, 23);
            numberProtocol.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 13);
            label2.Name = "label2";
            label2.Size = new Size(107, 15);
            label2.TabIndex = 26;
            label2.Text = "Номер протокола";
            // 
            // TournamentGridForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1104, 581);
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
        private TextBox nameTextBox;
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
        private TextBox gender;
        private Label programmText;
        private TextBox programm;
        private Label ageRange;
        private TextBox textBox3;
        private Label label6;
        private TextBox textBox4;
        private Label label7;
        private TextBox textBox5;
        private TextBox numberProtocol;
        private Label label2;
    }
}