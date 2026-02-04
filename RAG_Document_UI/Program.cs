using RAG_Document_UI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<EndpointSettings>(
    builder.Configuration.GetSection("Endpoints"));

builder.Services.AddHttpClient("AiApi", client =>
{
    var endpointUrl = builder.Configuration["Endpoints:ApiUrl"] ?? "https://localhost:7240";
    client.BaseAddress = new Uri("https://localhost:7240"); // API base URL
    client.Timeout = TimeSpan.FromSeconds(1200); // Set timeout to 10 min
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AiDocuments}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
