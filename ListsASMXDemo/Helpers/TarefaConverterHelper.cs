using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ListsASMXDemo.Entidades;

namespace ListsASMXDemo.Helpers
{
   public static class TarefaConverterHelper
    {
        public static List<Tarefa> ItensParaTarefas(XmlNode itens)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            XmlDocument xmlThumb = new XmlDocument();
            xmlThumb.Load(new XmlNodeReader(itens));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlThumb.NameTable);
            nsmgr.AddNamespace("z", "#RowsetSchema");
            nsmgr.AddNamespace("rs", "urn:schemas-microsoft-com:rowset");
            nsmgr.AddNamespace("rootNS", "http://schemas.microsoft.com/sharepoint/soap/");

            XmlNodeList node = xmlThumb.SelectNodes("/rootNS:listitems/rs:data/z:row", nsmgr);

            foreach (XmlNode item in node)
            {
                tarefas.Add(ItemParaTarefa(item));
            }

            return tarefas;
        }

        public static Tarefa ItemParaTarefa(XmlNode item)
        {
            Tarefa tarefa = new Tarefa();

            tarefa.ID = int.Parse(item.Attributes["ows_ID"].Value);
            tarefa.Titulo = item.Attributes["ows_Title"].Value;

            if (item.Attributes["ows_PercentComplete"] != null)
                tarefa.PercentComplete = item.Attributes["ows_PercentComplete"].Value;

            // if (item["AssignetTo"] != null)
            //     tarefa.AssignetTo = item["AssignetTo"].ToString();

            DateTime dataSaida;

            if (item.Attributes["ows_DueDate"] != null && DateTime.TryParse(item.Attributes["ows_DueDate"].Value, out dataSaida))
            {
                tarefa.DueDate = dataSaida;
            }

            if (item.Attributes["ows_StartDate"] != null && DateTime.TryParse(item.Attributes["ows_StartDate"].Value, out dataSaida))
            {
                tarefa.StartDate = dataSaida;
            }

            if (item.Attributes["ows_Body"] != null)
                tarefa.Body = item.Attributes["ows_Body"].Value;

            // tarefa.TaskGroup = item["TaskGroup"].ToString();
            tarefa.Predecessors = item.Attributes["ows_Predecessors"].Value;
            tarefa.Priority = item.Attributes["ows_Priority"].Value;

            if (item.Attributes["ows_Status"] != null)
                tarefa.Status = item.Attributes["ows_Status"].Value;

            //tarefa.Author = new Usuario(item["Author"] as FieldUserValue);
            //tarefa.Editor = new Usuario(item["Editor"] as FieldUserValue);


            return tarefa;
        }

        public static String TarefaParaXML(Tarefa tarefa)
        {
            StringBuilder xml = new StringBuilder();

            if (tarefa.ID == 0)
            {
                xml.Append("<Method ID = '0' Cmd='New'>");
            }
            else
            {
                xml.Append("<Method ID = '0' Cmd='Update'>");
                xml.Append("<Field Name='ID'>"+ tarefa.ID.ToString() +"</Field>");
            }

            xml.Append("<Field Name='Title'>" + tarefa.Titulo + "</Field>");
            xml.Append("</Method>");

            //"<Method ID='1' Cmd='Update'><Field Name='ID'>6</Field><Field Name='Title'>Modified sixth item</Field></Method>" +
            //"<Method ID='2' Cmd='Update'><Field Name='ID'>7</Field><Field Name='Title'>Modified seventh item</Field></Method>" +
            //"<Method ID='3' Cmd='Delete'><Field Name='ID'>5</Field></Method>" +
            //"<Method ID='4' Cmd='New'><Field Name='Title'>" + tarefa.Titulo + "</Field></Method>";

            //item["Title"] = tarefa.Titulo;
            //item["PercentComplete"] = tarefa.PercentComplete;
            //// if (item["AssignetTo"] != null)
            ////     tarefa.AssignetTo = item["AssignetTo"].ToString();
            //item["StartDate"] = tarefa.StartDate;
            //item["DueDate"] = tarefa.DueDate;
            //item["Body"] = tarefa.Body;
            //// tarefa.TaskGroup = item["TaskGroup"].ToString();
            //item["Predecessors"] = tarefa.Predecessors;
            //item["Priority"] = tarefa.Priority;
            //item["Status"] = tarefa.Status;
            //item["Author"] = tarefa.Author;
            //item["Editor"] = tarefa.Editor;

            return xml.ToString();
        }
    }
}
