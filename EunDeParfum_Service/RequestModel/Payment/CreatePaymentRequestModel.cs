using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Payment
{
    public class CreatePaymentRequestModel
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }

    }
}
