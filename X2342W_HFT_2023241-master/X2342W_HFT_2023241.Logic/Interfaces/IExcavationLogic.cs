using System.Collections.Generic;
using System;
using System.Linq;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Logic
{
    public interface IExcavationLogic
    {
        void Create(Excavation item);
        void Delete(int id);
        Excavation Read(int id);
        IQueryable<Excavation> ReadAll();
        void Update(Excavation item);
        IEnumerable<Excavation> GetExcavationsInPeriod(DateTime start, DateTime end);

	}
}