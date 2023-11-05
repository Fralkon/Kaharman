using System.Data;

namespace Kaharman
{
    internal class ContextMenuFilter : ContextMenuStrip 
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
    internal class ContextMenuFilterName : ContextMenuFilter
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
    internal class ContextMenuFilterWeigth: ContextMenuFilter
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
    internal class DataTableContextMenu : DataTable
    {
        public DataView dataView { get; }
        public DataTableContextMenu() : base()
        {
            RowChanged += DataTableContextMenu_RowChanged; 
            dataView = new DataView(this);
        }

        List<ContextMenuStrip> listContextMenu = new List<ContextMenuStrip>();
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
            dataView.RowFilter = string.Join(" AND ", filters);
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
        public void AddRow(DataRow Row)
        {
            object[]? cells = Row.ItemArray;
            if (cells != null && cells.Length == listContextMenu.Count)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    string? item = cells[i].ToString();
                    if (item != null && item.Length != 0)
                        AddItemContextMenu(listContextMenu[i], item);
                }
            }
            this.Rows.Add(Row);
        }
        private void AddItemContextMenu(ContextMenuStrip menuStrip, string item)
        {
            foreach (ToolStripMenuItem toolStrip in menuStrip.Items)
                if (toolStrip.Text == item)
                    return;
            ((ToolStripMenuItem)menuStrip.Items.Add(item)).Checked = true;            
        }
    }
}
