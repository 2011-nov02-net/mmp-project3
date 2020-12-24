using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace sharkFinApi.Controllers {

    [Route("api/trades")]
    [ApiController]
    public class TradesController : ControllerBase {

        private readonly ITradeRepository _tradeRepository;
        private readonly ILogger<TradesController> _logger;

        public TradesController(ITradeRepository tradeRepository, ILogger<TradesController> logger) {
            _tradeRepository = tradeRepository;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync() {
            var trades = await _tradeRepository.GetAllAsync();
            _logger.LogInformation("Fetched list of all trades.");
            return Ok(trades);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Trade trade;
            try {
                trade = await _tradeRepository.GetAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no trade entry with id: {id}.");
                return NotFound(e.Message);
            }

            _logger.LogInformation("Fetched trade.", trade);
            return Ok(trade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Trade trade) {
            try {
                trade.Id = id;
                await _tradeRepository.UpdateAsync(trade);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no trade entry with id: {id}.");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to update a trade, violating database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _tradeRepository.DeleteAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no trade entry with id: {id}.");
                return NotFound(e.Message);
            }

            return NoContent();
        }

    }
}
