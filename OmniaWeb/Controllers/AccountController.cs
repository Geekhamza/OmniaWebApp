using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MyService.Services;
using OmniaWeb.Extra;
using OmniaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace OmniaWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "admin")]
//        [AllowAnonymous]
            public ActionResult Register()
        {
            List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>();
            roles.Add(new KeyValuePair<string, string>("user", "user"));
          
            roles.Add(new KeyValuePair<string, string>("DG", "DG"));
            roles.Add(new KeyValuePair<string, string>("ProjectManger", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("Client", "user"));
            roles.Add(new KeyValuePair<string, string>("Supervisor", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("Subco", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("admin", "admin"));

            ViewBag.roles = roles;
            return View();
        }
        //
        // GET: /Account/EditUser
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(string userId)
        {
            List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>();
            roles.Add(new KeyValuePair<string, string>("user", "user"));
         
            roles.Add(new KeyValuePair<string, string>("DG", "DG"));
            roles.Add(new KeyValuePair<string, string>("ProjectManger", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("Client", "user"));
            roles.Add(new KeyValuePair<string, string>("Supervisor", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("Subco", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("admin", "admin"));
            
            ViewBag.roles = roles;
            var u = UserManager.FindById(userId);
            ViewBag.id = userId;
            EditUserViewModel model = new EditUserViewModel()
            {
                Email = u.UserName
            
            };
            if (u.Roles.FirstOrDefault() != null)
                model.Role = u.Roles.FirstOrDefault().ToString();
            return View(model);
        }
        //
        // Post: /Account/EditUser
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditUser(string userId, EditUserViewModel model)
        {

           
            try
            {
                var u = UserManager.FindById(userId);
                u.UserName = model.Email;
                UserManager.ChangePassword(userId, model.Password, model.NewPassword);
                UserManager.AddToRole(userId, model.Role);
                return RedirectToAction("UsersAdmin");

            }
            catch(Exception e)
            {
                List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>();
                roles.Add(new KeyValuePair<string, string>("user", "user"));
               
                roles.Add(new KeyValuePair<string, string>("DG", "DG"));
                roles.Add(new KeyValuePair<string, string>("ProjectManger", "ProjectManger"));
                roles.Add(new KeyValuePair<string, string>("Client", "user"));
                roles.Add(new KeyValuePair<string, string>("Supervisor", "ProjectManger"));
                roles.Add(new KeyValuePair<string, string>("Subco", "ProjectManger"));
                roles.Add(new KeyValuePair<string, string>("admin", "admin"));

                ViewBag.roles = roles;
                ViewBag.error = "An Error Has Occured, Make sure Old Password is Correct";
                ViewBag.id = userId;
                
                return View(model);
            }
            

        }
        //
        // GET /Account/AuthoriseUser
        [Authorize(Roles="admin")]
        public ActionResult AuthoriseUser()
        {
            var context= new ApplicationDbContext();
            ViewBag.users = context.Users.ToList();
            var projectS = new ProjectService();
            var AllProject = projectS.GetAll().ToList();
            ViewBag.projects = AllProject;
            return View();
        }
        // POST /Account/AuthoriseUser
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AuthoriseUser(AuthoriseUserModel model)
        {
            var context = new ApplicationDbContext();
            ViewBag.users = context.Users.ToList();
            var projectS = new ProjectService();
            var AllProject = projectS.GetAll().ToList();
            ViewBag.projects = AllProject;

            try
            {
                var list = new List<string>();
                list.Add(model.UserId);
                ProjectUsers.AddUsers(list, Convert.ToInt64(model.ProjectId));
                ViewBag.success = "User Authorised Succesfully , Want to Authorise Another ? ";
                return View();
            }
            catch
            {
                ViewBag.error = "Error While Authorising User";
                return View(model);
            }
        }

         // GET /Account/Delete/id
        [Authorize(Roles = "admin")]
        public  async Task<ActionResult> Delete(string id)
        {

          
            //ApplicationDbContext context = new ApplicationDbContext();
            //RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            //UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            //UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);
            //RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            var user =  await UserManager.FindByIdAsync(id);
            var logins = user.Logins;
            //var rolesForUser =  await UserManager.GetRolesAsync(id);

            //using (var transaction = context.Database.BeginTransaction())
            //{
            //    foreach (var login in logins.ToList())
            //    {
            //          await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            //    }

            //    if (rolesForUser.Count() > 0)
            //    {
            //        foreach (var item in rolesForUser.ToList())
            //        {
            //            // item should be the name of the role
            //            var result =  await UserManager.RemoveFromRoleAsync(user.Id, item);
            //        }
            //    }
                //System.Web.Security.Membership.DeleteUser(user.UserName);
               await UserManager.DeleteAsync(user);
               // transaction.Commit();
           // }

            return RedirectToAction("UsersAdmin");
            }
          
        




        ////
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "admin")]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            List<KeyValuePair<string, string>> roles = new List<KeyValuePair<string, string>>();
            roles.Add(new KeyValuePair<string, string>("user", "user"));
       
            roles.Add(new KeyValuePair<string, string>("DG", "DG"));
            roles.Add(new KeyValuePair<string, string>("ProjectManger", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("Client", "user"));
            roles.Add(new KeyValuePair<string, string>("Supervisor", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("Subco", "ProjectManger"));
            roles.Add(new KeyValuePair<string, string>("admin", "admin"));


            if (model.Role!=null&& ModelState.IsValid)
            {
                if (UserManager.FindByName(model.Email)!=null)
                {
                    ViewBag.error = "Email Already Used ! ";
                    // If we got this far, something failed, redisplay form
                    ViewBag.roles = roles;
                    return View(model);


                }
                var user = new ApplicationUser() { UserName = model.Email};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInAsync(user, isPersistent: false);
                    var context = new ApplicationDbContext();
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);

               
                    //if Role not found , add it 
                    if (!roleManager.RoleExists(model.Role))
                    {
                        roleManager.Create( new IdentityRole() { Name=model.Role});
                    }
                    UserManager.AddToRole(user.Id, model.Role);
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ViewBag.error = " An Error Has Occured, Please make sure Passwords Match and the Email Format is not Valid ";
                    // If we got this far, something failed, redisplay form
                    ViewBag.roles = roles;

                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.error = " An Error Has Occured, Please make sure Passwords Match and the Email Format is Valid ";
           
            ViewBag.roles = roles;

            return View(model);
        }

        // currenty only admin is authorised , later add Any Role to give it permission 
        [Authorize(Roles = "admin")]
        public ActionResult UsersAdmin()
        {

            var context = new ApplicationDbContext();
            var allUsers = context.Users.ToList();
            var models = new List<UserDetailModel>();
            foreach (var u in allUsers)
            {
                UserDetailModel model = new UserDetailModel();
                model.Email = u.UserName;
                model.Id = u.Id;
                model.Role = "Not Set";
                var role = UserManager.GetRoles(u.Id).LastOrDefault();
                if (role != null)
                {
                    model.Role = role.ToString();
                }
                models.Add(model);
            }
            return View(models);
        }

        
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

      
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

       

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}