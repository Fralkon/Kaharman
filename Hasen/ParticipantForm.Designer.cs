namespace Hasen
{
    partial class ParticipantForm
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
            name = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            weigth = new TextBox();
            label6 = new Label();
            qualification = new TextBox();
            label7 = new Label();
            city = new TextBox();
            label8 = new Label();
            trainer = new TextBox();
            gender = new ComboBox();
            dateOfBirth = new DateTimePicker();
            SuspendLayout();
            // 
            // name
            // 
            name.AllowDrop = true;
            name.Location = new Point(156, 66);
            name.Name = "name";
            name.Size = new Size(329, 23);
            name.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(411, 291);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Сохранить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(319, 291);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 69);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 3;
            label1.Text = "Фамилия и имя";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 98);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 5;
            label2.Text = "Пол";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 127);
            label3.Name = "label3";
            label3.Size = new Size(90, 15);
            label3.TabIndex = 7;
            label3.Text = "Дата рождения";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(44, 157);
            label5.Name = "label5";
            label5.Size = new Size(26, 15);
            label5.TabIndex = 11;
            label5.Text = "Вес";
            // 
            // weigth
            // 
            weigth.AllowDrop = true;
            weigth.Location = new Point(156, 154);
            weigth.Name = "weigth";
            weigth.Size = new Size(329, 23);
            weigth.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(44, 186);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 13;
            label6.Text = "Квалификация";
            // 
            // qualification
            // 
            qualification.AllowDrop = true;
            qualification.Location = new Point(156, 183);
            qualification.Name = "qualification";
            qualification.Size = new Size(329, 23);
            qualification.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(44, 215);
            label7.Name = "label7";
            label7.Size = new Size(40, 15);
            label7.TabIndex = 15;
            label7.Text = "Город";
            // 
            // city
            // 
            city.AllowDrop = true;
            city.Location = new Point(156, 212);
            city.Name = "city";
            city.Size = new Size(329, 23);
            city.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(44, 244);
            label8.Name = "label8";
            label8.Size = new Size(46, 15);
            label8.TabIndex = 17;
            label8.Text = "Тренер";
            // 
            // trainer
            // 
            trainer.AllowDrop = true;
            trainer.Location = new Point(156, 241);
            trainer.Name = "trainer";
            trainer.Size = new Size(329, 23);
            trainer.TabIndex = 16;
            // 
            // gender
            // 
            gender.FormattingEnabled = true;
            gender.Location = new Point(156, 95);
            gender.Name = "gender";
            gender.Size = new Size(329, 23);
            gender.TabIndex = 18;
            // 
            // dateOfBirth
            // 
            dateOfBirth.Location = new Point(156, 124);
            dateOfBirth.Name = "dateOfBirth";
            dateOfBirth.Size = new Size(329, 23);
            dateOfBirth.TabIndex = 19;
            // 
            // ParticipantForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(515, 343);
            Controls.Add(dateOfBirth);
            Controls.Add(gender);
            Controls.Add(label8);
            Controls.Add(trainer);
            Controls.Add(label7);
            Controls.Add(city);
            Controls.Add(label6);
            Controls.Add(qualification);
            Controls.Add(label5);
            Controls.Add(weigth);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(name);
            Name = "ParticipantForm";
            Text = "Участник";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox name;
        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private TextBox weigth;
        private Label label6;
        private TextBox qualification;
        private Label label7;
        private TextBox city;
        private Label label8;
        private TextBox trainer;
        private ComboBox gender;
        private DateTimePicker dateOfBirth;
    }
}