using BlogApi.Services;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using BlogApi.Core.IRepository;
using BlogApi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using BlogApi.IServices;
using BlogApi.Data.Repositories;

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
        return new X509Certificate2(path);
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

// Registro do IAsyncDocumentSession
builder.Services.AddScoped<IAsyncDocumentSession>(provider =>
    provider.GetRequiredService<IDocumentStore>().OpenAsyncSession());

// Adicionar serviços ao contêiner

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddUserStore<RavenUserStore>() // Use a implementação personalizada para usuários
    .AddRoleStore<RavenRoleStore>() // Use a implementação personalizada para papéis
    .AddDefaultTokenProviders();

// Registrando serviços e repositórios
builder.Services.AddScoped<ICommentsPostService, CommentsPostService>();
builder.Services.AddScoped<ILikePostService, LikePostService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

builder.Services.AddScoped<ICommentsPostRepository, CommentsPostRepository>();
builder.Services.AddScoped<ILikePostRepository, LikePostRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

builder.Services.AddControllers();
// Aprender mais sobre configuração Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();