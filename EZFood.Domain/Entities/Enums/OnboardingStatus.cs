using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Domain.Entities.Enums;

public enum OnboardingStatus
{
    Step1 = 0,
    Step2 = 1,
    Step3 = 2,
    Step4 = 3,
    Step5 = 4,
    Pending = 5,
    Submitted = 6,
    Approved = 7,
    Rejected = 8,
    Incomplete = 9,
    Objection = 10,    
}
