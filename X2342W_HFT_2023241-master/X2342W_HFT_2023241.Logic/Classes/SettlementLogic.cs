using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;
using X2342W_HFT_2023241.Repository;

namespace X2342W_HFT_2023241.Logic
{
    public class SettlementLogic : ISettlementLogic
    {
        IRepository<Settlement> repo;

        public SettlementLogic(IRepository<Settlement> repo)
        {
            this.repo = repo;
        }

        public void Create(Settlement item)
        {
            if (item.SettlementName != null && item.CountyName != null)
            {
                this.repo.Create(item);
            }
            else { throw new ArgumentNullException(); }
           
		}

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Settlement Read(int id)
        {
			var settlement = this.repo.Read(id);
			if (settlement == null)
			{
				throw new ArgumentException("Settlement does not exists");
			}
			return settlement;

		}

        public IQueryable<Settlement> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Settlement item)
        {
            this.repo.Update(item);
        }

        // non-crud
        public IEnumerable<Researcher> Researchers(string settlementName)
        {
            return this.repo.ReadAll().Where(r=>r.SettlementName==settlementName)
                .SelectMany(s=>s.Sites).SelectMany(s=>s.Researchers);
        }

        public IEnumerable<string> AgeOfArtifacts(string settlementName)
        {
            return this.repo.ReadAll().Where(s=>s.SettlementName==settlementName)
                .SelectMany(s=>s.Sites).Select(site=>site.AgeOfArtifact);
        }
		public int NumberOfExcavations(int id)
		{
			return this.repo.ReadAll()
				.Where(s => s.SettlementId == id)
				.SelectMany(s => s.Sites.SelectMany(site => site.Excavations))
				.Count();
		}
		public Excavation LatestExcavation(int id)
		{
			return this.repo.ReadAll()
				.Where(s => s.SettlementId == id)
				.SelectMany(s => s.Sites.SelectMany(site => site.Excavations))
				.OrderByDescending(ex => ex.StartOfExcavation)
				.FirstOrDefault();
		}

	}
}
