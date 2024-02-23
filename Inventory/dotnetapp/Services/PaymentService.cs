using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Services
{
    public class PaymentService
    {
        private readonly List<Payment> _payments;

        public PaymentService()
        {
            // Initialize an empty list of payments
            _payments = new List<Payment>();
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            // Return all payments in the list
            return _payments;
        }

        public async Task<Payment> GetPaymentById(int paymentId)
        {
            // Find the payment with the provided ID in the list
            return _payments.FirstOrDefault(p => p.PaymentID == paymentId);
        }

        public async Task CreatePayment(Payment payment)
        {
            // Assign a new ID to the payment
            payment.PaymentID = _payments.Count > 0 ? _payments.Max(p => p.PaymentID) + 1 : 1;
            
            // Add the new payment to the list
            _payments.Add(payment);
        }
    }
}