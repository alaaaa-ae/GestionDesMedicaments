using System.ComponentModel;

public class LigneCommandeView : INotifyPropertyChanged
{
    private int _quantite;

    // id_medicament est nécessaire pour enregistrer la ligne de commande en DB
    public int id_medicament { get; set; }
    public string Nom { get; set; } // Nom du médicament
    public decimal PrixUnitaire { get; set; } // Prix unitaire du médicament

    public int Quantite
    {
        get => _quantite;
        set
        {
            if (_quantite != value)
            {
                _quantite = value;
                OnPropertyChanged(nameof(Quantite));
                OnPropertyChanged(nameof(Total)); // Total change aussi si la quantité change
            }
        }
    }

    public decimal Total => Quantite * PrixUnitaire; // Propriété calculée

    // Événement nécessaire pour INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}