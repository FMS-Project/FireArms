using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FAViews.Models;
using System.Threading.Tasks;
using FAViews.Service_Layer;

namespace FAViews.Controllers
{
    public class ApplicantController : Controller
    {
        Applicant app = new Applicant();
        // GET: Applicant
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Index2(Applicant applicant)
        {
            return View();
        }
        public ActionResult Index3()
        {
            return View();
        }
        public ActionResult FTA()
        {
            return View();
        }
        public ActionResult Login()
        {
            //TempData["BackButton"] = "False";
            return View();
        }
       
        [HttpPost]
        public ActionResult EditApplicant(Applicant applicant)
        {
            
                GetApplicantStatus getstatus = new GetApplicantStatus();
                getstatus.GetStatus(applicant);
           
                if (applicant.Appstatus == "new")
                {
                    Getapplicantdetails getapp = new Getapplicantdetails();
                    getapp.Getrecord(applicant);
                    if (applicant.ApplicationType == "Dealer")
                    {
                        return View(applicant);
                    }
                    else if (applicant.ApplicationType == "Alien")
                    {
                        return RedirectToAction("EditAlienApplicant", applicant);
                    }
                    else 
                    {
                        return RedirectToAction("EditFTAApplicant", applicant);
                    }
                }
                else
                {
                    return RedirectToAction("UpdateButton", applicant);
                }
         }            
        
       
        public ActionResult EditAlienApplicant(Applicant applicant)
        {
            
                    return View(applicant);                
            
        }
        public ActionResult EditFTAApplicant(Applicant applicant)
        {

            return View(applicant);

        }
        public ActionResult UpdateButton(Applicant app)
        {
           
            if (app.Appstatus == "new")
            {
                UpdateApplicant objreg = new UpdateApplicant();
                objreg.UpdateRecords(app);
                return View(app);
            }
            else
            {
                ViewBag.Appstatus = app.Appstatus;
                return View(app);
            }
        }
        public ActionResult CreateButton(Applicant app)
        {
            if(TempData["UserId"]!=null)
            {
                app.Userid = (Guid)TempData["UserId"];
            }                
            CreateApplicant objreg = new CreateApplicant();
            objreg.CreateRecords(app);            
            return View(app);
        }
        public ActionResult GetLogin(Login login)
        {
            //if(TempData["BackButton"].ToString()=="True")
            //{
            //    login.Username = TempData["Username"].ToString();
            //    login.Password = TempData["PWD"].ToString();
            //}
          
            GetLoginDetails obj = new GetLoginDetails();
            obj.logindetails(login);
            ViewBag.status = login.LoginStatus;
            if (login.LoginStatus == "Login Success")
            {
                List<Applicant> applicantinfo= obj.RetriveRecords(login);
                TempData["Username"] = login.Username;
                TempData["PWD"] = login.Password;
                TempData["UserId"] = login.Userid;
                ViewBag.applicantinfo = applicantinfo;
            }
            return View();
        }
        public ActionResult Guestlogin()
        {
            return View();
        }
        public ActionResult Readonly(Applicant applicant)
        {
            GetApplicantStatus getstatus = new GetApplicantStatus();
            getstatus.GetStatus(applicant);

            if (applicant.Appstatus == "new")
            {
                Getapplicantdetails getapp = new Getapplicantdetails();
                getapp.Getrecord(applicant);
                if (applicant.ApplicationType == "Dealer")
                {
                    return View(applicant);
                }
                else if (applicant.ApplicationType == "Alien")
                {
                    return RedirectToAction("ReadAlien", applicant);
                }
                else
                {
                    return RedirectToAction("ReadFTA", applicant);
                }
            }
            else
            {
                return RedirectToAction("UpdateButton", applicant);
            }

        }
        public ActionResult ReadAlien(Applicant applicant)
        {
            return View(applicant);
        }
        public ActionResult ReadFTA(Applicant applicant)
        {
            return View(applicant);
        }
    }
}