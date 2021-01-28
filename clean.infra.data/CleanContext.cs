using System;
using Microsoft.EntityFrameworkCore;

namespace clean.infra.data
{
    public class CleanContext : DbContext
    {
        public CleanContext(DbContextOptions<CleanContext> options):base (options)
        {

        }
    }
}
