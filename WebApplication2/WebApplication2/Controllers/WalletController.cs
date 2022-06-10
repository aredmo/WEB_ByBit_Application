using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
    }
}