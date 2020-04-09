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
    public class GetLoginDetails
    {
        CRMConnection con = new CRMConnection();
        public void logindetails(Login login)
        {
            try
            {
                con.connection();
                if (con.organizationService != null)
                {
                    Guid userid = ((WhoAmIResponse)con.organizationService.Execute(new WhoAmIRequest())).UserId;

                    if (userid != Guid.Empty)
                    {
                        QueryByAttribute query = new QueryByAttribute("new_applicantionusers");
                        {

                            query.ColumnSet = new ColumnSet("new_applicantionusersid");
                            query.Attributes.AddRange("new_username", "new_password");
                            query.Values.AddRange(login.Username, login.Password);
                        };
                        EntityCollection accountRecord = con.organizationService.RetrieveMultiple(query);

                        if (accountRecord != null && accountRecord.Entities.Count > 0)
                        {
                            login.LoginStatus = "Login Success";
                            login.Userid = (Guid)accountRecord[0]["new_applicantionusersid"];
                            
                        }
                        else
                        {
                            login.LoginStatus = "Login Failed";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;

            }

        }
        public List<Applicant> RetriveRecords(Login login)
        {
            con.connection();
            QueryByAttribute query = new QueryByAttribute("new_applicant");
            {
                query.ColumnSet = new ColumnSet("new_id", "new_dealercompanyname", "new_name", "new_applicationtype","new_applicationstatus","createdon");
                query.Attributes.AddRange("new_user");
                query.Values.AddRange(login.Userid);
            };
            List<Applicant> info = new List<Applicant>();
            EntityCollection accountRecord = con.organizationService.RetrieveMultiple(query);
            if (accountRecord != null && accountRecord.Entities.Count > 0)
            {                
                for (int i = 0; i < accountRecord.Entities.Count; i++)
                {
                    Applicant applicantModel = new Applicant();
                    if (accountRecord[i].Contains("new_id") && accountRecord[i]["new_id"] != null)
                        applicantModel.Id = accountRecord[i]["new_id"].ToString();
                    if (accountRecord[i].Contains("new_dealercompanyname") && accountRecord[i]["new_dealercompanyname"] != null)
                        applicantModel.Companyname = accountRecord[i]["new_dealercompanyname"].ToString();                   
                    if (accountRecord[i].Contains("new_name") && accountRecord[i]["new_name"] != null)
                        applicantModel.Fullname = (accountRecord[i]["new_name"]).ToString();
                    if (accountRecord[i].Contains("createdon") && accountRecord[i]["createdon"] != null)
                        applicantModel.CreatedOn = (DateTime)(accountRecord[i]["createdon"]);
                    if (accountRecord[i].Contains("new_applicationstatus") && accountRecord[i]["new_applicationstatus"] != null)
                        //applicantModel.Appstatus = (accountRecord[i]["new_applicationstatus"]).ToString();
                    {
                        var appstatus = ((OptionSetValue)accountRecord[i]["new_applicationstatus"]).Value;
                        if (appstatus == 100000000)
                        {
                            applicantModel.Appstatus = "new";
                        }
                        else if (appstatus == 100000001)
                        {
                            applicantModel.Appstatus = "Approved";
                        }
                        else if (appstatus == 100000002)
                        {
                            applicantModel.Appstatus = "Denied";
                        }
                    }


                    if (accountRecord[i].Contains("new_applicationtype") && accountRecord[i]["new_applicationtype"] != null)
                    {
                        var apptype = ((OptionSetValue)accountRecord[i]["new_applicationtype"]).Value;
                        if (apptype == 100000000)
                        {
                            applicantModel.ApplicationType = "Dealer";
                        }
                        else if (apptype == 100000001)
                        {
                            applicantModel.ApplicationType = "Alien";
                        }
                        else if (apptype == 100000003)
                        {
                            applicantModel.ApplicationType = "FTA";
                        }
                    }
                    info.Add(applicantModel);
                    
                }
            }
            return info;
        }

      
    }
}