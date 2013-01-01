using System;
using System.Collections.Generic;
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
        public ActionResult Search(string searchString, string options)
        {
            List<Answer> results = null;

            switch (options)
            {
                case "number":
                    results = _Repository.Query<Answer>().Where(x => x.Number.Contains(searchString)).ToList();
                    break;
                case "question":
                    results = _Repository.Query<Answer>().Where(x => x.Title.Contains(searchString)).ToList();
                    break;
                case "answer":
                    results = _Repository.Query<Answer>().Where(x => x.Description.Contains(searchString)).ToList();
                    break;
            }

            var model = new SearchResultViewModel
                            {
                                Results = results
                            };

            return View(model);
        }
    }
}
