using ASP_Pz_2.DTO;
using ASP_Pz_2.Entity;
using ASP_Pz_2.Repositories;
using System;
using System.Windows.Forms;

namespace ASP_Pz_2
{
    public partial class AddUpdateClientForm : Form
    {
        private Repository<Client> _clientRepository;
        public AddUpdateClientForm()
        {
            InitializeComponent();
            _clientRepository = new Repository<Client>();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var clientAddDTO = new ClientAddDTO
            {
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                Gender = textBox3.Text,
                DateOfBirth = dateTimePicker1.Value,
                Phone = textBox5.Text,
                Email = textBox6.Text
            };
            await _clientRepository.AddAsync(Mapper.ClientAddDtoToClient(clientAddDTO));
            this.Close();
        }
    }
}
