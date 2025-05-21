using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.UserEvent;

public class UpdateUserEventDto
{
    public Guid Id { get; set; }
    public List<CreateUserEventDto> UserEvents { get; set; } = [];
}
