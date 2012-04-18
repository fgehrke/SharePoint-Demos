using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListDataDemo.Entidades
{
        public class Tarefa
        {
            public int ID { get; set; }
            public string Titulo { get; set; }
            public string PercentComplete { get; set; }
            public string AssignetTo { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime StartDate { get; set; }
            public string Body { get; set; }
            public string TaskGroup { get; set; }
            public string Predecessors { get; set; }
            public string Priority { get; set; }
            public string Status { get; set; }
            public Usuario Author { get; set; }
            public Usuario Editor { get; set; }
        }
}
