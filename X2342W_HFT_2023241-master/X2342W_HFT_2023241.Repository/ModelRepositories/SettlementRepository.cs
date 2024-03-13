using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Repository
{
    public class SettlementRepository : Repository<Settlement>, IRepository<Settlement>
    {
        public SettlementRepository(ArcheologyDbContext ctx) : base(ctx)
        {
        }

        public override Settlement Read(int id)
        {
            return ctx.Settlements.FirstOrDefault(t => t.SettlementId == id);
        }

        public override void Update(Settlement item)
        {
            var old = Read(item.SettlementId);
           
            old.SettlementName = item.SettlementName;
            old.CountyName = item.CountyName;
            ctx.SaveChanges();
        }
    }
}
