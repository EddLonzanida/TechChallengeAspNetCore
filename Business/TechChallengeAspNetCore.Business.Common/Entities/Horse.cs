using System.ComponentModel.DataAnnotations;
using Eml.EntityBaseClasses;

namespace TechChallengeAspNetCore.Business.Common.Entities
{
    public class Horse : EntityBaseSoftDeleteInt
    {
        [Required]
        [Display(Name = "Race")]
        public int RaceId { get; set; }

        public string Name { get; set; }

        public double Odds { get; set; }

        public virtual Race Race { get; set; }
    }
}
