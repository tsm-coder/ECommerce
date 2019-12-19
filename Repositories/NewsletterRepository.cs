using ECommerce.Database;
using ECommerce.Models;
using ECommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private ECommerceContext _banco;

        public NewsletterRepository(ECommerceContext banco)
        {
            _banco = banco;
        }
        public void Cadastrar(NewsletterEmail newsleter)
        {
            _banco.NewsletterEmails.Add(newsleter);
            _banco.SaveChanges();
        }

        public IEnumerable<NewsletterEmail> ObterTodasInscricoes()
        {
            return _banco.NewsletterEmails.ToList();
        }
    }
}
