using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace sharkFinApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase {

        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StocksController> _logger;

        public StocksController(IStockRepository stockRepository, ILogger<StocksController> logger) {
            _stockRepository = stockRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() {
            var stocks = await _stockRepository.GetAllAsync();
            _logger.LogInformation("Fetched list of all stocks.");
            return Ok(stocks);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Stock stock) {
            Stock created;
            try {
                created = await _stockRepository.AddAsync(stock);
            } catch (ArgumentException e) {
                _logger.LogInformation(e, "Attempted to add a stock with an already existing Id.");
                return BadRequest(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to add a stock that violated database constraints.");
                return BadRequest(e.Message);
            }

            _logger.LogInformation("Successfully added stock.", created);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Stock stock;
            try {
                stock = await _stockRepository.GetAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no stock entry with id: {id}.");
                return NotFound(e.Message);
            }

            _logger.LogInformation("Fetched stock.", stock);
            return Ok(stock);
        }


        [HttpGet("/symbol/{symbol}")]
        [ActionName(nameof(GetBySymbolAsync))]
        public async Task<IActionResult> GetBySymbolAsync(string symbol)
        {
            Stock stock;
            try
            {
                stock = await _stockRepository.GetAsync(symbol);
            }
            catch
            {
                return NotFound();
            }

            return Ok(stock);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Stock stock) {
            try {
                stock.Id = id;
                await _stockRepository.UpdateAsync(stock);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no stock entry with id: {id}.");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to update a stock, violating database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _stockRepository.DeleteAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no stock entry with id: {id}.");
                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}
