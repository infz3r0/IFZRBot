using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IFZRBot.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace IFZRBot.Controllers
{
    public class AdminUserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminUserManager
        [Authorize(Roles ="Admin")]
        public ActionResult Index(int? page)
        {
            List<ApplicationUser> users = db.Users.ToList();

            int pageSize = 10;
            int pageNum = page ?? 1;
            return View(users.ToPagedList(pageNum, pageSize));
        }

        [ChildActionOnly]
        public ActionResult _GetRoles(string uid)
        {
            List<string> uroles = new List<string>();
            List<IdentityUserRole> user_role = db.Users.Find(uid).Roles.ToList();
            foreach (IdentityUserRole ur in user_role)
            {
                IdentityRole role = db.Roles.Find(ur.RoleId);
                uroles.Add(role.Name);
            }

            
            return PartialView(uroles);
        }




        //end
    }
}