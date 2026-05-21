using Microsoft.EntityFrameworkCore;

namespace IngressoJa.Contexts.Eventos.Infrastructure.dbContext
{
    public class EventosDbContext : DbContext
    {
        public EventosDbContext(DbContextOptions<EventosDbContext> options)
            : base(options)
        {
        }
    }
}