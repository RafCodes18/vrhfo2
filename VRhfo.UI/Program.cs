using Microsoft.EntityFrameworkCore;
using VRhfo.PL;
using VRhfo.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//session configuration WE ADDED IN CLASS
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add VRhfoEntities
builder.Services.AddDbContext<VRhfoEntities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VRhfoEntities")));

// Add EmailClient
builder.Services.AddHttpClient<EmailClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("EmailService:BaseAddress"));
});

//add ability to access Http Context
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();


// Register routes directly on the app object (top-level route registrations)
app.MapControllerRoute(
    name: "ManageAccount",
    pattern: "/ManageAccount/u/{username}",
    defaults: new { controller = "User", action = "ManageAccount" }
);

app.MapControllerRoute(
    name: "VideoWatch",
    pattern: "/watch/{title}",
    defaults: new { controller = "Video", action = "Watch" }
);
app.MapControllerRoute(
    name: "liked",
    pattern: "/favorites",
    defaults: new { controller = "Home", action = "liked" }
    );
app.MapControllerRoute(
    name: "terms",
    pattern: "/terms-of-service",
    defaults: new { controller = "Home", action = "TermsAndConditions" }
    );
app.MapControllerRoute(
    name: "records",
    pattern: "/2257-statement",
    defaults: new { controller = "Home", action = "RecordsCompliance" }
    );
app.MapControllerRoute(
    name: "Category",
    pattern: "/c/{category}",
    defaults: new { controller = "Video", action = "Index" }
    );
app.MapControllerRoute(
    name: "login",
    pattern: "/login",
    defaults: new { controller = "Home", action = "Login" }
    );
app.MapControllerRoute(
    name: "join",
    pattern: "/JoinNow",
    defaults: new { controller = "Home", action = "JoinNow" }
    );
app.MapControllerRoute(
    name: "profile",
    pattern: "/uploads/{userid}",
    defaults: new { controller = "User", action = "Profile" }
    );
app.MapControllerRoute(
    name:"captions",
    pattern: "/captions",
    defaults: new { controller = "Home", action = "Captions"}
    );
// Default route
app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Video}/{action=Index}/{id?}"
);


Console.WriteLine($"SENDGRID_API_KEY: {Environment.GetEnvironmentVariable("SENDGRID_API_KEY")}");

app.Run();
