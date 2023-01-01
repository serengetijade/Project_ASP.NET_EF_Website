using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Blog.Models
{
    public class CommentModerator: ApplicationUser 
    {
        public int BanAppealsResolved { get; set; }
        public List<ApplicationUser> BannedUsers { get; set; }

        public static void Seed(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!roleManager.RoleExists("CommentModerator"))
            {
                //Create a new role (if there is none already) using built-in RoleManager class:
                var role = new IdentityRole();
                role.Name = "CommentModerator";
                roleManager.Create(role);

                //Create a default user:
                var user = new CommentModerator()
                {
                    UserName = "Admin",
                    Email = "Test@gmail.com",
                    BanAppealsResolved = 0
                };
                string password = "Admin123!";
                userManager.Create(user, password);
            
                //Add the default user to the CommentModerator role
                userManager.AddToRole(user.Id, "CommentModerator");
            }
        }

    }
    
    //Overload the built in HandleUnauthorizedRequest method of the AuthorizeAttribute class
    public class ModAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //Parameters: AuthorizationContext- Encapsulates the information for using AuthorizeAttribute.
        //filterContext- The filterContext object contains the controller, HTTP context, request context, action result, and route data.
        {
            base.HandleUnauthorizedRequest(filterContext);  //Apply this method to access fields/constructors/methods of the base class. It is not necessary here, but good to include in case the base class is overwritten or modified going forward. 
            if (filterContext.Result is HttpUnauthorizedResult)
                {
                filterContext.Result = new RedirectResult("~/Blog/Comments/AccessDenied");  //Must specify the full file path because the method is in a different controller.
                //Another option that will return the correct view: 
                //filterContext.Result = new ViewResult { ViewName = "AccessDenied" };
            }
        }
        //NOTES:
        //This will also work if you overload the OnAuthorization method, but that will require the access modifier to be 'public'. 
        //Both of the below if statements return the correct view when NOT logged in, but wrong view when logged in as another unauthorized user.
        //if(!filterContext.HttpContext.Request.IsAuthenticated)        
        //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)  
    }
}                                          