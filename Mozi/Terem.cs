namespace Mozi;

public class Terem
{
    public int Id { get; set; }
    public string Nev { get; set; }
    public Teremmeret Meret { get; set; }
    
    public List<Szek> Szekek { get; private set; }
    public List<Vetites> Vetitesek { get; private set; }

    public Terem(int i, string n, Teremmeret m)
    {
        Id = i;
        Nev = n;
        Meret = m;
        Szekek = new List<Szek>();
        Vetitesek = new List<Vetites>();
        InitializeSeats();
    }

    private void InitializeSeats()
    {
        int sorCount, szekekPerSor;
        
        switch (Meret)
        {
            case Teremmeret.Kicsi:
                sorCount = 5;
                szekekPerSor = 10;
                break;
            case Teremmeret.Kozepes:
                sorCount = 10;
                szekekPerSor = 10;
                break;
            case Teremmeret.Nagy:
                sorCount = 15;
                szekekPerSor = 10;
                break;
            case Teremmeret.Vip:
                sorCount = 5;
                szekekPerSor = 5;
                break;
            default:
                sorCount = 5;
                szekekPerSor = 10;
                break;
        }

        for (int i = 0; i < sorCount; i++)
        {
            char betu = (char)(i + 'A');
            for (int j = 1; j <= szekekPerSor; j++)
            {
                Szekek.Add(new Szek($"{betu}{j}"));
            }
        }
    }

    public void AddVetites(Vetites v)
    {
        //fedesek kizarasa, +15 a valtashoz

        foreach (var e in Vetitesek)
        {
            DateTime existingStart = e.Idopont;
            DateTime existingEnd = existingStart.AddMinutes(e.Film.Hossz + 15); 
                
            DateTime newStart = v.Idopont;
            DateTime newEnd = newStart.AddMinutes(v.Film.Hossz + 15);

            if ((newStart >= existingStart && newStart < existingEnd) ||
                (newEnd > existingStart && newEnd <= existingEnd) ||
                (newStart <= existingStart && newEnd >= existingEnd))
            {
                throw new InvalidOperationException("Az adott időben, ebben a teremben már van vetítés");
            }
        }
            
        Vetitesek.Add(v);
    }

    public override string ToString()
    {
        return $"Terem {Nev} ({Meret}) Székek száma:{Szekek.Count}";
    }
    


}