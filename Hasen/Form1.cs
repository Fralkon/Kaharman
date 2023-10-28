using Kaharman;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Xml.Linq;

namespace Hasen
{
    public partial class Form1 : Form
    {
        AccessSQL AccessSQL { get; set; }
        DataTableContextMenu ParticipantsTable = new DataTableContextMenu();
        public Form1()
        {
            InitializeComponent();
            AccessSQL = new AccessSQL();
            ParticipantsTable.AddColunm("ID", typeof(int));

            ContextMenuFilter contextMenuName = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuName.Items.Add("Пустые")).Checked = true;
            ParticipantsTable.AddColunm("Фамилия и имя", contextMenuName);

            ParticipantsTable.AddColunm("Пол");

            ParticipantsTable.AddColunm("Дата рождения");
            ParticipantsTable.AddColunm("Возраст", typeof(int));
            ParticipantsTable.AddColunm("Вес", typeof(float));

            ContextMenuFilter contextMenuQualiti = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuQualiti.Items.Add("Пустые")).Checked = true;
            ParticipantsTable.AddColunm("Квалификация", contextMenuQualiti);

            ContextMenuFilter contextMenuCity = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuCity.Items.Add("Пустые")).Checked = true;
            ParticipantsTable.AddColunm("Город", contextMenuCity);

            ContextMenuFilter contextMenuTrainer = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuTrainer.Items.Add("Пустые")).Checked = true;
            ParticipantsTable.AddColunm("Тренер", contextMenuTrainer);
            ParticipantsTable.RowChanged += ParticipantsTable_RowChanged;
            dataGridView1.DataSource = ParticipantsTable.dataView;
        }
        private void ParticipantsTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                try
                {
                    DataRow row = e.Row;
                    DataTable data = AccessSQL.GetDataTableSQL($"SELECT id FROM Participants WHERE name = '{row[1]}'");
                    if (data.Rows.Count == 0)
                    {
                        AccessSQL.InsertSQL($"INSERT INTO Participants (name,gender,date_of_birth,age,weight,qualification,city,trainer) VALUES ('{row[1]}','{row[2]}','{row[3]}',{row[4]},{row[5]},'{row[6]}','{row[7]}','{row[8]}')");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + "/Database.accdb"))
            {
                MessageBox.Show($"Файл базы данных не найден\n{Environment.CurrentDirectory}Database.accdb");
                Close();
            }
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Participants participants = new Participants();
            participants.ShowDialog();
        }
        private void городаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cites cites = new Cites(AccessSQL);
            cites.ShowDialog();
        }
        private void клубыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clubs clubs = new Clubs(AccessSQL);
            clubs.ShowDialog();
        }
        private void весовыеКатегорииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category category = new Category(AccessSQL);
            category.ShowDialog();
        }
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("Перетащите файлы");
                return;
            }
            LoadData(files);
        }
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private DataTable LoadDataTableOnFile(string file)
        {
            using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0;HDR=YES;\""))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string sheetName = String.Empty;
                try
                {
                    // dr["TABLE_NAME"] = "Table";
                    sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();
                    // Get all rows from the Sheet
                    cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                    DataTable dt = new DataTable();
                    dt.TableName = sheetName;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Ошибка заполнения таблицы. Error:{0}", ex.Message));
                    return null;
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trainers trainers = new Trainers();
            trainers.ShowDialog();
        }
        private void LoadData(string[] files)
        {
            if (files.Length != 0)
            {
                foreach (string file in files)
                {
                    using (DataTable dataExcel = LoadDataTableOnFile(file))
                    {
                        try
                        {
                            var rowExcel = dataExcel.Rows;
                            for (int i = 4; i < rowExcel.Count; i++)
                            {
                                if (rowExcel[i][1].ToString() == "")
                                    continue;
                                bool cont = false;
                                foreach (DataRow r in ParticipantsTable.Rows)
                                {
                                    if (r[1].ToString() == rowExcel[i][1].ToString())
                                    {
                                        cont = true;
                                        break;
                                    }
                                }
                                if (cont)
                                    continue;
                                DataRow newRow = ParticipantsTable.NewRow();
                                newRow[0] = ParticipantsTable.Rows.Count + 1;
                                newRow[1] = rowExcel[i][1].ToString();
                                newRow[2] = rowExcel[i][2].ToString();
                                if (DateTime.TryParse(rowExcel[i][3].ToString(), out DateTime time))
                                    newRow[3] = time.ToString("dd.MM.yyyy");
                                if (int.TryParse(rowExcel[i][4].ToString(), out int year))
                                    newRow[4] = year;
                                if (float.TryParse(rowExcel[i][5].ToString(), new NumberFormatInfo { NumberDecimalSeparator = "." }, out float wight))
                                    newRow[5] = wight;
                                else
                                {
                                    if (float.TryParse(rowExcel[i][5].ToString(), new NumberFormatInfo { NumberDecimalSeparator = "," }, out wight))
                                        newRow[5] = wight;
                                }
                                newRow[6] = rowExcel[i][6].ToString().ToLower();
                                newRow[7] = rowExcel[i][12].ToString();
                                newRow[8] = rowExcel[i][13].ToString();
                                ParticipantsTable.Rows.Add(newRow);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                dataGridView1.DataSource = ParticipantsTable.dataView;
                MessageBox.Show("Загрузка завершена");
            }
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ParticipantsTable.ShowContextMenu(e.ColumnIndex,MousePosition);
            }
        }
        private void ToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem? toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                if (toolStripMenuItem.Checked)
                    toolStripMenuItem.Checked = false;
                else
                    toolStripMenuItem.Checked = true;
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            ParticipantsTable.CloseContextMenu();
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem? toolStrip = e.ClickedItem as ToolStripMenuItem;
            if (toolStrip != null)
            {
                if (toolStrip.Checked)
                    toolStrip.Checked = false;
                else toolStrip.Checked = true;
            }
        }
    }
}