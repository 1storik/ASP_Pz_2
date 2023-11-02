using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Pz_2.DTO
{
    public class MembershipStartEndDTO
    {
        public string MembershipType { get; set; }
        public string Coach { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
