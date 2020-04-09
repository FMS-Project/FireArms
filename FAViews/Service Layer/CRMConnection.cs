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
    public class CRMConnection
    {
        public  IOrganizationService organizationService = null;
        public  void connection()
        {            
            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = "Officer@CodeSmartInc.com";
            clientCredentials.UserName.Password = "FRED123!!";

            // For Dynamics 365 Customer Engagement V9.X, set Security Protocol as TLS12
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Get the URL from CRM, Navigate to Settings -> Customizations -> Developer Resources
            // Copy and Paste Organization Service Endpoint Address URL
            organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri("https://csmart.crm.dynamics.com/XRMServices/2011/Organization.svc"),
             null, clientCredentials, null);

        }


    }
}