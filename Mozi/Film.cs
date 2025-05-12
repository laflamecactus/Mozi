namespace Mozi;

public class Film
{
    public string Cim { get; set; }
    public int Hossz{ get; set; } 
    public string Mufaj { get; set; }
    public int Ar { get; set; }

    public Film(string c, int h, string m, int a)
    {
        Cim = c;
        Hossz = h;
        Mufaj = m;
        Ar = a;
    }

    public override string ToString()
    {
        return $"{Cim} {Hossz} {Mufaj}";
    }
}