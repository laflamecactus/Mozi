namespace Mozi;

    public interface INezoVisitor
    {
        List <int> Visit(Gyerek gyerek);
        List<int> Visit(Diak diak);
        List<int> Visit(Felnott felnott);
        List<int> Visit(Nyugdijas nyugdijas);
        List<int> Visit(Torzstag torzstag);
    }