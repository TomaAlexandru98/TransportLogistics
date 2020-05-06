using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TransportLogistics.Controllers
{
    
    public class AdministratorController : Controller
    {
       public AdministratorController(UserManager<IdentityUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<IdentityUser> UserManager { get; }

        public IActionResult Index()
        {
            var users = UserManager.Users;
            return View(users);
        }
    }
}