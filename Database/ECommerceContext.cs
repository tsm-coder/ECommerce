using ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Database
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext>options): base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }

    }
}
