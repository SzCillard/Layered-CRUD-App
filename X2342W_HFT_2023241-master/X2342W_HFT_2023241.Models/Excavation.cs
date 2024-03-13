using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace X2342W_HFT_2023241.Models
{
    public class Excavation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExcavationId {  get; set; }
        [Required]
        public int SiteId {  get; set; }
        [Required]
        public int ResearcherId {  get; set; }
        public DateTime StartOfExcavation { get; set; }
        public DateTime EndOfExcavation { get; set; }
		[NotMapped]
		[JsonIgnore]
		public virtual ExcavationSite ExcavationSite {  get; set; }
		[NotMapped]
		public virtual Researcher Researcher { get; set; }
        public Excavation()
        {
            
        }
        public Excavation(string line)
        {
            string[] split=line.Split('#');
            ExcavationId = int.Parse(split[0]);
            SiteId = int.Parse(split[1]);
            ResearcherId = int.Parse(split[2]);
            StartOfExcavation = DateTime.Parse(split[3]);
			EndOfExcavation = DateTime.Parse(split[4]);

		}
		public override bool Equals(object obj)
		{
			Excavation b = obj as Excavation;
			if (b == null)
			{
				return false;
			}
			else
			{
				return this.ExcavationId == b.ExcavationId
					&& this.StartOfExcavation == b.StartOfExcavation
					&& this.EndOfExcavation == b.EndOfExcavation
					&& this.SiteId == b.SiteId
					&& this.ResearcherId == b.ResearcherId;

			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.SiteId, this.ResearcherId, this.ExcavationId, this.StartOfExcavation, this.EndOfExcavation);
		}
	}
}
