using System.Collections.Generic;

namespace ASP_Pz_2.DTO
{
    public class MembershipInformationDTO
    {
        public string MembershipType { get; set; }
        public string Coach { get; set; }
        public int Price { get; set; }
        public List<ClientDTO> clientDTOs { get; set; }
    }
}
