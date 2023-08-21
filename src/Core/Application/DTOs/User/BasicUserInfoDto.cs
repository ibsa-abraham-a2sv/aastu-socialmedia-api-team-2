using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class BasicUserInfoDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public byte[] Image { get; set; }
        public string Bio { get; set; }
    }
}