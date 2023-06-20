using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace JolConstructionWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Post> objPostList = _unitOfWork.Post.GetAll().ToList();
            return View(objPostList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post obj) 
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Post.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Post? postFromDb = _unitOfWork.Post.Get(u => u.Id == id);

            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(postFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Post obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Post.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Post updated successfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Post? postFromDb = _unitOfWork.Post.Get(u => u.Id == id);

            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(postFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Post? obj = _unitOfWork.Post.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Post.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Post deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
