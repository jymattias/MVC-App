using System;

namespace MVCApp.Models
{
    public abstract class ObjetoEscuelaBase
    {
        public string Id { get; set; }
        public virtual string Nombre { get; set; }

        public override string ToString()
        {
            return $"{Nombre},{Id}";
        }
    }
}