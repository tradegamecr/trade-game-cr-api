﻿using System.ComponentModel.DataAnnotations;

namespace TradeGameCRAPI.Models
{
    public class UserUpdateDTO : UserCreateDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
