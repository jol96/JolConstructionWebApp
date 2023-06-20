using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;
using JolConstruction.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            // Set Category based on CategoryId
            foreach (var post in objPostList) 
            {
                var categoryid = post.CategoryId;
                Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == categoryid);
                post.Category = categoryFromDb;
            }

            return View(objPostList);
        }

        public IActionResult Create()
        {
            PostVM postVm = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Post = new Post()
            };
            return View(postVm);
        }

        [HttpPost]
        public IActionResult Create(PostVM postVm) 
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Post.Add(postVm.Post);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                postVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(postVm);
            }
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
