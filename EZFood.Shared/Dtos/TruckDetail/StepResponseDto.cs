using EZFood.Domain.Entities.Enums;
using EZFood.Shared.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail
{
    public class StepResponseDto : ResponseDto
    {        
        public OnboardingStatus OnboardingStatus { get; set; }
    }
}
