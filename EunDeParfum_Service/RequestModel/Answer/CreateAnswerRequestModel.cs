using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Answer
{
    public class CreateAnswerRequestModel
    {
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public int ResultId { get; set; }
        public bool Status { get; set; }
    }
}
