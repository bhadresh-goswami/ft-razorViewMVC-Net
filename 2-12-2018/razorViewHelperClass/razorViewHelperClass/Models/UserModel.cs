using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace razorViewHelperClass.Models
{
    public class UserModel
    {
        //public UserModel()
        //{
        //    this.VerifiedKey = Guid.NewGuid();
        //}
        [Key]
        public Guid UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15,ErrorMessage ="Enter password between 8 to 15 char long!",MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailId { get; set; }
        //private int _Mobile;

        //public int Mobile
        //{
        //    get { return _Mobile; }
        //    set { _Mobile = value; }
        //}
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Gender { get; set; }
        public bool Subscribe { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string CountryName { get; set; }
        [Required]
        public string StateName { get; set; }
        [Required]
        public string CityName { get; set; }

        public string AvtarUrl { get; set; }
        //private Guid _VerificationKey;

        public Guid VerificationKey
        {
            get { return Guid.NewGuid(); }
        }

        //public Guid VerifiedKey { get; set; }
        public bool IsVerified { get; set; }

        public static List<SelectListItem> StateNames {
            get {


                List<SelectListItem> StateListItems = new List<SelectListItem>();

                SelectListItem item = new SelectListItem() { Text = "Gujrat", Value = "GUJ" };
                StateListItems.Add(item);

                item = new SelectListItem() { Text = "MP", Value = "MP" };
                StateListItems.Add(item);
                return StateListItems;

            }
        }
    }
}