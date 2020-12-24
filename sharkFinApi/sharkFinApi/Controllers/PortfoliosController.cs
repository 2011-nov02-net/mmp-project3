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
        private readonly IAssetRepository _assetRepository;
        private readonly ITradeRepository _tradeRepository;
        private readonly IStockRepository _stockRepository;

        public PortfoliosController(IPortfolioRepository portfolioRepository, IAssetRepository assetRepository, ITradeRepository tradeRepository, IStockRepository stockRepository) {
            _portfolioRepository = portfolioRepository;
            _assetRepository = assetRepository;
            _tradeRepository = tradeRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync() {
            var portfolios = await _portfolioRepository.GetAllAsync();
            return Ok(portfolios);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id) {
            Portfolio portfolio;
            try {
                portfolio = await _portfolioRepository.GetAsync(id);
            } catch {
                return NotFound();
            }

            return Ok(portfolio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Portfolio portfolio) {
            try {
                portfolio.Id = id;
                await _portfolioRepository.UpdateAsync(portfolio);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) {
            try {
                await _portfolioRepository.DeleteAsync(id);
            } catch {
                return BadRequest();
            }

            return NoContent();
        }
        [HttpPut("{id}/funds")]
        public async Task<IActionResult> PutFundsAsync(int id, Portfolio portfolio)
        {
            
            try
            {
                Portfolio existing = await _portfolioRepository.GetAsync(id);
                existing.Funds -= portfolio.Funds;               
                await _portfolioRepository.UpdateAsync(existing);
            }
            catch
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("{id}/assets")]
        public async Task<IActionResult> GetPortfolioAssetsAsync(int id) {
            Portfolio portfolio;
            IEnumerable<Asset> assets;
            try {
                portfolio = await _portfolioRepository.GetAsync(id);
                assets = await _assetRepository.GetAllAsync(portfolio);
            } catch {
                return NotFound();
            }

            return Ok(assets);
        }

        [HttpPost("{id}/assets")]
        public async Task<IActionResult> PostAssetAsync([FromBody] Asset asset, [FromRoute] int id) {
            Asset created;

            try {
                var stock = await _stockRepository.GetAsync(asset.Stock.Symbol);
                asset.Stock.Id = stock.Id;
                var portfolio = await _portfolioRepository.GetAsync(id);
                created = await _assetRepository.AddAsync(asset, portfolio);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(AssetsController.GetByIdAsync), "Assets", new { id = created.Id }, created);
        }

        [HttpGet("{id}/trades")]
        public async Task<IActionResult> GetPortfolioTradesAsync(int id) {
            Portfolio portfolio;
            IEnumerable<Trade> trades;
            try {
                portfolio = await _portfolioRepository.GetAsync(id);
                trades = await _tradeRepository.GetAllAsync(portfolio);
            } catch {
                return NotFound();
            }

            return Ok(trades);
        }

        [HttpPost("{id}/trades")]
        public async Task<IActionResult> PostTradeAsync([FromBody] Trade trade, [FromRoute] int id) {
            Trade created;
            try {
                var stock = await _stockRepository.GetAsync(trade.Stock.Symbol);
                trade.Stock.Id = stock.Id;
                trade.Time = DateTime.Now;
                var portfolio = await _portfolioRepository.GetAsync(id);
                created = await _tradeRepository.AddAsync(trade, portfolio);
            } catch (ArgumentException e) {
                return BadRequest(e.Message);
            } catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(TradesController.GetByIdAsync), "Trades", new { id = created.Id }, created);
        }
    }
}
