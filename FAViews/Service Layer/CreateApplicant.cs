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
using FAViews.Service_Layer;
namespace FAViews.Models
{
    public class CreateApplicant
    {

        CRMConnection con = new CRMConnection();
        public void CreateRecords(Applicant applicant)
        {
            
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
                        //retrieve applicant count
                        QueryExpression query = new QueryExpression
                        {
                            EntityName = "new_applicant"
                        };
                        EntityCollection accountRecord = con.organizationService.RetrieveMultiple(query);
                        int id = accountRecord.Entities.Count;
                        id = id + 1;
                        var applicantid = "DL-" + id;
                        applicant.Id = applicantid;
                        //create Applicant
                        var Applicant = new Entity("new_applicant");
                        Applicant["new_id"] = applicantid;
                        if(applicant.ApplicationType=="Dealer")
                        {
                            Applicant["new_applicationtype"] = new OptionSetValue(100000000);
                            Applicant["new_expirationdate"] = applicant.ExpirationDate;
                            Applicant["new_previousdealerlicensedate"] = applicant.PreviousDealerDate;
                            Applicant["new_dealeraddress"] = applicant.DealerAddress;
                            Applicant["new_driverlicenseidnumber"] = applicant.Identificationnumber;
                            OptionSetValueCollection MultiOptionSet = new OptionSetValueCollection();
                            if(applicant.Pistols)
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
                        //Applicant["new_age"] = applicant.Age;
                        Applicant["new_name"] = applicant.Fullname;
                        Applicant["new_aliasname"] = applicant.Aliasname;
                        Applicant["new_city"] = applicant.City;
                        Applicant["new_state"] = applicant.State;
                        Applicant["new_zipcode"] = applicant.Zipcode;
                        Applicant["new_country"] = applicant.Country;
                        Applicant["new_race"] = applicant.Race;
                        Applicant["new_tatoos"] = applicant.Tatoos;
                        if (applicant.Gender == "Female")
                        {
                            gender = false;
                        }
                        else if (applicant.Gender == "Male")
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

                        if (applicant.Userid!=Guid.Parse("{00000000-0000-0000-0000-000000000000}"))
                        {
                            Applicant["new_user"] = new EntityReference("new_applicantionusers", applicant.Userid);
                        }
                                             

                        //Create the account
                        Guid appid = con.organizationService.Create(Applicant);

                        if ((appid != null)&&(applicant.ApplicationType!="FTA"))
                        {

                            var Qansr = new Entity("new_applicantquestionanswers");
                            Guid ansrid;
                            Guid Qguid;
                            QueryByAttribute qns = new QueryByAttribute("new_questions");
                            qns.ColumnSet = new ColumnSet(true);
                            qns.Attributes.AddRange("new_questionid");
                            qns.Values.AddRange("Q1");

                            EntityCollection qnsguid = con.organizationService.RetrieveMultiple(qns);

                            if ((applicant.Question1Yes == false && applicant.Question1No == false) || (applicant.Question1No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question1Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid[0].Contains("new_questionsid") && qnsguid[0]["new_questionsid"] != null)
                            {
                                Qguid = (Guid)qnsguid[0]["new_questionsid"];
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);


                            QueryByAttribute qns2 = new QueryByAttribute("new_questions");
                            qns2.ColumnSet = new ColumnSet(true);
                            qns2.Attributes.AddRange("new_questionid");
                            qns2.Values.AddRange("Q2");

                            EntityCollection qnsguid2 = con.organizationService.RetrieveMultiple(qns2);
                            if ((applicant.Question2Yes == false && applicant.Question2No == false) || (applicant.Question2No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question2Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid2[0].Contains("new_questionsid") && qnsguid2[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid2[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns3 = new QueryByAttribute("new_questions");
                            qns3.ColumnSet = new ColumnSet(true);
                            qns3.Attributes.AddRange("new_questionid");
                            qns3.Values.AddRange("Q3");

                            EntityCollection qnsguid3 = con.organizationService.RetrieveMultiple(qns3);
                            if ((applicant.Question3Yes == false && applicant.Question3No == false) || (applicant.Question3No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question3Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid3[0].Contains("new_questionsid") && qnsguid3[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid3[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns4 = new QueryByAttribute("new_questions");
                            qns4.ColumnSet = new ColumnSet(true);
                            qns4.Attributes.AddRange("new_questionid");
                            qns4.Values.AddRange("Q4");

                            EntityCollection qnsguid4 = con.organizationService.RetrieveMultiple(qns4);
                            if ((applicant.Question4Yes == false && applicant.Question4No == false) || (applicant.Question4No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question4Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid4[0].Contains("new_questionsid") && qnsguid4[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid4[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns5 = new QueryByAttribute("new_questions");
                            qns5.ColumnSet = new ColumnSet(true);
                            qns5.Attributes.AddRange("new_questionid");
                            qns5.Values.AddRange("Q5");

                            EntityCollection qnsguid5 = con.organizationService.RetrieveMultiple(qns5);
                            if ((applicant.Question5Yes == false && applicant.Question5No == false) || (applicant.Question5No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question5Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid5[0].Contains("new_questionsid") && qnsguid5[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid5[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns6 = new QueryByAttribute("new_questions");
                            qns6.ColumnSet = new ColumnSet(true);
                            qns6.Attributes.AddRange("new_questionid");
                            qns6.Values.AddRange("Q6");

                            EntityCollection qnsguid6 = con.organizationService.RetrieveMultiple(qns6);
                            if ((applicant.Question6Yes == false && applicant.Question6No == false) || (applicant.Question6No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question6Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid6[0].Contains("new_questionsid") && qnsguid6[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid6[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns7 = new QueryByAttribute("new_questions");
                            qns7.ColumnSet = new ColumnSet(true);
                            qns7.Attributes.AddRange("new_questionid");
                            qns7.Values.AddRange("Q7");

                            EntityCollection qnsguid7 = con.organizationService.RetrieveMultiple(qns7);
                            if ((applicant.Question7Yes == false && applicant.Question7No == false) || (applicant.Question7No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question7Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid7[0].Contains("new_questionsid") && qnsguid7[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid7[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns8 = new QueryByAttribute("new_questions");
                            qns8.ColumnSet = new ColumnSet(true);
                            qns8.Attributes.AddRange("new_questionid");
                            qns8.Values.AddRange("Q8");

                            EntityCollection qnsguid8 = con.organizationService.RetrieveMultiple(qns8);
                            if ((applicant.Question8Yes == false && applicant.Question8No == false) || (applicant.Question8No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question8Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid8[0].Contains("new_questionsid") && qnsguid8[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid8[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns9 = new QueryByAttribute("new_questions");
                            qns9.ColumnSet = new ColumnSet(true);
                            qns9.Attributes.AddRange("new_questionid");
                            qns9.Values.AddRange("Q9");

                            EntityCollection qnsguid9 = con.organizationService.RetrieveMultiple(qns9);
                            if ((applicant.Question9Yes == false && applicant.Question9No == false) || (applicant.Question9No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question9Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid9[0].Contains("new_questionsid") && qnsguid9[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid9[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns10 = new QueryByAttribute("new_questions");
                            qns10.ColumnSet = new ColumnSet(true);
                            qns10.Attributes.AddRange("new_questionid");
                            qns10.Values.AddRange("Q10");

                            EntityCollection qnsguid10 = con.organizationService.RetrieveMultiple(qns10);
                            if ((applicant.Question10Yes == false && applicant.Question10No == false) || (applicant.Question10No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question10Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid10[0].Contains("new_questionsid") && qnsguid10[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid10[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);

                            QueryByAttribute qns11 = new QueryByAttribute("new_questions");
                            qns11.ColumnSet = new ColumnSet(true);
                            qns11.Attributes.AddRange("new_questionid");
                            qns11.Values.AddRange("Q11");

                            EntityCollection qnsguid11 = con.organizationService.RetrieveMultiple(qns11);
                            if ((applicant.Question11Yes == false && applicant.Question11No == false) || (applicant.Question11No == true))
                            {
                                Qansr["new_answer"] = false;
                            }
                            else if (applicant.Question11Yes == true)
                            {
                                Qansr["new_answer"] = true;
                            }
                            Qansr["new_applicant"] = new EntityReference("new_applicant", appid);
                            if (qnsguid11[0].Contains("new_questionsid") && qnsguid11[0]["new_questionsid"] != null)
                            {
                                Qansr["new_question"] = new EntityReference("new_questions", (Guid)qnsguid11[0]["new_questionsid"]);
                            }
                            ansrid = con.organizationService.Create(Qansr);
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