namespace Kaharman
{
    partial class TournamentForm
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
            gridContextMenuStrip = new ContextMenuStrip(components);
            создатьТурнирнуюТаблицуToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem = new ToolStripMenuItem();
            копToolStripMenuItem = new ToolStripMenuItem();
            изменитьToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            сохранитьToolStripMenuItem = new ToolStripMenuItem();
            сеткаТурнираToolStripMenuItem = new ToolStripMenuItem();
            протоколТурнираToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            турнирнаяТаблицаToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem2 = new ToolStripMenuItem();
            обновитьToolStripMenuItem = new ToolStripMenuItem();
            участникиToolStripMenuItem = new ToolStripMenuItem();
            создатьToolStripMenuItem1 = new ToolStripMenuItem();
            добавитьToolStripMenuItem1 = new ToolStripMenuItem();
            удалитьToolStripMenuItem3 = new ToolStripMenuItem();
            очиститьToolStripMenuItem = new ToolStripMenuItem();
            gridDataGridView = new DataGridView();
            participantGrid = new DataGridView();
            label6 = new Label();
            label7 = new Label();
            participantContextMenuStrip = new ContextMenuStrip(components);
            создатьToolStripMenuItem2 = new ToolStripMenuItem();
            добавитьToolStripMenuItem = new ToolStripMenuItem();
            удалитьToolStripMenuItem1 = new ToolStripMenuItem();
            panel1 = new Panel();
            panel2 = new Panel();
            progressBar1 = new ProgressBar();
            gridContextMenuStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)participantGrid).BeginInit();
            participantContextMenuStrip.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 19);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 2;
            label1.Text = "Наименование";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 19);
            label2.Name = "label2";
            label2.Size = new Size(74, 15);
            label2.TabIndex = 3;
            label2.Text = "Дата начала";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(346, 19);
            label3.Name = "label3";
            label3.Size = new Size(95, 15);
            label3.TabIndex = 4;
            label3.Text = "Дата окончания";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 48);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 5;
            label4.Text = "Примечание";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 48);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 6;
            label5.Text = "Главный судья";
            // 
            // secretLable
            // 
            secretLable.AutoSize = true;
            secretLable.Location = new Point(346, 48);
            secretLable.Name = "secretLable";
            secretLable.Size = new Size(64, 15);
            secretLable.TabIndex = 7;
            secretLable.Text = "Секретарь";
            // 
            // name
            // 
            name.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            name.Location = new Point(118, 16);
            name.Name = "name";
            name.Size = new Size(521, 23);
            name.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(119, 16);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(203, 23);
            dateTimePicker1.TabIndex = 9;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(447, 16);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(200, 23);
            dateTimePicker2.TabIndex = 10;
            // 
            // note
            // 
            note.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            note.Location = new Point(118, 45);
            note.Name = "note";
            note.Size = new Size(521, 23);
            note.TabIndex = 11;
            // 
            // mainJudge
            // 
            mainJudge.Location = new Point(119, 45);
            mainJudge.Name = "mainJudge";
            mainJudge.Size = new Size(203, 23);
            mainJudge.TabIndex = 12;
            // 
            // secret
            // 
            secret.Location = new Point(447, 45);
            secret.Name = "secret";
            secret.Size = new Size(200, 23);
            secret.TabIndex = 13;
            // 
            // gridContextMenuStrip
            // 
            gridContextMenuStrip.Items.AddRange(new ToolStripItem[] { создатьТурнирнуюТаблицуToolStripMenuItem, удалитьToolStripMenuItem, копToolStripMenuItem, изменитьToolStripMenuItem });
            gridContextMenuStrip.Name = "contextMenuStrip1";
            gridContextMenuStrip.Size = new Size(140, 92);
            // 
            // создатьТурнирнуюТаблицуToolStripMenuItem
            // 
            создатьТурнирнуюТаблицуToolStripMenuItem.Name = "создатьТурнирнуюТаблицуToolStripMenuItem";
            создатьТурнирнуюТаблицуToolStripMenuItem.Size = new Size(139, 22);
            создатьТурнирнуюТаблицуToolStripMenuItem.Text = "Создать";
            создатьТурнирнуюТаблицуToolStripMenuItem.Click += создатьТурнирнуюТаблицуToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(139, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
            // 
            // копToolStripMenuItem
            // 
            копToolStripMenuItem.Name = "копToolStripMenuItem";
            копToolStripMenuItem.Size = new Size(139, 22);
            копToolStripMenuItem.Text = "Копировать";
            копToolStripMenuItem.Click += копToolStripMenuItem_Click;
            // 
            // изменитьToolStripMenuItem
            // 
            изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            изменитьToolStripMenuItem.Size = new Size(139, 22);
            изменитьToolStripMenuItem.Text = "Изменить";
            изменитьToolStripMenuItem.Click += изменитьToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, турнирнаяТаблицаToolStripMenuItem, участникиToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1343, 24);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { сохранитьToolStripMenuItem, сеткаТурнираToolStripMenuItem, протоколТурнираToolStripMenuItem, выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            сохранитьToolStripMenuItem.Size = new Size(177, 22);
            сохранитьToolStripMenuItem.Text = "Сохранить";
            сохранитьToolStripMenuItem.Click += сохранитьToolStripMenuItem_Click;
            // 
            // сеткаТурнираToolStripMenuItem
            // 
            сеткаТурнираToolStripMenuItem.Name = "сеткаТурнираToolStripMenuItem";
            сеткаТурнираToolStripMenuItem.Size = new Size(177, 22);
            сеткаТурнираToolStripMenuItem.Text = "Сетка турнира";
            сеткаТурнираToolStripMenuItem.Click += сеткаТурнираToolStripMenuItem_Click;
            // 
            // протоколТурнираToolStripMenuItem
            // 
            протоколТурнираToolStripMenuItem.Name = "протоколТурнираToolStripMenuItem";
            протоколТурнираToolStripMenuItem.Size = new Size(177, 22);
            протоколТурнираToolStripMenuItem.Text = "Протокол турнира";
            протоколТурнираToolStripMenuItem.Click += протоколТурнираToolStripMenuItem_Click;
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(177, 22);
            выходToolStripMenuItem.Text = "Выход";
            выходToolStripMenuItem.Click += выходToolStripMenuItem_Click;
            // 
            // турнирнаяТаблицаToolStripMenuItem
            // 
            турнирнаяТаблицаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem, удалитьToolStripMenuItem2, обновитьToolStripMenuItem });
            турнирнаяТаблицаToolStripMenuItem.Name = "турнирнаяТаблицаToolStripMenuItem";
            турнирнаяТаблицаToolStripMenuItem.Size = new Size(126, 20);
            турнирнаяТаблицаToolStripMenuItem.Text = "Турнирная таблица";
            // 
            // создатьToolStripMenuItem
            // 
            создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            создатьToolStripMenuItem.Size = new Size(128, 22);
            создатьToolStripMenuItem.Text = "Создать";
            создатьToolStripMenuItem.Click += создатьТурнирнуюТаблицуToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem2
            // 
            удалитьToolStripMenuItem2.Name = "удалитьToolStripMenuItem2";
            удалитьToolStripMenuItem2.Size = new Size(128, 22);
            удалитьToolStripMenuItem2.Text = "Удалить";
            удалитьToolStripMenuItem2.Click += удалитьToolStripMenuItem_Click;
            // 
            // обновитьToolStripMenuItem
            // 
            обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            обновитьToolStripMenuItem.Size = new Size(128, 22);
            обновитьToolStripMenuItem.Text = "Обновить";
            обновитьToolStripMenuItem.Click += обновитьToolStripMenuItem_Click;
            // 
            // участникиToolStripMenuItem
            // 
            участникиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem1, добавитьToolStripMenuItem1, удалитьToolStripMenuItem3, очиститьToolStripMenuItem });
            участникиToolStripMenuItem.Name = "участникиToolStripMenuItem";
            участникиToolStripMenuItem.Size = new Size(77, 20);
            участникиToolStripMenuItem.Text = "Участники";
            // 
            // создатьToolStripMenuItem1
            // 
            создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
            создатьToolStripMenuItem1.Size = new Size(126, 22);
            создатьToolStripMenuItem1.Text = "Создать";
            создатьToolStripMenuItem1.Click += создатьToolStripMenuItem1_Click;
            // 
            // добавитьToolStripMenuItem1
            // 
            добавитьToolStripMenuItem1.Name = "добавитьToolStripMenuItem1";
            добавитьToolStripMenuItem1.Size = new Size(126, 22);
            добавитьToolStripMenuItem1.Text = "Добавить";
            добавитьToolStripMenuItem1.Click += добавитьToolStripMenuItem1_Click;
            // 
            // удалитьToolStripMenuItem3
            // 
            удалитьToolStripMenuItem3.Name = "удалитьToolStripMenuItem3";
            удалитьToolStripMenuItem3.Size = new Size(126, 22);
            удалитьToolStripMenuItem3.Text = "Удалить";
            удалитьToolStripMenuItem3.Click += удалитьToolStripMenuItem3_Click;
            // 
            // очиститьToolStripMenuItem
            // 
            очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            очиститьToolStripMenuItem.Size = new Size(126, 22);
            очиститьToolStripMenuItem.Text = "Очистить";
            очиститьToolStripMenuItem.Click += очиститьToolStripMenuItem_Click;
            // 
            // gridDataGridView
            // 
            gridDataGridView.AllowDrop = true;
            gridDataGridView.AllowUserToAddRows = false;
            gridDataGridView.AllowUserToDeleteRows = false;
            gridDataGridView.AllowUserToResizeColumns = false;
            gridDataGridView.AllowUserToResizeRows = false;
            gridDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridDataGridView.BackgroundColor = SystemColors.Window;
            gridDataGridView.BorderStyle = BorderStyle.Fixed3D;
            gridDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            gridDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridDataGridView.Location = new Point(11, 107);
            gridDataGridView.Name = "gridDataGridView";
            gridDataGridView.ReadOnly = true;
            gridDataGridView.RowHeadersVisible = false;
            gridDataGridView.RowTemplate.Height = 25;
            gridDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDataGridView.Size = new Size(630, 383);
            gridDataGridView.TabIndex = 14;
            gridDataGridView.MouseDoubleClick += dataGridView1_MouseDoubleClick;
            // 
            // participantGrid
            // 
            participantGrid.AllowDrop = true;
            participantGrid.AllowUserToAddRows = false;
            participantGrid.AllowUserToDeleteRows = false;
            participantGrid.AllowUserToResizeColumns = false;
            participantGrid.AllowUserToResizeRows = false;
            participantGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            participantGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            participantGrid.BackgroundColor = SystemColors.Window;
            participantGrid.BorderStyle = BorderStyle.Fixed3D;
            participantGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            participantGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            participantGrid.Location = new Point(25, 107);
            participantGrid.Name = "participantGrid";
            participantGrid.ReadOnly = true;
            participantGrid.RowHeadersVisible = false;
            participantGrid.RowTemplate.Height = 25;
            participantGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            participantGrid.Size = new Size(638, 383);
            participantGrid.TabIndex = 16;
            participantGrid.CellMouseClick += dataGridView2_CellClick;
            participantGrid.DragDrop += dataGridView_DragDrop;
            participantGrid.DragEnter += dataGridView_DragEnter;
            participantGrid.MouseDoubleClick += dataGridView2_MouseDoubleClick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(11, 82);
            label6.Name = "label6";
            label6.Size = new Size(98, 15);
            label6.TabIndex = 18;
            label6.Text = "Турнирная сетка";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 83);
            label7.Name = "label7";
            label7.Size = new Size(65, 15);
            label7.TabIndex = 19;
            label7.Text = "Участники";
            // 
            // participantContextMenuStrip
            // 
            participantContextMenuStrip.Items.AddRange(new ToolStripItem[] { создатьToolStripMenuItem2, добавитьToolStripMenuItem, удалитьToolStripMenuItem1 });
            participantContextMenuStrip.Name = "contextMenuStrip2";
            participantContextMenuStrip.Size = new Size(127, 70);
            // 
            // создатьToolStripMenuItem2
            // 
            создатьToolStripMenuItem2.Name = "создатьToolStripMenuItem2";
            создатьToolStripMenuItem2.Size = new Size(180, 22);
            создатьToolStripMenuItem2.Text = "Создать";
            создатьToolStripMenuItem2.Click += создатьToolStripMenuItem1_Click;
            // 
            // добавитьToolStripMenuItem
            // 
            добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            добавитьToolStripMenuItem.Size = new Size(180, 22);
            добавитьToolStripMenuItem.Text = "Добавить";
            добавитьToolStripMenuItem.Click += добавитьToolStripMenuItem1_Click;
            // 
            // удалитьToolStripMenuItem1
            // 
            удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
            удалитьToolStripMenuItem1.Size = new Size(180, 22);
            удалитьToolStripMenuItem1.Text = "Удалить";
            удалитьToolStripMenuItem1.Click += удалитьToolStripMenuItem3_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(gridDataGridView);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(note);
            panel1.Controls.Add(name);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(669, 502);
            panel1.TabIndex = 20;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Controls.Add(progressBar1);
            panel2.Controls.Add(secret);
            panel2.Controls.Add(dateTimePicker2);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(secretLable);
            panel2.Controls.Add(participantGrid);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(mainJudge);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(668, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(675, 502);
            panel2.TabIndex = 21;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            progressBar1.Location = new Point(425, 74);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(238, 23);
            progressBar1.TabIndex = 20;
            progressBar1.Visible = false;
            // 
            // TournamentForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1343, 526);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "TournamentForm";
            Text = "Турнир";
            Activated += TournamentForm_Activated;
            Load += TournamentForm_Load;
            Resize += TournamentForm_Resize;
            gridContextMenuStrip.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)participantGrid).EndInit();
            participantContextMenuStrip.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
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
        private ContextMenuStrip gridContextMenuStrip;
        private ToolStripMenuItem создатьТурнирнуюТаблицуToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem турнирнаяТаблицаToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private DataGridView gridDataGridView;
        private DataGridView participantGrid;
        private Label label6;
        private Label label7;
        private ContextMenuStrip participantContextMenuStrip;
        private ToolStripMenuItem добавитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem1;
        private ToolStripMenuItem удалитьToolStripMenuItem2;
        private ToolStripMenuItem участникиToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem1;
        private ToolStripMenuItem добавитьToolStripMenuItem1;
        private ToolStripMenuItem удалитьToolStripMenuItem3;
        private ToolStripMenuItem очиститьToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem протоколТурнираToolStripMenuItem;
        private Panel panel1;
        private Panel panel2;
        private ToolStripMenuItem обновитьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem2;
        private ToolStripMenuItem сеткаТурнираToolStripMenuItem;
        private ToolStripMenuItem копToolStripMenuItem;
        private ProgressBar progressBar1;
        private ToolStripMenuItem изменитьToolStripMenuItem;
    }
}