using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Repository
{
    public class ExcavationSiteRepository : Repository<ExcavationSite>, IRepository<ExcavationSite>
    {
        public ExcavationSiteRepository(ArcheologyDbContext ctx) : base(ctx)
        {
        }

        public override ExcavationSite Read(int id)
        {
            return ctx.ExcavationSites.FirstOrDefault(t => t.SiteId == id);
        }

        public override void Update(ExcavationSite item)
        {
            var old = Read(item.SiteId);
            old.SettlementId = item.SettlementId;
            old.SiteType = item.SiteType;
            old.AgeOfArtifact = item.AgeOfArtifact;
            ctx.SaveChanges();
        }
    }
}
