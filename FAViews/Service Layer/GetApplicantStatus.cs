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
    public class GetApplicantStatus
    {
        CRMConnection con = new CRMConnection();
        public void GetStatus(Applicant applicant)
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

                        EntityCollection accountRecord = con.organizationService.RetrieveMultiple(query);
                        //if (applicant.ApplicationType == "Dealer")
                        //    {
                        //        Applicant["new_applicationtype"] = new OptionSetValue(100000000);
                        //        Applicant["new_expirationdate"] = applicant.ExpirationDate;
                        //        Applicant["new_previousdealerlicensedate"] = applicant.PreviousDealerDate;
                        //    }
                        //    else
                        //    {
                        //        Applicant["new_applicationtype"] = new OptionSetValue(100000001);
                        //    }
                            
                        var appstatus = ((OptionSetValue)accountRecord[0]["new_applicationstatus"]).Value;

                        if (appstatus == 100000000)
                        {
                            applicant.Appstatus = "new";
                        }
                        else if (appstatus == 100000001)
                        {
                            applicant.Appstatus = "Approved";
                        }
                        else if (appstatus == 100000002)
                        {
                            applicant.Appstatus = "Denied";
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