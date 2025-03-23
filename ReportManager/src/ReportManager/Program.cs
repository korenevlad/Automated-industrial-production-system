using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReportManager.DataAccess.Data;
using ReportManager.DataAccess.Repository;
using ReportManager.DataAccess.Repository.Implementation;
using ReportManager.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthentication();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

await SeedUsers(app.Services);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task SeedUsers(IServiceProvider serviceProvider)
{
    Console.WriteLine("Запуск SeedUsers...");
    try
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        Console.WriteLine("UserManager и RoleManager получены");

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "users.json");
        Console.WriteLine($"Ожидаемый путь к файлу: {filePath}");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл {filePath} не найден.");
            return;
        }

        var usersJson = await File.ReadAllTextAsync(filePath);
        Console.WriteLine($"Содержимое файла: {usersJson}");

        var users = JsonSerializer.Deserialize<List<UserSeedModel>>(usersJson);

        if (users == null || users.Count == 0)
        {
            Console.WriteLine("Файл users.json пуст или десериализация не удалась.");
            return;
        }

        foreach (var userData in users)
        {
            Console.WriteLine($"Обрабатываем пользователя: {userData.UserName}");

            if (!await roleManager.RoleExistsAsync(userData.Role))
            {
                Console.WriteLine($"Создаём роль: {userData.Role}");
                await roleManager.CreateAsync(new IdentityRole(userData.Role));
            }

            var user = await userManager.FindByNameAsync(userData.UserName);
            if (user == null)
            {
                Console.WriteLine($"Создаём пользователя {userData.UserName}");
                user = new User { UserName = userData.UserName };
                var result = await userManager.CreateAsync(user, userData.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userData.Role);
                    Console.WriteLine($"Пользователь {userData.UserName} создан с ролью {userData.Role}");
                }
                else
                {
                    Console.WriteLine($"Ошибка при создании пользователя {userData.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine($"Пользователь {userData.UserName} уже существует");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка в SeedUsers: {ex.Message}");
    }
}


record UserSeedModel(string UserName, string Password, string Role);
