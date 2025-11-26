using System.ComponentModel;

public class LigneCommandeView : INotifyPropertyChanged
{
    private int _quantite;

    public int id_medicament { get; set; }
    public string Nom { get; set; }

    public int Quantite
    {
        get => _quantite;
        set
        {
            if (_quantite != value)
            {
                _quantite = value;
                OnPropertyChanged(nameof(Quantite));
                OnPropertyChanged(nameof(Total)); // Total change aussi
            }
        }
    }

    public decimal PrixUnitaire { get; set; }

    public decimal Total => Quantite * PrixUnitaire;

    // Événement nécessaire pour INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
