using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.Question
{
    public class QuestionResponseModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool Status { get; set; }
    }
}
