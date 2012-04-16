using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientObjectModelDemo.Entidades;
using Microsoft.SharePoint.Client;

namespace ClientObjectModelDemo.Helpers
{
   public static class TarefaConverterHelper
    {
       public static List<Tarefa> ItensParaTarefas(ListItemCollection itensTarefas)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            foreach (ListItem item in itensTarefas)
            {
                tarefas.Add(ItemParaTarefa(item));
            }

            return tarefas;
        }

       public static Tarefa ItemParaTarefa(ListItem item)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.ID = int.Parse(item["ID"].ToString());
            tarefa.Titulo = item["Title"].ToString();

            if (item["PercentComplete"] != null)
                tarefa.PercentComplete = item["PercentComplete"].ToString();

            // if (item["AssignetTo"] != null)
            //     tarefa.AssignetTo = item["AssignetTo"].ToString();

            DateTime dataSaida;

            if (item["DueDate"] != null && DateTime.TryParse(item["DueDate"].ToString(), out dataSaida))
            {
                tarefa.DueDate = dataSaida;
            }

            if (item["StartDate"] != null && DateTime.TryParse(item["StartDate"].ToString(), out dataSaida))
            {
                tarefa.StartDate = dataSaida;
            }

            if (item["Body"] != null)
                tarefa.Body = item["Body"].ToString();

            // tarefa.TaskGroup = item["TaskGroup"].ToString();
            tarefa.Predecessors = item["Predecessors"].ToString();
            tarefa.Priority = item["Priority"].ToString();

            if (item["Status"] != null)
                tarefa.Status = item["Status"].ToString();

            tarefa.Author = new Usuario(item["Author"] as FieldUserValue);
            tarefa.Editor = new Usuario(item["Editor"] as FieldUserValue);

            return tarefa;
        }

       public static ListItem TarefaParaItem(Tarefa tarefa, ListItem item)
        {
            item["Title"] = tarefa.Titulo;
            item["PercentComplete"] = tarefa.PercentComplete;
            // if (item["AssignetTo"] != null)
            //     tarefa.AssignetTo = item["AssignetTo"].ToString();
            item["StartDate"] = tarefa.StartDate;
            item["DueDate"] = tarefa.DueDate;
            item["Body"] = tarefa.Body;
            // tarefa.TaskGroup = item["TaskGroup"].ToString();
            item["Predecessors"] = tarefa.Predecessors;
            item["Priority"] = tarefa.Priority;
            item["Status"] = tarefa.Status;
            item["Author"] = tarefa.Author;
            item["Editor"] = tarefa.Editor;

            return item;
        }
    }
}
