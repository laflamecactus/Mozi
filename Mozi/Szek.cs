namespace Mozi;

public class Szek
{
    public string Id;
    public Foglaltsag Statusz;
    public Nezo? Foglalo;

    public Szek(string i)
    {
        Id = i;
        Statusz = Foglaltsag.Szabad;
        Foglalo = null;
    }

    public bool Foglalas(Nezo n)
    {
        if (Statusz == Foglaltsag.Szabad)
        {
            Statusz = Foglaltsag.Foglalt;
            Foglalo = n;
            return true;
        }
        return false;
    }

    public bool Lemondas()
    {
        if (Statusz == Foglaltsag.Foglalt)
        {
            Statusz = Foglaltsag.Szabad;
            Foglalo = null;
            return true;
        }
        return false;
    }

    public bool Vasarlas()
    {
        if (Statusz == Foglaltsag.Foglalt)
        {
            Statusz = Foglaltsag.Megvasarolt;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Szék {Id}: {Statusz}" + (Foglalo != null ? $"{Foglalo.Id}" : "");
    }
}