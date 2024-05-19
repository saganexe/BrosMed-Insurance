    using BrosMed_Insurance.Data;
using BrosMed_Insurance.Models;
    using BrosMed_Insurance.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace BrosMed_Insurance.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ReservationDbContext context)
        {
            _context = context;
            _userManager = userManager;
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

            var godziny = await _context.Godziny.ToListAsync();

            var uslugi = await _context.Usluga.ToListAsync();



            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var terminy = new Terminy
                    {
                        Data = viewModel.Data,
                        GodzinaId = viewModel.SelectedGodzinaId,
                        UslugaId = viewModel.SelectedUslugaId,

                    };

                    _context.Terminy.Add(terminy);
                    await _context.SaveChangesAsync();

                    var userVisitHistory = new UserVisitHistory
                    {
                        UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                        TerminyId = terminy.TerminyId
                    };

                    _context.UserVisitHistory.Add(userVisitHistory);
                    await _context.SaveChangesAsync();

                    var finalizacja = new Finalizacja
                    {
                        UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                        TerminyId = terminy.TerminyId
                    };

                    _context.Finalizacja.Add(finalizacja);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Confirmed");
                }
            }

            return View("AddReservation", viewModel);
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> CheckVisits()
        {
            ViewBag.UserManager = _userManager;
            //ViewBag.AllVisits = await _context.Finalizacja.ToListAsync();
            var allVisits = await _context.Finalizacja
                                  .Include(f => f.Terminy)
                                      .ThenInclude(t => t.Godzina)
                                  .Include(f => f.Terminy)
                                      .ThenInclude(t => t.Usluga)
                                  .ToListAsync();
            allVisits = allVisits
                                  .OrderBy(visit => visit.Terminy?.Data ?? DateOnly.MinValue)
                                        .ThenBy(visit => visit.Terminy?.Godzina?.GodzinaId ?? int.MinValue)
                                  .ToList();
            ViewBag.AllVisits = allVisits;
            ViewBag.Hours = await _context.Finalizacja
                                    .Include(f => f.Terminy)
                                        .ThenInclude(t => t.Godzina)
                                    .ToListAsync();
            ViewBag.Serrvices = await _context.Finalizacja
                                    .Include(f => f.Terminy)
                                        .ThenInclude(t => t.Usluga)
                                    .ToListAsync();
            return View();
        }
        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> MoreInfo(string userId)
        {
            var allVisits = await _context.UserVisitHistory
                                  .Include(f => f.Terminy)
                                      .ThenInclude(t => t.Godzina)
                                  .Include(f => f.Terminy)
                                      .ThenInclude(t => t.Usluga)
                                  .Where(f => f.UserId == userId)
                                  .ToListAsync();

            allVisits = allVisits
                                  .OrderBy(visit => visit.Terminy?.Data ?? DateOnly.MinValue)
                                        .ThenBy(visit => visit.Terminy?.Godzina?.GodzinaId ?? int.MinValue)
                                  .ToList();
            ViewBag.AllVisits = allVisits;


            var user = await _userManager.FindByIdAsync(userId);
            return View(user);
        }    
    }
    }

