using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace sharkFinApi.Controllers {
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly IUserRepository _userRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public UsersController(IUserRepository userRepository, IPortfolioRepository portfolioRepository) {
            _userRepository = userRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(User user) {
            User created;
            try {
                created = await _userRepository.AddAsync(user);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) {
            User user;
            try {
                user = await _userRepository.GetAsync(id);
            } catch {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(User user) {
            try {
                await _userRepository.UpdateAsync(user);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _userRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("{id}/portfolios")]
        public async Task<IActionResult> GetUserPortfoliosAsync(int id) {
            User user;
            IEnumerable<Portfolio> portfolios;
            try {
                user = await _userRepository.GetAsync(id);
                portfolios = await _portfolioRepository.GetAllAsync(user);
            } catch {
                return NotFound();
            }

            return Ok(portfolios);
        }

        [HttpPost("{id}/portfolios")]
        public async Task<IActionResult> PostPortfolioAsync([FromBody] Portfolio portfolio, [FromRoute]int id) {
            Portfolio created;
            try {
                var user = await _userRepository.GetAsync(id);
                created = await _portfolioRepository.AddAsync(portfolio, user);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }
    }
}
