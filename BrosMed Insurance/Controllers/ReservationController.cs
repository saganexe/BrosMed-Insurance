    using BrosMed_Insurance.Data;
    using BrosMed_Insurance.Models;
    using BrosMed_Insurance.Models.Reservation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection.Metadata.Ecma335;

    namespace BrosMed_Insurance.Controllers
    {
        public class ReservationController : Controller
        {
            private readonly ReservationDbContext _context;
            private readonly SignInManager<User> signInManager;
            private readonly UserManager<User> userManager;
            private readonly RoleManager<IdentityRole> roleManager;

            public ReservationController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,ReservationDbContext context)
            {
                _context = context;
            }        
            [HttpGet]
            public async Task<IActionResult> AddReservation(Terminy terminyVM)
            {
                var godziny = await _context.Godziny.ToListAsync();

            var viewModel = new Godzina
            {
                Godzinki = godziny,
                NewGodzina = new Godzina()

            };
                
                return View(viewModel);
             }
        [HttpPost]
        public async Task<IActionResult> AddReservation()
        {

            return View();
        }

    }
    }
