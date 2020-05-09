using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportLogistics.Data;
using TransportLogistics.DataAccess.Abstractions;
using TransportLogistics.ViewModels;
using TransportLogistics.ViewModels.Administrator;

namespace TransportLogistics.Controllers
{
    [Authorize(Roles="Administrator")]
    public class AdministratorsController : Controller
    {
       public AdministratorsController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager,IEmployeeRepository employeeRepository) 
        {
            UserManager = userManager;
            RoleManager = roleManager;
            EmployeeRepository = employeeRepository;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public IEmployeeRepository EmployeeRepository { get; }

        public IActionResult Index()
        
        
        {
            try
            {
                var users = UserManager.Users;
                return View(users);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> CreateUserAccount(UserAccountCreateViewModel model)
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
               // var createdUser = await UserManager.FindByEmailAsync(model.Email);
                //EmployeeRepository.AddEmployee(createdUser.Id , model.Name , model.Email , model.Role);
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
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditUserAccount([FromForm] UserAccontEditViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var userSaved = await UserManager.FindByIdAsync(model.UserId);
                userSaved.Email = model.Email;
                userSaved.UserName = model.Name;
                userSaved.PhoneNumber = model.PhoneNumber;
                await UserManager.UpdateAsync(userSaved);
                var roles = await UserManager.GetRolesAsync(userSaved);
                
                if (await UserManager.IsInRoleAsync(userSaved, model.Role) == false)
                {
                    await UserManager.RemoveFromRoleAsync(userSaved,roles[0]);
                    await UserManager.AddToRoleAsync(userSaved, model.Role);
                }
            }
            else
            {
                
                return View();
                //create new view to inform administrator he could not create the user
            }
            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> DeleteUserAccount(UserDeleteViewModel model)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(model.UserId);
                await UserManager.DeleteAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
        //    public async Task<IActionResult> DeleteUserAccount(string Id)
        //    {
        //        try
        //        {
        //            var user = await UserManager.FindByIdAsync(Id);
        //            await UserManager.DeleteAsync(user);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest();
        //        }
        //        return PartialView("_TablePartial");
        //    }
        }
    }