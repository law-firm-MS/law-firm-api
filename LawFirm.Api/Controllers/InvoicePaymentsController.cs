using System.Security.Claims;
using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    [Authorize(Roles = "Client")]
    public class InvoicePaymentsController : ControllerBase
    {
        private readonly IInvoicePaymentService _paymentService;

        public InvoicePaymentsController(IInvoicePaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{invoiceId}/pay")]
        public async Task<IActionResult> PayInvoice(
            int invoiceId,
            [FromBody] InvoicePaymentRequestDto dto
        )
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name;
            if (email == null)
                return Unauthorized();
            var result = await _paymentService.PayInvoiceAsync(invoiceId, dto, email);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
