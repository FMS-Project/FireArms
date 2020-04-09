using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
//using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Tooling.Connector;
using FAViews.Models;

namespace FAViews.Service_Layer
{
    public class UpdateApplicant
    {
        public void UpdateRecords(Applicant applicant)
        {
            CRMConnection con = new CRMConnection();
            bool gender = false;
            bool condition = false;
            try
            {
                con.connection();

                if (con.organizationService != null)
                {
                    Guid userid = ((WhoAmIResponse)con.organizationService.Execute(new WhoAmIRequest())).UserId;

                    if (userid != Guid.Empty)
                    {
                        //retrieve applicant Guid
                        QueryByAttribute query = new QueryByAttribute("new_applicant");
                        query.ColumnSet = new ColumnSet(true);
                        query.Attributes.AddRange("new_id");
                        query.Values.AddRange(applicant.Id);

                        EntityCollection accountRecord = con.organizationService.RetrieveMultiple(query);
                        Guid guid = (Guid)accountRecord[0]["new_applicantid"];

                        //Update Applicant
                        var Applicant = new Entity("new_applicant");
                        Applicant.Id = guid;
                        if (applicant.ApplicationType == "Dealer")
                        {
                            Applicant["new_applicationtype"] = new OptionSetValue(100000000);
                            Applicant["new_expirationdate"] = applicant.ExpirationDate;
                            Applicant["new_previousdealerlicensedate"] = applicant.PreviousDealerDate;
                            Applicant["new_driverlicenseidnumber"] = applicant.Identificationnumber;
                            Applicant["new_dealeraddress"] = applicant.DealerAddress;
                            OptionSetValueCollection MultiOptionSet = new OptionSetValueCollection();
                            if (applicant.Pistols)
                            { MultiOptionSet.Add(new OptionSetValue(100000000)); }
                            if (applicant.Firearms)
                            { MultiOptionSet.Add(new OptionSetValue(100000001)); }
                            if (applicant.Ammunition)
                            { MultiOptionSet.Add(new OptionSetValue(100000002)); }
                            Applicant["new_intendtodeal"] = new OptionSetValueCollection(MultiOptionSet);
                        }
                        else if (applicant.ApplicationType == "Alien")
                        {
                            Applicant["new_applicationtype"] = new OptionSetValue(100000001);
                            Applicant["new_address"] = applicant.DealerAddress;
                            Applicant["new_driverlicenseidnumber"] = applicant.DriverLicenseNumber;
                        }
                        else if (applicant.ApplicationType == "FTA")
                        {
                            Applicant["new_applicationtype"] = new OptionSetValue(100000003);
                            Applicant["new_expirationdate"] = applicant.ExpirationDate;
                            Applicant["new_dealeraddress"] = applicant.DealerAddress;
                            Applicant["new_dealeremail"] = applicant.Email;
                            Applicant["new_dateweapondelivered"] = applicant.weapondelivereddate;
                            Applicant["new_applicationinitiated"] = applicant.ApplicationDate;
                            if (applicant.Firearms)
                            {
                                Applicant["new_ftaapplicationtype"] = new OptionSetValue(100000000);
                            }
                            else if (applicant.Pistols)
                            {
                                Applicant["new_ftaapplicationtype"] = new OptionSetValue(100000001);
                            }

                        }
                        Applicant["new_applicationstatus"] = new OptionSetValue(100000000);
                        Applicant["new_dealercompanyname"] = applicant.Companyname;                       
                        Applicant["new_mailingcity"] = applicant.DealerCity;
                        Applicant["new_mailingstate"] = applicant.DealerState;
                        Applicant["new_mailingzipcode"] = applicant.DealerZipcode;
                        Applicant["new_mailingcountry"] = applicant.DealerCountry;
                        Applicant["new_dealerareacode"] = applicant.DealerAreaCode;
                        Applicant["new_federalfirearmslicensenumber"] = applicant.firearmslicensenumber;
                        Applicant["new_ubinumber"] = applicant.UBInumber;
                        Applicant["new_identificationtype"] = applicant.Identificationtype;                       
                        Applicant["new_name"] = applicant.Fullname;
                        Applicant["new_aliasname"] = applicant.Aliasname;
                        Applicant["new_city"] = applicant.City;
                        Applicant["new_state"] = applicant.State;
                        Applicant["new_zipcode"] = applicant.Zipcode;
                        Applicant["new_country"] = applicant.Country;
                        Applicant["new_address"] = applicant.Address;
                        Applicant["new_race"] = applicant.Race;
                        Applicant["new_tatoos"] = applicant.Tatoos;
                        if (applicant.Gender == "Female")
                        {
                            gender = false;
                        }
                        else
                        {
                            gender = true;
                        }
                        Applicant["new_gender"] = gender;
                        Applicant["new_height"] = applicant.Height;
                        Applicant["new_weight"] = applicant.Weight;
                        Applicant["new_eyes"] = applicant.Eyes;
                        Applicant["new_hair"] = applicant.Hair;
                        Applicant["new_areacode"] = applicant.Areacode;
                        Applicant["new_birthplace"] = applicant.Placeofbirth;
                        Applicant["new_dob"] = applicant.DOB;
                        Applicant["new_email"] = applicant.Email;                        
                        Applicant["new_alienregistrationnumber"] = applicant.AlienRegistrationnumber;
                        Applicant["new_passportnumber"] = applicant.PassportNumber;
                        Applicant["new_visanumber"] = applicant.VisaNumber;
                        Applicant["new_privatetransfer"] = applicant.PrivateTransfer;
                        Applicant["new_approvalcode"] = applicant.Approvalcode;
                        Applicant["new_appropriatelea"] = applicant.AppropriateLea;
                        Applicant["new_others"] = applicant.Others;
                        Applicant["new_dealertransaction"] = applicant.Dealertransaction;
                        Applicant["new_caliber"] = applicant.Caliber;
                        Applicant["new_barrellength"] = applicant.Barrellength;
                        if (applicant.condition == "New")
                        {
                            condition = false;
                        }
                        else if (applicant.condition == "Used")
                        {
                            condition = true;
                        }
                        Applicant["new_condition"] = condition;
                        Applicant["new_modelnumberorname"] = applicant.Modelno;                       
                        Applicant["new_businessid"] = applicant.businessId;
                        Applicant["new_locationid"] = applicant.LocationId;
                        Applicant["new_dealerstorename"] = applicant.storename;
                        Applicant["new_permanentresidentcardnumber"] = applicant.residentcardno;
                        Applicant["new_alienfirearmslicensenumber"] = applicant.washintonfirearmnumber;
                        Applicant["new_cplnumber"] = applicant.Concealedlicensenumber;
                        Applicant["new_issuingauthority"] = applicant.issuingauthority;                        
                        Applicant["new_firearmserialnumber"] = applicant.FireArmSerialnumber;


                        //Update the application
                        con.organizationService.Update(Applicant);
                        if(applicant.ApplicationType!="FTA")
                        {
                        //update Question answers
                        var Qansr = new Entity("new_applicantquestionanswers");
                        QueryByAttribute qnsquery = new QueryByAttribute("new_applicantquestionanswers");
                        qnsquery.ColumnSet = new ColumnSet(true);
                        qnsquery.Attributes.AddRange("new_applicant");
                        qnsquery.Values.AddRange(guid);
                        EntityCollection qnsRecord = con.organizationService.RetrieveMultiple(qnsquery);

                            if (qnsRecord != null && qnsRecord.Entities.Count > 0)
                            {
                                for (int i = 0; i < qnsRecord.Entities.Count; i++)
                                {
                                    Qansr.Id = (Guid)qnsRecord[i]["new_applicantquestionanswersid"];
                                    EntityReference entref = (EntityReference)qnsRecord[i].Attributes["new_question"];
                                    if (entref.Name == "Q1")
                                    {
                                        if ((applicant.Question1Yes == false && applicant.Question1No == false) || (applicant.Question1No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question1Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q2")
                                    {
                                        if ((applicant.Question2Yes == false && applicant.Question2No == false) || (applicant.Question2No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question2Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q3")
                                    {
                                        if ((applicant.Question3Yes == false && applicant.Question3No == false) || (applicant.Question3No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question3Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q4")
                                    {
                                        if ((applicant.Question4Yes == false && applicant.Question4No == false) || (applicant.Question4No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question4Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q5")
                                    {
                                        if ((applicant.Question5Yes == false && applicant.Question5No == false) || (applicant.Question5No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question5Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q6")
                                    {
                                        if ((applicant.Question6Yes == false && applicant.Question6No == false) || (applicant.Question6No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question6Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q7")
                                    {
                                        if ((applicant.Question7Yes == false && applicant.Question7No == false) || (applicant.Question7No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question7Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q8")
                                    {
                                        if ((applicant.Question8Yes == false && applicant.Question8No == false) || (applicant.Question8No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question8Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }

                                    if (entref.Name == "Q9")
                                    {
                                        if ((applicant.Question9Yes == false && applicant.Question9No == false) || (applicant.Question9No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question9Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q10")
                                    {
                                        if ((applicant.Question10Yes == false && applicant.Question8No == false) || (applicant.Question10No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question10Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                    if (entref.Name == "Q11")
                                    {
                                        if ((applicant.Question11Yes == false && applicant.Question11No == false) || (applicant.Question11No == true))
                                        {
                                            Qansr["new_answer"] = false;
                                        }
                                        else if (applicant.Question11Yes == true)
                                        {
                                            Qansr["new_answer"] = true;
                                        }
                                        con.organizationService.Update(Qansr);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("Failed to Established Connection!!!");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
    }
}