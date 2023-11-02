using ASP_Pz_2.DTO;
using ASP_Pz_2.Entity;
using ASP_Pz_2.Repositories;
using SQLitePCL;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ASP_Pz_2
{
    public partial class ClientForm : Form
    {
        private Repository<Client> _clientRepository;
        public ClientForm()
        {
            InitializeComponent();
            _clientRepository = new Repository<Client>();
            this.Load += new System.EventHandler(ClientForm_Load);
        }

        public async void ClientForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Mapper.ClientToClientDTO(await _clientRepository.GetAllAsync());

            DataGridViewButtonColumn detailColumn = new DataGridViewButtonColumn();
            detailColumn.HeaderText = "";
            detailColumn.Name = "Detail Request";
            detailColumn.Text = "Detail";
            detailColumn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(detailColumn);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex !=
            dataGridView1.Columns["Detail Request"].Index
            ) return;
            int id = (int)dataGridView1["ClientId", e.RowIndex].Value;
            if (e.ColumnIndex ==
            dataGridView1.Columns["Detail Request"].Index)
            {
                ClientDetailForm clientDetailForm = new ClientDetailForm(id);
                clientDetailForm.StartPosition = FormStartPosition.CenterScreen;
                clientDetailForm.Show();
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUpdateClientForm addUpdateClientForm = new AddUpdateClientForm();
            addUpdateClientForm.StartPosition = FormStartPosition.CenterScreen;
            addUpdateClientForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            var searchResult = Mapper.ClientToClientDTO(await _clientRepository.GetAllAsync(c => c.FirstName.Contains(textBox1.Text) || c.LastName.Contains(textBox1.Text)));

            dataGridView1.DataSource = searchResult;
        }
    }
}
