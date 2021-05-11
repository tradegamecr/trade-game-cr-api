using System;
using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class BaseDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
