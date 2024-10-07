using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public readonly IPaymentRepo _paymentRepo;
        public PaymentController(IPaymentRepo paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentRepo.GetAll();
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] PaymentDetail paymentDetail)
        {
            var createdPayment = await _paymentRepo.AddPayment(paymentDetail);
            return Ok(await _paymentRepo.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentRepo.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, PaymentDetail paymentDetail)
        {
            var existingPayment = await _paymentRepo.GetById(id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            paymentDetail.PaymentDetailId = existingPayment.PaymentDetailId;

            var updatedPayment = await _paymentRepo.UpdatePayment(paymentDetail);
            return Ok(await _paymentRepo.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingPayment = await _paymentRepo.GetById(id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            await _paymentRepo.DeletePayment(id);
            return Ok(await _paymentRepo.GetAll());
        }
    }
}
