using LibraryManagementSystem.DataAccess.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }
            _memberService.AddMember(member);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _memberService.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }
            _memberService.UpdateMember(member);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var member = _memberService.GetMember(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _memberService.DeleteMember(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
