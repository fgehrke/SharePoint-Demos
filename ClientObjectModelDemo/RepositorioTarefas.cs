using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using ClientObjectModelDemo.Entidades;
using System.Net;
using ClientObjectModelDemo.Helpers;

namespace ClientObjectModelDemo
{
    public class RepositorioTarefas
    {
        private ClientContext Contexto;
        private Web Web;
        private List ListaTarefas;

        public RepositorioTarefas()
        {
            // http://code.msdn.microsoft.com/Remote-Authentication-in-b7b6f43c/sourcecode?fileId=21439&pathId=1168218626
            this.Contexto = ClaimClientContext.GetAuthenticatedContext(Constants.ENDRECO_SITE);
            this.Web = Contexto.Web;
            this.ListaTarefas = this.Web.Lists.GetByTitle("Tarefas");
        }

        public List<Tarefa> ObterTodos()
        {
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View/>";
            ListItemCollection itens = ListaTarefas.GetItems(camlQuery);

            Contexto.Load(itens);
            Contexto.ExecuteQuery();

            return TarefaConverterHelper.ItensParaTarefas(itens);
        }

        public List<Tarefa> ObterPorFiltro(string titulo)
        {
            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View><Query><Where> " +
                                "<Contains> " +
                                "<FieldRef Name='Title' /> " +
                                "<Value Type='Text'>" + titulo + "</Value> " +
                                "</Contains> " +
                                "</Where></Query></View> ";

            ListItemCollection itens = ListaTarefas.GetItems(camlQuery);

            Contexto.Load(itens);
            Contexto.ExecuteQuery();

            return TarefaConverterHelper.ItensParaTarefas(itens);
        }

        public Tarefa ObterPorID(int id)
        {
            ListItem item = ListaTarefas.GetItemById(id);

            Contexto.Load(item);
            Contexto.ExecuteQuery();

            return TarefaConverterHelper.ItemParaTarefa(item);
        }

        public int Salvar(Tarefa tarefa)
        {
            ListItem itemTarefa;

            if (tarefa.ID == (default(int)))
            {
                ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                itemTarefa = ListaTarefas.AddItem(itemCreateInfo);
            }
            else
            {
                itemTarefa = ListaTarefas.GetItemById(tarefa.ID);
                Contexto.Load(itemTarefa);
                Contexto.ExecuteQuery();
            }

            itemTarefa = TarefaConverterHelper.TarefaParaItem(tarefa, itemTarefa);

            itemTarefa.Update();
            Contexto.ExecuteQuery();

            return itemTarefa.Id;
        }

        public void Excluir(int id)
        {
            ListItem tarefa = ListaTarefas.GetItemById(id);

            tarefa.DeleteObject();

            Contexto.ExecuteQuery();
        }
    }
}
