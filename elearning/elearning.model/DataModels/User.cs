
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace elearning.model.DataModels
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Identity { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(150)]
        public string Password { get; set; }        

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        [StringLength(255)]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        public string AddressLine2 { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        [StringLength(100)]
        public string StateProvince { get; set; }

        [StringLength(100)]
        public string CountryId { get; set; }

        public bool ReceiveNewsletter { get; set; } 
        
        public DateTime DateRegistered { get; set; } 

        public DateTime? SubscriptionStart { get; set; }

        public DateTime? SubscriptionEnd { get; set; }

        public DateTime? LastModified { get; set; }

        public int? LastModifiedBy { get; set; }

        public bool Active { get; set; } 

        public string ActivationLink { get; set; }

        public DateTime? DateActivated { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Roles { get; set; }
    }
}
