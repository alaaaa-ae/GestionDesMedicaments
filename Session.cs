using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesMedicaments.Classes
{
    public static class Session
    {
        // Stocke l'ID de l'utilisateur connecté
        public static int IdUtilisateur { get; set; }

        // Stocke le nom de l'utilisateur (optionnel)
        public static string NomUtilisateur { get; set; }
    }
}
