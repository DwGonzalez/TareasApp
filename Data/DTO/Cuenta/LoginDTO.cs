﻿using System.ComponentModel.DataAnnotations;

namespace TareasApp.Data.DTO.Cuenta
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
