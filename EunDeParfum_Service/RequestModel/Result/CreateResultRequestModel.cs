using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Result
{
    public class CreateResultRequestModel
    {
        public string ResultName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
