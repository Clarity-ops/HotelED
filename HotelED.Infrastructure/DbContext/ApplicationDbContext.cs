using HotelED.Core.Entities;
using HotelED.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelED.Infrastructure.DbContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(opts)
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // 1–1 між Booking і Payment
        builder.Entity<Booking>()
            .HasOne(b => b.Payment)
            .WithOne(p => p.Booking)
            .HasForeignKey<Payment>(p => p.BookingId);

        // Cascade delete: зображення при видаленні готелю
        builder.Entity<Hotel>()
            .HasMany(h => h.Images)
            .WithOne(i => i.Hotel)
            .OnDelete(DeleteBehavior.Cascade);

        // Інші FK: за замовчуванням Restrict
    }
}
