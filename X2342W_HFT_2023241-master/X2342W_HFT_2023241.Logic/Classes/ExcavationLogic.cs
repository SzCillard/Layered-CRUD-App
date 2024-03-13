using System;
using System.Linq;
using X2342W_HFT_2023241.Repository;
using X2342W_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;

namespace X2342W_HFT_2023241.Logic
{
    public class ExcavationLogic : IExcavationLogic
    {
        IRepository<Excavation> repo;

        public ExcavationLogic(IRepository<Excavation> repo)
        {
            this.repo = repo;
        }

        public void Create(Excavation item)
        {

			if (item.SiteId>0 && item.ResearcherId>0)
			{
					this.repo.Create(item);
			}
			else
			{
				throw new ArgumentException("Id must be positive");
			}

		}

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Excavation Read(int id)
        {
			var excavation = this.repo.Read(id);
			if (excavation == null)
			{
				throw new ArgumentException("Excavation does not exists");
			}
			return excavation;
		}

        public IQueryable<Excavation> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Excavation item)
        {
            this.repo.Update(item);
        }
        //non-crud
        public IEnumerable<Excavation> GetExcavationsInPeriod(DateTime start, DateTime end)
        {
            return this.repo.ReadAll().Where(e => e.StartOfExcavation >= start && e.EndOfExcavation <= end).ToList();
        }
        
    }
}
