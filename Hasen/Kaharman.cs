using Kaharman;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hasen
{
    enum TableVisible
    {
        Participants,
        DataParticipants,
        HistoryTournaments
    }
    public partial class Kaharman : Form
    {
        TableVisible tableVisible;
        AccessSQL AccessSQL { get; set; }
        DataTableContextMenu ParticipantsTable = new DataTableContextMenu();
        DataTableContextMenu DataParticipantsTable = new DataTableContextMenu();
        DataTableContextMenu DataHistoryTournaments = new DataTableContextMenu();
        public Kaharman()
        {
            InitializeComponent();
            AccessSQL = new AccessSQL();
            InitializeDataParticipant(ParticipantsTable);
            ParticipantsTable.RowChanged += ParticipantsTable_RowChanged;
            InitializeDataParticipant(DataParticipantsTable);
            InitializeDataTournament(DataHistoryTournaments);
            tableVisible = TableVisible.Participants;
            UpDateTable();

        }
        private void UpDateTable()
        {
            ������������������ToolStripMenuItem.Checked = false;
            ����������ToolStripMenuItem.Checked = false;
            ���������������ToolStripMenuItem.Checked = false;
            switch (tableVisible)
            {
                case TableVisible.DataParticipants:
                    {
                        ����������ToolStripMenuItem.Checked = true;
                        dataGridView1.ContextMenuStrip = null;
                        AccessSQL.FillDataTableSQL("SELECT * FROM Participants", DataParticipantsTable);
                        dataGridView1.DataSource = DataParticipantsTable.dataView;
                        //dataGridView1.Columns[0].Visible = false;
                        return;
                    }
                case TableVisible.HistoryTournaments:
                    {
                        ���������������ToolStripMenuItem.Checked = true;
                        DataHistoryTournaments.Rows.Clear();
                        dataGridView1.ContextMenuStrip = contextMenuStrip1;
                        using (DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM Tournament"))
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                DataRow newRow = DataHistoryTournaments.NewRow();
                                for (int i = 0; i < 5; i++)
                                {
                                    newRow[i] = row[i];
                                }
                                DataHistoryTournaments.Rows.Add(newRow);
                            }
                        }
                        dataGridView1.DataSource = DataHistoryTournaments.dataView;
                        //dataGridView1.Columns[0].Visible = false;
                        return;
                    }
                case TableVisible.Participants:
                    {
                        dataGridView1.ContextMenuStrip = null;
                        ������������������ToolStripMenuItem.Checked = true;
                        dataGridView1.DataSource = ParticipantsTable.dataView;
                        //dataGridView1.Columns[0].Visible = false;
                        return;
                    }
            }
        }
        private void InitializeDataParticipant(DataTableContextMenu dataTable)
        {
            dataTable.AddColunm("ID", typeof(int));

            ContextMenuFilterName contextMenuName = new ContextMenuFilterName();
            dataTable.AddColunm("������� � ���", contextMenuName);

            dataTable.AddColunm("���");

            dataTable.AddColunm("���� ��������", typeof(DateTime));
            dataTable.AddColunm("�������", typeof(int));

            DataTable dataWeigth = AccessSQL.GetDataTableSQL($"SELECT * FROM Catigory");
            List<string> weigth = new List<string>();
            foreach (DataRow row in dataWeigth.Rows)
                weigth.Add(row["cat"].ToString());
            dataTable.AddColunm("���", typeof(float), new ContextMenuFilterWeigth(weigth));

            ContextMenuFilter contextMenuQualiti = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuQualiti.Items.Add("������")).Checked = true;
            dataTable.AddColunm("������������", contextMenuQualiti);

            ContextMenuFilter contextMenuCity = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuCity.Items.Add("������")).Checked = true;
            dataTable.AddColunm("�����", contextMenuCity);

            ContextMenuFilter contextMenuTrainer = new ContextMenuFilter();
            ((ToolStripMenuItem)contextMenuTrainer.Items.Add("������")).Checked = true;
            dataTable.AddColunm("������", contextMenuTrainer);
        }
        private void InitializeDataTournament(DataTableContextMenu dataTable)
        {
            dataTable.AddColunm("ID", typeof(int));

            ContextMenuFilterName contextMenuName = new ContextMenuFilterName();
            dataTable.AddColunm("������������", contextMenuName);

            dataTable.AddColunm("���� ������", typeof(DateTime));

            dataTable.AddColunm("���� ����������", typeof(DateTime));

            dataTable.AddColunm("����������");
        }
        private void ParticipantsTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
            {
                DataRow row = e.Row;
                DataTable data = AccessSQL.GetDataTableSQL($"SELECT id FROM Participants WHERE name = '{row[1]}'");
                if (data.Rows.Count == 0)
                {
                    AccessSQL.SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],age,weight,qualification,city,trainer) VALUES ('{row[1]}','{row[2]}','{((DateTime)row["���� ��������"]).ToString("dd.MM.yyyy")}',{row[4]},{row[5]},'{row[6]}','{row[7]}','{row[8]}')");
                    row["ID"] = AccessSQL.GetIDInsert();
                }
                else
                    row["ID"] = data.Rows[0]["id"];
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + "/Database.accdb"))
            {
                MessageBox.Show($"���� ���� ������ �� ������\n{Environment.CurrentDirectory}Database.accdb");
                Close();
            }
        }
        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm(AccessSQL);
            participants.ShowDialog();
        }
        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cites cites = new Cites(AccessSQL);
            cites.ShowDialog();
        }
        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm(AccessSQL);
            category.ShowDialog();
        }
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("���������� �����");
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
                    MessageBox.Show(string.Format("������ ���������� �������. Error:{0}", ex.Message));
                    return null;
                }
            }
        }
        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ��������ToolStripMenuItem1_Click(object sender, EventArgs e)
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
                                newRow[1] = rowExcel[i][1].ToString();
                                newRow[2] = rowExcel[i][2].ToString();
                                if (DateTime.TryParse(rowExcel[i][3].ToString(), out DateTime time))
                                    newRow[3] = time;
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
                MessageBox.Show("�������� ���������");
            }
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ParticipantsTable.ShowContextMenu(e.ColumnIndex, MousePosition);
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show();
        }
        private void ����������������ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm(AccessSQL);
            category.ShowDialog();
        }
        private void ������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ParticipantsTable.dataView;
        }
        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTournament createTournament = new CreateTournament(Participant.ToList(dataGridView1), AccessSQL);
            createTournament.ShowDialog();
        }
        private void ������������������ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.Participants;
            UpDateTable();
        }
        private void ���������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableVisible = TableVisible.HistoryTournaments;
            UpDateTable();
        }
        private void ����������ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.DataParticipants;
            UpDateTable();
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("�������� ������");
                return;
            }
            if (tableVisible == TableVisible.HistoryTournaments)
            {
                CreateTournament createTournament = new CreateTournament(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString(), AccessSQL);
                createTournament.ShowDialog();
                return;
            }
            else if (tableVisible == TableVisible.Participants || tableVisible == TableVisible.DataParticipants)
            {
                ParticipantForm participants = new ParticipantForm(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString(), AccessSQL);
                participants.ShowDialog();
                return;
            }
        }
        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("�������� ������");
                return;
            }
            DialogResult dr = MessageBox.Show($"������� ������ {dataGridView1.SelectedRows[0].Cells["������������"].Value}?", "�������� �������", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                AccessSQL.SendSQL("DELETE * FROM Tournament WHERE id = " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                UpDateTable();
            }
        }
    }
}