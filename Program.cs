using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Data;
using Server.Helpers;
using Server.Repositories.Contracts;
using Server.Repositories.Implementations;
using Shared.Project.Entities;
using System.Net;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Server.Repositories.Contrats;
using Microsoft.Extensions.FileProviders;


using Microsoft.AspNetCore.ResponseCompression;
using Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAzureStaticApps",
        builder =>
        {
            builder.WithOrigins("https://localhost:7010") // Your Azure URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


// Add services to the container.
builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(
 new MemoryCacheOptions
 {
     TrackStatistics = true,
     SizeLimit = 50 // Products.
 }));

builder.Services.AddControllers();

builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "BIZS5APP", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});



// Starting
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException(" Sorry, your connection is not found");
    }
    else
    {
        connectionString = connectionString + "user id=BIZS_APP;password=BIZS!_APP!P12Ca_2#;";
    }
    options.UseSqlServer(connectionString ??
        throw new InvalidOperationException(" Sorry, your connection is not found"));
});

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSection!.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Skip the default behavior
            context.HandleResponse();

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new { message = "Sie sind nicht authorisiert (angemeldet)" });
            return context.Response.WriteAsync(result);
        }
    };
});

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});



builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<Imitarbeiterdevicelogininterface, mitarbeiterdeviceloginRepository>();
builder.Services.AddScoped<Imitarbeiterdeviceinterface<Mitarbeiterdevice>, MitarbeiterdeviceRepository>();
builder.Services.AddScoped<Iadresseninterface<Adressen>, AdressenRepository>();
builder.Services.AddScoped<Iprojektinterface<Projekt>, ProjektRepository>();
builder.Services.AddScoped<IAdressensuchbegriffInterface<DtoAdressensuchbegriff>, AdressenSuchbegriffRepository>();
builder.Services.AddScoped<iprojektsuchbegriffinterface<DtoProjektsuchbegriff>, ProjektSuchbegriffRepository>();


builder.Services.AddScoped<iansprechpartnerinterface<Ansprechpartner>, AnsprechpartnerRepository>();
builder.Services.AddScoped<iAnsprechpartnersuchbegriffinterface<DtoAnsprechpartnersuchbegriff>, AnsprechpartnerSuchbegriffRepository>();

builder.Services.AddScoped<IArtikelinterface<Artikel>, ArtikelRepository>();
builder.Services.AddScoped<IArtikelsuchbegriffinterface<DtoArtikelsuchbegriff>, ArtikelsuchbegriffRepository>();
builder.Services.AddScoped<ILeistungInterface<Leistungen>, LeistungRepository>();
builder.Services.AddScoped<ILeistungsuchbegriffInterface<DtoLeistunguchbegriff>, LeistungsuchbegriffRepository>();
builder.Services.AddScoped<IFremdleistungInterface<Fremdleistung>, FremdleistungRepository>();
builder.Services.AddScoped<IFremdleistungsuchbegriffInterface<DtoFremdleistungSuchbegriff>, FremdleistungsuchbegriffRepository>();



builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "files")),
    RequestPath = "/files",
    OnPrepareResponse = ctx =>
    {
        Console.WriteLine($"Attempting to serve file: {ctx.File.PhysicalPath}");
    }
});

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAzureStaticApps");

app.UseCors();
app.MapHub<ChatHubs>("/chathub");

app.MapControllers();



app.Run();

//public class CorsSettings
//{
//    public string[] AllowedOrigins { get; set; }
//}
