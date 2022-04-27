namespace benchAPI.Models;

public class Stock
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal ClosePrice { get; set; }

    public StockDTO toDTO()
    {
        return new StockDTO
        {
            Id = this.Id,
            Date = this.Date,
            ClosePrice = this.ClosePrice.ToString()
        };
    }
}