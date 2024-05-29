﻿using EventSphere.Business.Helper;
using EventSphere.Business.Services.Interfaces;
using EventSphere.Domain.DTOs;
using EventSphere.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventSphere.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;
        public PaymentController(IPaymentService paymentService, IEmailService emailService)
        {
            _paymentService = paymentService;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var ticket = await _paymentService.GetAllPaymentsAsync();
            return Ok(ticket);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentId(int id)
        {
            var ticket = await _paymentService.GetPaymentByIdAsync(id);
            return Ok(ticket);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PaymentDTO PaymentDTO)
        {
            var paymentResponse = await _paymentService.AddPaymentAsync(PaymentDTO);
            var mailRequest = new MailRequest
            {
                ToEmail = paymentResponse.User.Email, // Assume User object has an Email property
                Subject = "Payment Confirmation",
                Body = $@"
                <p>Thank You <strong>{paymentResponse.User.Name}</strong> for buying a ticket to.</p>
                <p>The price of the ticket is <strong>{paymentResponse.Payment.Amount:C}</strong>.</p>"
            };

            // Send the email
            await _emailService.SendEmailAsync(mailRequest);
            return CreatedAtAction(nameof(GetPaymentId), new { id = PaymentDTO.ID }, PaymentDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PaymentDTO PaymentDTO)
        {
            await _paymentService.UpdatePaymentAsync(id, PaymentDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}