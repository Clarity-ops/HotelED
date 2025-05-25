var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseHsts();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
