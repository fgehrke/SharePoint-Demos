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
        public RepositorioTarefas Repositorio { get; set; }

        public RepositorioTarefaTeste()
        {
            Repositorio = new RepositorioTarefas();
        }

        [TestMethod]
        public void ObterTodos_COM()
        {
            List<Tarefa> tarefas = Repositorio.ObterTodos();

            Assert.IsNotNull(tarefas);
            Assert.AreNotEqual(0, tarefas.Count);
        }

        [TestMethod]
        public void ObterPorFiltro_COM()
        {
            List<Tarefa> tarefas = Repositorio.ObterPorFiltro("Tarefa Gerada");

            Assert.IsNotNull(tarefas);
            Assert.AreNotEqual(0, tarefas.Count);
        }

        [TestMethod]
        public void ObterTarefaPorID_COM()
        {
            Tarefa tarefa = Repositorio.ObterPorID(1);

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(1, tarefa.ID);
        }

        [TestMethod]
        public void Excluir_COM()
        {
            List<Tarefa> tarefas = Repositorio.ObterPorFiltro("Tarefa Gerada");

            int totalAnterior = Repositorio.ObterTodos().Count;
            Repositorio.Excluir(tarefas[0].ID);
            int totalDepois = Repositorio.ObterTodos().Count;

            Assert.IsNotNull(tarefas);
            Assert.AreEqual(totalAnterior, totalDepois + 1);
        }

        [TestMethod]
        public void AdicionarTarefa_DadosNecessarios_COM()
        {
            Tarefa tarefa = new Tarefa();
            string titulo = "Tarefa Gerada no Teste";
            tarefa.Titulo = titulo;

            int totalAnterior = Repositorio.ObterTodos().Count;
            Tarefa tarefaRetorno = Repositorio.Salvar(tarefa);
            int totalDepois = Repositorio.ObterTodos().Count;

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(titulo, tarefa.Titulo);
            Assert.AreEqual(totalAnterior +1, totalDepois);
        }


        [TestMethod]
        public void AdicionarTarefa_TodosDados_COM()
        {
            Tarefa tarefa = new Tarefa();
            string titulo = "Tarefa Gerada no Teste";
            tarefa.Titulo = titulo;
            tarefa.PercentComplete = "50";
            tarefa.Priority = "1";
            tarefa.StartDate = DateTime.Now;
            tarefa.DueDate = DateTime.Now.AddDays(5);
            tarefa.Body = "<div>Descrição da tarefa</div>";
            tarefa.Status = "Em Andamento";
            tarefa.Predecessors = "1";
            tarefa.AssignetTo = new Usuario(1, "Fabian André Gehrke");

            int totalAnterior = Repositorio.ObterTodos().Count;
            Tarefa tarefaRetorno = Repositorio.Salvar(tarefa);
            int totalDepois = Repositorio.ObterTodos().Count;

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(titulo, tarefa.Titulo);
            Assert.AreEqual(totalAnterior + 1, totalDepois);
        }     
    }
}