using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sharkFinApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase {

        private readonly IAssetRepository _assetRepository;

        public AssetsController(IAssetRepository assetRepository) {
            _assetRepository = assetRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync() {
            var assets = await _assetRepository.GetAllAsync();
            return Ok(assets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Asset asset;
            try {
                asset = await _assetRepository.GetAsync(id);
            } catch {
                return NotFound();
            }

            return Ok(asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Asset asset) {
            try {
                await _assetRepository.UpdateAsync(asset);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _assetRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
