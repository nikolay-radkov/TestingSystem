namespace TestingSystem.Web.InputModels
{
    using System.Collections.Generic;

    public class AnswerBindingModel
    {
        public int QuestionID { get; set; }

        public ICollection<int> AnswerIDs { get; set; }
    }
}