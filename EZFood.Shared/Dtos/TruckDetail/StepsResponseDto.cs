using EZFood.Shared.Dtos.CuisineType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail
{
    public class StepsResponseDto<T>
    {
        public bool Result { get; set; } = true;
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}

