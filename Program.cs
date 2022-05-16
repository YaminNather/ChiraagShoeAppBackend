using CatalogueMicroservice = ChiraagShoeAppBackend.CatalogueMicroservice;
using AuthenticationMicroservice = ChiraagShoeAppBackend.AuthenticationMicroservice;
using Supabase;

string url = "https://nzjzbovrzkimbccsxptb.supabase.co";
string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im56anpib3ZyemtpbWJjY3N4cHRiIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NTE2NzI2OTcsImV4cCI6MTk2NzI0ODY5N30.99oke2t10CO3VteLjLZwwE-zphKEBGTPOhNi3G4oBys";
// string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im56anpib3ZyemtpbWJjY3N4cHRiIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY0NDgyMzEyNiwiZXhwIjoxOTYwMzk5MTI2fQ.JwlSQBQGEV5EdV8dF5QY4fiEnPidSRKLMEg-JxB_75k";
await Supabase.Client.InitializeAsync(url, apiKey);

// await DatabaseCreater.CreateDatabase();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Supabase.Client>(Supabase.Client.Instance);

new CatalogueMicroservice.DependencyInjectionRegistrar().AddDependencies(builder);

new AuthenticationMicroservice.DependencyInjectionRegistrar().AddDependencies(builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
