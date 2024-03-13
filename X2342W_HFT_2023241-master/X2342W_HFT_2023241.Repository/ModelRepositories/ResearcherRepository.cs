using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Repository
{
    public class ResearcherRepository : Repository<Researcher>, IRepository<Researcher>
    {
        public ResearcherRepository(ArcheologyDbContext ctx) : base(ctx)
        {
        }

        public override Researcher Read(int id)
        {
            return ctx.Researchers.FirstOrDefault(t => t.ResearcherId == id);
        }

        public override void Update(Researcher item)
        {
            var old = Read(item.ResearcherId);
            old.ResearcherName = item.ResearcherName;
            old.Profession= item.Profession;
            ctx.SaveChanges();
        }
    }
}
