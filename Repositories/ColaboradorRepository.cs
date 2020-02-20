using ECommerce.Models;
using ECommerce.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        public void Atualizar(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Colaborador Login(string Email, string Senha)
        {
            throw new NotImplementedException();
        }

        public Colaborador ObterColaborador(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> ObterTodosColaboradores()
        {
            throw new NotImplementedException();
        }
    }
}
