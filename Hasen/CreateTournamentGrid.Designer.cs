﻿namespace Kaharman
{
    partial class CreateTournamentGrid
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
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            genderComboBox = new ComboBox();
            categoryComboBox = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            dataGridView1 = new DataGridView();
            ageMinTextBox = new TextBox();
            ageMaxTextBox = new TextBox();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(643, 396);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Создать";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(544, 396);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(130, 70);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(753, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(130, 99);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(588, 23);
            nameTextBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 102);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 5;
            label1.Text = "Наименование";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 44);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 7;
            label2.Text = "Соревнование";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(130, 41);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(588, 23);
            textBox2.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(34, 73);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 8;
            label3.Text = "Дата";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 131);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 9;
            label4.Text = "Пол";
            // 
            // genderComboBox
            // 
            genderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            genderComboBox.FormattingEnabled = true;
            genderComboBox.Location = new Point(130, 128);
            genderComboBox.Name = "genderComboBox";
            genderComboBox.Size = new Size(90, 23);
            genderComboBox.TabIndex = 10;
            genderComboBox.SelectedIndexChanged += categoryComboBox_SelectedIndexChanged;
            // 
            // categoryComboBox
            // 
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.FormattingEnabled = true;
            categoryComboBox.Location = new Point(371, 128);
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(119, 23);
            categoryComboBox.TabIndex = 12;
            categoryComboBox.SelectedIndexChanged += categoryComboBox_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(256, 131);
            label5.Name = "label5";
            label5.Size = new Size(109, 15);
            label5.TabIndex = 11;
            label5.Text = "Весовая категория";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(517, 131);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 13;
            label6.Text = "Возраст";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowDrop = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(34, 158);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(684, 216);
            dataGridView1.TabIndex = 15;
            // 
            // ageMinTextBox
            // 
            ageMinTextBox.Location = new Point(573, 128);
            ageMinTextBox.Name = "ageMinTextBox";
            ageMinTextBox.Size = new Size(61, 23);
            ageMinTextBox.TabIndex = 16;
            ageMinTextBox.TextChanged += categoryComboBox_SelectedIndexChanged;
            // 
            // ageMaxTextBox
            // 
            ageMaxTextBox.Location = new Point(658, 128);
            ageMaxTextBox.Name = "ageMaxTextBox";
            ageMaxTextBox.Size = new Size(61, 23);
            ageMaxTextBox.TabIndex = 17;
            ageMaxTextBox.TextChanged += categoryComboBox_SelectedIndexChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(640, 131);
            label7.Name = "label7";
            label7.Size = new Size(12, 15);
            label7.TabIndex = 18;
            label7.Text = "-";
            // 
            // CreateTournamentGrid
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 442);
            Controls.Add(label7);
            Controls.Add(ageMaxTextBox);
            Controls.Add(ageMinTextBox);
            Controls.Add(dataGridView1);
            Controls.Add(label6);
            Controls.Add(categoryComboBox);
            Controls.Add(label5);
            Controls.Add(genderComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(nameTextBox);
            Controls.Add(dateTimePicker1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "CreateTournamentGrid";
            Text = "CreateTournamentGrid";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private Label label4;
        private ComboBox genderComboBox;
        private ComboBox categoryComboBox;
        private Label label5;
        private Label label6;
        private DataGridView dataGridView1;
        private TextBox ageMinTextBox;
        private TextBox ageMaxTextBox;
        private Label label7;
    }
}