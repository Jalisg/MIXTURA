using Microsoft.EntityFrameworkCore;
using MIXTURAA.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DbContext con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Agregar Servicios de Controladores con Vistas
builder.Services.AddControllersWithViews();

// 3. Registrar el servicio de Sesión en Memoria
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // La sesión expira tras 60 min de inactividad
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. Habilitar la Sesión (DEBE ir antes de UseAuthorization)
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Ruta predeterminada del sistema (Redirige al Login si no hay inicio de sesión)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Accesos}/{action=Login}/{id?}");

app.Run();