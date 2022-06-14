using System;
using System.Collections.Generic;
using System.Linq;
using BybitMapper.InversePerpetual.RestV2;
using BybitMapper.Perpetual.RestV2.Data.ObjectDTO.Market;
using BybitMapper.Perpetual.RestV2.Requests.Account.Wallet;
using BybitMapper.Perpetual.RestV2.Requests.Market;
using BybitMapper.Perpetual.RestV2.Responses.Market;
using Microsoft.AspNetCore.Http;
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
            ByBitClient client = new ByBitClient();
            var res = client.QuerySymbolRequest();
            var ResList = res.Result;
            List<QueryEntity> listData = new List<QueryEntity>();
            foreach (var item in ResList)
            {
                QueryEntity t = new QueryEntity();
                t.Name = item.Name;
                t.BaseCurrency = item.BaseCurrency;
                t.Status = item.Status;
                t.MaxPrice = item.PriceFilter.MaxPrice;
                t.MinPrice = item.PriceFilter.MinPrice;
                t.TakerFee = item.TakerFee;
                t.MakerFee = item.MakerFee;
                listData.Add(t);
            }
            return Ok(listData);
        }
        
        [HttpGet]
        [Route("getWalletBalance")]
        public IActionResult GetWalletBalance()
        { 
        //public InversePerpetualV2HandlerComposition RESTHandlers = new InversePerpetualV2HandlerComposition(new InversePerpetualV2HandlerFactory());
        
            GetWalletBalanceRequest tester = new GetWalletBalanceRequest();
            Func<long> GetTime = () =>
            {
                return (long)Math.Round(tester.RESTHandlers.HandleServerTimeResponse(tester.SendTest(new BybitMapper.InversePerpetual.RestV2.Requests.Market.ServerTimeRequest())).Timestamp * 1000);
            }; 
            ByBitClient client = new ByBitClient("LcNzNNR8ii0SxIlW6W", "kcnjSyAhj0mF5kvCqbr9oP0d3q0BpsagYrJc", GetTime ); 
            var res = client.GetWalletBalanceRequest();
            var WalletBalance = res.CoinInfo.Wallets;
            var Currency = res.CoinInfo.Wallets;
            List<BalanceEntity> listData = new List<BalanceEntity>();
            
            foreach (var item in Currency)
            {
                BalanceEntity t = new BalanceEntity();
                t.Currency = item.Key;
                listData.Add(t);
            }

            foreach (var item in WalletBalance)
            {
                BalanceEntity t = new BalanceEntity();
                t.WalletBalance = item.Value.WalletBalance;
                listData.Add(t);
            }
            return Ok(listData);
        }
    }
}