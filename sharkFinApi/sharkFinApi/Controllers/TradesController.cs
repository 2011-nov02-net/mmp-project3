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

    [Route("api/trades")]
    [ApiController]
    public class TradesController : ControllerBase {

        private readonly ITradeRepository _tradeRepository;

        public TradesController(ITradeRepository tradeRepository) {
            _tradeRepository = tradeRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync() {
            var trades = await _tradeRepository.GetAllAsync();
            return Ok(trades);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Trade trade;
            try {
                trade = await _tradeRepository.GetAsync(id);
            } catch {
                return NotFound();
            }

            return Ok(trade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Trade trade) {
            try {
                trade.Id = id;
                await _tradeRepository.UpdateAsync(trade);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _tradeRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
