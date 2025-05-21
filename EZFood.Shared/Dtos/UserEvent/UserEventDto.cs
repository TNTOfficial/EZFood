using EZFood.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.UserEvent;

public class CreateUserEventDto
{
    public required string EventId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? TimeZone { get; set; }
    public EventType EventType { get; set; } = EventType.External;
}
