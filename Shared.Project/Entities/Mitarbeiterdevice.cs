using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Project.Entities
{
  public class Mitarbeiterdevice : BaseEntity
    {

       
        [Key] 
        public string Device { get; set; } = string.Empty;

        public string Bezeichnung { get; set; } = string.Empty;

        public string Passwort { get; set; } = string.Empty;

        public bool Adressen { get; set; } = false;
        public bool Ansprechpartner { get; set; } = false;
        public bool Projekte { get; set; } = false;
        public bool Aufgaben { get; set; } = false;
        public bool GPS { get; set; } = false;
        public bool Inaktiv { get; set; } = false;
        public string? Token { get; set; }

        public int Adressenrecht { get; set; } = 0;
        //public string Image { get; set; } = string.Empty;
    }
}
