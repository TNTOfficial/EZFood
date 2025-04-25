using EZFood.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFood.Shared.Dtos.TruckDetail;

public class DocumentDto
{
    public DocumentType Name { get; set; }
    public FileType FileType { get; set; }
    public string? MimeType { get; set; }
}
