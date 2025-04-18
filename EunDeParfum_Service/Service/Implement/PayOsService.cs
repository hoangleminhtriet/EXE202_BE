using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Implement
{
    public class PayOsService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _payOsSetting;

        public PayOsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _payOsSetting = _configuration.GetSection("PayOs");
        }
        public async Task<CreatePaymentResult> createPaymentLink(PaymentData paymentData)
        {
            // Xử lý PaymentData ở đây
            var client_id = _payOsSetting.GetSection("ClientId").Value;
            var api_key = _payOsSetting.GetSection("ApiKey").Value;
            var checkSum_key = _payOsSetting.GetSection("CheckSumKey").Value;

            PayOS payOS = new PayOS(client_id, api_key, checkSum_key);

            // Sử dụng paymentData để tạo liên kết thanh toán
            var result = await payOS.createPaymentLink(paymentData);
            return result;
        }


        public async Task<PaymentLinkInformation> getPaymentLinkInformation(int id)
        {

            var client_id = _payOsSetting.GetSection("ClientId").Value;
            var api_key = _payOsSetting.GetSection("ApiKey").Value;
            var checkSum_key = _payOsSetting.GetSection("CheckSumKey").Value;

            PayOS payOS = new PayOS(client_id, api_key, checkSum_key);
            PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(id);
            return paymentLinkInformation;
        }

        public async Task<PaymentLinkInformation> cancelPaymentLink(int id, string reason)
        {
            var client_id = _payOsSetting.GetSection("ClientId").Value;
            var api_key = _payOsSetting.GetSection("ApiKey").Value;
            var checkSum_key = _payOsSetting.GetSection("CheckSumKey").Value;

            PayOS payOS = new PayOS(client_id, api_key, checkSum_key);

            PaymentLinkInformation cancelledPaymentLinkInfo = await payOS.cancelPaymentLink(id, reason);
            return cancelledPaymentLinkInfo;
        }

        public async Task<string> confirmWebhook(string url)
        {
            var client_id = _payOsSetting.GetSection("ClientId").Value;
            var api_key = _payOsSetting.GetSection("ApiKey").Value;
            var checkSum_key = _payOsSetting.GetSection("CheckSumKey").Value;

            PayOS payOS = new PayOS(client_id, api_key, checkSum_key);
            return await payOS.confirmWebhook(url);

        }



        public WebhookData verifyPaymentWebhookData(WebhookType webhookType)
        {
            var client_id = _payOsSetting.GetSection("ClientId").Value;
            var api_key = _payOsSetting.GetSection("ApiKey").Value;
            var checkSum_key = _payOsSetting.GetSection("CheckSumKey").Value;

            PayOS payOS = new PayOS(client_id, api_key, checkSum_key);
            WebhookData webhookData = payOS.verifyPaymentWebhookData(webhookType);
            return webhookData;
        }
    }
}
