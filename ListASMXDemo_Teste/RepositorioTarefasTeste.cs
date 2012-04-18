using System;
using System.Collections.Generic;
using ListsASMXDemo;
using ListsASMXDemo.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ListASMXDemo_Teste
{
    [TestClass]
    public class RepositorioTarefasTeste
    {
        private RepositorioTarefas Repositorio;

        public RepositorioTarefasTeste()
        {
            Repositorio = new RepositorioTarefas();
        }

        [TestMethod]
        public void ObterTodos_ListASMX()
        {
            List<Tarefa> tarefas = Repositorio.ObterTodos();

            Assert.IsNotNull(tarefas);
            Assert.AreNotEqual(0, tarefas.Count);
        }

        [TestMethod]
        public void ObterPorFiltro_ListASMX()
        {
            List<Tarefa> tarefas = Repositorio.ObterPorFiltro("Tarefa Gerada");

            Assert.IsNotNull(tarefas);
            Assert.AreNotEqual(0, tarefas.Count);
        }

        [TestMethod]
        public void ObterTarefaPorID_ListASMX()
        {
            Tarefa tarefa = Repositorio.ObterPorID(1);

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(1, tarefa.ID);
        }

        [TestMethod]
        public void Excluir_ListASMX()
        {
            List<Tarefa> tarefas = Repositorio.ObterPorFiltro("Tarefa Gerada");

            int totalAnterior = Repositorio.ObterTodos().Count;
            Repositorio.Excluir(tarefas[0].ID);
            int totalDepois = Repositorio.ObterTodos().Count;

            Assert.IsNotNull(tarefas);
            Assert.AreEqual(totalAnterior, totalDepois + 1);
        }

        [TestMethod]
        public void AdicionarTarefa_ListASMX()
        {
            Tarefa tarefa = new Tarefa();
            string titulo = "Tarefa Gerada no Teste";
            tarefa.Titulo = titulo;
            tarefa.PercentComplete = "0";
            tarefa.Priority = "0";
            tarefa.StartDate = DateTime.Now;
            tarefa.DueDate = DateTime.Now.AddDays(5);

            int totalAnterior = Repositorio.ObterTodos().Count;
            Tarefa tarefaRetorno = Repositorio.Salvar(tarefa);
            int totalDepois = Repositorio.ObterTodos().Count;

            Assert.IsNotNull(tarefa);
            Assert.AreEqual(titulo, tarefa.Titulo);
            Assert.AreEqual(totalAnterior + 1, totalDepois);
        }

        [TestMethod]
        public void EditarTarefa_ListASMX()
        {
            Tarefa tarefa = Repositorio.ObterPorID(31);

            string tituloAnterior = tarefa.Titulo;
            tarefa.Titulo = DateTime.Now.ToString();
            Tarefa tarefaRetorno = Repositorio.Salvar(tarefa);
            string tituloNovo = tarefaRetorno.Titulo;

            Assert.IsNotNull(tarefa);
            Assert.AreNotEqual(tituloAnterior, tituloNovo);
        }
    }
}
