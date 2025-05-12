namespace Mozi;

public class Program
{
    static void Main(string[] args)
    {
        Mozi mozi = new Mozi("Mi Mozink Tatabánya");
        mozi.TeremInfo();
        
        try
        {
            //a, Lehessen új vetítő termet létrehozni, egy régit megszűntetni.
            mozi.AddTerem("Terem5", Teremmeret.Nagy);
            mozi.TeremInfo();
            mozi.RemoveTerem(5);
            mozi.TeremInfo();
            
            //b, Lehessen filmvetítést végezni: ehhez filmet kell beszerezni, előadást szervezni az egyik terem adott időpontjában.
            mozi.VetitesSzervezes(mozi.Filmek[0], mozi.Termek[0], DateTime.Now.AddDays(1).Date.AddHours(18));
            mozi.VetitesSzervezes(mozi.Filmek[1], mozi.Termek[1], DateTime.Now.AddDays(2).Date.AddHours(18));
            mozi.VetitesInfo();
            
            //c, Lehessen egy nézőnek törzstaggá válni.
            Nezo nezo1 = new Diak(0001);
            mozi.Nezok.Add(nezo1);
            Console.WriteLine(nezo1.GetType());
            Console.WriteLine("Törzstaggá tétel");
            nezo1 = mozi.TorzstaggaValas(0001);
            Console.WriteLine(nezo1.GetType());
            
            //d, Lehessen egy nézőnek jegyet foglalni, azt visszamondani, vagy megvenni.
            mozi.JegyFoglalas(mozi.Nezok[0].Id, mozi.Vetitesek[0].Id, "A1");
            mozi.JegyFoglalas(mozi.Nezok[0].Id, mozi.Vetitesek[0].Id, "A2");
            Console.WriteLine($"Foglalva: {mozi.Jegyek[0]}");
            Console.WriteLine($"Foglalva: {mozi.Jegyek[1]}");
            mozi.VetitesInfo();
            mozi.JegyVasarlas(mozi.Jegyek[0].Id);
            mozi.FoglalasLemondas(mozi.Jegyek[1].Id);
            mozi.VetitesInfo();
            
            //e, Melyik filmet nézte meg a legtöbb néző?
            Console.WriteLine("legnézettebb film:");
            Film legnez = mozi.LegnezettebbFilm();
            Console.WriteLine(legnez != null ? legnez.ToString() : "Nincs még megtekintett film");
            
            //f, Számoljuk meg egy adott előadásra megvett, csak lefoglalt, illetve szabad helyeket!
            (int szabad, int foglalt, int megvett) = mozi.SzekStatCounts(mozi.Vetitesek[0].Id);
            Console.WriteLine(mozi.Vetitesek[0]);
            Console.WriteLine($"Székek, Szabad:{szabad}, Foglalt:{foglalt}, Megvett:{megvett}");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
}