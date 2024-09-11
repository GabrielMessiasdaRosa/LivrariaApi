using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=sql-server;Database=LivrariaDb;User Id=sa;Password=TwtBv4z!Q*AJhA5u;TrustServerCertificate=True;Encrypt=False;");
        }
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Livro> Livros { get; set; }
}