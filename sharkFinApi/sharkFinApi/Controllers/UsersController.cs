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
        [HttpGet]
        public IActionResult GetUsers()
        {
            return StatusCode(200);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserByID()
        {
            return StatusCode(200);
        }
        [HttpGet("{id}/portfolios")]
        public IActionResult GetUserPortfolios()
        {
            return StatusCode(200);
        }

        [HttpGet("{id}/portfolios/{id}")]
        public IActionResult GetPortfolioById()
        {
            return StatusCode(200);
        }

        [HttpGet("{id}/portfolios/{id}/trades")]
        public IActionResult GetTrades()
        {
            return StatusCode(200);
        }

        [HttpGet("{id}/potfolios/{id}/trades/{id}")]
        public IActionResult GetTradeById()
        {
            return StatusCode(200);
        }
    }
}
