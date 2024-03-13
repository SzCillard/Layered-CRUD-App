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
    public class Researcher
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ResearcherId { get; set; }
        [Required]
        [StringLength(240)]
        public string ResearcherName {  get; set; }
        [StringLength(240)]
        public string Profession {  get; set; }
		[NotMapped]
		[JsonIgnore]
        public virtual ICollection<Excavation> Excavations { get; set; }
		[NotMapped]
		[JsonIgnore]
        public virtual ICollection<ExcavationSite> Sites { get; set; }
        public Researcher()
        {
                        
        }
        public Researcher(string line)
        {
            string[] split= line.Split('#');
            ResearcherId = int.Parse(split[0]);
            ResearcherName = split[1];
            Profession = split[2];
        }
		public override bool Equals(object obj)
		{
			Researcher b = obj as Researcher;
			if (b == null)
			{
				return false;
			}
			else
			{
				return this.ResearcherId == b.ResearcherId
					&& this.ResearcherName == b.ResearcherName
					&& this.Profession == b.Profession;
			}
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.ResearcherId, this.ResearcherName, this.Profession);
		}
	}
}
