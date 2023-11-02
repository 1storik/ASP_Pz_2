using System;

namespace ASP_Pz_2.Entity
{
    public class ClientMembership
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
