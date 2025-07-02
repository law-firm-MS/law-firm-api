using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Invoices;
using LawFirm.Application.Modules.Invoices.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers.Modules.Invoices
{
    [ApiController]
    [Route("admin/invoices")]
    [Authorize(Roles = "Admin,Lawyer")]
    public class AdminInvoicesController : ControllerBase
    {
        private readonly IAdminInvoiceService _service;

        public AdminInvoicesController(IAdminInvoiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            var result = await _service.CreateInvoiceAsync(dto);
            return CreatedAtAction(nameof(GetById), new { invoiceId = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] InvoiceQueryParametersDto query)
        {
            var result = await _service.GetAllInvoicesAsync(query);
            return Ok(result);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetById(int invoiceId)
        {
            var result = await _service.GetInvoiceByIdAsync(invoiceId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> Update(int invoiceId, [FromBody] UpdateInvoiceDto dto)
        {
            var success = await _service.UpdateInvoiceAsync(invoiceId, dto);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> Delete(int invoiceId)
        {
            var success = await _service.DeleteInvoiceAsync(invoiceId);
            if (!success)
                return NotFound();
            return NoContent();
        }

        [HttpPost("{invoiceId}/mark-paid")]
        public async Task<IActionResult> MarkPaid(int invoiceId)
        {
            var success = await _service.MarkInvoicePaidAsync(invoiceId);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
