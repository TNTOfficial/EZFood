namespace EZFood.Shared.Dtos.CuisineType;
public class CuisineTypeDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

}
