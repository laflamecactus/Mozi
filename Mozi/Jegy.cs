using System.Linq.Expressions;

namespace Mozi;

public class Jegy
{
    public int Id { get; set; }
    public Nezo Nezo { get; set; }
    public Vetites Vetites { get; set; }
    public Szek Szek { get; set; }
    public double Fizetendo{ get; set; }

    public Jegy(int i, Nezo n, Vetites v, Szek sz)
    {
        Id = i;
        Nezo = n;
        Vetites = v;
        Szek = sz;
        FizetendoCalculator();
    }

    private void FizetendoCalculator()
    {
        List<int> kedvezmenyek = Nezo.Accept(DiscountCal.Instance());
        if (Vetites.Terem.Meret == Teremmeret.Kicsi)
        {
            Fizetendo = Vetites.Film.Ar * ((100 - kedvezmenyek[0])/100);
        }
        if (Vetites.Terem.Meret == Teremmeret.Kozepes)
        {
            Fizetendo = Vetites.Film.Ar * ((100 - kedvezmenyek[1])/100);
        }
        if (Vetites.Terem.Meret == Teremmeret.Nagy)
        {
            Fizetendo = Vetites.Film.Ar * ((100 - kedvezmenyek[2])/100);
        }
        if (Vetites.Terem.Meret == Teremmeret.Vip)
        {
            Fizetendo = Vetites.Film.Ar * ((100 - kedvezmenyek[3])/100);
        }
    }
    

    public override string ToString()
    {
        return $"Jegy: {Id}, Ár: {Fizetendo}, Státusz{Szek.Statusz}";
    }
}