using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult AddUser()
        {
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByID()
        {
            return StatusCode(200);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id)
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
        public IActionResult AddPortfolio(int id)
        {
            return StatusCode(201);
        }

        [HttpGet("{id}/portfolios/{id}")]
        public IActionResult GetPortfolioById(int userId, int portId)
        {
            return StatusCode(200);
        }
        [HttpPut("{id}/portfolios/{id}")]
        public IActionResult UpdatePortfolio(int userId, int portId)
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
