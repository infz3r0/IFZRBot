using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IFZRBot.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;

namespace IFZRBot.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: AdminUserManager
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

        public ActionResult ChangeRole(string uid, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (string.IsNullOrEmpty(uid))
            {
                return RedirectToAction("Index");
            }

            ApplicationUser user = db.Users.Find(uid);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            List<SelectListItem> items = new List<SelectListItem>();
            List<IdentityRole> roles = db.Roles.ToList();
            foreach (IdentityRole role in roles)
            {
                items.Add(new SelectListItem { Text = role.Name.ToString(), Value = role.Id.ToString() });
            }

            ViewBag.listrole = items;

            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRole(ApplicationUser model, string listrole, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangeRole", new { uid = model.Id });
            }

            ApplicationUser user = db.Users.Find(model.Id);
            IdentityRole r = db.Roles.Find(listrole);

            if (user == null || r == null)
            {
                return RedirectToAction("ChangeRole", new { uid = model.Id });
            }

            UserManager.RemoveFromRole(user.Id, "User");
            UserManager.RemoveFromRole(user.Id, "Admin");
            UserManager.AddToRole(user.Id, r.Name);


            ViewBag.Message = "Change role success";
            return RedirectToLocal(returnUrl);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection form, string returnUrl)
        {
            string uid = form["id"];

            ApplicationUser user = db.Users.Find(uid);
            if (user != null)
            {
                UserManager.RemoveFromRole(user.Id, "User");
                UserManager.RemoveFromRole(user.Id, "Admin");
                try
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
                ViewBag.Message = "User has been deleted.";
            }
            else
            {
                ViewBag.Message = "Error";
            }
            ViewBag.IsMessage = true;


            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //end
    }
}