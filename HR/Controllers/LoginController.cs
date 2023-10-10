using Dao;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        
        public async Task<ActionResult> LoginIndex(users u)
        {
            usersDao udao=new usersDao();
            int dore = await udao.LoginAsync(u);
            return Content(dore.ToString());
        }
    }
}