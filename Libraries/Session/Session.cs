using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Libraries.Session
{
    public class Session
    {
        private IHttpContextAccessor _contexto;

        public Session(IHttpContextAccessor contexto )
        {
            _contexto = contexto;
        }

        public void Cadastrar(string chave, string valor)
        {
            _contexto.HttpContext.Session.SetString(chave, valor);
        }

        public void Atualizar(string chave, string valor)
        {
            if (Existe(chave))
            {
                _contexto.HttpContext.Session.Remove(chave);
            }
            _contexto.HttpContext.Session.SetString(chave, valor);
        }

        public void Remover(string chave)
        {
            _contexto.HttpContext.Session.Remove(chave);
        }

        public string Consultar(string chave)
        {
            return _contexto.HttpContext.Session.GetString(chave);

        }

        public bool Existe(string chave)
        {
            if(_contexto.HttpContext.Session.GetString(chave) == null)
            {
                return false;
            }
            return true;
        }

        public void RemoverTodos()
        {
            _contexto.HttpContext.Session.Clear();
        }
    }
}
