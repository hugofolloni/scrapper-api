using Microsoft.EntityFrameworkCore;

namespace scrapper_api.Models;

public class PageContext : DbContext {
    public PageContext(DbContextOptions<PageContext> options) : base(options){}

    public DbSet<Page> PageItems {get; set;} = null!;
}
