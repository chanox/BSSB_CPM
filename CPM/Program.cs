using System.Net;
using CPM_Project.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using WebMarkupMin.AspNetCore6;
using Westwind.AspNetCore.LiveReload;

var builder = WebApplication.CreateBuilder(args);

GlobalVariable _GlobalVariable = new()
{
    ConnectionDB = builder.Configuration.GetValue<string>("ConnectionStrings:ConnectionDB"),
    ContentRoot = builder.Environment.ContentRootPath,
    WebRoot = builder.Environment.WebRootPath,
    ServiceAddress = builder.Configuration.GetValue<string>("ServiceAddress")
};

// Add services to LiveReload.
builder.Services.AddLiveReload(config =>
{
    config.LiveReloadEnabled = true;
    //config.FolderToMonitor = Path.GetFullname(Path.Combine(Env.ContentRootPath,"..")) ;
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// JSON Format
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    }); // HURUF BESAR JSON

// XML Format
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

builder.Services.AddWebMarkupMin(o =>
    {
        o.AllowMinificationInDevelopmentEnvironment = false;
        o.AllowCompressionInDevelopmentEnvironment = false;
    })
    .AddHtmlMinification()
    .AddXmlMinification()
    .AddHttpCompression();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "BaNGsaT";
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

//builder.Services.AddSession();

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseLiveReload();

app.UseWebMarkupMin();
// app.UseHttpsRedirection();
app.UseStaticFiles();

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(
//         Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
//     RequestPath = "/wwwroot",
//     ServeUnknownFileTypes = false,
//     DefaultContentType = "text/plain",
//     OnPrepareResponse = ctx =>
//     {
//         ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
//         ctx.Context.Response.Headers["Pragma"] = "no-cache";
//         ctx.Context.Response.Headers["Expires"] = "-1";
//         ctx.Context.Response.Headers["X-Content-Type-Options"] = "nosniff";
//     }
// });

// app.UseStatusCodePages(async context => {
//     if (context.HttpContext.Response.StatusCode == 404)
//     {
//         context.HttpContext.Response.Redirect("/User/Login");
//     }
// });

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.Use( async (context, next) =>
{
    HostString host = context.Request.Host;
    
    context.Request.EnableBuffering(); // Use For Server Can Read Body Request

    context.Response.Headers.Remove("X-Powered-By");
    //context.Response.Headers.Add("Content-Security-Policy", $"default-src 'self' {host}");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    context.Response.Headers.Add("Access-Control-Allow-Origin", context.Request.Headers["Origin"]);
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    
    await next();
});

app.UseCors("CorsPolicy");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();