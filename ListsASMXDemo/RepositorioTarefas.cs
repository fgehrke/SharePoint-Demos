using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ListsASMXDemo.Entidades;
using ListsASMXDemo.Helpers;

namespace ListsASMXDemo
{
    public class RepositorioTarefas
    {
        private SharePointLists.Lists Contexto;

        public RepositorioTarefas()
        {
            Contexto = ClaimClientContext.GetAuthenticatedContext(Constants.ENDRECO_SITE);
            Contexto.Url = Constants.ENDRECO_SITE + "/_vti_bin/Lists.asmx";
        }

        public List<Tarefa> ObterTodos()
        {
            XmlDocument xmlDoc = new System.Xml.XmlDocument();

            XmlNode ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "");
            XmlNode ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "");
            
            XmlNode ndQueryOptions = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "");
            ndQueryOptions.InnerXml = "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>";

            XmlNode ndListItems = Contexto.GetListItems("Tarefas", null, ndQuery, ndViewFields, null, ndQueryOptions, null);

            return TarefaConverterHelper.ItensParaTarefas(ndListItems);
        }

        public Tarefa ObterPorID(int id)
        {
            XmlDocument xmlDoc = new System.Xml.XmlDocument();

            XmlNode ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "");
            XmlNode ndViewFields =
                xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "");
            XmlNode ndQueryOptions =
                xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "");

            ndQueryOptions.InnerXml = "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>";

            ndQuery.InnerXml = "<Where><Eq><FieldRef Name='ID'/>" +
                               "<Value Type='Number'>" + id.ToString() + "</Value></Eq></Where>";

            XmlNode ndListItems = Contexto.GetListItems("Tarefas", null, ndQuery, ndViewFields, null, ndQueryOptions, null);

            return TarefaConverterHelper.ItensParaTarefas(ndListItems)[0];
        }

        public List<Tarefa> ObterPorFiltro(string titulo)
        {
            XmlDocument xmlDoc = new System.Xml.XmlDocument();

            XmlNode ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "");
            XmlNode ndViewFields =
                xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "");
            XmlNode ndQueryOptions =
                xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "");

            ndQueryOptions.InnerXml = "<IncludeMandatoryColumns>FALSE</IncludeMandatoryColumns>";

            ndQuery.InnerXml = "<Where><Contains><FieldRef Name='Title'/>" +
                               "<Value Type='Text'>" + titulo + "</Value></Contains></Where>";

            XmlNode ndListItems = Contexto.GetListItems("Tarefas", null, ndQuery, ndViewFields, null, ndQueryOptions, null);

            return TarefaConverterHelper.ItensParaTarefas(ndListItems);
        }

        public Tarefa Salvar(Tarefa tarefa)
        {
            ///*Get Name attribute values (GUIDs) for list and view. */
            System.Xml.XmlNode ndListView = Contexto.GetListAndView("Tarefas", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            /*Create an XmlDocument object and construct a Batch element and its
            attributes. Note that an empty ViewName parameter causes the method to use the default view. */
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            /*Specify methods for the batch post using CAML. To update or delete, 
            specify the ID of the item, and to update or add, specify 
            the value to place in the specified column.*/
            batchElement.InnerXml = TarefaConverterHelper.TarefaParaXML(tarefa);

            /*Update list items. This example uses the list GUID, which is recommended, 
            but the list display name will also work.*/
            Contexto.UpdateListItems(strListID, batchElement);

            return tarefa;
        }

        public void Excluir(int id)
        {
            /*Get Name attribute values (GUIDs) for list and view. */
            System.Xml.XmlNode ndListView = Contexto.GetListAndView("Tarefas", "");
            string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
            string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

            /*Create an XmlDocument object and construct a Batch element and its
            attributes. Note that an empty ViewName parameter causes the method to use the default view. */
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
            batchElement.SetAttribute("OnError", "Continue");
            batchElement.SetAttribute("ListVersion", "1");
            batchElement.SetAttribute("ViewName", strViewID);

            /*Specify methods for the batch post using CAML. To update or delete, 
            specify the ID of the item, and to update or add, specify 
            the value to place in the specified column.*/
            batchElement.InnerXml = "<Method ID='0' Cmd='Delete'><Field Name='ID'>" + id.ToString() + "</Field></Method>";

            /*Update list items. This example uses the list GUID, which is recommended, 
            but the list display name will also work.*/
            Contexto.UpdateListItems(strListID, batchElement);
        }
    }
}
