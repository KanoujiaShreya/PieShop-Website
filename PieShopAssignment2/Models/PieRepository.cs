namespace PieShopAssignment2.Models
{
    public class PieRepository : IPieRepository

    {
        private readonly AppDbContext appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IEnumerable<Pie> AllPies => appDbContext.Pies;



        public IEnumerable<Pie> PiesOfTheWeek => appDbContext.Pies.Where(pie => pie.IsPieOfTheWeek);

        public IEnumerable<Pie> GetFruitpies => appDbContext.Pies.Where(pie => pie.CategoryId == 1);

        public IEnumerable<Pie> GetSeasonal => appDbContext.Pies.Where(pie => pie.CategoryId == 3);

        public IEnumerable<Pie> GetCheesecakes => appDbContext.Pies.Where(pie => pie.CategoryId == 2);

        public Pie GetPieById(int pieId)
        {
            return this.AllPies.FirstOrDefault(pie => pie.PieId == pieId);
        }

        public IEnumerable<Pie> GetCategory(int Id)
        {
            return appDbContext.Pies.Where(c => c.CategoryId == Id);
        }

        public Pie AddPie(Pie pie)
        {
            var pies = this.appDbContext.Pies.Add(pie);
            this.appDbContext.SaveChanges();
            return pies.Entity;

        }

        public Pie UpdatePie(Pie pie)
        {
            var pies = this.appDbContext.Pies.Update(pie);
            this.appDbContext.SaveChanges();
            return pies.Entity;
        }

            public Pie DeletePie(Pie pie)
        {
            var pies = this.appDbContext.Pies.Remove(pie);
            this.appDbContext.SaveChanges();
            return pies.Entity;

        }
    }
}
