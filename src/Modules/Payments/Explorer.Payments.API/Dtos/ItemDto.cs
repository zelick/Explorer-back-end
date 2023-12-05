namespace Explorer.Payments.API.Dtos;

public class ItemDto
{
    public long SellerId{ get; set; }
    public long ItemId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Type { get; set; }
    public List<long> BundleItemIds { get; set; }
}