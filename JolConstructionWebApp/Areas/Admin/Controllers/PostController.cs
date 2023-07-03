using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;
using JolConstruction.Models.ViewModels;
using JolConstruction.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net.Http.Headers;

namespace JolConstructionWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Post> objPostList = _unitOfWork.Post.GetAll(includeProperties: "Category").ToList();

            // Set Category based on CategoryId
            //foreach (var post in objPostList) 
            //{
            //    var categoryid = post.CategoryId;
            //    Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == categoryid);
            //    post.Category = categoryFromDb;
            //}

            return View(objPostList);
        }


        public IActionResult Upsert(int? id)
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
            if (id == null || id == 0)
            {
                //create
                return View(postVm);
            }
            else
            {
                //update
                postVm.Post = _unitOfWork.Post.Get(u => u.Id == id, includeProperties:"PostImages");
                return View(postVm);
            }

        }

        

        [HttpPost]
        public IActionResult Upsert(PostVM postVm, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (postVm.Post.Id == 0)
                {
                    _unitOfWork.Post.Add(postVm.Post);
                }
                else
                {
                    _unitOfWork.Post.Update(postVm.Post);
                }

                _unitOfWork.Save();


                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {

                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string postPath = @"images\posts\post-" + postVm.Post.Id;
                        string finalPath = Path.Combine(wwwRootPath, postPath);

                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        PostImage postImage = new()
                        {
                            ImageUrl = @"\" + postPath + @"\" + fileName,
                            PostId = postVm.Post.Id,
                        };

                        if (postVm.Post.PostImages == null)
                            postVm.Post.PostImages = new List<PostImage>();

                        postVm.Post.PostImages.Add(postImage);

                    }

                    _unitOfWork.Post.Update(postVm.Post);
                    _unitOfWork.Save();
                }

                TempData["success"] = "Post created/updated successfully";
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

            string postPath = @"images\posts\post-" + id;
            string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, postPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach(string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }
                Directory.Delete(finalPath);
            }
                
            _unitOfWork.Post.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Post deleted successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteImage(int imageId) 
        {
            var imageToBeDeleted = _unitOfWork.PostImage.Get(i => i.Id == imageId);
            var postId = imageToBeDeleted.PostId;
            if(imageToBeDeleted != null)
            {
                if(!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageToBeDeleted.ImageUrl.TrimStart('/'));
                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.PostImage.Remove(imageToBeDeleted);
                _unitOfWork.Save();
                TempData["success"] = "Deleted successfully";
            }
            return RedirectToAction(nameof(Upsert), new {id = postId});
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Post> objPostList = _unitOfWork.Post.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objPostList });
        }
        #endregion
    }
}
