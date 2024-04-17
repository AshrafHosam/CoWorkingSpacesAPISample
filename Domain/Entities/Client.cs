using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ProfessionalTitle { get; set; }
        public string Interests { get; set; }
        public string Source { get; set; }

        [ForeignKey(nameof(Brand))]
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<SharedAreaVisit> Visits { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var otherClient = (Client)obj;

            return Email == otherClient.Email && MobileNumber == otherClient.MobileNumber;
        }

        public override int GetHashCode() => (Email + MobileNumber).GetHashCode();
    }
}
