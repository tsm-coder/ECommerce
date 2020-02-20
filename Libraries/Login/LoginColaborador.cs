using ECommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Libraries.Login
{
    public class LoginColaborador
    {
        private string chave = "Login.Colaborador";
        private Session.Session _session;

        public LoginColaborador(Session.Session session)
        {
            _session = session;
        }

        public void Login(Colaborador colaborador)
        {

            string colaboradorJson = JsonConvert.SerializeObject(colaborador);
            _session.Cadastrar(chave, colaboradorJson);
        }

        public Colaborador GetColaborador()
        {
            if (_session.Existe(chave))
            {
                string colaboradorJson = _session.Consultar(chave);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJson);
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
