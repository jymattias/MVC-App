using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required(ErrorMessage="El nombre del curso es requerido")]
        [StringLength(5, ErrorMessage="La longitud maxima es de 5 carcteres")]
        public override string Nombre { get; set; }
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }

        public string Direccion { get; set; }

        public string EscuelaId { get; set;}
        public Escuela Escuela { get; set; }

    }
    
 
}