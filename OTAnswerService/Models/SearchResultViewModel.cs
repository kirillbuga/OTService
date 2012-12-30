using System.Collections.Generic;
using OTAnswerService.Entities;

namespace OTAnswerService.Models
{
    public class SearchResultViewModel
    {
        public List<Answer> SearchByTitle { get; set; } 
        public List<Answer> SearchByAnswers { get; set; } 
        public List<Answer> SearchByNumber { get; set; } 
    }
}