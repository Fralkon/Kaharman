namespace Kaharman
{
    partial class CreateTournament
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
            components = new System.ComponentModel.Container();
            createButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            secretLable = new Label();
            name = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            note = new TextBox();
            mainJudge = new TextBox();
            secret = new TextBox();
            dataGridView1 = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            создатьТурнирнуюТаблицуToolStripMenuItem = new ToolStripMenuItem();
            изменитьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            турнирнаяТаблицаToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // createButton
            // 
            createButton.Location = new Point(682, 442);
            createButton.Name = "createButton";
            createButton.Size = new Size(75, 23);
            createButton.TabIndex = 0;
            createButton.Text = "Создать";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(592, 442);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 47);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 2;
            label1.Text = "Наименование";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 79);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 3;
            label2.Text = "Дата начала";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(456, 79);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 4;
            label3.Text = "Дата окончания";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 105);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 5;
            label4.Text = "Примечание";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 134);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 6;
            label5.Text = "Главный судья";
            // 
            // secretLable
            // 
            secretLable.AutoSize = true;
            secretLable.Location = new Point(430, 134);
            secretLable.Name = "secretLable";
            secretLable.Size = new Size(64, 15);
            secretLable.TabIndex = 7;
            secretLable.Text = "Секретарь";
            // 
            // name
            // 
            name.Location = new Point(133, 44);
            name.Name = "name";
            name.Size = new Size(624, 23);
            name.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(133, 73);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 9;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(557, 73);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(200, 23);
            dateTimePicker2.TabIndex = 10;
            // 
            // note
            // 
            note.Location = new Point(133, 102);
            note.Name = "note";
            note.Size = new Size(624, 23);
            note.TabIndex = 11;
            // 
            // mainJudge
            // 
            mainJudge.Location = new Point(133, 131);
            mainJudge.Name = "mainJudge";
            mainJudge.Size = new Size(274, 23);
            mainJudge.TabIndex = 12;
            // 
            // secret
            // 
            secret.Location = new Point(500, 131);
            secret.Name = "secret";
            secret.Size = new Size(257, 23);
            secret.TabIndex = 13;
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
            dataGridView1.Location = new Point(35, 160);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(722, 261);
            dataGridView1.TabIndex = 14;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { создатьТурнирнуюТаблицуToolStripMenuItem, изменитьToolStripMenuItem, удалитьToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(181, 92);
            // 
            // создатьТурнирнуюТаблицуToolStripMenuItem
            // 
            создатьТурнирнуюТаблицуToolStripMenuItem.Name = "создатьТурнирнуюТаблицуToolStripMenuItem";
            создатьТурнирнуюТаблицуToolStripMenuItem.Size = new Size(180, 22);
            создатьТурнирнуюТаблицуToolStripMenuItem.Text = "Создать";
            создатьТурнирнуюТаблицуToolStripMenuItem.Click += создатьТурнирнуюТаблицуToolStripMenuItem_Click;
            // 
            // изменитьToolStripMenuItem
            // 
            изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            изменитьToolStripMenuItem.Size = new Size(180, 22);
            изменитьToolStripMenuItem.Text = "Изменить";
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(180, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, турнирнаяТаблицаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(785, 24);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // турнирнаяТаблицаToolStripMenuItem
            // 
            турнирнаяТаблицаToolStripMenuItem.Name = "турнирнаяТаблицаToolStripMenuItem";
            турнирнаяТаблицаToolStripMenuItem.Size = new Size(126, 20);
            турнирнаяТаблицаToolStripMenuItem.Text = "Турнирная таблица";
            // 
            // CreateTournament
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(785, 487);
            Controls.Add(menuStrip1);
            Controls.Add(dataGridView1);
            Controls.Add(secret);
            Controls.Add(mainJudge);
            Controls.Add(note);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(name);
            Controls.Add(secretLable);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(createButton);
            MainMenuStrip = menuStrip1;
            Name = "CreateTournament";
            Text = "Создать соревнование";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button createButton;
        private Button cancelButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label secretLable;
        private TextBox name;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private TextBox note;
        private TextBox mainJudge;
        private TextBox secret;
        private DataGridView dataGridView1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem создатьТурнирнуюТаблицуToolStripMenuItem;
        private ToolStripMenuItem изменитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem турнирнаяТаблицаToolStripMenuItem;
    }
}