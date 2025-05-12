namespace Mozi;

public class Vetites
{
    public int Id { get; set; }
    public Film Film { get; set; }
    public Terem Terem { get; set; }
    public DateTime Idopont { get; set; }
    
    public List<Jegy> Jegyek { get; private set; }

    public Vetites(int i, Film f, Terem t, DateTime ido)
    {
        Id = i;
        Film = f;
        Terem = t;
        Idopont = ido;
        Jegyek = new List<Jegy>();
    }

    public int FoglaltSzekCount()
    {
        return Terem.Szekek.Count(s => s.Statusz != Foglaltsag.Foglalt);
    }

    public int SzabadSzekCount()
    {
        return Terem.Szekek.Count(s => s.Statusz != Foglaltsag.Szabad);
    }
    
    public List<Szek> SzabadSzekLista()
    {
        return Terem.Szekek.Where(s => s.Statusz == Foglaltsag.Szabad).ToList();
    }

    public override string ToString()
    {
        return $"Vetítés {Id}: {Film.Cim} a {Terem.Nev} teremben {Idopont}";
    }
}