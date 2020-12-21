using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace sharkFinApi.Controllers
{
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
        public async Task<IActionResult> GetUsersAsync() {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(User user) {
            User created;
            try {
                created = await _userRepository.AddAsync(user);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id) {
            User user;
            try {
                user = await _userRepository.GetAsync(id);
            } catch {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(User user) {
            try {
                await _userRepository.UpdateAsync(user);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id) {
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
            try {
                user = await _userRepository.GetAsync(id);
            } catch {
                return BadRequest();
            }
            var portfolios = await _portfolioRepository.GetAllAsync(user);
            return Ok(portfolios);
        }

        [HttpPost("{id}/portfolios")]
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

        [HttpGet("{id}/portfolios/{id}")]
        public async Task<IActionResult> GetPortfolioByIdAsync(int id) {
            Portfolio portfolio;
            try {
                portfolio = await _portfolioRepository.GetAsync(id);
            } catch {
                return BadRequest();
            }

            return Ok(portfolio);
        }

        [HttpPut("{id}/portfolios/{id}")]
        public async Task<IActionResult> UpdatePortfolioAsync(Portfolio portfolio) {
            try {
                await _portfolioRepository.UpdateAsync(portfolio);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}/portfolios/{id}")]
        public async Task<IActionResult> DeletePortfolioAsync(int id)  {
            try {
                await _portfolioRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("{id}/portfolios/{id}/trades")]
        public async Task<IActionResult> GetTradesAsync(int id) {
            throw new NotImplementedException();
        }

        [HttpGet("{id}/potfolios/{id}/trades/{id}")]
        public async Task<IActionResult> GetTradeByIdAsync(int id) {
            throw new NotImplementedException();
        }
    }
}
