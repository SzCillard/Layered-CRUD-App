using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Logic
{
    public interface IResearcherLogic
    {
        void Create(Researcher item);
        void Delete(int id);
        Researcher Read(int id);
        IQueryable<Researcher> ReadAll();
        void Update(Researcher item);
        Researcher WithTheMostExcavation();
        IEnumerable<ExcavationSite> ExcavationSitesOf(int id);
    }
}