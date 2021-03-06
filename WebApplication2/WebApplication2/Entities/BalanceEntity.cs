using System;

namespace WebApplication2.Entities
{
    public class BalanceEntity : BaseEntity

    {
    /// <summary>
    /// Текущий баланс аккаунта
    /// </summary>
    public decimal WalletBalance { get; set; }

    /// <summary>
    /// Валюта
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Дата записи 
    /// </summary>
    public DateTime RecordingDate { get; set; }
    
    }
}