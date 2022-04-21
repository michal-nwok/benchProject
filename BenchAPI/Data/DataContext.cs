using benchAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace benchAPI.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Stock>? Stocks {get; set; }
}