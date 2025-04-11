using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.VIETQR
{
    public class VietQrResponse
    {
        public string QrBase64 { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
