using System.Web.Mvc;
using OTAnswerService.Entities;
using OTAnswerService.Infrastructure;

namespace OTAnswerService.Models
{
    public class EditableViewModel: IIdentifiable
    {
        public SelectList Answers { get; set; }

        public Answer Current { get; set; }

        public int Id { get; set; }
    }
}