using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace MVCApp.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }

        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();
            escuela.AñoDeCreacion = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Platzi Academy";
            escuela.Ciudad = "Bogota";
            escuela.Pais = "Colombia";
            escuela.Direccion = "Av. Siempre Viva";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            

            //Cargar Cursos de la escuela
            var cursos = CargarCursos(escuela);

            //Cargar Asignaturas x cada Curso
            var asignaturas = CargarAsignaturas(cursos);

            //Cargar alumnos x cada Curso
            var alumnos = CargarAlumnos(cursos);

            ///Al metodo HasData(), solo soporta arrays, por eso hay que convertirlos SIEMPRE.
            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
            
        }
        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();

            Random rnd = new Random();
            foreach (var curso in cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                var tmpList = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmpList);
            }
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {
                
                var tmpList = new List<Asignatura>
                {
                    new Asignatura{ Nombre = "Matematicas", CursoId = curso.Id, Id = Guid.NewGuid().ToString() },
                    new Asignatura{ Nombre = "Ed. fisica", CursoId = curso.Id, Id = Guid.NewGuid().ToString() },
                    new Asignatura{ Nombre = "Programacion", CursoId = curso.Id, Id = Guid.NewGuid().ToString() },
                    new Asignatura{ Nombre = "Castellano", CursoId = curso.Id, Id = Guid.NewGuid().ToString() },
                    new Asignatura{ Nombre = "Cs. Naturales", CursoId = curso.Id, Id = Guid.NewGuid().ToString() }
                };
                listaCompleta.AddRange(tmpList);
                //curso.Asignaturas = tmpList;
            }
            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>()
            {
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "101", Jornada = TiposJornada.Manaña},
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "102", Jornada = TiposJornada.Manaña},
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "201", Jornada = TiposJornada.Tarde},
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "202", Jornada = TiposJornada.Tarde},
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "301", Jornada = TiposJornada.Noche}
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int cantidad)
        {
            
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Matias", "Donald", "Alvaro", "Nicolas" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Cati", "Silvana", "Dionedes", "Mercedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno
                               {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                               };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }
    }
}