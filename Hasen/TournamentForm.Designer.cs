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
            this.components = new System.ComponentModel.Container();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.secretLable = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.note = new System.Windows.Forms.TextBox();
            this.mainJudge = new System.Windows.Forms.TextBox();
            this.secret = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.создатьТурнирнуюТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.турнирнаяТаблицаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.участникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(1237, 435);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Наименование";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(694, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дата начала";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1017, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата окончания";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Примечание";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(694, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Главный судья";
            // 
            // secretLable
            // 
            this.secretLable.AutoSize = true;
            this.secretLable.Location = new System.Drawing.Point(1017, 76);
            this.secretLable.Name = "secretLable";
            this.secretLable.Size = new System.Drawing.Size(64, 15);
            this.secretLable.TabIndex = 7;
            this.secretLable.Text = "Секретарь";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(133, 44);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(521, 23);
            this.name.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(788, 44);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(199, 23);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(1118, 44);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(194, 23);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(133, 73);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(521, 23);
            this.note.TabIndex = 11;
            // 
            // mainJudge
            // 
            this.mainJudge.Location = new System.Drawing.Point(788, 73);
            this.mainJudge.Name = "mainJudge";
            this.mainJudge.Size = new System.Drawing.Size(199, 23);
            this.mainJudge.TabIndex = 12;
            // 
            // secret
            // 
            this.secret.Location = new System.Drawing.Point(1118, 73);
            this.secret.Name = "secret";
            this.secret.Size = new System.Drawing.Size(194, 23);
            this.secret.TabIndex = 13;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьТурнирнуюТаблицуToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // создатьТурнирнуюТаблицуToolStripMenuItem
            // 
            this.создатьТурнирнуюТаблицуToolStripMenuItem.Name = "создатьТурнирнуюТаблицуToolStripMenuItem";
            this.создатьТурнирнуюТаблицуToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.создатьТурнирнуюТаблицуToolStripMenuItem.Text = "Создать";
            this.создатьТурнирнуюТаблицуToolStripMenuItem.Click += new System.EventHandler(this.создатьТурнирнуюТаблицуToolStripMenuItem_Click);
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.турнирнаяТаблицаToolStripMenuItem,
            this.участникиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1343, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // турнирнаяТаблицаToolStripMenuItem
            // 
            this.турнирнаяТаблицаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.удалитьToolStripMenuItem2});
            this.турнирнаяТаблицаToolStripMenuItem.Name = "турнирнаяТаблицаToolStripMenuItem";
            this.турнирнаяТаблицаToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.турнирнаяТаблицаToolStripMenuItem.Text = "Турнирная таблица";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            // 
            // удалитьToolStripMenuItem2
            // 
            this.удалитьToolStripMenuItem2.Name = "удалитьToolStripMenuItem2";
            this.удалитьToolStripMenuItem2.Size = new System.Drawing.Size(118, 22);
            this.удалитьToolStripMenuItem2.Text = "Удалить";
            // 
            // участникиToolStripMenuItem
            // 
            this.участникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem1,
            this.добавитьToolStripMenuItem1,
            this.удалитьToolStripMenuItem3,
            this.очиститьToolStripMenuItem});
            this.участникиToolStripMenuItem.Name = "участникиToolStripMenuItem";
            this.участникиToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.участникиToolStripMenuItem.Text = "Участники";
            // 
            // создатьToolStripMenuItem1
            // 
            this.создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
            this.создатьToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.создатьToolStripMenuItem1.Text = "Создать";
            // 
            // добавитьToolStripMenuItem1
            // 
            this.добавитьToolStripMenuItem1.Name = "добавитьToolStripMenuItem1";
            this.добавитьToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.добавитьToolStripMenuItem1.Text = "Добавить";
            // 
            // удалитьToolStripMenuItem3
            // 
            this.удалитьToolStripMenuItem3.Name = "удалитьToolStripMenuItem3";
            this.удалитьToolStripMenuItem3.Size = new System.Drawing.Size(126, 22);
            this.удалитьToolStripMenuItem3.Text = "Удалить";
            // 
            // очиститьToolStripMenuItem
            // 
            this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.очиститьToolStripMenuItem.Text = "Очистить";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(35, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(619, 261);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowDrop = true;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(694, 143);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 25;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(618, 261);
            this.dataGridView2.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Турнирная сетка";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(694, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "Участники";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.удалитьToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(127, 48);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            // 
            // удалитьToolStripMenuItem1
            // 
            this.удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
            this.удалитьToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.удалитьToolStripMenuItem1.Text = "Удалить";
            // 
            // TournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 485);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.secret);
            this.Controls.Add(this.mainJudge);
            this.Controls.Add(this.note);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.name);
            this.Controls.Add(this.secretLable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TournamentForm";
            this.Text = "Турнир";
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem создатьТурнирнуюТаблицуToolStripMenuItem;
        private ToolStripMenuItem изменитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem турнирнаяТаблицаToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Label label6;
        private Label label7;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem добавитьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem1;
        private ToolStripMenuItem удалитьToolStripMenuItem2;
        private ToolStripMenuItem участникиToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem1;
        private ToolStripMenuItem добавитьToolStripMenuItem1;
        private ToolStripMenuItem удалитьToolStripMenuItem3;
        private ToolStripMenuItem очиститьToolStripMenuItem;
    }
}