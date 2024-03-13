using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;
using X2342W_HFT_2023241.Repository;

namespace X2342W_HFT_2023241.Logic
{
    public class ResearcherLogic : IResearcherLogic
    {
        IRepository<Researcher> repo;
        public ResearcherLogic(IRepository<Researcher> repo)
        {
            this.repo = repo;
        }
        public void Create(Researcher item)
        {
                if (item.ResearcherName != null && item.Profession != null)
                {
                    this.repo.Create(item);
                }
                else { throw new ArgumentNullException(); }
        }
        public void Delete(int id)
        {
            this.repo.Delete(id);
        }
        public Researcher Read(int id)
        {
            var researcher=this.repo.Read(id);
            if (researcher == null) 
            {
                throw new ArgumentException("Researcher does not exists");
            }
            return researcher;
        }
        public IQueryable<Researcher> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Researcher item)
        {
            this.repo.Update(item);
        }

		

		//non-crud

		public IEnumerable<ExcavationSite> ExcavationSitesOf(int researcherId) 
        { 
            return this.repo.ReadAll().Where(r=>r.ResearcherId==researcherId)
                .SelectMany(r=>r.Sites);
        }

		public Researcher WithTheMostExcavation()
		{
			return this.repo.ReadAll()
				.Where(r => r.Excavations != null && r.Excavations.Any())
				.OrderByDescending(r => r.Excavations.Count())
				.FirstOrDefault();
		}


	}
}
