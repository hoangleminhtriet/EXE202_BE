using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Question
{
    public class CreateQuestionRequestModel
    {
        public string QuestionText { get; set; }
        public bool Status { get; set; }
    }
}
