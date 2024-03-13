using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Repository
{
    public class ExcavationRepository : Repository<Excavation>, IRepository<Excavation>
    {
        public ExcavationRepository(ArcheologyDbContext ctx) : base(ctx)
        {
        }

        public override Excavation Read(int id)
        {
            return ctx.Excavations.FirstOrDefault(t => t.ExcavationId == id);
        }

        public override void Update(Excavation item)
        {
            var old = Read(item.ExcavationId);
          
            old.ResearcherId = item.ResearcherId;
            old.SiteId = item.SiteId;
            old.StartOfExcavation = item.StartOfExcavation;
            old.EndOfExcavation = item.EndOfExcavation;
            ctx.SaveChanges();
        }
    }
}
