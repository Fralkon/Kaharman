namespace Hasen
{
    partial class Kaharman
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.турнирыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьУчастниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.историяТурнировToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.участникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.найтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.тренераToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базаДанныхToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.весовыеКатегорииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.городаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.создатьТурнирToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.турнирыToolStripMenuItem,
            this.участникиToolStripMenuItem,
            this.тренераToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(48, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(109, 22);
            выходToolStripMenuItem.Text = "Выход";
            // 
            // турнирыToolStripMenuItem
            // 
            турнирыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { создатьToolStripMenuItem, показатьУчастниковToolStripMenuItem, историяТурнировToolStripMenuItem });
            турнирыToolStripMenuItem.Name = "турнирыToolStripMenuItem";
            турнирыToolStripMenuItem.Size = new Size(59, 20);
            турнирыToolStripMenuItem.Text = "Турнир";
            // 
            // создатьToolStripMenuItem
            // 
            создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            создатьToolStripMenuItem.Size = new Size(190, 22);
            создатьToolStripMenuItem.Text = "Создать";
            создатьToolStripMenuItem.Click += создатьToolStripMenuItem_Click;
            // 
            // показатьУчастниковToolStripMenuItem
            // 
            показатьУчастниковToolStripMenuItem.Name = "показатьУчастниковToolStripMenuItem";
            показатьУчастниковToolStripMenuItem.Size = new Size(190, 22);
            показатьУчастниковToolStripMenuItem.Text = "Показать участников";
            показатьУчастниковToolStripMenuItem.Click += показатьУчастниковToolStripMenuItem_Click_1;
            // 
            // историяТурнировToolStripMenuItem
            // 
            историяТурнировToolStripMenuItem.Name = "историяТурнировToolStripMenuItem";
            историяТурнировToolStripMenuItem.Size = new Size(190, 22);
            историяТурнировToolStripMenuItem.Text = "История турниров";
            историяТурнировToolStripMenuItem.Click += историяТурнировToolStripMenuItem_Click;
            // 
            // участникиToolStripMenuItem
            // 
            участникиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { добавитьToolStripMenuItem, найтиToolStripMenuItem, базаДанныхToolStripMenuItem });
            участникиToolStripMenuItem.Name = "участникиToolStripMenuItem";
            участникиToolStripMenuItem.Size = new Size(77, 20);
            участникиToolStripMenuItem.Text = "Участники";
            // 
            // добавитьToolStripMenuItem
            // 
            добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            добавитьToolStripMenuItem.Size = new Size(142, 22);
            добавитьToolStripMenuItem.Text = "Добавить";
            добавитьToolStripMenuItem.Click += добавитьToolStripMenuItem_Click;
            // 
            // найтиToolStripMenuItem
            // 
            найтиToolStripMenuItem.Name = "найтиToolStripMenuItem";
            найтиToolStripMenuItem.Size = new Size(142, 22);
            найтиToolStripMenuItem.Text = "Найти";
            // 
            // базаДанныхToolStripMenuItem
            // 
            базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            базаДанныхToolStripMenuItem.Size = new Size(142, 22);
            базаДанныхToolStripMenuItem.Text = "База данных";
            базаДанныхToolStripMenuItem.Click += базаДанныхToolStripMenuItem_Click_1;
            // 
            // тренераToolStripMenuItem
            // 
            this.тренераToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem1,
            this.показатьToolStripMenuItem,
            this.базаДанныхToolStripMenuItem1});
            this.тренераToolStripMenuItem.Name = "тренераToolStripMenuItem";
            this.тренераToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.тренераToolStripMenuItem.Text = "Тренера";
            // 
            // добавитьToolStripMenuItem1
            // 
            this.добавитьToolStripMenuItem1.Name = "добавитьToolStripMenuItem1";
            this.добавитьToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.добавитьToolStripMenuItem1.Text = "Добавить";
            // 
            // показатьToolStripMenuItem
            // 
            this.показатьToolStripMenuItem.Name = "показатьToolStripMenuItem";
            this.показатьToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.показатьToolStripMenuItem.Text = "Показать";
            // 
            // базаДанныхToolStripMenuItem1
            // 
            this.базаДанныхToolStripMenuItem1.Name = "базаДанныхToolStripMenuItem1";
            this.базаДанныхToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.базаДанныхToolStripMenuItem1.Text = "База данных";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.весовыеКатегорииToolStripMenuItem,
            this.городаToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // весовыеКатегорииToolStripMenuItem
            // 
            весовыеКатегорииToolStripMenuItem.Name = "весовыеКатегорииToolStripMenuItem";
            весовыеКатегорииToolStripMenuItem.Size = new Size(180, 22);
            весовыеКатегорииToolStripMenuItem.Text = "Весовые категории";
            весовыеКатегорииToolStripMenuItem.Click += весовыеКатегорииToolStripMenuItem_Click;
            // 
            // городаToolStripMenuItem
            // 
            this.городаToolStripMenuItem.Name = "городаToolStripMenuItem";
            this.городаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.городаToolStripMenuItem.Text = "Города";
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
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(834, 494);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { создатьТурнирToolStripMenuItem, удалитьToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(119, 48);
            // 
            // создатьТурнирToolStripMenuItem
            // 
            создатьТурнирToolStripMenuItem.Name = "создатьТурнирToolStripMenuItem";
            создатьТурнирToolStripMenuItem.Size = new Size(118, 22);
            создатьТурнирToolStripMenuItem.Text = "Создать";
            создатьТурнирToolStripMenuItem.Click += создатьToolStripMenuItem_Click;
            // 
            // удалитьToolStripMenuItem
            // 
            удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            удалитьToolStripMenuItem.Size = new Size(118, 22);
            удалитьToolStripMenuItem.Text = "Удалить";
            удалитьToolStripMenuItem.Click += удалитьToolStripMenuItem_Click;
            // 
            // Kaharman
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 518);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Kaharman";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem турнирыToolStripMenuItem;
        private ToolStripMenuItem участникиToolStripMenuItem;
        private DataGridView dataGridView1;
        private ToolStripMenuItem добавитьToolStripMenuItem;
        private ToolStripMenuItem настройкиToolStripMenuItem;
        private ToolStripMenuItem весовыеКатегорииToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private ToolStripMenuItem историяТурнировToolStripMenuItem;
        private ToolStripMenuItem найтиToolStripMenuItem;
        private ToolStripMenuItem базаДанныхToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem создатьТурнирToolStripMenuItem;
        private ToolStripMenuItem показатьУчастниковToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
    }
}