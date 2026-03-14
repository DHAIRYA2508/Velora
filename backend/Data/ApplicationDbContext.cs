using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>(e => {
                e.HasKey(m => m.Id);
                e.Property(m => m.Price).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Order>(e => {
                e.HasKey(o => o.Id);
                e.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
                e.HasMany(o => o.OrderItems).WithOne(oi => oi.Order)
                 .HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(e => {
                e.HasKey(oi => oi.Id);
                e.Property(oi => oi.Price).HasColumnType("decimal(10,2)");
                e.HasOne(oi => oi.MenuItem).WithMany(m => m.OrderItems)
                 .HasForeignKey(oi => oi.MenuItemId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(e => {
                e.HasKey(u => u.Id);
                e.HasIndex(u => u.Email).IsUnique();
            });
        }
    }

    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                context.Users.Add(new User
                {
                    Username = "Admin",
                    Email = "admin@velora.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = "Admin"
                });
                context.SaveChanges();
            }

            if (!context.MenuItems.Any())
            {
                                context.MenuItems.AddRange(new List<MenuItem>
                {
                    // STARTERS
                    new() { Name = "Burrata Caprese", Description = "Fresh burrata, heirloom tomatoes, basil oil, Sicilian sea salt, and aged 25-year balsamic.", Price = 1800.00m, Category = "Starters", ImageUrl = "https://images.unsplash.com/photo-1592417817098-8fd3d9eb14a5?w=600&h=400&fit=crop" },
                    new() { Name = "Artisan Cheese Board", Description = "Selection of five aged European cheeses, honeycomb, quince paste, walnuts, and sourdough.", Price = 950.00m, Category = "Starters", ImageUrl = "https://images.unsplash.com/photo-1452195100486-9cc805987862?w=600&h=400&fit=crop" },
                    new() { Name = "Wild Mushroom Bruschetta", Description = "Toasted sourdough topped with sautéed wild mushrooms, fresh thyme, garlic, and shaved truffle.", Price = 1600.00m, Category = "Starters", ImageUrl = "https://images.unsplash.com/photo-1572695157366-5e585ab2b69f?w=600&h=400&fit=crop" },
                    new() { Name = "Roasted Beetroot Tartare", Description = "Slow-roasted beetroot, Dijon mustard, capers, shallots, lemon zest, and herbs with garlic crostini.", Price = 1400.00m, Category = "Starters", ImageUrl = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=600&h=400&fit=crop" },
                    new() { Name = "French Onion Soup", Description = "Slow-caramelised onion broth, toasted baguette, and a golden Gruyère crust.", Price = 1200.00m, Category = "Starters", ImageUrl = "https://images.unsplash.com/photo-1547592166-23ac45744acd?w=600&h=400&fit=crop" },

                    // MAINS
                    new() { Name = "Truffle Risotto", Description = "Carnaroli risotto, 24-month Parmigiano-Reggiano, black truffle shavings, and truffle oil.", Price = 820.00m, Category = "Mains", ImageUrl = "https://images.unsplash.com/photo-1476124369491-e7addf5db371?w=600&h=400&fit=crop" },
                    new() { Name = "Pasta alla Norma", Description = "Crispy roasted eggplant in a robust tomato sauce with garlic, basil and olive oil, tossed with rigatoni and finished with ricotta salata.", Price = 2800.00m, Category = "Mains", ImageUrl = "https://images.unsplash.com/photo-1621996346565-e3dbc646d9a9?w=600&h=400&fit=crop" },
                    new() { Name = "Wild Mushroom Wellington", Description = "Golden puff pastry filled with a duxelles of wild mushrooms, spinach, and ricotta with a red wine jus.", Price = 3600.00m, Category = "Mains", ImageUrl = "https://images.unsplash.com/photo-1574894709920-11b28e7367e3?w=600&h=400&fit=crop" },
                    new() { Name = "Pumpkin Gnocchi", Description = "Hand-rolled pumpkin gnocchi, sage brown butter, toasted pine nuts, and aged Parmesan.", Price = 3200.00m, Category = "Mains", ImageUrl = "https://images.unsplash.com/photo-1548869206-93b036288d7e?w=600&h=400&fit=crop" },
                    new() { Name = "Roasted Cauliflower Steak", Description = "Whole roasted cauliflower, harissa butter, golden raisins, crispy capers, and herb yoghurt.", Price = 2600.00m, Category = "Mains", ImageUrl = "https://images.unsplash.com/photo-1568625365131-079e026a927d?w=600&h=400&fit=crop" },

                    // DESSERTS
                    new() { Name = "Valrhona Chocolate Fondant", Description = "Warm Valrhona 70% dark chocolate fondant, Tahitian vanilla bean ice cream, and gold leaf.", Price = 180.00m, Category = "Desserts", ImageUrl = "https://images.unsplash.com/photo-1606313564200-e75d5e30476c?w=600&h=400&fit=crop" },
                    new() { Name = "Crème Brûlée", Description = "Classic Madagascan vanilla crème brûlée, caramelised sugar crust, and seasonal berry compote.", Price = 140.00m, Category = "Desserts", ImageUrl = "https://images.unsplash.com/photo-1470124182917-cc6e71b22ecc?w=600&h=400&fit=crop" },
                    new() { Name = "Mille-Feuille", Description = "Layers of caramelised puff pastry, Diplomat cream, fresh raspberries, and rose water glaze.", Price = 160.00m, Category = "Desserts", ImageUrl = "https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=600&h=400&fit=crop" },
                    new() { Name = "Lemon Tart", Description = "Crisp buttery pastry shell filled with silky lemon curd, Italian meringue, and candied zest.", Price = 130.00m, Category = "Desserts", ImageUrl = "https://images.unsplash.com/photo-1519915028121-7d3463d20b13?w=600&h=400&fit=crop" },
                    new() { Name = "Strawberry Pavlova", Description = "Crisp meringue, whipped vanilla cream, fresh strawberries, passion fruit curd, and mint.", Price = 150.00m, Category = "Desserts", ImageUrl = "https://images.unsplash.com/photo-1488477181946-6428a0291777?w=600&h=400&fit=crop" },
                    new() { Name = "Tiramisu", Description = "Espresso-soaked savoiardi, mascarpone cream, and a dusting of Valrhona cocoa powder.", Price = 140.00m, Category = "Desserts", ImageUrl = "https://images.unsplash.com/photo-1571877227200-a0d98ea607e9?w=600&h=400&fit=crop" },

                    // BEVERAGES
                    new() { Name = "Sommelier Wine Pairing", Description = "Three-glass curated wine pairing selected by our sommelier to complement your meal.", Price = 150.00m, Category = "Beverages", ImageUrl = "https://images.unsplash.com/photo-1510812431401-41d2bd2722f3?w=600&h=400&fit=crop" },
                    new() { Name = "Signature Velora Cocktail", Description = "Aged rum, elderflower liqueur, fresh lime, cucumber, and sparkling water with edible flowers.", Price = 220.00m, Category = "Beverages", ImageUrl = "https://images.unsplash.com/photo-1514362545857-3bc16c4c7d1b?w=600&h=400&fit=crop" },
                    new() { Name = "Champagne Flute", Description = "Chilled Moët & Chandon Brut Impérial. Crisp notes of green apple, citrus, and brioche.", Price = 180.00m, Category = "Beverages", ImageUrl = "https://images.unsplash.com/photo-1560512823-829485b8bf24?w=600&h=400&fit=crop" },
                    new() { Name = "Artisan Lemonade", Description = "Freshly pressed lemon, house-made elderflower syrup, crushed ice, and sparkling water.", Price = 80.00m, Category = "Beverages", ImageUrl = "https://images.unsplash.com/photo-1523677011781-c91d1bbe2f9e?w=600&h=400&fit=crop" },
                    new() { Name = "Cold Brew Coffee", Description = "Single-origin Ethiopian cold brew, steeped 18 hours, served over crystal ice with oat milk.", Price = 90.00m, Category = "Beverages", ImageUrl = "https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=600&h=400&fit=crop" },
                    new() { Name = "Velvet Rose Mocktail", Description = "Rose water, raspberry purée, lychee juice, fresh lime, and soda — a non-alcoholic elegance.", Price = 120.00m, Category = "Beverages", ImageUrl = "https://images.unsplash.com/photo-1497534446932-c925b458314e?w=600&h=400&fit=crop" },
                });
                context.SaveChanges();
            }
        }
    }
}
