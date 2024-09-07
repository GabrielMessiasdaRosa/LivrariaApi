public interface ILivroRepository
{
    Task<IEnumerable<Livro>> GetLivros();

    Task<Livro> GetLivroPorId(int id);

    Task AdicionarLivro(Livro livro);

    Task AtualizarLivro(Livro livro);

    Task RemoverLivro(int id);

}