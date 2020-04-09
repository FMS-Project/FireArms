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
    public class RegisterController : Controller
    {
       
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult RegisterButton(Register reg)
        {

            CreateUser objreg = new CreateUser();
            objreg.Register(reg);

            return View();
        }
    }
}