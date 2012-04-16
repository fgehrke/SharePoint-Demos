using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientObjectModelDemo;
using ClientObjectModelDemo.Entidades;

namespace ClientObjectModelDemo_Teste
{
    /// <summary>
    /// Summary description for RepositorioTarefaTeste
    /// </summary>
    [TestClass]
    public class RepositorioTarefaTeste
    {
        public RepositorioTarefas RepositorioTarefas { get; set; }

        public RepositorioTarefaTeste()
        {
            RepositorioTarefas = new RepositorioTarefas();
        }

        [TestMethod]
        public void ObterTodos()
        {
            List<Tarefa> tarefas = RepositorioTarefas.ObterTodos();

            Assert.IsNotNull(tarefas);
            Assert.AreNotEqual(0, tarefas.Count);
        }

        [TestMethod]
        public void ObterPorFiltro()
        {
            List<Tarefa> tarefas = RepositorioTarefas.ObterPorFiltro("Tarefa Gerada");

            Assert.IsNotNull(tarefas);
            Assert.AreNotEqual(0, tarefas.Count);
        }

        [TestMethod]
        public void ObterTarefaPorID()
        {
            Tarefa tarefa = RepositorioTarefas.ObterPorID(1);

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(1, tarefa.ID);
        }

        [TestMethod]
        public void Excluir()
        {
            List<Tarefa> tarefas = RepositorioTarefas.ObterPorFiltro("Tarefa Gerada");

            int totalAnterior = RepositorioTarefas.ObterTodos().Count;
            RepositorioTarefas.Excluir(tarefas[0].ID);
            int totalDepois = RepositorioTarefas.ObterTodos().Count;

            Assert.IsNotNull(tarefas);
            Assert.AreEqual(totalAnterior, totalDepois + 1);
        }

        [TestMethod]
        public void AdicionarTarefa()
        {
            Tarefa tarefa = new Tarefa();
            string titulo = "Tarefa Gerada no Teste";
            tarefa.Titulo = titulo;
            tarefa.PercentComplete = "0";
            tarefa.Priority = "0";
            tarefa.StartDate = DateTime.Now;
            tarefa.DueDate = DateTime.Now.AddDays(5);

            int totalAnterior = RepositorioTarefas.ObterTodos().Count;
            Tarefa tarefaRetorno = RepositorioTarefas.Salvar(tarefa);
            int totalDepois = RepositorioTarefas.ObterTodos().Count;

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(titulo, tarefa.Titulo);
            Assert.AreEqual(totalAnterior +1, totalDepois);
        }
    }
}
