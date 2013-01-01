using OTAnswerService.Infrastructure;

namespace OTAnswerService.Entities
{
    public class Answer: IIdentifiable
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}