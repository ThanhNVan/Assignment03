using System.ComponentModel;

namespace Assignment03.EntityProviders;

public class ProductCart : INotifyPropertyChanged
{
    #region [ Properties ]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Weight { get; set; }

    public decimal Price { get; set; }

    public int InStock { get; set; }

    public string Category { get; set; }

    private int _unit;
    

    public int Unit
    {
        get { return this._unit; }
        set
        {
            this._unit = value;
            NotifyPropertyChanged(nameof(Unit));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion
}
