using ASP_Pz_2.Entity;
using ASP_Pz_2.Repositories;
using System;
using System.Windows.Forms;

namespace ASP_Pz_2
{
    public partial class MembershipsForm : Form
    {
        private Repository<Membership> _membershipRepository;
        public MembershipsForm()
        {
            _membershipRepository = new Repository<Membership>();
            Load += new EventHandler(MembershipsForm_Load);
            InitializeComponent();
        }

        private async void MembershipsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Mapper.MembershipToMembershipDTO(await _membershipRepository.GetAllAsync());

            DataGridViewButtonColumn detailColumn = new DataGridViewButtonColumn();
            detailColumn.HeaderText = "";
            detailColumn.Name = "Detail Request";
            detailColumn.Text = "Detail";
            detailColumn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(detailColumn);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);

            ComboBox();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridView1.Columns["Detail Request"].Index) 
                return;

            int id = (int)dataGridView1["MembershipId", e.RowIndex].Value;

            if (e.ColumnIndex ==dataGridView1.Columns["Detail Request"].Index)
            {
                MembershipDetailsForm showClient = new MembershipDetailsForm(id);
                showClient.StartPosition = FormStartPosition.CenterScreen;
                showClient.Show();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUpdateMembershipForm addUpdateMembershipForm = new AddUpdateMembershipForm();
            addUpdateMembershipForm.StartPosition = FormStartPosition.CenterScreen;
            addUpdateMembershipForm.Show();
        }

        private async void ComboBox()
        {
            comboBox1.DataSource =  await _membershipRepository.GetAllAsync();

            comboBox1.DisplayMember = "MembershipType";
            comboBox1.ValueMember = "MembershipId";
        }

        private async void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                int selectedValue = (int)comboBox1.SelectedValue;

                dataGridView1.DataSource = Mapper.MembershipToMembershipDTO(await _membershipRepository.GetAllAsync(c => c.MembershipId == selectedValue));
            }
        }
    }
}
