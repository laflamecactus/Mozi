namespace Mozi
{
    public class DiscountCal : INezoVisitor
    {
        private static DiscountCal _instance;

        private DiscountCal() { }

        public static DiscountCal Instance()
        {
            {
                if (_instance == null)
                    _instance = new DiscountCal();
                return _instance;
            }
        }

        public List<int> Visit(Gyerek gyerek) => [40,30,20,0];
        public List<int> Visit(Diak diak) => [20,30,40,0];
        public List<int> Visit(Felnott felnott) => [0,5,10,5] ;
        public List<int> Visit(Nyugdijas nyugdijas) => [30,20,20,0];
        public List<int> Visit(Torzstag torzstag) => [40,40,40,0];
    }
}