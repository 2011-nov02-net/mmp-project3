using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace sharkFinApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        //returns just holders for the moment
        [HttpGet]
        public IActionResult GetUsers()
        {
            return StatusCode(200);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID(int id)
        {
            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(User user)
        {
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            return StatusCode(200);
        }

        [HttpGet("{id}/portfolios")]
        public IActionResult GetUserPortfolios(int id)
        {
            return StatusCode(200);
        }
        [HttpPost("{id}/portfolios")]
        public IActionResult CreatePortfolio(int id)
        {
            return StatusCode(201);
        }

        [HttpGet("{id}/portfolios/{id}")]
        public IActionResult GetPortfolioById(int userId, int portId)
        {
            return StatusCode(200);
        }
        [HttpPut("{id}/portfolios/{id}")]
        public IActionResult UpdatePortfolio(int userId, Portfolio portfolio)
        {
            return StatusCode(200);
        }
        [HttpDelete("{id}/portfolios/{id}")]
        public IActionResult DeletePortfolio(int userId, int portfolioId)
        {
            return StatusCode(200);
        }

        [HttpGet("{id}/portfolios/{id}/trades")]
        public IActionResult GetTrades(int userId, int portId)
        {
            return StatusCode(200);
        }

        [HttpGet("{id}/potfolios/{id}/trades/{id}")]
        public IActionResult GetTradeById(int userId, int portId, int tradeId)
        {
            return StatusCode(200);
        }
    }
}
