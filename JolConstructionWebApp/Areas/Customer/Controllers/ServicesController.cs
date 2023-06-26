using JolConstruction.DataAccess.Repository.IRepository;
using JolConstruction.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JolConstructionWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ServicesController : Controller
    {
        private readonly ILogger<ServicesController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ServicesController(ILogger<ServicesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string? filterServices)
        {
            IEnumerable<Post> postList = _unitOfWork.Post.GetAll(includeProperties: "Category,PostImages");
            switch (filterServices)
            {
                case "Roofing":
                    List<Post> roofingPost = postList.Where(p => p.Title == "Roofing").ToList();
                    return View(roofingPost);
                case "Timber Framing":
                    List<Post> timberFramingPost = postList.Where(p => p.Title == "Timber Framing").ToList();
                    return View(timberFramingPost);
                case "Extensions":
                    List<Post> extensionsPost = postList.Where(p => p.Title == "Extensions").ToList();
                    return View(extensionsPost);
                case "Garden Room":
                    List<Post> gardenRoomPost = postList.Where(p => p.Title == "Garden Room").ToList();
                    return View(gardenRoomPost);
                case "Patio":
                    List<Post> patioPost = postList.Where(p => p.Title == "Patio").ToList();
                    return View(patioPost);
                default:
                    return View(postList);
            }
        }
    }
}