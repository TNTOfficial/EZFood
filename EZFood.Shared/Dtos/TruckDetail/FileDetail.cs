using EZFood.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail
{
    public class FileDetail
    {
        public FileType FileType { get; set; }
        public string? File { get; set; }
    }
}

