using AutoMapper;
using PieShopAssignment2.Models;

namespace PieShopAssignment2
{
    public class stuProfile : Profile
    {
        public stuProfile()
        {
            this.CreateMap<Pie, PieMini>();
        }
    }
}
