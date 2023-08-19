using Microsoft.AspNetCore.ResponseCompression;
using PlaningPoker.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(options =>
{
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes
        .Concat(new[] { "application/octet-stream" });
});

builder.Services.AddSingleton<RoomState>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseResponseCompression();

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapHub<PlaningPokerHub>("/pokerhub");
app.MapFallbackToFile("index.html");

app.Run();

