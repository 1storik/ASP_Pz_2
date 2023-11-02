using System.Collections.Generic;

namespace ASP_Pz_2.Entity
{
    public class Membership
    {
        public int MembershipId { get; set; }

        public string MembershipType { get; set; }
        public string Coach { get; set; }
        public int Price { get; set; }

        public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
    }
}
