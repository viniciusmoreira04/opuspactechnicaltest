namespace Opuspac.TechnicalTest.Application.DTOs;

public class CreateProductDTO
{
    public UserDTO User { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}
