using BrosMed_Insurance.Models.Reservation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BrosMed_Insurance.Data
{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options)
        {
        }
        public DbSet<Usluga> Usluga { get; set; }
        public DbSet<Terminy> Terminy { get; set; }
        public DbSet<Finalizacja> Finalizacja { get; set; }
        public DbSet<Godzina> Godziny { get; set;}
       
    }
}
