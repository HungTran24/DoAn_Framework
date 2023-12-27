using DoAn_FrameWork.ModelViews;

namespace DoAn_FrameWork.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context,VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collect);
    }
}
