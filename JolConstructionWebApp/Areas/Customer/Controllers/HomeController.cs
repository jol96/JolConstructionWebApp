using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JolConstructionWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Post> postList = _unitOfWork.Post.GetAll(includeProperties: "Category");
            return View(postList);
        }

        public IActionResult Details(int postId)
        {
            Post post = _unitOfWork.Post.Get(u => u.Id == postId, includeProperties: "Category");
            return View(post);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}