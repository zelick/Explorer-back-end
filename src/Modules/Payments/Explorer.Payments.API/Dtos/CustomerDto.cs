namespace Explorer.Payments.API.Dtos;

public class CustomerDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public List<TourPurchaseTokenDto>? TourPurchaseTokens { get; set; }
    public long ShoppingCartId { get; set; }
}