using AutoMapper;
using Domian.Models;
using Service.ViewModel;


namespace Service.ModelMap
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ShopVM, Shop>();
        }
    }
}
