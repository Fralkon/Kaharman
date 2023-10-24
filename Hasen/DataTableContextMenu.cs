using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kaharman
{
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
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
            {
                AutoClose = false,
                Name = name,
                ShowCheckMargin = true
            };
            ((ToolStripMenuItem)contextMenuStrip.Items.Add("Пустые")).Checked = true;
            contextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
            listContextMenu.Add(contextMenuStrip);
        }

        private void ContextMenuStrip_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
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
            foreach(ContextMenuStrip contextMenuStrip in listContextMenu)
            {
                string filter = $"[{contextMenuStrip.Name}] NOT IN (";
                bool bf = false;
                foreach (ToolStripMenuItem item in contextMenuStrip.Items)
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
                if(bf)
                    filters.Add(filter);
            }
            dataView.RowFilter = string.Join(" AND ", filters);
        }
        public void AddColunm(string name, Type type)
        {
            Columns.Add(name, type);
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip()
            {
                AutoClose = false,
                Name = name,
                ShowCheckMargin = true
            };
            contextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
            listContextMenu.Add(contextMenuStrip);
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
                        string? item = cells[i].ToString();
                        if (item != null && item.Length != 0)
                            AddItemContextMenu(listContextMenu[i], item);
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
