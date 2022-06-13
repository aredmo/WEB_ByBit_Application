namespace WebApplication2.Entities
{
    public class QueryEntity
    {
        /// <summary>
        /// Валютная пара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Базовая валюта в паре
        /// </summary>
        public string BaseCurrency { get; set; }
        /// <summary>
        /// Статус валюты
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Максимальная цена
        /// </summary>
        public string MaxPrice { get; set; }
        /// <summary>
        ///  Минимальная цена
        /// </summary>
        public string MinPrice { get; set; }
        /// <summary>
        /// Комиссия тейкера
        /// </summary>
        public string TakerFee { get; set; }
        /// <summary>
        /// Комиссия мейкера
        /// </summary>
        public decimal MakerFee { get; set; }
    }
}