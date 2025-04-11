using EunDeParfum_Service.RequestModel.VIETQR;
using EunDeParfum_Service.ResponseModel.VIETQR;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using EunDeParfum_Service.Service.Interface;

namespace EunDeParfum_Service.Service.Implement
{
    public class PaymentsService : IPaymentService
    {
        public async Task<VietQrResponse> GenerateQrAsync(VietQrRequest request)
        {
            var payload = new
            {
                accountNo = request.AccountNo,
                accountName = request.AccountName,
                acqId = request.AcqId,
                amount = request.Amount,
                addInfo = request.AddInfo,
                format = "text"
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.vietqr.io/v2/generate", content);
            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            var qrData = result["data"]?["qrData"]?.ToString();

            if (string.IsNullOrEmpty(qrData))
            {
                return new VietQrResponse
                {
                    Success = false,
                    Message = "Không lấy được dữ liệu QR"
                };
            }

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrBytes = qrCode.GetGraphic(20);
            string qrBase64 = Convert.ToBase64String(qrBytes);

            return new VietQrResponse
            {
                Success = true,
                QrBase64 = "data:image/png;base64," + qrBase64
            };
        }
    }
}
