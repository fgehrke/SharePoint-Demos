using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace ClientObjectModelDemo.Entidades
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public Usuario(FieldUserValue usuario)
        {
            this.ID = usuario.LookupId;
            this.Nome = usuario.LookupValue;
        }
    }
}
