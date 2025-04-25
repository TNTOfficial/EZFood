using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.Response
{
    public class ResponseDto
    {
        public bool Result { get; set; } = true;
        public string? Message { get; set; }
    }
}
