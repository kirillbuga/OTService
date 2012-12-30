using System;
using System.Linq;
using System.Web.Mvc;
using OTAnswerService.DataAccess;
using OTAnswerService.Entities;
using OTAnswerService.Models;

namespace OTAnswerService.Controllers
{
    public class ReadController : Controller
    {
        public IRepository _Repository;

        public ReadController()
        {
            _Repository = new EfRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FullList()
        {
            var list = _Repository.Query<Answer>()
                .Where(x => !string.IsNullOrEmpty(x.Title))
                .ToList();

            return View(list);
        }
        
        public ActionResult Details(int id)
        {
            var answer = _Repository.Get<Answer>(id);
            return PartialView("Details", answer);
        }

        public ActionResult Search()
        {
            return View(new SearchResultViewModel());
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            int number;
            Int32.TryParse(searchString, out number);

            var searchByNumber = _Repository.Query<Answer>().Where(x => x.Number == number).ToList();
            var searchByTitle = _Repository.Query<Answer>().Where(x => x.Title.Contains(searchString)).ToList();
            var searchByAnswers = _Repository.Query<Answer>().Where(x => x.Description.Contains(searchString)).ToList();

            var model = new SearchResultViewModel
                            {
                                SearchByAnswers = searchByAnswers,
                                SearchByNumber = searchByNumber,
                                SearchByTitle = searchByTitle
                            };

            return View(model);
        }
    }
}
