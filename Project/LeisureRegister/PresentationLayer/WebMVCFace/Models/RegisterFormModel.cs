using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVCFace.Models
{
    public class RegisterFormModel
    {
        
        public string action { get; set; }
        [Required] public string name { get; set; }
        [Required] public string surname { get; set; }
        public string schoolname { get; set; }
        [Required]
        public int registrationnumber { get; set; }
        public string street { get; set; }
        public int orientationnumber { get; set; }
        public int descriptionnumber { get; set; }
        public string cityname { get; set; }
        public string citypartname { get; set; }
        public string healthstate { get; set; }
        public string comments { get; set; }
        public int postalcode { get; set; }
        public string mothername { get; set; }
        public string mothersurname { get; set; }
        public string motherjob { get; set; }
        [EmailAddress(ErrorMessage = "Email matky nemá správný formát")] public string motheremail { get; set; }
        public int motherphone { get; set; }
        public string fathername { get; set; }
        public string fathersurname { get; set; }
        public string fatherjob { get; set; }
        [EmailAddress(ErrorMessage = "Email otce nemá správný formát")] public string fatheremail { get; set; }
        public int fatherphone { get; set; }
        [Required] public DateTime birthdate { get; set; }
        [EmailAddress(ErrorMessage ="Email dítěte nemá správný formát")]public string email { get; set; }
        public int phone { get; set; }
        public bool photoagree { get; set; }
        public int insurancecompany { get; set; }
        public bool gpdragree { get; set; }
        public bool newsletteragree { get; set; }
        public bool leavingalone { get; set; }



    }
}
