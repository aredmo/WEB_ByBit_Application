namespace WebApplication2.Entities
{
    public class InfoWallet : BaseEntity
    {
        /// <summary>
        /// Валюта
        /// </summary>
        public string Сurrency { get; set; }
        /// <summary>
        /// Баланс
        /// </summary>
        public decimal Balance { get; set; }
    }
}