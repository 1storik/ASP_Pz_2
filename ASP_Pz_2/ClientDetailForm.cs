using ASP_Pz_2.Entity;
using ASP_Pz_2.Repositories;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ASP_Pz_2
{
    public partial class ClientDetailForm : Form
    {
        private int _id;
        private ClientRepository _clientRepository;
        private MembershipRepository _memberRepository;
        private Repository<ClientMembership> _clientMembershipRepository;
        public ClientDetailForm(int Id)
        {
            InitializeComponent();
            _id = Id;
            _clientRepository = new ClientRepository();
            _memberRepository = new MembershipRepository();
            _clientMembershipRepository = new Repository<ClientMembership>();
            Load += new EventHandler(MembershipDetailsForm_Load);
        }

        private async void MembershipDetailsForm_Load(object sender, EventArgs e)
        {
            var clientInfoDTO = Mapper.ClientToClientInformationDTO(await _clientRepository.GetClientInformation(_id));

            textBox1.Text = clientInfoDTO.FirstName;
            textBox2.Text = clientInfoDTO.LastName;
            textBox3.Text = clientInfoDTO.Gender;
            dateTimePicker1.Value = clientInfoDTO.DateOfBirth;
            textBox5.Text = clientInfoDTO.Phone;
            textBox6.Text = clientInfoDTO.Email;
            dateTimePicker2.Value = clientInfoDTO.RegistrationDate;

            dataGridView1.DataSource = clientInfoDTO.membershipStartEndDTOs;

            Combobox();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _clientRepository.UpdateAsync(new Client
            {
                ClientId = _id,
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                Gender = textBox3.Text,
                DateOfBirth = dateTimePicker1.Value,
                Phone = textBox5.Text,
                Email = textBox6.Text,
                RegistrationDate = dateTimePicker2.Value
            });
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _clientRepository.RemoveAsync(await _clientRepository.GetByIdAsync(_id));
            this.Close();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var clientMembersip = new ClientMembership
            {
                ClientId = _id,
                MembershipId = (int)comboBox1.SelectedValue,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30)
            };

            await _clientMembershipRepository.AddAsync(clientMembersip);
        }

        private async void Combobox()
        {
            var allMembership = await _memberRepository.GetAllAsync();
            var allSubcription = await _clientRepository.GetAsync(c => c.ClientId == _id, "ClientMemberships");

            comboBox1.DataSource = Mapper.MembershipToMembershipDTO(allMembership
                .Where(m => !allSubcription.ClientMemberships
                .Select(c => c.MembershipId)
                .Contains(m.MembershipId))
                .ToList());


            comboBox1.DisplayMember = "MembershipType";
            comboBox1.ValueMember = "MembershipId";
        }
    }
}
