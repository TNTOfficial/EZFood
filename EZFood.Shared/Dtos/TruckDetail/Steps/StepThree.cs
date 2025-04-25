using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail.Steps
{
    public class StepThree
    {
        public string? BusinessDescription { get; set; }
        public int? BussinessStartYear { get; set; }
        public string? EIN { get; set; }
        public bool IsBreakfast { get; set; } = false;
        public bool IsLunch { get; set; } = false;
        public bool IsDinner { get; set; } = false;
        public Decimal? MinimumGuaranteeAmount { get; set; }
        public string? COI { get; set; }
        public string? W9 { get; set; }
        public string? ServeSafeCertificate { get; set; }
        public string? DCHCertificate { get; set; }
    }
}
