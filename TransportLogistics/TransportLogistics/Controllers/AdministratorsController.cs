using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TransportLogistics.ApplicationLogic.Services;
using TransportLogistics.Data;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.ViewModels;
using TransportLogistics.ViewModels.Administrator;

namespace TransportLogistics.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdministratorsController : Controller
    {
       public AdministratorsController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager,EmployeeServices employeeServices,
           ILogger<AdministratorsController>logger ) 
        {
            UserManager = userManager;
            RoleManager = roleManager;
            EmployeeServices = employeeServices;
            Logger = logger;
        }

        private UserManager<IdentityUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private EmployeeServices EmployeeServices;
        private ILogger<AdministratorsController> Logger;

        public IActionResult Index()
        {
            try
            {
                var users = UserManager.Users;
                Logger.LogInformation("Users were retrieved successfully");
                return View(users);
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to retrieve users accounts {@Exception}", e);
                Logger.LogError("Failed to retrieve users accounts {Exception}", e.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAccount([FromForm]UserAccountCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                await UserManager.CreateAsync(user, model.Password);
                var createdUser = await UserManager.FindByEmailAsync(model.Email);
                EmployeeServices.AddEmployee(createdUser.Id , model.Name , model.Email , model.Role);
                if (await RoleManager.FindByNameAsync(model.Role) == null)
                {
                    var role = new IdentityRole(model.Role);
                    await RoleManager.CreateAsync(role);
                    await UserManager.AddToRoleAsync(user, role.Name);
                }
                else
                {
                    await UserManager.AddToRoleAsync(user, model.Role);
                }
           
        }
            var users = UserManager.Users;
            return PartialView("_TablePartial", users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUserAccount(string userId)
        {
            var user =await UserManager.FindByIdAsync(userId.ToString());
            UserAccontEditViewModel model = new UserAccontEditViewModel()
            {
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserId = userId.ToString()
            };
            return PartialView("_EditUserAccount",model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserAccount([FromForm] UserAccontEditViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var userSaved = await UserManager.FindByIdAsync(model.UserId);
                var formerRole = await UserManager.GetRolesAsync(userSaved);
                userSaved.Email = model.Email;
                userSaved.UserName = model.Name;
                userSaved.PhoneNumber = model.PhoneNumber;
                await UserManager.UpdateAsync(userSaved);
                var roles = await UserManager.GetRolesAsync(userSaved);
                EmployeeServices.UpdateEmployee(model.Name , model.Email , formerRole[0],model.UserId );
                if (await UserManager.IsInRoleAsync(userSaved, model.Role) == false)
                {
                    await UserManager.RemoveFromRoleAsync(userSaved,roles[0]);
                    await UserManager.AddToRoleAsync(userSaved, model.Role);
                }
            }
            else
            {
                
                return View();
               
            }
            
            return PartialView("_EditUserAccount",model);
           }
        public IActionResult GetUsersPartialView()
        {
            var users = UserManager.Users;
            return PartialView("_TablePartial", users);
        }
        public async Task<IActionResult> DeleteUserAccount(UserDeleteViewModel model)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(model.UserId);
                await UserManager.DeleteAsync(user);
               EmployeeServices.DeleteEmployee(model.UserId);
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to delete user account,most likely model was not correct {@Exception}", e);
                Logger.LogError("Failed to delete user account,most likely model was not correct {Exception}", e.Message);
                return BadRequest();
            }
            var users = UserManager.Users;
            return PartialView("_TablePartial", users);
        }
     
        }
    }