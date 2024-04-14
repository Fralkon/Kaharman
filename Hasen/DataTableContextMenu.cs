using Hasen;
using NPOI.SS.Formula.Functions;
using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;

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
                    if (item.Text == " Выделить всё")
                        continue;
                    else if (item.Text != " Пустые")
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
                    if (toolStrip.Text == " Выделить всё")
                    {
                        bool value;
                        if (toolStrip.Checked)
                            value = false;
                        else value = true;
                        toolStrip.Checked = value;
                        for (int i = 0; i < contextMenuStrip.Items.Count; i++)
                            ((ToolStripMenuItem)contextMenuStrip.Items[i]).Checked = value; 
                    }
                    else {
                        if (toolStrip.Checked)
                            toolStrip.Checked = false;
                        else toolStrip.Checked = true;
                    }
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
            if (filters.Count > 0)
                DataView.RowFilter = string.Join(" AND ", filters);
            else
                DataView.RowFilter = String.Empty;
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
        public async void FillTable(DataTable table, Form form, ProgressBar progressBar)
        {
            progressBar.Visible = true;
            progressBar.Maximum = table.Rows.Count;
            await Task.Run(() =>
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    form.Invoke(() => {
                        this.AddRow(row);
                    });
                    progressBar.Value = i;
                }
            });
            progressBar.Visible = false;
        }
        public async void FillTableOnAccess(DataTable table,Form form, ProgressBar progressBar)
        {
            progressBar.Visible = true;
            progressBar.Maximum = table.Rows.Count;
            await Task.Run(() =>
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    var objects = row.ItemArray;
                    objects[3] = (int)((int)(DateTime.Now - DateTime.Parse(row["date_of_birth"].ToString())).TotalDays / Participant.ValyeDayYear);
                    form.Invoke(() => {
                        AddRow(objects);
                    });
                    progressBar.Value = i;
                }
            });
            progressBar.Visible = false;
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
            System.Collections.IList list = menuStrip.Items;
            List<string> text = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                ToolStripMenuItem? toolStrip = list[i] as ToolStripMenuItem;
                if (toolStrip != null)
                {
                    if (toolStrip.Text == item)
                        return;
                    text.Add(toolStrip.Text);
                }
            }
            text.Add(item);
            text.Sort();
            
            for (int i =0; i < text.Count; ++i)
            {
                if (text[i] == item)
                {
                    ToolStripMenuItem toolStrip = new ToolStripMenuItem();
                    toolStrip.Text = item;
                    toolStrip.Checked = true;
                    menuStrip.Items.Insert(i, toolStrip);
                }
            }      
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
            for (int j = 0; j < Columns.Count; j++)
            {
                if (row[j].ToString() == "" || !((ContextMenuFilter)listContextMenu[j]).AddAutoItems)
                    continue;
                DeleteItemContextMenu(j, row[j].ToString(), (ContextMenuFilter)listContextMenu[j]);
            }
            Rows.Remove(row);
        }
        private void DeleteItemContextMenu(int index, string item, ContextMenuFilter contextMenu)
        {
            int c = 0;
            for (int i = 0; i < Rows.Count; i++)
            {
                if (Rows[i][index].ToString() == item)
                    c++;
                if (c == 2)
                    return;
            }
            for (int i = 0; i < contextMenu.Items.Count; i++)
            {
                if (contextMenu.Items[i].Text == item)
                {
                    contextMenu.Items.RemoveAt(i);
                    return;
                }
            }
        }
        public void DeleteRow(string id)
        {
            foreach (DataRow row in Rows)
            {
                if (row["ID"].ToString() == id)
                {
                    for (int j = 0; j < Columns.Count; j++)
                    {
                        if (row[j].ToString() == "" || !((ContextMenuFilter)listContextMenu[j]).AddAutoItems)
                            continue;
                        DeleteItemContextMenu(j, row[j].ToString(), (ContextMenuFilter)listContextMenu[j]);
                    }
                    Rows.Remove(row);                   
                    return;
                }
            }
        }
    }
    public class ParticipantDataTable : DataTableContextMenu {
        public ParticipantDataTable() : base() {
            Initialize();
        }
        public ParticipantDataTable(DataGridView dataGridView) : base()
        {
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

            ContextMenuFilter contextMenuAge = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuAge.Items.Add(" Выделить всё")).Checked = true;
            AddColunm("Возраст", typeof(int), contextMenuAge);

            ContextMenuFilter contextMenuWeigth = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuWeigth.Items.Add(" Выделить всё")).Checked = true;
            AddColunm("Вес",typeof(float), contextMenuWeigth);

            ContextMenuFilter contextMenuQualiti = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuQualiti.Items.Add(" Выделить всё")).Checked = true;
            ((ToolStripMenuItem)contextMenuQualiti.Items.Add(" Пустые")).Checked = true;
            AddColunm("Квалификация", contextMenuQualiti);

            ContextMenuFilter contextMenuCity = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuCity.Items.Add(" Выделить всё")).Checked = true;
            ((ToolStripMenuItem)contextMenuCity.Items.Add(" Пустые")).Checked = true;
            AddColunm("Город", contextMenuCity);

            ContextMenuFilter contextMenuTrainer = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuTrainer.Items.Add(" Выделить всё")).Checked = true;
            ((ToolStripMenuItem)contextMenuTrainer.Items.Add(" Пустые")).Checked = true;
            AddColunm("Тренер", contextMenuTrainer);
        }
        public string GetIDsPartString()
        {
            string[] strings = new string[Rows.Count];
            for(int i = 0; i < Rows.Count; i++)
                strings[i] = $"\"{Rows[i]["ID"]}\"";
            return string.Join(";",strings);
        }
        public void LoadPartOnIDs(string ids, Form form, ProgressBar progressBar)
        {
            string[] strings = ids.Split(';');
            for(int i = 0; i < strings.Length; i++)
                strings[i] = strings[i].Trim('\"');
            if (ids.Length > 0)
            {
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ", strings)})"))
                {
                    this.FillTableOnAccess(data, form, progressBar);
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
                                Participant participant = new Participant();
                                participant.Name = rowExcel[i][1].ToString().Trim();
                                participant.Gender = ValidateGender(rowExcel[i][2].ToString().ToLower().Trim());
                                if (DateTime.TryParse(rowExcel[i][3].ToString(), out DateTime time))
                                    participant.DayOfBirth = time;
                                try
                                {
                                    if (float.TryParse(rowExcel[i][5].ToString().Replace(',', '.'), new NumberFormatInfo { NumberDecimalSeparator = "." }, out float wight))
                                        participant.Weight = wight;
                                }
                                catch
                                {
                                    participant.Weight = 0;
                                }
                                participant.Gualiti = rowExcel[i][6].ToString().ToLower().Trim();
                                participant.City = rowExcel[i][12].ToString().Trim();
                                participant.Trainer = rowExcel[i][13].ToString().Trim();
                                AccessSQL.UpDateParticipant(participant);
                                Rows.Add(participant.GetDataRow(this));
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
        private string ValidateGender(string gender)
        {
            if (gender == "муж")
                return "м";
            else if (gender == "жен")
                return "ж";
            else
                return gender;
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
