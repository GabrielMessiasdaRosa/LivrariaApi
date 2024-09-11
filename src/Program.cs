using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                // Carrega o appsettings.json e outras configurações
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"));
                });

                services.AddScoped<ILivroRepository, LivroRepository>();

            }).Build();

        var livroRepository = host.Services.GetRequiredService<ILivroRepository>();


        var httpListener = new HttpListener();
        httpListener.Prefixes.Add("http://*:5000/");
        httpListener.Start();
        Console.WriteLine("Ouvindo requisições em http://localhost:5000/");

        while (true)
        {
            var context = await httpListener.GetContextAsync();
            var request = context.Request;
            var response = context.Response;

            try
            {
                if (request.HttpMethod == "GET")
                {
                    if (request.Url?.AbsolutePath == "/livros")
                    {
                        var idString = request.QueryString["id"];
                        if (int.TryParse(idString, out int id))
                        {
                            var livro = await livroRepository.GetLivroPorId(id);
                            if (livro == null)
                            {
                                response.StatusCode = 404;
                                return;
                            }
                            var livroJson = JsonSerializer.Serialize(livro);
                            var livroBytes = Encoding.UTF8.GetBytes(livroJson);
                            response.ContentType = "application/json";
                            response.ContentLength64 = livroBytes.Length;
                            await response.OutputStream.WriteAsync(livroBytes);
                        }
                        else
                        {
                            var livros = await livroRepository.GetLivros();
                            var livrosJson = JsonSerializer.Serialize(livros);
                            var livrosBytes = Encoding.UTF8.GetBytes(livrosJson);
                            response.ContentType = "application/json";
                            response.ContentLength64 = livrosBytes.Length;
                            await response.OutputStream.WriteAsync(livrosBytes);
                        }
                    }
                }
                else if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/livros")
                {
                    var livroJson = new StreamReader(request.InputStream).ReadToEnd();
                    var livro = JsonSerializer.Deserialize<Livro>(livroJson);
                    await livroRepository.AdicionarLivro(livro);
                    response.StatusCode = 201;
                }
                else if (request.HttpMethod == "PATCH" && request.Url.AbsolutePath == "/livros")
                {
                    var livroJson = new StreamReader(request.InputStream).ReadToEnd();
                    var livro = JsonSerializer.Deserialize<Livro>(livroJson);
                    await livroRepository.AtualizarLivro(livro);
                    response.StatusCode = 204;
                }
                else if (request.HttpMethod == "DELETE" && request.Url?.AbsolutePath == "/livros")
                {
                    var id = int.Parse(request.QueryString["id"]);
                    await livroRepository.RemoverLivro(id);
                    response.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                response.StatusCode = 500;
            }
            finally
            {
                response.OutputStream.Close();
            }
        }
    }
}
