using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mobin.CoreProject.AuthAdmin.Extensions;
using Mobin.CoreProject.Core.DTOs.Forest;
using Mobin.CoreProject.Core.SearchCriteria.Forest;
using Mobin.CoreProject.Core.ServiceContracts;
using Mobin.CoreProject.Core.SSOT;
using Mobin.CoreProject.Core.ViewModels.Forest;
using Mobin.CoreProject.CrossCutting.Notification.Services;
using Mobin.CoreProject.CrossCutting.Security.ActionFilters;
using Mobin.CoreProject.CrossCutting.Security.Helper;

namespace Mobin.CoreProject.AuthAdmin.Controllers
{
    public class ForestController : Controller
    {
        private readonly IForestService _forestService;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;

        public ForestController(IForestService forestService, 
            ISmsService smsService, 
            IEmailService emailService)
        {
            _forestService = forestService;
            _smsService = smsService;
            _emailService = emailService;
        }

        public IActionResult Index(ForestGetDataSC criteria, int page = 1, int size = 10)
        {
            var model = _forestService.GetData(criteria, page, size);
            ViewBag.Q = criteria.Q;

            return View(model);
        }

        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToArray();
            return Json(claims);
        }

        public IActionResult GetEmployeeId()
        {
            var employeeId = User.GetClaim(Claims.EmployeeId.ToString());
            return Json(employeeId);
        }

        public async Task<IActionResult> SendSms()
        {
            var result = await _smsService.SendAsync(
                "09121063717",
                "http://localhost:5407/forest/sendsms",
                "50004001575",
                "Test");

            return Json(result);
        }

        public async Task<IActionResult> SendEmail()
        {
            var result = await _emailService.SendAsync(
                "test Client.SDK",
                "m.dashtinejad@mobinnet.net",
                "http://localhost:5407/forest/sendEmail");

            return Json(result);
        }


        #region Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ForestCreateVM vm)
        {
            var result = _forestService.Create(vm);
            TempData.AddResult(result);
            return RedirectToAction(nameof(Edit), new { id = result.Data });
        }
        #endregion



        #region Edit
        public IActionResult Edit(int id)
        {
            var model = _forestService.Get<ForestEditDTO>(id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ForestEditVM vm, bool isAjax = false)
        {
            var result = _forestService.Update(id, vm);
            
            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Edit), new { id });
        }
        #endregion


        #region Delete
        public IActionResult Delete(int id, bool isAjax = false)
        {
            var result = _forestService.Delete(id);

            if (isAjax) return Json(result);

            TempData.AddResult(result);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        //[HasClaim(Claims.HotelId,"hotelId")]
        [HasClaim(Claims.HotelId)]
        public IActionResult HasClaim(int id /*int hotelId*/)
        {
            var list = User.Claims
                .Select(s => new
                {
                    s.Type,
                    s.Value
                })
                .ToList();
                

            return Json(list);

        }

        [HasPermission(Permissions.Dashboard)]
        public IActionResult HasPermission()
        {
            var list = User.Claims
                .Select(s => new
                {
                    s.Type,
                    s.Value
                })
                .ToList();
                

            return Json(list);
        }
    }


}
