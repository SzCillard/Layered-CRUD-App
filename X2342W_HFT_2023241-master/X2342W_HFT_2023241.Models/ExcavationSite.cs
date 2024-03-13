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
    public class ExcavationSite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SiteId {  get; set; }
        [Required]
        public int SettlementId {  get; set; }
        [StringLength(240)]
        public string SiteType {  get; set; }
        [StringLength(240)]
        public string AgeOfArtifact {  get; set; }
        [NotMapped]
        public virtual Settlement Settlement { get; set; }
		[NotMapped]
		public virtual ICollection<Excavation> Excavations { get; set; }
		[NotMapped]
		[JsonIgnore]
		public virtual ICollection<Researcher> Researchers { get; set; }
        public ExcavationSite()
        {
            
        }
        public ExcavationSite(string line)
        {
            string[] split= line.Split('#');
            SiteId = int.Parse(split[0]);
            SettlementId = int.Parse(split[1]);
            SiteType = split[2];
            AgeOfArtifact = split[3];
        }
		public override bool Equals(object obj)
		{
			ExcavationSite b = obj as ExcavationSite;
			if (b == null)
			{
				return false;
			}
			else
			{
				return this.SiteId == b.SiteId
					&& this.SettlementId == b.SettlementId
                    && this.SiteType == b.SiteType
					&& this.AgeOfArtifact == b.AgeOfArtifact;
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.SiteId, this.SettlementId, this.SiteType, this.AgeOfArtifact);
		}
	}
}
