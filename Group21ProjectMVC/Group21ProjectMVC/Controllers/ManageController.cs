using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Group21ProjectMVC.Models;
using Group21ProjectMVC.Models.ManageViewModels;
using Group21ProjectMVC.Services;
using Group21ProjectMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group21ProjectMVC.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFlightStore<ApplicationFlight> _flightStore;
        private readonly ITicketStore<ApplicationTicket> _ticketStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";


        private readonly CancellationTokenSource source = new();

        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IFlightStore<ApplicationFlight> flightStore,
          ITicketStore<ApplicationTicket> ticketStore,
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _flightStore = flightStore;
            _ticketStore = ticketStore;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerificationEmail(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            var email = user.Email;
            await _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        [HttpGet]
        public async Task<IActionResult> SetPassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                AddErrors(addPasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            StatusMessage = "Your password has been set.";

            return RedirectToAction(nameof(SetPassword));
        }

        [HttpGet]
        public async Task<IActionResult> AddFlights()
        {
            List<string> times = new() { "6:00 AM", "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM", "9:00 PM", "10:00 PM" };
            List<string> airports = new() { "ATL", "DFW", "DEN", "ORD", "LAX", "CLT", "LAS", "PHX", "MCO", "MHK" };
            List<SelectListItem> Times = new();
            List<SelectListItem> Airports = new();
            for (int i = 0; i < times.Count; i++)
            {
                Times.Add(new SelectListItem { Text = times[i], Value = i.ToString() });
            }
            for (int j = 0; j < airports.Count; j++)
            {
                Airports.Add(new SelectListItem { Text = airports[j], Value = j.ToString() });
            }
            var airlines = await _flightStore.GetAirlinesAsync(source.Token);
            return View(new AddFlightsViewModel { Times = Times, Airports = Airports, StatusMessage = StatusMessage, Airlines = airlines });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFlights(AddFlightsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            List<string> stringTimes = new() { "6:00 AM", "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM", "9:00 PM", "10:00 PM" };
            List<string> Airports = new() { "ATL", "DFW", "DEN", "ORD", "LAX", "CLT", "LAS", "PHX", "MCO", "MHK" };
            List<DateTime> selectedTimes = new();
            List<string> selectedAirports = new();
            foreach (var x in model.TimesIdSelected)
            {
                selectedTimes.Add(Convert.ToDateTime(stringTimes[x]));
            }
            foreach (var y in model.AirportsIdSelected)
            {
                selectedAirports.Add(Airports[y]);
            }
            model.TimesSelected = selectedTimes;
            model.AirportsSelected = selectedAirports;
            var success = await _flightStore.AddFlightsAsync(GenFlights(model), source.Token);
            if (!success)
            {
                throw new ApplicationException($"Unable to add flights.");
            }
            StatusMessage = "Flights have been added.";
            return RedirectToAction(nameof(AddFlights)); ;
        }

        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View(new DeleteUserViewModel { StatusMessage = StatusMessage });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(DeleteUserViewModel dvm)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred removing roles from user.");
            }
            await _ticketStore.DeleteAllUserTicketsAsync(user.Id, source.Token);
            result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #region Helpers
        private static readonly Random random = new();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private IEnumerable<ApplicationFlight> GenFlights(AddFlightsViewModel model)
        {
            IList<ApplicationFlight> Flights = new List<ApplicationFlight>();
            List<string> Airports = model.AirportsSelected.ToList();
            //Flight time between 2 airports 
            Dictionary<string, double> FlightTimes = new()
            {
                { "ATLToDFW", 1.30725572451624 },
                { "ATLToDEN", 2.14277369488298 },
                { "ATLToORD", 1.08555689404232 },
                { "ATLToLAX", 3.47703881172972 },
                { "ATLToCLT", 0.404457030679804 },
                { "ATLToLAS", 3.1200264269319 },
                { "ATLToPHX", 2.83505944218935 },
                { "ATLToMCO", 0.723430371758545 },
                { "ATLToMHK", 1.3946579707286 },
                { "DFWToATL", 1.30725572451624 },
                { "DFWToDEN", 1.14648906852173 },
                { "DFWToORD", 1.43537463591358 },
                { "DFWToLAX", 2.20537374697025 },
                { "DFWToCLT", 1.67189997986523 },
                { "DFWToLAS", 1.8847514660962 },
                { "DFWToPHX", 1.54937003452177 },
                { "DFWToMCO", 1.75997783848851 },
                { "DFWToMHK", 0.772957335795331 },
                { "DENToATL", 2.14277369488298 },
                { "DENToDFW", 1.14648906852173 },
                { "DENToORD", 1.58631543509905 },
                { "DENToLAX", 1.53983461791215 },
                { "DENToCLT", 2.38886976857147 },
                { "DENToLAS", 1.1216902167879 },
                { "DENToPHX", 1.07537771608774 },
                { "DENToMCO", 2.7643288235683 },
                { "DENToMHK", 0.768316122264706 },
                { "ORDToATL", 1.08555689404232 },
                { "ORDToDFW", 1.43537463591358 },
                { "ORDToDEN", 1.58631543509905 },
                { "ORDToLAX", 3.11632645413679 },
                { "ORDToCLT", 1.07268950225617 },
                { "ORDToLAS", 2.70410349949855 },
                { "ORDToPHX", 2.57213717738108 },
                { "ORDToMCO", 1.80138179698141 },
                { "ORDToMHK", 0.89468348426939 },
                { "LAXToATL", 3.47703881172972 },
                { "LAXToDFW", 2.20537374697025 },
                { "LAXToDEN", 1.53983461791215 },
                { "LAXToORD", 3.11632645413679 },
                { "LAXToCLT", 3.79518071339672 },
                { "LAXToLAS", 0.423011407621394 },
                { "LAXToPHX", 0.661567966144413 },
                { "LAXToMCO", 3.96172336722519 },
                { "LAXToMHK", 2.2469664778604 },
                { "CLTToATL", 0.404457030679804 },
                { "CLTToDFW", 1.67189997986523 },
                { "CLTToDEN", 2.38886976857147 },
                { "CLTToORD", 1.07268950225617 },
                { "CLTToLAX", 3.79518071339672 },
                { "CLTToLAS", 3.42147670118808 },
                { "CLTToPHX", 3.16710251920464 },
                { "CLTToMCO", 0.83951540869202 },
                { "CLTToMHK", 1.62150185160082 },
                { "LASToATL", 3.1200264269319 },
                { "LASToDFW", 1.8847514660962 },
                { "LASToDEN", 1.1216902167879 },
                { "LASToORD", 2.70410349949855 },
                { "LASToLAX", 0.423011407621394 },
                { "LASToCLT", 3.42147670118808 },
                { "LASToPHX", 0.457579020373236 },
                { "LASToMCO", 3.64345441439641 },
                { "LASToMHK", 1.84593454014099 },
                { "PHXToATL", 2.83505944218935 },
                { "PHXToDFW", 1.54937003452177 },
                { "PHXToDEN", 1.07537771608774 },
                { "PHXToORD", 2.57213717738108 },
                { "PHXToLAX", 0.661567966144413 },
                { "PHXToCLT", 3.16710251920464 },
                { "PHXToLAS", 0.457579020373236 },
                { "PHXToMCO", 3.3020571975155 },
                { "PHXToMHK", 1.68049203295766 },
                { "MCOToATL", 0.723430371758545 },
                { "MCOToDFW", 1.75997783848851 },
                { "MCOToDEN", 2.7643288235683 },
                { "MCOToORD", 1.80138179698141 },
                { "MCOToLAX", 3.96172336722519 },
                { "MCOToCLT", 0.83951540869202 },
                { "MCOToLAS", 3.64345441439641 },
                { "MCOToPHX", 3.3020571975155 },
                { "MCOToMHK", 2.05550742495893 },
                { "MHKToATL", 1.3946579707286 },
                { "MHKToDFW", 0.772957335795331 },
                { "MHKToDEN", 0.768316122264706 },
                { "MHKToORD", 0.89468348426939 },
                { "MHKToLAX", 2.2469664778604 },
                { "MHKToCLT", 1.62150185160082 },
                { "MHKToLAS", 1.84593454014099 },
                { "MHKToPHX", 1.68049203295766 },
                { "MHKToMCO", 2.05550742495893 }
            };
            Random r = new();
            foreach (DateTime day in EachDay(model.StartingDate, model.EndingDate))
            {
                int rInt = r.Next(100, 999);
                foreach (string airport in model.AirportsSelected)
                {
                    for (int i = 0; i < Airports.Count; i++)
                    {
                        if (Airports[i] != airport)
                        {
                            foreach (var time in model.TimesSelected)
                            {
                                r.Next(100, 999);
                                FlightTimes.TryGetValue(airport + "To" + Airports[i], out double flightTime);
                                decimal price = (decimal)(200 * flightTime);
                                if (flightTime < 1) price += (decimal)(300 - (100 * flightTime));
                                if (flightTime > 1 && flightTime < 2) price += (decimal)(200 - (100 * (flightTime - 1)));
                                if (flightTime > 2 && flightTime < 3) price += (decimal)(100 * (flightTime - 2));
                                Flights.Add(new ApplicationFlight
                                {
                                    FlightNumber = RandomString(3) + rInt,
                                    DepartingAirportCode = airport,
                                    DestinationAirportCode = Airports[i],
                                    Airline = model.Airline,
                                    DepartureDate = day,
                                    DepartureTime = time,
                                    ArrivalTime = time.AddHours(flightTime),
                                    Capacity = 126,
                                    SeatsTaken = 0,
                                    Price = price/2
                                });
                            }
                        }
                    }
                }
            }
            return Flights;
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        #endregion
    }
}
