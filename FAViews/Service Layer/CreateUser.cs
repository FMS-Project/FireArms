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

namespace FAViews.Service_Layer
{
    public class CreateUser
    {
        public void Register(Register register)
        {
            CRMConnection con = new CRMConnection();
           
            try
            {
                con.connection();

                if (con.organizationService != null)
                {
                    Guid userid = ((WhoAmIResponse)con.organizationService.Execute(new WhoAmIRequest())).UserId;

                    if (userid != Guid.Empty)
                    {
                       
                        var Register = new Entity("new_applicantionusers");
                        Register["new_name"] = register.Firstname;
                        Register["new_lastname"] = register.Lastname;
                        Register["new_email"] = register.Email;
                        Register["new_username"] = register.Username;
                        Register["new_password"] = register.Password;
                        
                        //Create the user
                        Guid regid = con.organizationService.Create(Register);
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