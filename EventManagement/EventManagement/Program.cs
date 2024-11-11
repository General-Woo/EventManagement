using EventManagement.Components;
using EventManagement.Services.EventService;
using EventManagement.Services.ReservationService;
using EventManagement.Services.UserService;
using EventManagement.Services.VenueService;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7082/")
});
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IEventService, EventService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//app.MapRazorComponents<App>()
//    .AddInteractiveWebAssemblyRenderMode()
//    .AddAdditionalAssemblies(typeof(EventManagement.Client._Imports).Assembly);
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();//.AddAdditionalAssemblies(typeof(EventManagement.Client._Imports).Assembly); ;

app.Run();
