using Hasen;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kaharman
{
    public class ContextMenuFilter : ContextMenuStrip 
    {
        public bool AddAutoItems = true;
        public virtual string? GetFilter()
        {
            string filter = $"[{Name}] NOT IN (";
            bool bf = false;
            foreach (ToolStripMenuItem item in Items)
            {
                if (item.Checked == false)
                {
                    bf = true;
                    if (item.Text != "Пустые")
                        filter += $"'{item.Text}',";
                    else filter += "'',";
                }
            }
            filter += ")";
            if (bf)
                return filter;
            return null;
        }
    }
    public class ContextMenuFilterName : ContextMenuFilter
    {
        ToolStripTextBox TextBox = new ToolStripTextBox();
        public ContextMenuFilterName() : base()
        {
            AddAutoItems = false;
            TextBox.TextChanged += TextBox_TextChanged;
            Items.Add(TextBox);
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            OnItemClicked(null);
        }

        public override string? GetFilter()
        {
            if (TextBox.Text.Length != 0)
                return $"[{Name}] LIKE '%{TextBox.Text}%'";
            return null;
        }
    }
    public class ContextMenuFilterWeigth: ContextMenuFilter
    {
        public ContextMenuFilterWeigth(List<string> values) : base()
        {
            AddAutoItems = false;
            foreach (string value in values)
                ((ToolStripMenuItem)Items.Add(value)).Checked = true;
        }
        public override string? GetFilter()
        {
            List<string> filters = new List<string>();
            bool bf = false;
            foreach (ToolStripMenuItem item in Items)
            {
                if (item.Checked == false)
                {
                    bf = true;
                    string[] filter = item.Text.Split('-');
                    if (filter.Length != 2)
                        continue;
                    filters.Add($"{Name} < {filter[0]} AND {Name} > {filter[1]}");
                }
            }
            if (bf)
                return string.Join(" AND ", filters);
            return null;
        }
    }
    public class DataTableContextMenu : DataTable
    {
        List<ContextMenuStrip> listContextMenu = new List<ContextMenuStrip>();
        public DataView DataView { get; }
        public DataTableContextMenu() : base()
        {
            RowChanged += DataTableContextMenu_RowChanged; 
            DataView = new DataView(this);
        }
        public void AddColunm(string name)
        {
            Columns.Add(name);
            ContextMenuFilter contextMenu = new ContextMenuFilter()
            {
                Name = name,
                AutoClose = false,
                ShowCheckMargin = true
            };
            contextMenu.ItemClicked += ContextMenuStrip_ItemClicked;
            listContextMenu.Add(contextMenu);
        }
        public void AddColunm(string name, ContextMenuFilter contextMenu)
        {
            Columns.Add(name);
            contextMenu.Name = name;
            contextMenu.AutoClose = false;
            contextMenu.ShowCheckMargin = true;
            contextMenu.ItemClicked += ContextMenuStrip_ItemClicked;
            listContextMenu.Add(contextMenu);
        }
        public void AddColunm(string name, Type type)
        {
            Columns.Add(name, type);
            ContextMenuFilter contextMenuStrip = new ContextMenuFilter()
            {
                AutoClose = false,
                Name = name,
                ShowCheckMargin = true
            };
            contextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
            listContextMenu.Add(contextMenuStrip);
        }
        public void AddColunm(string name, Type type, ContextMenuFilter contextMenu)
        {
            Columns.Add(name, type);
            contextMenu.Name = name;
            contextMenu.AutoClose = false;
            contextMenu.ShowCheckMargin = true;
            contextMenu.ItemClicked += ContextMenuStrip_ItemClicked;
            listContextMenu.Add(contextMenu);
        }
        private void ContextMenuStrip_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuFilterName? contextMenu = sender as ContextMenuFilterName;
            if(contextMenu != null)
            {
                FilterContent();
                return;
            }
            ContextMenuStrip? contextMenuStrip = sender as ContextMenuStrip;
            if(contextMenuStrip != null)
            {
                ToolStripMenuItem? toolStrip = e.ClickedItem as ToolStripMenuItem;
                if (toolStrip != null)
                {
                    if (toolStrip.Checked)
                        toolStrip.Checked = false;
                    else toolStrip.Checked = true;
                    FilterContent();
                }
            }            
        }
        private void FilterContent() {
            List<string> filters = new List<string>();
            foreach(ContextMenuFilter contextMenuStrip in listContextMenu)
            {
                string? filter = contextMenuStrip.GetFilter();
                if (filter != null)
                {
                    filters.Add(filter);
                }
            }
            DataView.RowFilter = string.Join(" AND ", filters);
        }      
        private void DataTableContextMenu_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                object[]? cells = e.Row.ItemArray;
                if (cells != null && cells.Length == listContextMenu.Count)
                {
                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (((ContextMenuFilter)listContextMenu[i]).AddAutoItems)
                        {
                            string? item = cells[i].ToString();
                            if (item != null && item.Length != 0)
                                AddItemContextMenu(listContextMenu[i], item);
                        }
                    }
                }
            }
        }        
        public void ShowContextMenu(int IdColumn, Point Position)
        {
            CloseContextMenu();
            listContextMenu[IdColumn].Show(Position, ToolStripDropDownDirection.Default);
        }
        public void CloseContextMenu()
        {
            foreach (ContextMenuStrip c in listContextMenu)
                c.Close();
        }
        public void FillTable(DataTable table)
        {
            foreach(DataRow row in table.Rows)
            {
                this.AddRow(row);
            }
        }
        public void FillTableOnAccess(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                var objects = row.ItemArray;
                objects[3] = (int)(DateTime.Now - DateTime.Parse(row["date_of_birth"].ToString())).TotalDays / ParticipantForm.ValyeDayYear;
                AddRow(objects);
            }
        }
        public void AddRow(DataRow Row)
        {
            object[]? cells = Row.ItemArray;
            if (cells != null && cells.Length == listContextMenu.Count)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (((ContextMenuFilter)listContextMenu[i]).AddAutoItems)
                    {
                        string? item = cells[i].ToString();
                        if (item != null && item.Length != 0)
                            AddItemContextMenu(listContextMenu[i], item);
                    }
                }
            }
            this.Rows.Add(cells);
        }
        public void AddRow(object[]? cells)
        {
            if (cells != null && cells.Length == listContextMenu.Count)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (((ContextMenuFilter)listContextMenu[i]).AddAutoItems)
                    {
                        string? item = cells[i].ToString();
                        if (item != null && item.Length != 0)
                            AddItemContextMenu(listContextMenu[i], item);
                    }
                }
            }
            this.Rows.Add(cells);
        }
        private void AddItemContextMenu(ContextMenuStrip menuStrip, string item)
        {
            foreach (ToolStripMenuItem toolStrip in menuStrip.Items)
                if (toolStrip.Text == item)
                    return;
            ((ToolStripMenuItem)menuStrip.Items.Add(item)).Checked = true;
            
        }
        public DataRow? GetRowToID(string id)
        {
            foreach(DataRow row in Rows)
            {
                if (row["ID"].ToString() == id)
                    return row;
            }
            return null;
        }
        public void DeleteRow(DataRow row)
        {
            Rows.Remove(row);
        }
        public void DeleteRow(string id)
        {
            foreach (DataRow row in Rows)
            {
                if (row["ID"].ToString() == id)
                {
                    Rows.Remove(row);
                    return;
                }
            }
        }
    }
    public class ParticipantDataTable : DataTableContextMenu {
        AccessSQL AccessSQL;
        public ParticipantDataTable(AccessSQL accessSQL) : base() {
            AccessSQL = accessSQL;
            Initialize();
        }
        public ParticipantDataTable(DataGridView dataGridView, AccessSQL accessSQL) : base()
        {
            AccessSQL = accessSQL;
            Initialize();
            dataGridView.DataSource = DataView;
            dataGridView.MouseClick += MouseClick;
            dataGridView.ColumnHeaderMouseClick += ColumnHeaderMouseClick;
            dataGridView.Columns[0].Visible = false;
        }
        private void Initialize()
        {
            AddColunm("ID", typeof(int));

            ContextMenuFilterName contextMenuName = new ContextMenuFilterName();
            AddColunm("Фамилия и имя", contextMenuName);

            AddColunm("Пол");

            AddColunm("Возраст", typeof(int));

            ContextMenuFilter contextMenuWeigth = new ContextMenuFilter();
            AddColunm("Вес", contextMenuWeigth);

            ContextMenuFilter contextMenuQualiti = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuQualiti.Items.Add("Пустые")).Checked = true;
            AddColunm("Квалификация", contextMenuQualiti);

            ContextMenuFilter contextMenuCity = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuCity.Items.Add("Пустые")).Checked = true;
            AddColunm("Город", contextMenuCity);

            ContextMenuFilter contextMenuTrainer = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuTrainer.Items.Add("Пустые")).Checked = true;
            AddColunm("Тренер", contextMenuTrainer);

            RowChanged += ParticipantsTable_RowChanged;
        }
        private void ParticipantsTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                DataRow row = e.Row;
                DataTable data = AccessSQL.GetDataTableSQL($"SELECT id FROM Participants WHERE name = '{row[1]}'");
                if (data.Rows.Count == 0)
                {
                    AccessSQL.SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],weight,qualification,city,trainer) VALUES ('{row[1]}','{row[2]}','{((DateTime)row["Дата рождения"]).ToString("dd.MM.yyyy")}',{row[4]},'{row[5]}','{row[6]}','{row[7]}')");
                    row["ID"] = AccessSQL.GetIDInsert();
                }
                else
                {
                    row["ID"] = data.Rows[0]["id"];
                    AccessSQL.SendSQL($"UPDATE Participants SET weight = {row[4]}, qualification = '{row[5]}', city = '{row[6]}', trainer = '{row[7]}' WHERE id = {row["ID"]}");
                }
            }
        }
        public string GetIDsPartString()
        {
            string[] strings = new string[Rows.Count];
            for(int i = 0; i < Rows.Count; i++)
                strings[i] = $"\"{Rows[i]["ID"]}\"";
            return string.Join(";",strings);
        }
        public void LoadPartOnIDs(string ids)
        {
            string[] strings = ids.Split(';');
            for(int i = 0; i < strings.Length; i++)
                strings[i] = strings[i].Trim('\"');
            if (ids.Length > 0)
            {
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ", strings)})"))
                {
                    this.FillTableOnAccess(data);
                }
            }
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
                    sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();
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
        public void LoadDataOnFiles(string[] files)
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
                                foreach (DataRow r in Rows)
                                {
                                    if (r[1].ToString() == rowExcel[i][1].ToString())
                                    {
                                        cont = true;
                                        break;
                                    }
                                }
                                if (cont)
                                    continue;
                                DataRow newRow = NewRow();
                                newRow[1] = rowExcel[i][1].ToString();
                                newRow[2] = rowExcel[i][2].ToString();
                                if (DateTime.TryParse(rowExcel[i][3].ToString(), out DateTime time))
                                    newRow[3] = (int)((DateTime.Now - time).TotalDays / ParticipantForm.ValyeDayYear);
                                if (float.TryParse(rowExcel[i][5].ToString(), new NumberFormatInfo { NumberDecimalSeparator = "." }, out float wight))
                                    newRow[4] = wight;
                                else
                                {
                                    if (float.TryParse(rowExcel[i][5].ToString(), new NumberFormatInfo { NumberDecimalSeparator = "," }, out wight))
                                        newRow[4] = wight;
                                }
                                newRow[5] = rowExcel[i][6].ToString().ToLower();
                                newRow[6] = rowExcel[i][12].ToString();
                                newRow[7] = rowExcel[i][13].ToString();
                                Rows.Add(newRow);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                MessageBox.Show("Загрузка завершена");
            }
        }
        public void ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowContextMenu(e.ColumnIndex, Control.MousePosition);
            }
        }
        public void MouseClick(object sender, MouseEventArgs e)
        {
            CloseContextMenu();
        }
    }
}
