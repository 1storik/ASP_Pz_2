using ASP_Pz_2.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace ASP_Pz_2.Repositories
{
    public class ClientRepository : Repository<Client>
    {
        public async Task<Client> GetClientInformation(int id)
        {
            return await dbSet.Where(c => c.ClientId == id)
                .Include(c => c.ClientMemberships)
                .ThenInclude(cm => cm.Membership)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
