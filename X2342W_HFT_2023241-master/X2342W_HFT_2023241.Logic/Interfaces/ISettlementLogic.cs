using System.Collections.Generic;
using System.Linq;
using X2342W_HFT_2023241.Models;

namespace X2342W_HFT_2023241.Logic
{
    public interface ISettlementLogic
    {
        void Create(Settlement item);
        void Delete(int id);
        Settlement Read(int id);
        IQueryable<Settlement> ReadAll();
        void Update(Settlement item);
        IEnumerable<Researcher> Researchers(string settlementName);
        IEnumerable<string> AgeOfArtifacts(string settlementName);
        int NumberOfExcavations(int id);
        Excavation LatestExcavation(int id);
	}
}