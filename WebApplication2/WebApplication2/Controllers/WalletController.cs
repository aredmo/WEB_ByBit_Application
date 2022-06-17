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
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("/wallet/")]
    public class WalletController : ControllerBase
    {
        //public MySQL _mySQL;
        private readonly IRepository<BalanceEntity> _repository;
        public WalletController(IRepository<BalanceEntity> repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        [Route("getQuerySymbol")] //получение таблицы инструментов
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
        [Route("getWalletBalance")] //получение баланса
        public IActionResult GetWalletBalance()
        { 
            ByBitClient client = new ByBitClient("LcNzNNR8ii0SxIlW6W", "kcnjSyAhj0mF5kvCqbr9oP0d3q0BpsagYrJc",getTime:null); 
            var res = client.GetWalletBalanceRequest();
            var WalletBalance = res.CoinInfo.Wallets;
            List<BalanceEntity> listData = new List<BalanceEntity>();
            foreach (var item in WalletBalance)
            {
                BalanceEntity t = new BalanceEntity();
                t.Currency = item.Key;
                t.WalletBalance = item.Value.WalletBalance;
                t.RecordingDate = DateTime.Now;
                listData.Add(t);
                //_repository.Create(t);
                var reult = _repository.Create(t);
                if (reult != 0)
                    return Ok(t);
                else 
                    return BadRequest();
                
                //_mySQL.BalanceEntity.Add(t);
                //_mySQL.SaveChanges();
                
            }
            return Ok(listData);
        }
        
        
        
        
    }
}