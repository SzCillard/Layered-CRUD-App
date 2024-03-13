using System.Collections.Generic;
using System;
using System.Linq;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Logic
{
    public interface IExcavationSiteLogic
    {
        void Create(ExcavationSite item);
        void Delete(int id);
        ExcavationSite Read(int id);
        IQueryable<ExcavationSite> ReadAll();
        void Update(ExcavationSite item);
        IEnumerable<DateTime> GetExcavationStartDates(int excavationSiteId);

	}
}