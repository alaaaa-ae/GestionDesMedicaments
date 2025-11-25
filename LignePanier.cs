using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesMedicaments
{
    public class LignePanier
    {
        public int MedicamentId { get; set; }
        public string NomMedicament { get; set; }
        public decimal PrixUnitaire { get; set; }
        public int Quantite { get; set; }
        public decimal Total => PrixUnitaire * Quantite;
    }
}
