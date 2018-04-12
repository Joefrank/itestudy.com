
using elearning.model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace elearning.model.ViewModels
{
    public class RegisterVm
    {        
        public UserType Type { get; set; }
       
        [Required(ErrorMessage ="You need to enter an Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You need to enter a Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You need to enter a Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You need to enter a Firstname")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "You need to enter your Lastname")]
        public string Lastname { get; set; }
            
        public bool AcceptedTerms { get; set; }

        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
    
        public string City { get; set; }
    
        public string PostCode { get; set; }
    
        public string StateProvince { get; set; }
    
        public string CountryId { get; set; }

        public bool ReceiveNewsletter { get; set; }

        public DateTime? SubscriptionStart { get; set; }

        public DateTime? SubscriptionEnd { get; set; }
    
        public string PhoneNumber { get; set; }

        public string ActivationLink { get; set; }

        public bool IsDirty { get; set; }
    }
}
