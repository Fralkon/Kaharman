using Hasen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.Streaming.Values;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Forms;
using static NPOI.HSSF.Util.HSSFColor;

namespace Kaharman
{
    public enum TypeContextFilter
    {
        Value,
        Name
    }
    public abstract class ContextFilter : ContextMenuStrip
    {
        public ContextFilter(int index) : base()
        {
            Index = index;
        }
        public int Index { get;set; }
        public abstract bool Filter(string value);
        public TypeContextFilter TypeContext;
    }
    public class ContextFilterValue : ContextFilter
    {
        public ContextFilterValue(int index) : base(index)
        {
            TypeContext = TypeContextFilter.Value;
        }
        public override bool Filter(string value)
        {
            foreach (ToolStripMenuItem item in this.Items)
            {
                if (item.Checked)
                {
                    if (item.Text == " Выделить всё")
                        continue;
                    else if (item.Text == " Пустые")
                    {
                        if (value == "")
                            return true;
                    }
                    else
                    {
                        if (value == item.Text)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void AddValue(string value)
        {
            if (value != String.Empty)
            {
                foreach (ToolStripMenuItem v in Items)
                    if (v.Text == value)
                        return;
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem() { Text = value, Checked = true };
                Items.Add(toolStripMenuItem);
            }
        }
    }
    public class ContextFilterName : ContextFilter
    {
        ToolStripTextBox TextBox;
        public ContextFilterName(int index) : base(index)
        {
            TypeContext = TypeContextFilter.Name;
            TextBox = new ToolStripTextBox(); 
            TextBox.TextChanged += TextBox_TextChanged;
            Items.Add(TextBox);
            AutoClose = false;
        }
        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            OnItemClicked(new ToolStripItemClickedEventArgs(TextBox));
        }
        public string GetValye()
        {
            return TextBox.Text;
        }
        public override bool Filter(string value)
        {
            if (TextBox.Text == "")
                return true;
            return value.IndexOf(TextBox.Text) != -1;
        }
    }
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
        public int Index {get;set;} = 1;
    }
    public abstract class TableContextMenu<T>
    {
        protected BindingList<T> bindingList = new BindingList<T>();
        protected BindingSource pSource = new BindingSource();
        protected List<ContextFilter> listContextMenu = new List<ContextFilter>();
        protected DataGridView GridView;
        protected ContextMenuStrip? DataGridContextMenu;
        public TableContextMenu(DataGridView dataGridView, ContextMenuStrip? dataGridContextMenu)
        {
            bindingList.AllowEdit = true;
            bindingList.AllowNew = true;
            pSource.DataSource = bindingList;
            pSource.AllowNew = true;
            GridView = dataGridView;
            dataGridView.DataSource = pSource;
            dataGridView.ColumnHeaderMouseClick += ColumnHeaderMouseClick;
            dataGridView.MouseClick += MouseClick;
            DataGridContextMenu = dataGridContextMenu;
            InitTable();
        }
        public virtual void LoadData(List<T> items)
        {
            bindingList = new BindingList<T>(items);
            GridView.DataSource = bindingList;
            SetValueContextFilter();
        }
        public void SetValueContextFilter()
        {
            foreach (var item in listContextMenu)
            {
                ContextFilterValue? filterValue = item as ContextFilterValue;
                if(filterValue != null)
                {
                    filterValue.Items.Clear();
                    filterValue.AddValue(" Выделить всё");
                    filterValue.AddValue(" Пустые");
                    foreach (DataGridViewRow row in GridView.Rows)
                    {
                        string? value = row.Cells[item.Index].Value.ToString();
                        if (value == null)
                            value = String.Empty;
                        filterValue.AddValue(value);
                    }
                    filterValue.Items.AddRange(filterValue.Items.OfType<ToolStripItem>().OrderBy(x => x.Text).ToArray());
                }
            }
        }
        public virtual void ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (DataGridContextMenu != null)
                DataGridContextMenu.Close();
            if (e.Button == MouseButtons.Right)
            {
                foreach (var contextMenu in listContextMenu)
                    if(contextMenu.Index ==e.ColumnIndex)
                        contextMenu.Show(Control.MousePosition, ToolStripDropDownDirection.Default);
            }
        }
        public void MouseClick(object? sender, MouseEventArgs e)
        {
            if (DataGridContextMenu != null)
                if (e.Button == MouseButtons.Right)
                    DataGridContextMenu.Show(Control.MousePosition);
            foreach (var c in listContextMenu)
                c.Close();
        }
        protected virtual void InitTable()
        {
            GridView.Columns[0].Visible = false;
            foreach (var item in listContextMenu)
            {
                item.ItemClicked += TournamentDataGrid_ItemClicked;
            }
        }
        public List<T> GetList()
        {
            return bindingList.ToList();
        }
        protected virtual void TournamentDataGrid_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            ContextFilterValue? contextMenuStrip = sender as ContextFilterValue;
            if (contextMenuStrip == null) { return; }
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
                else
                {
                    if (toolStrip.Checked)
                        toolStrip.Checked = false;
                    else toolStrip.Checked = true;
                }
            }
        }
    }
    public class TournamentDataGrid : TableContextMenu<Tournament>
    {
        public TournamentDataGrid(DataGridView dataGridView, ContextMenuStrip contextMenuStrip) : base(dataGridView, contextMenuStrip)
        {
        }
        protected override void InitTable()
        {
            listContextMenu.Add(new ContextFilterName(1));
            base.InitTable();
        }

        protected override void TournamentDataGrid_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            base.TournamentDataGrid_ItemClicked(sender, e);
            GridView.DataSource = bindingList.Where(data => listContextMenu[0].Filter(data.NameTournament)).ToList();
        }
        public Tournament? GetItem(int id)
        {
            return bindingList.FirstOrDefault(p => p.Id == id);
        }
    }
    public class ParticipantDataGrid : TableContextMenu<Participant>
    {
        public ParticipantDataGrid(DataGridView dataGridView, ContextMenuStrip? contextMenuStrip = null) : base(dataGridView, contextMenuStrip)
        {
        }
        protected override void TournamentDataGrid_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            base.TournamentDataGrid_ItemClicked(sender, e);
            GridView.DataSource = bindingList.Where(data =>
                listContextMenu[0].Filter(data.FIO) &&
                listContextMenu[1].Filter(data.Gender) &&
                listContextMenu[2].Filter(data.Qualification) &&
                listContextMenu[3].Filter(data.City) &&
                listContextMenu[4].Filter(data.Trainer)).ToList();
        }
        protected override void InitTable()
        {
            listContextMenu.Add(new ContextFilterName(1));
            listContextMenu.Add(new ContextFilterValue(2));
            listContextMenu.Add(new ContextFilterValue(6));
            listContextMenu.Add(new ContextFilterValue(7));
            listContextMenu.Add(new ContextFilterValue(8));
            base.InitTable();
        }
        public override void ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            base.ColumnHeaderMouseClick(sender, e);
            if (e.Button == MouseButtons.Left)
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        GridView.DataSource = bindingList.OrderBy(t => t.FIO).ToList();
                        break;
                    case 2:
                        GridView.DataSource = bindingList.OrderBy(t => t.Gender).ToList();
                        break;
                    case 3:
                        GridView.DataSource = bindingList.OrderBy(t => t.Age).ToList();
                        break;
                    case 4:
                        GridView.DataSource = bindingList.OrderBy(t => t.DateOfBirth).ToList();
                        break;
                    case 5:
                        GridView.DataSource = bindingList.OrderBy(t => t.Weight).ToList();
                        break;
                    case 6:
                        GridView.DataSource = bindingList.OrderBy(t => t.Qualification).ToList();
                        break;
                    case 7:
                        GridView.DataSource = bindingList.OrderBy(t => t.City).ToList();
                        break;
                    case 8:
                        GridView.DataSource = bindingList.OrderBy(t => t.Trainer).ToList();
                        break;
                }
            }
        }
        public Participant? GetItem(int id)
        {
            return bindingList.FirstOrDefault(p => p.Id == id);
        }
        public void AddParticipant(Participant participant)
        {
            bindingList.Add(participant);
        }
        public void DeleteParticipant(Participant participant)
        {
            bindingList.Remove(participant);
        }
    }
    public class TournamentGridDataGrid : TableContextMenu<TournamentGrid>
    {
        public TournamentGridDataGrid(DataGridView dataGridView, ContextMenuStrip contextMenuStrip) : base(dataGridView, contextMenuStrip) { }
        protected override void TournamentDataGrid_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            base.TournamentDataGrid_ItemClicked(sender, e);
            GridView.DataSource = GetFilterList();
        }
        protected override void InitTable()
        {
            listContextMenu.Add(new ContextFilterValue(1));
            listContextMenu.Add(new ContextFilterName(3));
            listContextMenu.Add(new ContextFilterValue(4));
            listContextMenu.Add(new ContextFilterValue(5));
            listContextMenu.Add(new ContextFilterValue(6));
            listContextMenu.Add(new ContextFilterValue(7));
            listContextMenu.Add(new ContextFilterValue(8));
            base.InitTable();
        }
        private List<TournamentGrid> GetFilterList()
        {
            return bindingList.Where(data =>
                listContextMenu[0].Filter(data.Number.ToString()) &&
                listContextMenu[1].Filter(data.NameGrid) &&
                listContextMenu[2].Filter(data.Gender) &&
                listContextMenu[3].Filter(data.Programm) &&
                listContextMenu[4].Filter(data.AgeRange) &&
                listContextMenu[5].Filter(data.Qualification) &&
                listContextMenu[6].Filter(data.Status)).ToList();
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

            for (int i = 0; i < text.Count; ++i)
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
            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        form.Invoke(() =>
                        {
                            this.AddRow(row);
                        });
                        progressBar.Value = i;
                    }
                });
            }
            catch
            {  }
            progressBar.Visible = false;
        }
        public async void FillTableOnAccess(DataTable table, Form form, ProgressBar progressBar)
        {
            progressBar.Visible = true;
            progressBar.Maximum = table.Rows.Count;
            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        var objects = row.ItemArray;
                        objects[3] = (int)((int)(DateTime.Now - DateTime.Parse(row["date_of_birth"].ToString())).TotalDays / ParticipantX.ValyeDayYear);
                        form.Invoke(() =>
                        {
                            AddRow(objects);
                        });
                        progressBar.Value = i;
                    }
                });
            }
            catch
            { }
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
                                ParticipantX participant = new ParticipantX();
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
