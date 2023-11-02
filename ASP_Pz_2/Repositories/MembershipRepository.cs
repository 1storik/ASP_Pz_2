using ASP_Pz_2.Entity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ASP_Pz_2.Repositories
{
    public class MembershipRepository : Repository<Membership>
    {
        public async Task<Membership> GetMembershipInformation(int id)
        {
            return await dbSet.Where(m => m.MembershipId == id)
                .Include(m => m.ClientMemberships)
                .ThenInclude(cm => cm.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
