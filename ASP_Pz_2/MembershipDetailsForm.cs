using ASP_Pz_2.Repositories;
using System;
using System.Windows.Forms;
using ASP_Pz_2.Entity;
using System.Collections.Generic;
using ASP_Pz_2.DTO;
using System.Linq;

namespace ASP_Pz_2
{
    public partial class MembershipDetailsForm : Form
    {
        private int _id;
        private MembershipRepository _membershipRepository;
        private Repository<ClientMembership> _clientMembershipRepository;

        public MembershipDetailsForm(int id)
        {
            InitializeComponent();
            _id = id;
            _membershipRepository = new MembershipRepository();
            _clientMembershipRepository = new Repository<ClientMembership>();
        }

        private async void MembershipDetailsForm_Load(object sender, EventArgs e)
        {
            var x = await _membershipRepository.GetMembershipInformation(_id);
            var memberInformationDto = Mapper.MembershipToMembershipInformationDTO(x);

            textBox1.Text = memberInformationDto.MembershipType;
            textBox2.Text = memberInformationDto.Coach;
            textBox3.Text = memberInformationDto.Price.ToString();

            dataGridView1.DataSource = memberInformationDto.clientDTOs;

            comboBox2.Items.AddRange(new string[] { "Active", "Disactive" });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _membershipRepository.UpdateAsync(new Membership
            {
                MembershipId = _id,
                MembershipType = textBox1.Text,
                Coach = textBox2.Text,
                Price = int.Parse(textBox3.Text)
            });
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _membershipRepository.RemoveAsync(await _membershipRepository.GetByIdAsync(_id));
        }

        private async void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string selectedState = comboBox2.SelectedItem.ToString();

            List<ClientDTO> selectedResult;

            if (selectedState == "Active")
                selectedResult = Mapper.ClientToClientDTO((await _clientMembershipRepository.GetAllAsync(cm => cm.EndDate >= DateTime.Today && cm.MembershipId == _id, includeProperties: "Client")).Select(x => x.Client).ToList());
            else
                selectedResult = Mapper.ClientToClientDTO((await _clientMembershipRepository.GetAllAsync(cm => cm.EndDate < DateTime.Today && cm.MembershipId == _id, includeProperties: "Client")).Select(x => x.Client).ToList());


            dataGridView1.DataSource = selectedResult;
        }
    }
}
