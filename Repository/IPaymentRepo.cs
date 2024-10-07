using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IPaymentRepo
    {
        public Task<IEnumerable<PaymentDetail>> GetAll();
        public Task<PaymentDetail> GetById(int id);
        public Task<PaymentDetail> AddPayment(PaymentDetail paymentDetail);
        public Task<bool> UpdatePayment(PaymentDetail paymentDetail);
        public Task<bool> DeletePayment(int id);
    }
}
