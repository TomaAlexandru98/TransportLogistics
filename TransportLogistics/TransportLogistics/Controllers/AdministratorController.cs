using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportLogistics.Data;
using TransportLogistics.ViewModels;

namespace TransportLogistics.Controllers
{
    
    public class AdministratorController : Controller
    {
       public AdministratorController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole>roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

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
            return RedirectToAction("Index", "Administrator");
            
        }
        
        public async Task<IActionResult> DeleteUserAccount(string UserId)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(UserId);
                await UserManager.DeleteAsync(user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}