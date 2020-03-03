using System;
using System.Collections.Generic;

namespace MVCApp.Models
{
    public class Escuela:ObjetoEscuelaBase
    {

       public int AñoDeCreacion { get; set; }
       public string Pais { get; set; }
       public string Ciudad { get; set; }
       public string Direccion { get; set; }

       public TiposEscuela TipoEscuela {get; set;}
       public List<Curso> Cursos { get; set; }
       public Escuela(string nombre, int año, TiposEscuela tipo, string pais = "", string ciudad ="")
       {
           (Nombre, AñoDeCreacion) = (nombre, año);
           Pais = pais;
           Ciudad = ciudad;
           TipoEscuela = tipo;
       } 

       public Escuela(){}

       public override string ToString()
       {
           return $"Nombre: {Nombre}\nTipo: {TipoEscuela}\nAño de Creacion: {AñoDeCreacion}\nPais: {Pais}\nCiudad: {Ciudad}";
       }
        
    }
}