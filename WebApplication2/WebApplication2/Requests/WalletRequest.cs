using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Requests
{
    public class WalletRequest
    {
        [Required]
        public string Сurrency { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public DateTime RecordingDate { get; set; }
    }
}