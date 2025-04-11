using EunDeParfum_Service.RequestModel.VIETQR;
using EunDeParfum_Service.ResponseModel.VIETQR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IPaymentService
    {
        Task<VietQrResponse> GenerateQrAsync(VietQrRequest request);
    }
}
