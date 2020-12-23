using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sharkFinApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase {

        private readonly IStockRepository _stockRepository;
        public StocksController(IStockRepository stockRepository) {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Stock stock) {
            Stock created;
            try {
                created = await _stockRepository.AddAsync(stock);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Stock stock;
            try {
                stock = await _stockRepository.GetAsync(id);
            } catch {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Stock stock) {
            try {
                stock.Id = id;
                await _stockRepository.UpdateAsync(stock);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _stockRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
