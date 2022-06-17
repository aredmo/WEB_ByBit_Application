using System;
using BybitMapper.Perpetual.RestV2.Requests.Market;
using BybitMapper.Perpetual.RestV2.Requests.Account.Wallet;
using BybitMapper.Perpetual.RestV2;
using BybitMapper.Requests;
using RestSharp;

using BybitMapper.Perpetual.RestV2.Responses.Account.Wallet;
using BybitMapper.Perpetual.RestV2.Responses.Market;

namespace WebApplication2
{
    public class ByBitClient
    {
        private static PerpetualHandlerComposition m_HandlerComposition;
        public PerpetualHandlerComposition PRestHandler = new PerpetualHandlerComposition(new PerpetualHandlerFactory());
        private static RequestArranger m_RequestArranger;
        private static RestClient _restClient;
        private string _baseUrl = "https://api.bybit.com";
        /// <summary>
        /// Публичный конструктор класса
        /// </summary>
        public ByBitClient()
        {
            m_HandlerComposition = new PerpetualHandlerComposition(new PerpetualHandlerFactory());
            m_RequestArranger = new RequestArranger();
            _restClient = new RestClient(_baseUrl);
        }

        
        /// <summary>
        /// Приватный конструктор класса
        /// </summary>
        /// <param name="api_key"></param>
        /// <param name="secret"></param>
        /// <param name="func"></param>
        public ByBitClient(string api_key, string secret, Func<long> getTime)
        {
            m_HandlerComposition = new PerpetualHandlerComposition(new PerpetualHandlerFactory());
            m_RequestArranger = new RequestArranger(api_key, secret, getTime);
            _restClient = new RestClient(_baseUrl);
        }
        
        #region [Base]

        private Func<long> getTime = () =>
        {
            var req = new ServerTimeRequest();
            var res = SendTest(req);
            return (long)Math.Round(m_HandlerComposition.HandlerServerTimeResponse(res).Timestamp * 1000);
        }; 
        public static string SendTest(RequestPayload payload)
        {
            // var h = new Dictionary<string, string> { { "Referer", "Cscalp" } };
            var request = m_RequestArranger.Arrange(payload);
            var req = new RestRequest(request.Query, MapRequestMethod(request.Method));

            if (request.Body != null)
            {
                req.RequestFormat = DataFormat.Json;
                req.AddBody(request.Body);
            }

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    req.AddHeader(header.Key, header.Value);
                }
            }
            var result = _restClient.Execute(req)?.Content;
            return (result);
        }
        public string SendTestWallet(KeyedRequestPayload payload)
        {
            // var h = new Dictionary<string, string> { { "Referer", "Cscalp" } };
            var request = m_RequestArranger.Arrange(payload);
            var req = new RestRequest(request.Query, MapRequestMethod(request.Method));

            if (request.Body != null)
            {
                req.RequestFormat = DataFormat.Json;
                req.AddBody(request.Body);
            }

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    req.AddHeader(header.Key, header.Value);
                }
            }
            var wallet = _restClient.Execute(req)?.Content;
            return (wallet);
        }
        private static Method MapRequestMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.GET:
                    return Method.GET;
                case RequestMethod.POST:
                    return Method.POST;
                case RequestMethod.PUT:
                    return Method.PUT;
                case RequestMethod.DELETE:
                    return Method.DELETE;
                default:
                    throw new NotImplementedException();
            }
        }
        
        #endregion
        
        public QuerySymbolResponse QuerySymbolRequest() //для таблицы инструментов 
        {
            var request = new QuerySymbolRequest();
            var response = SendTest(request);
            var obj = m_HandlerComposition.HandleQuerySymbolResponse(response);
            return obj;
        }
         
        public GetWalletBalanceResponse GetWalletBalanceRequest() //для баланса
        { 
            var request = new GetWalletBalanceRequest();
            var response = SendTestWallet(request);
            var obj = m_HandlerComposition.HandleWalletBalanceResponse(response);
            return obj;
        }
    }
}