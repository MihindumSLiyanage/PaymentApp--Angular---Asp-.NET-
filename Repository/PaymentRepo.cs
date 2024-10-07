using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        public readonly AppDbContext _dbContext;

        public PaymentRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PaymentDetail> AddPayment(PaymentDetail paymentDetail)
        {
            await _dbContext.PaymentDetails.AddAsync(paymentDetail);
            await _dbContext.SaveChangesAsync();
            return paymentDetail;
        }

        public async Task<bool> DeletePayment(int id)
        {
            var product = await _dbContext.PaymentDetails.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _dbContext.PaymentDetails.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PaymentDetail>> GetAll()
        {
            return await _dbContext.PaymentDetails.ToListAsync();
        }

        public async Task<PaymentDetail> GetById(int id)
        {
            return await _dbContext.PaymentDetails.FirstOrDefaultAsync(p => p.PaymentDetailId == id);
        }

        public async Task<bool> UpdatePayment(PaymentDetail paymentDetail)
        {
            var existingPayment= await _dbContext.PaymentDetails.FindAsync(paymentDetail.PaymentDetailId);
            if (existingPayment == null)
            {
                return false;
            }

            existingPayment.CardOwnerName = paymentDetail.CardOwnerName;
            existingPayment.CardNumber = paymentDetail.CardNumber;
            existingPayment.ExpirationDate = paymentDetail.ExpirationDate;
            existingPayment.SecurityCode = paymentDetail.SecurityCode;

            _dbContext.Update(existingPayment);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
