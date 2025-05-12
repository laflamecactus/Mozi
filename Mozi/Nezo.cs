namespace Mozi;

public abstract class Nezo
{
    public int Id { get; set; }
    public List<Jegy> Jegyek { get; private set; }

    public Nezo(int i)
    {
        Id = i;
        Jegyek = new List<Jegy>();
    }

    public void AddJegy(Jegy jegy)
    {
        Jegyek.Add(jegy);
    }

    public Nezo ToTorzstag()
    {
        if (this is Torzstag)
        {
            throw new Exception("Ez a néző már törzstag");
        }
        
        Nezo ujNezo = new Torzstag(Id);

        foreach (var jegy in Jegyek)
        {
            ujNezo.Jegyek.Add(new Jegy(jegy.Id, ujNezo, jegy.Vetites, jegy.Szek));
        }
        
        return ujNezo;
    }

    public abstract List<int> Accept(INezoVisitor visitor);

    public override string ToString()
    {
        return $"Néző {Id}";
    }
}

public class Gyerek : Nezo
{
    public Gyerek(int i) : base(i){}
    public override List<int> Accept(INezoVisitor visitor) => visitor.Visit(this);
}

public class Diak : Nezo
{
    public Diak(int i) : base(i){}
    public override List<int> Accept(INezoVisitor visitor) => visitor.Visit(this);
}

public class Felnott : Nezo
{
    public Felnott(int i) : base(i){}
    public override List<int> Accept(INezoVisitor visitor) => visitor.Visit(this);
}

public class Nyugdijas : Nezo
{
    public Nyugdijas(int i) : base(i){}
    public override List<int> Accept(INezoVisitor visitor) => visitor.Visit(this);
}

public class Torzstag : Nezo
{
    public Torzstag(int i) : base(i){}
    public override List<int> Accept(INezoVisitor visitor) => visitor.Visit(this);
}