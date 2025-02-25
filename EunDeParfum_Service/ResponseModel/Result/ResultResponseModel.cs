using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.Result
{
    public class ResultResponseModel
    {
        public int Id { get; set; }
        public string ResultName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
