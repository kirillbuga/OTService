using System;
using System.Web.Mvc;
using OTAnswerService.DataAccess;
using OTAnswerService.Entities;
using System.Linq;
using OTAnswerService.Extensions;
using OTAnswerService.Models;

namespace OTAnswerService.Controllers
{
    public class WriteController : Controller
    {
        public IRepository _Repository;

        public WriteController()
        {
            _Repository = new EfRepository();
        }

        //
        // GET: /Write/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Write/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Write/Create

        [HttpPost]
        public ActionResult Create(AnswerViewModel answerViewModel)
        {
            try
            {
                var answer = new Answer
                                 {
                                     Description = answerViewModel.Description,
                                     Title = answerViewModel.Title,
                                     Number = answerViewModel.Number
                                 };
                _Repository.Save(answer);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
                return View(answerViewModel);
            }
        }
        
        public ActionResult Search()
        {
            var model = _Repository.Query<Answer>()
                .Where(x => !string.IsNullOrEmpty(x.Title))
                .ToSelectList(x => x.Title, x => x.Id);
            return View(model);
        }
 
        public ActionResult Edit(int id)
        {
            var model = _Repository.Get<Answer>(id);
            var viewModel = new AnswerViewModel
                                {
                                    Description = model.Description,
                                    Id = model.Id,
                                    Number = model.Number,
                                    Title = model.Title
                                };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AnswerViewModel model)
        {
            try
            {
                var answer = new Answer
                {
                    Description = model.Description,
                    Title = model.Title,
                    Number = model.Number,
                    Id = model.Id
                };
                _Repository.Save(answer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
