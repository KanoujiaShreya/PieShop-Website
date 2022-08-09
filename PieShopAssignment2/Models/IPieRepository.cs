namespace PieShopAssignment2.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }

        //homepage
        IEnumerable<Pie> PiesOfTheWeek { get; }


        Pie GetPieById(int pieId);

        IEnumerable<Pie> GetCategory(int Id);

        IEnumerable<Pie> GetFruitpies { get; }

        IEnumerable<Pie> GetSeasonal { get; }
        IEnumerable<Pie> GetCheesecakes { get; }

        Pie AddPie(Pie pie);
        Pie UpdatePie(Pie pie);
        Pie DeletePie(Pie pie);

    }
}
