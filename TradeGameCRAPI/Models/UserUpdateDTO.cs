using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Models
{
    public class UserUpdateDTO : UserCreateDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
