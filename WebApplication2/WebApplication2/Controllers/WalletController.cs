using System.Linq;
using BybitMapper.Perpetual.RestV2.Requests.Market;
using BybitMapper.Perpetual.RestV2.Responses.Market;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Contexts;
using WebApplication2.Entities;
using WebApplication2.Requests;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("/wallet/")]
    public class WalletController : ControllerBase
    {
        public MySQL _mySQL;

        public WalletController(MySQL mySQL)
        {
            _mySQL = mySQL;
        }

        [HttpPost]
        [Route("createWallet")]
        public IActionResult CreateWallet([FromBody] WalletRequest request)
        {
            InfoWallet wallet = new InfoWallet();
            wallet.Balance = request.Balance;
            wallet.Сurrency = request.Сurrency;
            wallet.RecordingDate = request.RecordingDate;

            _mySQL.InfoWallets.Add(wallet);
            _mySQL.SaveChanges();
            return Ok(wallet.Id);
        }
        
        [HttpGet]
        [Route("getWallet")] 
        public IActionResult GetUsers()
        {
            return Ok(_mySQL.InfoWallets.ToList());
        }
        
        [HttpGet]
        [Route("getQuerySymbol")] 
        public IActionResult GetQuerySymbol()
        {
            ByBitClient query = ; //дб 2
            QuerySymbolRequest List<QueryEntity>() = new QuerySymbolRequest();
        }
        
    }
}