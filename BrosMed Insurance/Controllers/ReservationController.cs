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

        public ReservationController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ReservationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> AddReservation()
        {
            ViewBag.GodzinkiList = await _context.Godziny.ToListAsync();
            ViewBag.UslugiList = await _context.Usluga.ToListAsync();


            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddReservation(ReservationViewModel viewModel)
        {
            Console.WriteLine($"Data: {viewModel.Data}");
            Console.WriteLine($"SelectedGodzinaId: {viewModel.SelectedGodzinaId}");
            Console.WriteLine($"SelectedUslugaId: {viewModel.SelectedUslugaId}");



            var godziny = await _context.Godziny.ToListAsync();
       
            var uslugi = await _context.Usluga.ToListAsync();

            

            if (ModelState.IsValid)
            {

                // Tworzenie nowego obiektu Terminy na podstawie danych przesłanych z formularza
                var terminy = new Terminy
                {
                    Data = viewModel.Data,
                    GodzinaId = viewModel.SelectedGodzinaId,
                    UslugaId = viewModel.SelectedUslugaId,

                };

                // Dodanie nowego obiektu Terminy do kontekstu bazy danych
                _context.Terminy.Add(terminy);
                await _context.SaveChangesAsync();

                // Przekierowanie użytkownika np. do strony potwierdzenia rezerwacji
                return RedirectToAction("Confirmed");
            }
            else
            {
                Console.WriteLine("ModelState is not valid:");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Console.WriteLine($"Field: {key}");
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"- Error: {error.ErrorMessage}");
                        }
                    }
                }

                // Jeśli model nie jest poprawny, zwracamy widok z błędami walidacji
                return View("AddReservation", viewModel);
            }

        }
    }
}
