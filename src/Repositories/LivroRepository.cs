using Microsoft.EntityFrameworkCore;

public class LivroRepository : ILivroRepository
{
    private readonly AppDbContext _context;

    public LivroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Livro>> GetLivros()
    {
        var livros = await _context.Livros.ToListAsync();
        return livros;
    }

    public async Task<Livro> GetLivroPorId(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null)
        {
            return null;
        }
        return livro;
    }

    public async Task AdicionarLivro(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
        var returnMessage = $"Livro {livro.Titulo} adicionado com sucesso!";
        Console.WriteLine(returnMessage);
    }

    public async Task AtualizarLivro(Livro livro)
    {
        var trackedEntity = await _context.Livros.FindAsync(livro.Id);
        if (trackedEntity != null)
        {
            _context.Entry(trackedEntity).State = EntityState.Detached;
        }

        _context.Entry(livro).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task RemoverLivro(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null)
        {
            return;
        }
        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();
    }

}