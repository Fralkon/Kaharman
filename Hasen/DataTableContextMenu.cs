using System.ComponentModel;
using System.Data;

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
        int MinAgeFilter = 0;
        int MaxAgeFiter = 100;
        string GenderFilter = "";
        Qualification? MinQualification;
        Qualification? MaxQualification;
        bool filterForm;
        public ParticipantDataGrid(DataGridView dataGridView, ContextMenuStrip? contextMenuStrip = null, bool FilterForm = false) : base(dataGridView, contextMenuStrip)
        {
            filterForm = FilterForm;
        }
        protected override void TournamentDataGrid_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            base.TournamentDataGrid_ItemClicked(sender, e);
            GridView.DataSource = GetFilterList();
        }
        private List<Participant> GetFilterList()
        {
            return GetFilterForm().Where(data =>
                listContextMenu[0].Filter(data.FIO) &&
                listContextMenu[1].Filter(data.Gender) &&
                listContextMenu[2].Filter(data.Qualification) &&
                listContextMenu[3].Filter(data.City) &&
                listContextMenu[4].Filter(data.Trainer)).ToList();
        }
        private List<Participant> GetFilterForm()
        {
            if (filterForm == false)
                return bindingList.ToList();
            return bindingList.Where(FilterParticipant).ToList();
        }
        private bool FilterParticipant(Participant participant)
        {
            if (MinAgeFilter != 0 || MaxAgeFiter != 100)
            {
                if (MinAgeFilter > participant.Age || MaxAgeFiter < participant.Age)
                    return false;
            }
            if (GenderFilter != "")
            {
                if (participant.Gender != GenderFilter)
                    return false;
            }
            if (MinQualification != null)
            {
                Qualification qualification = new Qualification(participant.Qualification);
                if (qualification.CompareTo(MinQualification) < 0)
                    return false;
            }
            if (MaxQualification != null)
            {
                Qualification qualification = new Qualification(participant.Qualification);
                if (qualification.CompareTo(MaxQualification) > 0)
                    return false;
            }
            return true;
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
                        GridView.DataSource = GetFilterList().OrderBy(t => t.FIO).ToList();
                        break;
                    case 2:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.Gender).ToList();
                        break;
                    case 3:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.Age).ToList();
                        break;
                    case 4:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.DateOfBirth).ToList();
                        break;
                    case 5:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.Weight).ToList();
                        break;
                    case 6:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.Qualification).ToList();
                        break;
                    case 7:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.City).ToList();
                        break;
                    case 8:
                        GridView.DataSource = GetFilterList().OrderBy(t => t.Trainer).ToList();
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
        public void SetAgeMinFilter(int age)
        {
            MinAgeFilter = age;
            TournamentDataGrid_ItemClicked(null, null);
        }
        public void SetAgeMaxFilter(int age)
        {
            MaxAgeFiter = age;
            TournamentDataGrid_ItemClicked(null, null);
        }
        public void SetGenderFilter(string gender)
        {
            GenderFilter = gender;
            TournamentDataGrid_ItemClicked(null, null);
        }
        public void SetMinQualification(string  minQualification)
        {
            if (minQualification != "")
                MinQualification = new Qualification(minQualification);
            else
                MinQualification = null;
            TournamentDataGrid_ItemClicked(null, null);
        }
        public void SetMaxQualification(string maxQualification)
        {
            if (maxQualification != "")
                MaxQualification = new Qualification(maxQualification);
            else MaxQualification = null;
            TournamentDataGrid_ItemClicked(null, null);
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
               //listContextMenu[1].Filter(data.NameGrid) &&
                listContextMenu[2].Filter(data.Gender) &&
                listContextMenu[3].Filter(data.Programm) &&
                listContextMenu[4].Filter(data.AgeRange) &&
                listContextMenu[5].Filter(data.Qualification) &&
                listContextMenu[6].Filter(data.Status)).ToList();
        }
    }
}