using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace sharkFinApi.Controllers {

    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly IUserRepository _userRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, IPortfolioRepository portfolioRepository, ILogger<UsersController> logger) {
            _userRepository = userRepository;
            _portfolioRepository = portfolioRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() {
            var users = await _userRepository.GetAllAsync();
            _logger.LogInformation("Fetched list of all users.");
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(User user) {
            User created;
            try {
                created = await _userRepository.AddAsync(user);
            } catch (ArgumentException e) {
                _logger.LogInformation(e, "Attempted to add a user with an already existing Id.");
                return BadRequest(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to add a user that violated database constraints.");
                return BadRequest(e.Message);
            }

            _logger.LogInformation("Successfully added user.", created);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmailAsync(string email) {
            User user;
            try {
                user = await _userRepository.GetAsync(email);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no user entry with email: {email}.");
                return NotFound(e.Message);
            }

            _logger.LogInformation("Fetched user.", user);
            return Ok(user);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            User user;
            try {
                user = await _userRepository.GetAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no user entry with id: {id}.");
                return NotFound(e.Message);
            }

            _logger.LogInformation("Fetched user.", user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, User user) {
            try {
                user.Id = id;
                await _userRepository.UpdateAsync(user);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no user entry with id: {id}.");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to update a user, violating database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _userRepository.DeleteAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no user entry with id: {id}.");
                return NotFound(e.Message);
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
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no user entry with id: {id}.");
                return NotFound(e.Message);
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
                _logger.LogInformation(e, "Attempted to add a portfolio with an already existing Id.");
                return BadRequest(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to add a portfolio that violated database constraints.");
                return BadRequest(e.Message);
            }

            _logger.LogInformation("Successfully created portfolio.", created);
            return CreatedAtAction(nameof(PortfoliosController.GetByIdAsync), "Portfolios", new { id = created.Id }, created);
        }
    }
}
