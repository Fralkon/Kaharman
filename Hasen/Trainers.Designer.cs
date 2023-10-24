namespace Kaharman
{
    partial class Trainers
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(286, 188);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Ок";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(205, 188);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 29);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 2;
            label1.Text = "Ф.И.О";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 67);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 3;
            label2.Text = "Дата рождения";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(30, 99);
            label3.Name = "label3";
            label3.Size = new Size(88, 15);
            label3.TabIndex = 4;
            label3.Text = "Квалификация";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 132);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 5;
            label4.Text = "Область / город";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(134, 26);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(227, 23);
            textBox1.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(134, 96);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(227, 23);
            textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(134, 129);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(227, 23);
            textBox4.TabIndex = 9;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(134, 61);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(227, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // Trainers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(391, 232);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Trainers";
            Text = "Тренер";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox3;
        private TextBox textBox4;
        private DateTimePicker dateTimePicker1;
    }
}