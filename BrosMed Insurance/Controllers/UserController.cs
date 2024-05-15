using Microsoft.AspNetCore.Mvc;
using BrosMed_Insurance.Models;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BrosMed_Insurance.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> UserRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(RegisterUser viewModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(viewModel.email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "Adres e-mail jest już zajęty.");
                    return View(viewModel);
                }
                var user = new User()
                {
                    UserName = viewModel.name,
                    name = viewModel.name,
                    surname = viewModel.surname,
                    Email = viewModel.email,
                    PhoneNumber = viewModel.PhoneNumber,
                    address = viewModel.address,
                    pesel = viewModel.pesel
                };
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(viewModel.password);

                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                   // await userManager.AddToRoleAsync(user, "Admin");
                   // await userManager.AddToRoleAsync(user, "Employee");
                    await userManager.AddToRoleAsync(user, "Member");
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginUser viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(viewModel.email);
                
                if (user != null && BCrypt.Net.BCrypt.Verify(viewModel.password, user.PasswordHash))
                {
                    await signInManager.SignInAsync(user, viewModel.rememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Zły login lub hasło.");
            }
            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //  //    
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var roles = await roleManager.Roles.ToListAsync();

            ViewBag.UsersWithRoles = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                ViewBag.UsersWithRoles[user.Id] = userRoles;
            }

            ViewBag.Roles = roles.Select(x => x.Name);

            return View(users);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUsers(string id)
        {
            var roles = await roleManager.Roles.ToListAsync();
            ViewBag.RoleList = roles.Select(x => x.Name);
            var users = await userManager.FindByIdAsync(id);
            var userRoles = await userManager.GetRolesAsync(users);
            ViewBag.CurrentUserRole = userRoles.FirstOrDefault();

            return View(users);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUsers(User viewModel, string role)
        {

            var users = await userManager.FindByIdAsync(viewModel.Id);
            if (users is not null)
            {
                users.name = viewModel.name;
                users.surname = viewModel.surname;
                users.Email = viewModel.Email;
                users.PhoneNumber = viewModel.PhoneNumber;

                await userManager.UpdateAsync(users);

                var userRoles = await userManager.GetRolesAsync(users);

                foreach (var userRole in userRoles)
                {
                    await userManager.RemoveFromRoleAsync(users, userRole);
                }

                var selectedRole = await roleManager.FindByNameAsync(role);

                if (selectedRole != null)
                {
                    await userManager.AddToRoleAsync(users, selectedRole.Name!);
                }

            }

            return RedirectToAction("ListUsers", "User");

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUsers(User viewModel)
        {
            var users = await userManager.FindByIdAsync(viewModel.Id);
            if (users != null)
            {
                var result = await userManager.DeleteAsync(users);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "User");
                }
                else
                {
                    //
                }
            }
            return NotFound();
        }
    }
}

