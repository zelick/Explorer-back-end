namespace Explorer.Payments.API.Dtos;

public class ShoppingCartDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public List<OrderItemDto>? Items { get; set; }
    public double Price { get; set; }
}