using Palmfit.Data.EntityEnums;

namespace Palmfit.Data.Entities
{
    public class Health : BaseEntity
    {
        public string AppUserId { get; set; }
        public double Height { get; set; }
        public HeightUnit HeightUnit { get; set; }
        public double Weight { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public GenoType GenoType { get; set; }
        public AppUser AppUser { get; set; }

        public string Balance { get; set; } // needs verification on this property
        public string Reference { get; set; } //needs verification on this property
        public ICollection<WalletHistory> Histories { get; set; }
    }
}