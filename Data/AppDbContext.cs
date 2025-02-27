using Microsoft.EntityFrameworkCore;
using UrlShorteners.Models;

namespace UrlShorteners.Data;

public class AppDbContext : DbContext
{
    public DbSet<Url> Urls { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}