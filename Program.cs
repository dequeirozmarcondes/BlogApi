using BlogApi.Services;
using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using static BlogApi.Data.Repositories;
using static BlogApi.Models.IPostRepositories;

var builder = WebApplication.CreateBuilder(args);

// Método auxiliar para carregar o certificado
static X509Certificate2 LoadCertificate(string path)
{
    if (string.IsNullOrWhiteSpace(path))
    {
        throw new ArgumentException("O caminho do certificado não pode ser nulo ou vazio.", nameof(path));
    }

    var certType = X509Certificate2.GetCertContentType(path);

    if (certType == X509ContentType.Pfx || certType == X509ContentType.Authenticode)
    {
#pragma warning disable SYSLIB0057
        return new X509Certificate2(path);
#pragma warning restore SYSLIB0057
    }

    throw new CryptographicException($"O tipo do certificado no caminho especificado ({certType}) não é compatível.");
}

// Configure RavenDB with client certificate
builder.Services.AddSingleton<IDocumentStore>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var certificatePath = configuration["RavenDB:CertificatePath"];

    if (string.IsNullOrEmpty(certificatePath))
    {
        throw new ArgumentNullException(nameof(certificatePath), "O caminho do certificado não pode ser nulo ou vazio.");
    }

    // Carrega o certificado usando o método auxiliar
    var certificate = LoadCertificate(certificatePath);

    var store = new DocumentStore
    {
        Urls = new[] { "https://a.dequeirozmarcondes.ravendb.community" },
        Database = "Blog",
        Certificate = certificate
    };
    store.Initialize();
    return store;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
