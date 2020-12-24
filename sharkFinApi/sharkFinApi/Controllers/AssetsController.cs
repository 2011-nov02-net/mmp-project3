using System;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace sharkFinApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase {

        private readonly IAssetRepository _assetRepository;
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IAssetRepository assetRepository, ILogger<AssetsController> logger) {
            _assetRepository = assetRepository;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync() {
            var assets = await _assetRepository.GetAllAsync();
            _logger.LogInformation("Fetched list of all assets.");
            return Ok(assets);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Asset asset;
            try {
                asset = await _assetRepository.GetAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no asset entry with id: {id}.");
                return NotFound(e.Message);
            }

            _logger.LogInformation("Fetched asset.", asset);
            return Ok(asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Asset asset) {
            try {
                asset.Id = id;
                await _assetRepository.UpdateAsync(asset);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no asset entry with id: {id}.");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to update an asset, violating database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _assetRepository.DeleteAsync(id);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no asset entry with id: {id}.");
                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}
