using Exception = System.Exception;

namespace Mozi;

public class Mozi
{
    public string Nev { get; set; }
    public List<Terem> Termek { get; private set; }
    public List<Film> Filmek { get; private set; }
    public List<Vetites> Vetitesek { get; private set; }
    public List<Nezo> Nezok { get; private set; }
    public List<Jegy> Jegyek { get; private set; }

    private int _nextJegyId;
    private int _nextVetitesId;

    public Mozi(string n)
    {
        Nev = n;
        Termek = new List<Terem>();
        Filmek = new List<Film>();
        Vetitesek = new List<Vetites>();
        Nezok = new List<Nezo>();
        Jegyek = new List<Jegy>();
        
        Termek.Add(new Terem(1, "Kisterem", Teremmeret.Kicsi));
        Termek.Add(new Terem(2, "Közepes Terem", Teremmeret.Kozepes));
        Termek.Add(new Terem(3, "Nagyterem", Teremmeret.Nagy));
        Termek.Add(new Terem(4, "VIP Terem", Teremmeret.Vip));
        
        Filmek.Add(new Film("Gettómilliomos", 120, "Dráma", 2000));
        Filmek.Add(new Film("Nagymenők", 146, "Krimi/Thriller", 1800));
        Filmek.Add(new Film("Tavasz, nyár, ősz, tél… és tavasz", 103, "Dráma", 1900));
        Filmek.Add(new Film("Valami madarak", 94, "Dráma", 2200));
    }
    
    
    //a
    public void AddTerem(string nev, Teremmeret meret)
    {
        int nextId = Termek.Count > 0 ? Termek.Max(x => x.Id) + 1 : 1;
        Termek.Add(new Terem(nextId, nev, meret));
    }

    public void RemoveTerem(int id)
    {
        Terem terem = Termek.FirstOrDefault(x => x.Id == id);
        if (terem != null)
        {
            //ellenorzes hogy van e a teremben vetites
            if (terem.Vetitesek.Any(x => x.Idopont > DateTime.Now))
            {
                throw new Exception("Nem távolíthat el olyan termet, amibe már szerveztek vetítést");
            }
            Termek.Remove(terem);
        }
        //throw new Exception();
    }
    
    //b
    public Vetites VetitesSzervezes(Film f, Terem t, DateTime d)
    {
        if (!Filmek.Contains(f))
        {
            throw new ArgumentException("Nincs ilyen film a Mozi kínálatában");
        }

        if (!Termek.Contains(t))
        {
            throw new ArgumentException("Nincs ilyen terem a Moziban");
        }

        Vetites vetites = new Vetites(_nextVetitesId++, f, t, d);
        t.AddVetites(vetites);
        Vetitesek.Add(vetites);
        return vetites;
    }
    
    //c
    public Nezo TorzstaggaValas(int id)
    {
        Nezo nezo = Nezok.FirstOrDefault(x => x.Id == id);
            
        if (nezo == null)
        {
            throw new Exception("Nincs ilyen néző");
        }
        
        return nezo.ToTorzstag();
    }
    
    //d

    public Jegy JegyFoglalas(int nezoId, int vetitesId, string szekId)
    {
        Nezo nezo = Nezok.FirstOrDefault(x => x.Id == nezoId);
        if (nezo == null)
        {
            throw new ArgumentException("Nincs ilyen néző regisztrálva");
        }
        
        Vetites vetites = Vetitesek.FirstOrDefault(x => x.Id == vetitesId);
        if (vetites == null)
        {
            throw new ArgumentException("Nincs ilyen vetítés");
        }
        
        Szek szek = vetites.Terem.Szekek.FirstOrDefault(x => x.Id == szekId);
        if (szek == null)
        {
            throw new ArgumentException("Nincs ilyen szék");
        }

        if (szek.Statusz != Foglaltsag.Szabad)
        {
            throw new Exception("A széket már lefoglalták vagy megvásárolták");
        }

        szek.Foglalas(nezo);
        int alapar = vetites.Film.Ar;
        Jegy jegy = new Jegy(_nextJegyId++, nezo, vetites, szek);
        
        nezo.AddJegy(jegy);
        vetites.Jegyek.Add(jegy);
        Jegyek.Add(jegy);
        
        return jegy;
    }

    public bool FoglalasLemondas(int jegyId)
    {
        Jegy jegy = Jegyek.FirstOrDefault(x => x.Id == jegyId);

        if (jegy == null || jegy.Szek.Statusz != Foglaltsag.Foglalt)
        {
            return false;
        } 
        return jegy.Szek.Lemondas();
    }

    public bool JegyVasarlas(int jegyId)
    {
        Jegy jegy = Jegyek.FirstOrDefault(x => x.Id == jegyId);

        if (jegy == null || jegy.Szek.Statusz != Foglaltsag.Foglalt)
        {
            return false;
        }
        return jegy.Szek.Vasarlas();
    }
    
    //e

    public Film LegnezettebbFilm()
    {
        Dictionary<Film, int> Lf = new Dictionary<Film, int>();

        foreach (var jegy in Jegyek.Where(x => x.Szek.Statusz == Foglaltsag.Megvasarolt))
        {
            if (!Lf.ContainsKey(jegy.Vetites.Film))
            {
                Lf[jegy.Vetites.Film] = 0;
            }
            Lf[jegy.Vetites.Film]++;
        }

        if (Lf.Count == 0)
        {
            return null;
        }
        
        return Lf.OrderByDescending(x => x.Value).First().Key;
    }

    //f
    public (int Szabad, int Foglalt, int Megvett) SzekStatCounts(int vetitesId)
    {
        Vetites vetites = Vetitesek.FirstOrDefault(x => x.Id == vetitesId);

        if (vetites == null)
        {
            throw new ArgumentException("Nincs ilyen vetítés");
        }
        
        int szabad = vetites.Terem.Szekek.Count(x => x.Statusz == Foglaltsag.Szabad);
        int foglalt = vetites.Terem.Szekek.Count(x => x.Statusz == Foglaltsag.Foglalt);
        int megvett = vetites.Terem.Szekek.Count(x => x.Statusz == Foglaltsag.Megvasarolt);
        return (szabad, foglalt, megvett);
    }
    
    public void TeremInfo()
    {
        Console.WriteLine($"Mozi: {Nev}");
        Console.WriteLine("Termek:");
        foreach (var t in Termek)
        {
            Console.WriteLine($"- {t}");
        }
    }
    
    public void FilmInfo()
    {
        Console.WriteLine("Filmek:");
        foreach (var f in Filmek)
        {
            Console.WriteLine($"- {f}");
        }
    }
    
    public void VetitesInfo()
    {
        Console.WriteLine("Vetítések:");
        foreach (var v in Vetitesek)
        {
            var (szabad, foglalt, eladott) = SzekStatCounts(v.Id);
            Console.WriteLine($"- {v}");
            Console.WriteLine($"  Székek: {szabad} szabad, {foglalt} foglalt, {eladott} eladott");
        }
    }
    
}
