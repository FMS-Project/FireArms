using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Tooling.Connector;
using FAViews.Models;
namespace FAViews.Service_Layer
{
    public class Getapplicantdetails
    {
        CRMConnection con = new CRMConnection();
        public void Getrecord(Applicant applicant)
        {
            try
            {
                con.connection();

                if (con.organizationService != null)
                {
                    Guid userid = ((WhoAmIResponse)con.organizationService.Execute(new WhoAmIRequest())).UserId;

                    if (userid != Guid.Empty)
                    {
                        QueryByAttribute query = new QueryByAttribute("new_applicant");
                        query.ColumnSet = new ColumnSet(true);
                        query.Attributes.AddRange("new_id");
                        query.Values.AddRange(applicant.Id);

                        EntityCollection applicantRecord = con.organizationService.RetrieveMultiple(query);

                        if (applicantRecord != null && applicantRecord.Entities.Count > 0)
                        {
                            if (applicantRecord[0].Contains("new_applicantid") && applicantRecord[0]["new_applicantid"] != null)
                            {
                                applicant.ApplicantID = (Guid)applicantRecord[0]["new_applicantid"];
                            }
                            if (applicantRecord[0].Contains("new_applicationtype") && applicantRecord[0]["new_applicationtype"] != null)
                            {
                                var apptype = ((OptionSetValue)applicantRecord[0]["new_applicationtype"]).Value;
                                if (apptype == 100000000)
                                {
                                    applicant.ApplicationType = "Dealer";
                                }
                                else if (apptype == 100000001)
                                {
                                    applicant.ApplicationType = "Alien";
                                }
                                else if (apptype == 100000003)
                                {
                                    applicant.ApplicationType = "FTA";
                                }
                            }
                            object FieldObj;
                            if (applicantRecord[0].Attributes.TryGetValue("new_intendtodeal", out FieldObj))
                            {
                                //Get the collection of value(s) for MultiSelect Option Set field
                                OptionSetValueCollection Field = (OptionSetValueCollection)FieldObj;
                                if (Field.Count > 0)
                                {
                                    for (int i = 0; i < Field.Count; i++)
                                    {
                                        if (Field[i].Value == 100000000)
                                        {
                                            applicant.Pistols = true;
                                        }
                                        if (Field[i].Value == 100000001)
                                        {
                                            applicant.Firearms = true;
                                        }
                                        if (Field[i].Value == 100000002)
                                        {
                                            applicant.Ammunition = true;
                                        }
                                    }
                                }
                            }
                            if (applicantRecord[0].Contains("new_dealercompanyname") && applicantRecord[0]["new_dealercompanyname"] != null)
                            {
                                applicant.Companyname = applicantRecord[0]["new_dealercompanyname"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_tatoos") && applicantRecord[0]["new_tatoos"] != null)
                            {
                                applicant.Tatoos = applicantRecord[0]["new_tatoos"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_dealeraddress") && applicantRecord[0]["new_dealeraddress"] != null)
                            {
                                applicant.DealerAddress = applicantRecord[0]["new_dealeraddress"].ToString();
                            }
                            if (applicant.ApplicationType == "Alien")
                            {
                                if (applicantRecord[0].Contains("new_address") && applicantRecord[0]["new_address"] != null)
                                {
                                    applicant.DealerAddress = applicantRecord[0]["new_address"].ToString();
                                }

                            }
                            if (applicantRecord[0].Contains("new_mailingcity") && applicantRecord[0]["new_mailingcity"] != null)
                            {
                                applicant.DealerCity = applicantRecord[0]["new_mailingcity"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_mailingstate") && applicantRecord[0]["new_mailingstate"] != null)
                            {
                                applicant.DealerState = applicantRecord[0]["new_mailingstate"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_mailingzipcode") && applicantRecord[0]["new_mailingzipcode"] != null)
                            {
                                applicant.DealerZipcode = applicantRecord[0]["new_mailingzipcode"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_mailingcountry") && applicantRecord[0]["new_mailingcountry"] != null)
                            {
                                applicant.DealerCountry = applicantRecord[0]["new_mailingcountry"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_dealerareacode") && applicantRecord[0]["new_dealerareacode"] != null)
                            {
                                applicant.DealerAreaCode = applicantRecord[0]["new_dealerareacode"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_federalfirearmslicensenumber") && applicantRecord[0]["new_federalfirearmslicensenumber"] != null)
                            {
                                applicant.firearmslicensenumber = applicantRecord[0]["new_federalfirearmslicensenumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_expirationdate") && applicantRecord[0]["new_expirationdate"] != null)
                            {
                                applicant.ExpirationDate = (DateTime)applicantRecord[0]["new_expirationdate"];
                            }
                            if (applicantRecord[0].Contains("new_ubinumber") && applicantRecord[0]["new_ubinumber"] != null)
                            {
                                applicant.UBInumber = applicantRecord[0]["new_ubinumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_previousdealerlicensedate") && applicantRecord[0]["new_previousdealerlicensedate"] != null)
                            {
                                applicant.PreviousDealerDate = (DateTime)applicantRecord[0]["new_previousdealerlicensedate"];
                            }
                            if (applicantRecord[0].Contains("new_identificationtype") && applicantRecord[0]["new_identificationtype"] != null)
                            {
                                applicant.Identificationtype = applicantRecord[0]["new_identificationtype"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_driverlicenseidnumber") && applicantRecord[0]["new_driverlicenseidnumber"] != null)
                            {

                                applicant.Identificationnumber = applicantRecord[0]["new_driverlicenseidnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_name") && applicantRecord[0]["new_name"] != null)
                            {
                                applicant.Fullname = applicantRecord[0]["new_name"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_aliasname") && applicantRecord[0]["new_aliasname"] != null)
                            {
                                applicant.Aliasname = applicantRecord[0]["new_aliasname"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_city") && applicantRecord[0]["new_city"] != null)
                            {
                                applicant.City = applicantRecord[0]["new_city"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_state") && applicantRecord[0]["new_state"] != null)
                            {
                                applicant.State = applicantRecord[0]["new_state"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_zipcode") && applicantRecord[0]["new_zipcode"] != null)
                            {
                                applicant.Zipcode = applicantRecord[0]["new_zipcode"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_country") && applicantRecord[0]["new_country"] != null)
                            {
                                applicant.Country = applicantRecord[0]["new_country"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_address") && applicantRecord[0]["new_address"] != null)
                            {
                                applicant.Address = applicantRecord[0]["new_address"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_race") && applicantRecord[0]["new_race"] != null)
                            {
                                applicant.Race = applicantRecord[0]["new_race"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_gender") && applicantRecord[0]["new_gender"] != null)
                            {
                                var gender = applicantRecord[0].Contains("new_gender").ToString();
                                if (gender == "True")
                                {
                                    applicant.Gender = "Male";
                                }
                                else if (gender == "False")
                                {
                                    applicant.Gender = "Female";
                                }
                                //applicant.Gender = applicantRecord[0]["new_gender"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_height") && applicantRecord[0]["new_height"] != null)
                            {
                                applicant.Height = applicantRecord[0]["new_height"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_weight") && applicantRecord[0]["new_weight"] != null)
                            {
                                applicant.Weight = applicantRecord[0]["new_weight"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_eyes") && applicantRecord[0]["new_eyes"] != null)
                            {
                                applicant.Eyes = applicantRecord[0]["new_eyes"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_hair") && applicantRecord[0]["new_hair"] != null)
                            {
                                applicant.Hair = applicantRecord[0]["new_hair"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_areacode") && applicantRecord[0]["new_areacode"] != null)
                            {
                                applicant.Areacode = applicantRecord[0]["new_areacode"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_birthplace") && applicantRecord[0]["new_birthplace"] != null)
                            {
                                applicant.Placeofbirth = applicantRecord[0]["new_birthplace"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_dob") && applicantRecord[0]["new_dob"] != null)
                            {
                                applicant.DOB = (DateTime)applicantRecord[0]["new_dob"];
                            }
                            if (applicantRecord[0].Contains("new_email") && applicantRecord[0]["new_email"] != null)
                            {
                                applicant.Email = applicantRecord[0]["new_email"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_driverlicenseidnumber") && applicantRecord[0]["new_driverlicenseidnumber"] != null)
                            {
                                applicant.DriverLicenseNumber = applicantRecord[0]["new_driverlicenseidnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_alienregistrationnumber") && applicantRecord[0]["new_alienregistrationnumber"] != null)
                            {
                                applicant.AlienRegistrationnumber = applicantRecord[0]["new_alienregistrationnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_passportnumber") && applicantRecord[0]["new_passportnumber"] != null)
                            {
                                applicant.PassportNumber = applicantRecord[0]["new_passportnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_visanumber") && applicantRecord[0]["new_visanumber"] != null)
                            {
                                applicant.VisaNumber = applicantRecord[0]["new_visanumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_ftaapplicationtype") && applicantRecord[0]["new_ftaapplicationtype"] != null)
                            {
                                var ftatype = ((OptionSetValue)applicantRecord[0]["new_ftaapplicationtype"]).Value;
                                if(ftatype == 100000000)
                                {
                                    applicant.Firearms = true;
                                }
                                if (ftatype == 100000001)
                                {
                                    applicant.Pistols = true;
                                }
                            }
                                if (applicantRecord[0].Contains("new_approvalcode") && applicantRecord[0]["new_approvalcode"] != null)
                            {
                                applicant.Approvalcode = applicantRecord[0]["new_approvalcode"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_dealertransaction") && applicantRecord[0]["new_dealertransaction"] != null)
                            {
                                applicant.Dealertransaction = applicantRecord[0]["new_dealertransaction"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_caliber") && applicantRecord[0]["new_caliber"] != null)
                            {
                                applicant.Caliber = applicantRecord[0]["new_caliber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_barrellength") && applicantRecord[0]["new_barrellength"] != null)
                            {
                                applicant.Barrellength = applicantRecord[0]["new_barrellength"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_condition") && applicantRecord[0]["new_condition"] != null)
                            {
                                var condition = applicantRecord[0].Contains("new_condition").ToString();
                                if (condition == "True")
                                {
                                    applicant.condition = "New";
                                }
                                else if (condition == "False")
                                {
                                    applicant.condition = "Used";
                                }                               
                            }
                            if (applicantRecord[0].Contains("new_modelnumberorname") && applicantRecord[0]["new_modelnumberorname"] != null)
                            {
                                applicant.Modelno = applicantRecord[0]["new_modelnumberorname"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_dateweapondelivered") && applicantRecord[0]["new_dateweapondelivered"] != null)
                            {
                                applicant.weapondelivereddate = (DateTime)applicantRecord[0]["new_dateweapondelivered"];
                            }
                            if (applicantRecord[0].Contains("new_businessid") && applicantRecord[0]["new_businessid"] != null)
                            {
                                applicant.businessId = applicantRecord[0]["new_businessid"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_locationid") && applicantRecord[0]["new_locationid"] != null)
                            {
                                applicant.LocationId = applicantRecord[0]["new_locationid"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_dealerstorename") && applicantRecord[0]["new_dealerstorename"] != null)
                            {
                                applicant.storename = applicantRecord[0]["new_dealerstorename"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_permanentresidentcardnumber") && applicantRecord[0]["new_permanentresidentcardnumber"] != null)
                            {
                                applicant.residentcardno = applicantRecord[0]["new_permanentresidentcardnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_alienfirearmslicensenumber") && applicantRecord[0]["new_alienfirearmslicensenumber"] != null)
                            {
                                applicant.washintonfirearmnumber = applicantRecord[0]["new_alienfirearmslicensenumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_cplnumber") && applicantRecord[0]["new_cplnumber"] != null)
                            {
                                applicant.Concealedlicensenumber = applicantRecord[0]["new_cplnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_issuingauthority") && applicantRecord[0]["new_issuingauthority"] != null)
                            {
                                applicant.issuingauthority = applicantRecord[0]["new_issuingauthority"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_applicationinitiated") && applicantRecord[0]["new_applicationinitiated"] != null)
                            {
                                applicant.ApplicationDate = (DateTime)applicantRecord[0]["new_applicationinitiated"];
                            }
                            if (applicantRecord[0].Contains("new_firearmserialnumber") && applicantRecord[0]["new_firearmserialnumber"] != null)
                            {
                                applicant.FireArmSerialnumber = applicantRecord[0]["new_firearmserialnumber"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_privatetransfer") && applicantRecord[0]["new_privatetransfer"] != null)
                            {
                                applicant.PrivateTransfer = (bool)applicantRecord[0]["new_privatetransfer"];
                            }
                            if (applicantRecord[0].Contains("new_others") && applicantRecord[0]["new_others"] != null)
                            {
                                applicant.Others = applicantRecord[0]["new_others"].ToString();
                            }
                            if (applicantRecord[0].Contains("new_appropriatelea") && applicantRecord[0]["new_appropriatelea"] != null)
                            {
                                applicant.AppropriateLea = applicantRecord[0]["new_appropriatelea"].ToString();
                            }

                            if (applicant.ApplicationType != "FTA")
                            { 
                                QueryByAttribute qnsquery = new QueryByAttribute("new_applicantquestionanswers");
                                qnsquery.ColumnSet = new ColumnSet(true);
                                qnsquery.Attributes.AddRange("new_applicant");
                                qnsquery.Values.AddRange(applicant.ApplicantID);
                                EntityCollection qnsRecord = con.organizationService.RetrieveMultiple(qnsquery);

                                if (qnsRecord != null && qnsRecord.Entities.Count > 0)
                                {
                                    for (int i = 0; i < qnsRecord.Entities.Count; i++)
                                    {
                                        if (qnsRecord[i].Contains("new_question") && qnsRecord[i]["new_question"] != null)
                                        {
                                            EntityReference entref = (EntityReference)qnsRecord[i].Attributes["new_question"];
                                            if (entref.Name == "Q1")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question1Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question1No = true;
                                                    }

                                                }
                                            }
                                            if (entref.Name == "Q2")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question2Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question2No = true;
                                                    }

                                                }
                                            }
                                            if (entref.Name == "Q3")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question3Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question3No = true;
                                                    }

                                                }
                                            }
                                            if (entref.Name == "Q4")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question4Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question4No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q5")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question5Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question5No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q6")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question6Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question6No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q7")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question7Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question7No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q8")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question8Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question8No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q9")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question9Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question9No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q10")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question10Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question10No = true;
                                                    }
                                                }
                                            }
                                            if (entref.Name == "Q11")
                                            {
                                                if (qnsRecord[i].Contains("new_answer") && qnsRecord[i]["new_answer"] != null)
                                                {
                                                    if ((bool)qnsRecord[i]["new_answer"] == true)
                                                    {
                                                        applicant.Question11Yes = true;
                                                    }
                                                    else
                                                    {
                                                        applicant.Question11No = true;
                                                    }
                                                }
                                            }
                                        }
                                    }



                                }
                            }

                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
