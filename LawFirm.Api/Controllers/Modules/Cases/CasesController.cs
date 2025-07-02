using LawFirm.Application.Interfaces;
using LawFirm.Application.Modules.Cases;
using LawFirm.Application.Modules.Cases.Dto;
using LawFirm.Application.Modules.Documents;
using LawFirm.Application.Modules.Documents.Dto;
using LawFirm.Application.Modules.Expenses.Dto;
using LawFirm.Application.Modules.Invoices;
using LawFirm.Application.Modules.Invoices.Dto;
using LawFirm.Application.Modules.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LawFirm.Api.Controllers.Modules.Cases
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Lawyer")]
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;
        private readonly IInvoiceService _invoiceService;
        private readonly IExpenseService _expenseService;
        private readonly IDocumentService _documentService;
        private readonly ICaseUpdateService _caseUpdateService;

        public CasesController(
            ICaseService caseService,
            IInvoiceService invoiceService,
            IExpenseService expenseService,
            IDocumentService documentService,
            ICaseUpdateService caseUpdateService
        )
        {
            _caseService = caseService;
            _invoiceService = invoiceService;
            _expenseService = expenseService;
            _documentService = documentService;
            _caseUpdateService = caseUpdateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CaseQueryParametersDto query)
        {
            var result = await _caseService.GetAllCasesAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _caseService.GetByIdAsync(id);
            if (c == null)
                return NotFound();
            return Ok(c);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCaseDto dto)
        {
            var c = await _caseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCaseDto dto)
        {
            var updated = await _caseService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _caseService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{caseId}/invoices")]
        public async Task<IActionResult> GetInvoices(int caseId)
        {
            var invoices = await _invoiceService.GetByCaseIdAsync(caseId);
            return Ok(invoices);
        }

        [HttpPost("{caseId}/invoices")]
        public async Task<IActionResult> CreateInvoice(int caseId, [FromBody] CreateInvoiceDto dto)
        {
            var invoice = await _invoiceService.CreateAsync(caseId, dto);
            return CreatedAtAction(nameof(GetInvoices), new { caseId }, invoice);
        }

        [HttpGet("{caseId}/expenses")]
        public async Task<IActionResult> GetExpenses(int caseId)
        {
            var expenses = await _expenseService.GetByCaseIdAsync(caseId);
            return Ok(expenses);
        }

        [HttpPost("{caseId}/expenses")]
        public async Task<IActionResult> CreateExpense(int caseId, [FromBody] CreateExpenseDto dto)
        {
            var expense = await _expenseService.CreateAsync(caseId, dto);
            return CreatedAtAction(nameof(GetExpenses), new { caseId }, expense);
        }

        [HttpPost("{caseId}/documents")]
        public async Task<IActionResult> CreateDocument(
            int caseId,
            [FromBody] CreateDocumentDto dto
        )
        {
            var document = await _documentService.CreateAsync(caseId, dto);
            return CreatedAtAction(nameof(CreateDocument), new { caseId }, document);
        }

        [HttpPost("{caseId}/updates")]
        public async Task<IActionResult> CreateCaseUpdate(
            int caseId,
            [FromBody] CreateCaseUpdateDto dto
        )
        {
            var update = await _caseUpdateService.CreateAsync(caseId, dto);
            return CreatedAtAction(nameof(CreateCaseUpdate), new { caseId }, update);
        }
    }
}
