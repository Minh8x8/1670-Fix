using FPTBook.DataAccess.Repository.IRepository;
using FPTBook.Models;
using FPTBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll(c => c.Status == "Pending").ToList();
            return View(objCategoryList);
        }

        public IActionResult Approval(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Approval(Category obj)
        {
            if (ModelState.IsValid)
            {
                obj.Status = "Approval";
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category request successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}