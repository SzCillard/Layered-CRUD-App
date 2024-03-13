using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2342W_HFT_2023241.Models;
using X2342W_HFT_2023241.Repository;

namespace X2342W_HFT_2023241.Logic
{
    public class ExcavationSiteLogic : IExcavationSiteLogic
    {
        IRepository<ExcavationSite> repo;

        public ExcavationSiteLogic(IRepository<ExcavationSite> repo)
        {
            this.repo = repo;
        }

        public void Create(ExcavationSite item)
        {
            if (item.SettlementId>0)
            {
					if (item.SiteType != null && item.AgeOfArtifact != null)
					{
						this.repo.Create(item);
					}
					else
					{
						throw new ArgumentNullException();
					}
			}
            else
            {
                throw new ArgumentException("Settlement Id must be positive");
            }
		}

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public ExcavationSite Read(int id)
        {
			var excavationSite = this.repo.Read(id);
			if (excavationSite == null)
			{
				throw new ArgumentException("Excavation site does not exists");
			}
			return excavationSite;

		}

        public IQueryable<ExcavationSite> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(ExcavationSite item)
        {
            this.repo.Update(item);
        }

        //non-crud

        public IEnumerable<DateTime> GetExcavationStartDates(int excavationSiteId)
        {
            if (excavationSiteId>0)
            {
                return this.repo.ReadAll().Where(s => s.SiteId == excavationSiteId)
                    .SelectMany(r => r.Excavations.Select(e => e.StartOfExcavation)).ToList();
			}
            else
            {
                throw new ArgumentException("Input parameter ID must be positive");
            }
           
        }

    }
}
