using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace FAViews.Models
{
    public class Applicant
    {
        public bool Pistols { get; set; }
        public bool Firearms { get; set; }
        public bool Ammunition { get; set; }
        public string ApplicationType { get; set; }
        public string Appstatus { get; set; }
        public Guid ApplicantID { get; set; }
        public string Id { get; set; }
        public string Companyname { get; set; }
        public string DealerAddress { get; set; }
        public string DealerCity { get; set; }
        public string DealerState { get; set; }
        public string DealerZipcode { get; set; }
        public string DealerAreaCode { get; set; }
        public string DealerCountry { get; set; }
        public string firearmslicensenumber { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        public string UBInumber { get; set; }
        public string Identificationnumber { get; set; }
        public DateTime PreviousDealerDate { get; set; }
       
        public string Identificationtype { get; set; }
        public string Tatoos { get; set; }

        public string Fullname { get; set; }
        public string Aliasname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        //public string Age { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Areacode { get; set; }
        public string Placeofbirth { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public bool Question1Yes { get; set; }
        public bool Question1No { get; set; }
        public bool Question2Yes { get; set; }
        public bool Question2No { get; set; }
        public bool Question3Yes { get; set; }
        public bool Question3No { get; set; }
        public bool Question4Yes { get; set; }
        public bool Question4No { get; set; }
        public bool Question5Yes { get; set; }
        public bool Question5No { get; set; }
        public bool Question6Yes { get; set; }
        public bool Question6No { get; set; }
        public bool Question7Yes { get; set; }
        public bool Question7No { get; set; }
        public bool Question8Yes { get; set; }
        public bool Question8No { get; set; }
        public bool Question9Yes { get; set; }
        public bool Question9No { get; set; }
        public bool Question10Yes { get; set; }
        public bool Question10No { get; set; }
        public bool Question11Yes { get; set; }
        public bool Question11No { get; set; }
        public string Residentialaddress { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string AlienRegistrationnumber { get; set; }
        public string PassportNumber { get; set; }
        public string VisaNumber { get; set; }
        public Guid Userid { get; set; }

        public bool PrivateTransfer { get; set; }
        public string Approvalcode { get; set; }
        public string Dealertransaction { get; set; }
        public string Others { get; set; }
        public string Caliber { get; set; }
        public string Barrellength { get; set; }
        public string condition { get; set; }
        public string Modelno { get; set; }
        public DateTime weapondelivereddate { get; set; }
        public string businessId { get; set; }
        public string LocationId { get; set; }
        public string storename { get; set; }
        public string residentcardno { get; set; }
        public string washintonfirearmnumber { get; set; }
        public string Concealedlicensenumber { get; set; }
        public string issuingauthority { get; set; }
        public string FireArmSerialnumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string AppropriateLea { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}