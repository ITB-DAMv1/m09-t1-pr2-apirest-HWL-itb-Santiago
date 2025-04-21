using Server.Data;
using Server.DTO;
using Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class DbInitializer
{
    public static async Task SeedGamesAsync(ApplicationDbContext context)
    {
        if (!context.Games.Any())
        {
            var juegos = new List<Game>
        {
            new Game
            {
                Titulo = "The Legend of Zelda: Tears of the Kingdom",
                Descripcion = "La esperada secuela de 'Breath of the Wild' que expande el mundo de Hyrule con islas flotantes y nuevas mecánicas de construcción.",
                Desarrollador = "Nintendo EPD",
                Imagen = "https://assets.nintendo.com/image/upload/ar_16:9,c_lpad,w_656/b_white/f_auto/q_auto/ncom/software/switch/70010000063714/fb30eab428df3fc993b41c76e20f72e4d76d49734d17d31996b5ab61c414b117",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 5, 12)
            },
            new Game
            {
                Titulo = "Baldur's Gate 3",
                Descripcion = "Un RPG de fantasía que ofrece una narrativa profunda y opciones de juego variadas en el universo de Dungeons & Dragons.",
                Desarrollador = "Larian Studios",
                Imagen = "https://img.goodfon.com/original/1854x946/8/f0/baldur-s-gate-3-larian-studios-game-igra.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 8, 3)
            },
            new Game
            {
                Titulo = "Marvel's Spider-Man 2",
                Descripcion = "Peter Parker y Miles Morales unen fuerzas en esta emocionante aventura por la ciudad de Nueva York.",
                Desarrollador = "Insomniac Games",
                Imagen = "https://img.goodfon.ru/original/1920x1200/6/a0/marvel-s-spider-man-2-spider-man-game-chelovek-pauk-piter-pa.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 10, 20)
            },
            new Game
            {
                Titulo = "Alan Wake II",
                Descripcion = "Una secuela que combina horror psicológico y narrativa envolvente en un mundo oscuro y misterioso.",
                Desarrollador = "Remedy Entertainment",
                Imagen = "https://img.goodfon.com/original/1920x1200/1/18/alan-wake-2-survival-horror-remedy-entertainment-epic-game-1.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 10, 17)
            },
            new Game
            {
                Titulo = "Resident Evil 4 Remake",
                Descripcion = "Una reinvención del clásico juego de terror con gráficos mejorados y jugabilidad modernizada.",
                Desarrollador = "Capcom",
                Imagen = "https://img.goodfon.com/original/1920x1080/e/eb/resident-evil-4-resident-evil-4-remaker-resident-evil-4-wa-1.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 3, 24)
            },
            new Game
            {
                Titulo = "Final Fantasy XVI",
                Descripcion = "Una nueva entrega de la saga que ofrece un mundo medieval lleno de magia, conflictos políticos y criaturas épicas.",
                Desarrollador = "Square Enix",
                Imagen = "https://img.goodfon.com/original/1920x1200/4/af/final-fantasy-xvi-klaiv-rosfild-art.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 6, 22)
            },
            new Game
            {
                Titulo = "Diablo IV",
                Descripcion = "La esperada continuación de la saga que regresa a sus raíces oscuras con un mundo abierto y multijugador.",
                Desarrollador = "Blizzard Entertainment",
                Imagen = "https://img.goodfon.com/original/1920x1200/d/60/diablo-iv-blizzard-entertainment-2023-lilit-demonitsa-game-2.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 6, 6)
            },
            new Game
            {
                Titulo = "Starfield",
                Descripcion = "Una nueva IP de los creadores de Skyrim, que lleva a los jugadores a una aventura espacial épica.",
                Desarrollador = "Bethesda Game Studios",
                Imagen = "https://img.goodfon.com/original/1920x1200/a/6a/starfield-bethesda-softworks-microsoft-bethesda-game-studios.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 9, 6)
            },
            new Game
            {
                Titulo = "Street Fighter 6",
                Descripcion = "La última entrega de la icónica saga de lucha, con nuevos personajes y mecánicas de combate.",
                Desarrollador = "Capcom",
                Imagen = "https://img.goodfon.com/original/1920x1200/4/65/ryu-street-fighter-6-game-capcom-muzhchina.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 6, 2)
            },
            new Game
            {
                Titulo = "Hogwarts Legacy",
                Descripcion = "Un RPG de mundo abierto ambientado en el universo de Harry Potter, ambientado en el siglo XIX.",
                Desarrollador = "Portkey Games",
                Imagen = "https://img.goodfon.com/original/1920x1200/4/8f/hogwarts-legacy-khogvarts-nasledie-avalanche-software-warn-1.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 2, 10)
            },
            new Game
            {
                Titulo = "Assassin's Creed Mirage",
                Descripcion = "Un regreso a las raíces de la saga con una historia centrada en el sigilo y la exploración urbana.",
                Desarrollador = "Ubisoft",
                Imagen = "https://img.goodfon.com/original/1920x1200/5/1f/assassin-s-creed-mirage-basim-assasin.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 10, 5)
            },
            new Game
            {
                Titulo = "Forza Motorsport",
                Descripcion = "La última entrega de la serie de simulación de carreras, con gráficos realistas y física mejorada.",
                Desarrollador = "Turn 10 Studios",
                Imagen = "https://img.goodfon.com/original/1920x1200/1/fa/forza-motorsport-5-mashina-5803.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 10, 10)
            },
            new Game
            {
                Titulo = "Lies of P",
                Descripcion = "Un juego de acción inspirado en la historia de Pinocho, con una estética oscura y desafiante.",
                Desarrollador = "Neowiz Games",
                Imagen = "https://img.goodfon.com/original/1920x1200/9/23/lies-of-p-round-8-studio-neowiz-game-igra-rpg.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2023, 9, 19)
            },
            new Game
            {
                Titulo = "Armored Core VI: Fires of Rubicon",
                Descripcion = "El regreso de la saga de mechas de FromSoftware, con combates intensos y personalización profunda.",
                Desarrollador = "FromSoftware",
                Imagen = "https://img.goodfon.com/original/1600x1200/3/cd/armored-core-mecha-robot.jpg",
                Numero_Votos = 0,
                Fecha_Lanzamiento = new DateTime(2022, 8, 25)
            }
        };

            context.Games.AddRange(juegos);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        context.Database.Migrate();

        // Crear rol si no existe
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Crear usuario Admin si no existe
        var usuarioAdmin = await userManager.FindByEmailAsync("admin@xat.com");
        if (usuarioAdmin == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@xat.com",
                EmailConfirmed = true,
                Name = "Administrador",
                Surname = "General"
            };
            var result = await userManager.CreateAsync(user, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        // Crear usuario normal si no existe
        var usuarioNormal = await userManager.FindByEmailAsync("normal@xat.com");
        if (usuarioNormal == null)
        {
            var user = new ApplicationUser
            {
                UserName = "normalUser",
                Email = "normal@xat.com",
                EmailConfirmed = true,
                Name = "Normal",
                Surname = "User"
            };
            var result = await userManager.CreateAsync(user, "Normal123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}
