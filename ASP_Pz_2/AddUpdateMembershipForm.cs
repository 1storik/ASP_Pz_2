using ASP_Pz_2.DTO;
using ASP_Pz_2.Repositories;
using System;
using System.Windows.Forms;

namespace ASP_Pz_2
{
    public partial class AddUpdateMembershipForm : Form
    {
        private MembershipRepository _membershipRepository;
        public AddUpdateMembershipForm()
        {
            InitializeComponent();
            _membershipRepository = new MembershipRepository();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var membersipAddDto = new MembershipAddDTO
            {
                MembershipType = textBox1.Text,
                Coach = textBox2.Text,
                Price = int.Parse(textBox3.Text)
            };

            await _membershipRepository.AddAsync(Mapper.MembershipAddDtoToMembership(membersipAddDto));
        }

        private void AddUpdateMembershipForm_Load(object sender, EventArgs e)
        {

        }
    }
}
