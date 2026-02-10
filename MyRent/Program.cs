var builder = WebApplication.CreateBuilder(args);

var myRentSettings = builder.Configuration.GetSection("MyRentSettings");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("MyRentAPI", client =>
{
    client.BaseAddress = new Uri(myRentSettings["BaseUrl"]);
    client.DefaultRequestHeaders.Add("guid", myRentSettings["Guid"]);
    client.DefaultRequestHeaders.Add("token", myRentSettings["Token"]);
});

//logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
