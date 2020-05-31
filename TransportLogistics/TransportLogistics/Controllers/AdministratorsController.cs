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
using TransportLogistics.Model;
using TransportLogistics.ViewModels;
using TransportLogistics.ViewModels.Administrator;

namespace TransportLogistics.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdministratorsController : Controller
    {
       public AdministratorsController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager,EmployeeServices employeeServices,
           ILogger<AdministratorsController>logger,EditInfoRequestService requestService,DriverService driverService ) 
        {
            UserManager = userManager;
            RoleManager = roleManager;
            EmployeeServices = employeeServices;
            Logger = logger;
            EditInfoRequestService = requestService;
            DriverService = driverService;
        }

        private UserManager<IdentityUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        private EmployeeServices EmployeeServices;
        private ILogger<AdministratorsController> Logger;
        private EditInfoRequestService EditInfoRequestService;
        private DriverService DriverService;


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
        [HttpGet]
        public IActionResult EditInfoRequests()
        {
            try
            {
                var model = EditInfoRequestService.GetAllCreatedRequests();
                return View(model);
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to retrieve the requests for editing personal informations {@Exception}", e);
                Logger.LogError("Failed to retrieve the requests for editing personal informations {Exception}", e.Message);
                return BadRequest("Failed to retrieve the requests for editing personal informations");
            }
        }
        public IActionResult EditInfoRequestsPartial()
        {
            try
            {
                var model = EditInfoRequestService.GetAllCreatedRequests();
                return PartialView("_RequestsTable",model);
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to retrieve the requests for editing personal informations {@Exception}", e);
                Logger.LogError("Failed to retrieve the requests for editing personal informations {Exception}", e.Message);
                return BadRequest("Failed to retrieve the requests for editing personal informations");
            }
        }
        public IActionResult RejectEditInfoRequest(Guid requestId)
        {
            try
            {
                
                EditInfoRequestService.SetStatus(requestId,EditStatusRequest.Refused);
                return RedirectToAction("EditInfoRequestsPartial");
            }
            catch(Exception e)
            {
                Logger.LogDebug("Failed to reject reqest {@Exception}", e);
                Logger.LogError("Failed to reject request {Exception}", e.Message);
                return BadRequest("Failed to reject request");
            }
        }
        public IActionResult ApproveEditInfoRequest(Guid requestId)
        {
            try
            {
                var request = EditInfoRequestService.GetById(requestId);
                var driver = DriverService.GetById(request.Applicant);
                DriverService.UpdateDriver(driver , request.NewName , request.NewEmail);
                var user = UserManager.GetUserAsync(User).GetAwaiter().GetResult();
                EditInfoRequestService.ApproveRequest(request,user.Id);
                var driverAccount = UserManager.FindByEmailAsync(request.OldEmail).GetAwaiter().GetResult();
                driverAccount.UserName = request.NewName;
                driverAccount.Email = request.NewEmail;
                driverAccount.PhoneNumber = request.NewPhoneNumber;
                UserManager.UpdateAsync(driverAccount).GetAwaiter().GetResult();
                return RedirectToAction("EditInfoRequestsPartial");
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to approve request {@Exception}", e);
                Logger.LogError("Failed to approve request {Exception}", e.Message);
                return BadRequest("Failed to approve request ");
            }
        }


        }
    }