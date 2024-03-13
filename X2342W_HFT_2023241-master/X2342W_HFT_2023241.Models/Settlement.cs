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
    public class Settlement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettlementId {  get; set; }
        [StringLength(240)]
        public string SettlementName {  get; set; }
        [StringLength(240)]
        public string CountyName {  get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<ExcavationSite> Sites { get; set; }
        public Settlement()
        {
            Sites = new HashSet<ExcavationSite>();
        }
        public Settlement(string line)
        {
            string[] split=line.Split('#');
            SettlementId = int.Parse(split[0]);
            SettlementName = split[1];
            CountyName = split[2];
            Sites = new HashSet<ExcavationSite>();
        }
		public override bool Equals(object obj)
		{
			Settlement b = obj as Settlement;
			if (b == null)
			{
				return false;
			}
			else
			{
                return this.CountyName == b.CountyName
                    && this.SettlementId == b.SettlementId
                    && this.SettlementName == b.SettlementName;
				
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.CountyName, this.SettlementId, this.SettlementName);
		}
	}
}
