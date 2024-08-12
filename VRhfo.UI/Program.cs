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
//add ability to access Http Context
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
    pattern: "/view/{title}",
    defaults: new { controller = "Video", action = "Watch" }
);
app.MapControllerRoute(
    name: "liked",
    pattern: "/favorites",
    defaults: new { controller = "Home", action = "liked" }
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



app.Run();
