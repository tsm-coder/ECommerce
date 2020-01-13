using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Libraries.Session;
using ECommerce.Models;
using Newtonsoft.Json;

namespace ECommerce.Libraries.Login
{
    public class LoginCliente
    {
        private string chave = "Login.Cliente";
        private Session.Session _session;

        public LoginCliente(Session.Session session)
        {
            _session = session;
        }

        public void Login(Cliente cliente)
        {

            string clienteJson = JsonConvert.SerializeObject(cliente);
            _session.Cadastrar(chave, clienteJson);
        }

        public Cliente GetCliente()
        {
            if (_session.Existe(chave))
            {
                string clienteJson = _session.Consultar(chave);
                return JsonConvert.DeserializeObject<Cliente>(clienteJson);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _session.RemoverTodos();
        }

    }
}
