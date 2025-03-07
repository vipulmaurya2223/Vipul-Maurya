var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.WebHost.UseUrls("https://localhost:7007");

var app = builder.Build();

// ✅ Correct order: Place UseStaticFiles before UseRouting
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization(); // 🔄 Added for completeness

// ✅ Correct Middleware Order
app.Use(async (context, next) =>
{
    if (context.Request.Path.ToString().Contains("hello"))
    {
        await context.Response.WriteAsync("Hello ");
    }
    await next();
});

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Hello2 ");
    await next();
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.ToString().Contains("end"))
    {
        await context.Response.WriteAsync("Terminating middleware chain");
        return; // Stops further execution
    }
    await next();
});

// ✅ Default Route Configuration (Make sure FirstController exists)
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=First}/{action=Index1}/{id?}");
});

app.Run();
