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

    [Route("api/portfolios")]
    [ApiController]
    public class PortfoliosController : ControllerBase {

        private readonly IPortfolioRepository _portfolioRepository;

        public PortfoliosController(IPortfolioRepository portfolioRepository) {
            _portfolioRepository = portfolioRepository;
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePortfolioAsync(Portfolio portfolio) {
            Portfolio created;
            try {
                created = await _portfolioRepository.AddAsync(portfolio);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetPortfolioByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPortfolioByIdAsync(int id) {
            Portfolio portfolio;
            try {
                portfolio = await _portfolioRepository.GetAsync(id);
            } catch {
                return BadRequest();
            }

            return Ok(portfolio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortfolioAsync(Portfolio portfolio) {
            try {
                await _portfolioRepository.UpdateAsync(portfolio);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolioAsync(int id) {
            try {
                await _portfolioRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
