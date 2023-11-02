using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Pz_2.Entity
{
    public class Client
    {
        public int ClientId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }


        public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
    }
}
