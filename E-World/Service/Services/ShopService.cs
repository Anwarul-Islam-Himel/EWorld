using AutoMapper;
using Domian;
using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IShopService
    {
        Task<ShopVM> AddShopAsync(ShopVM model);
        Task<IList<ShopVM>> GetShopsAsync();
    }
    public class ShopService : IShopService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ShopService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ShopVM> AddShopAsync(ShopVM model)
        {
            try
            {
                var shop = _mapper.Map<Shop>(model);
                await _context.Shop.AddAsync(shop);
                await _context.SaveChangesAsync();
                return _mapper.Map<ShopVM>(shop);
            }
            catch
            {
                return null;
            }
        }
        public async Task<IList<ShopVM>> GetShopsAsync()
        {
            try
            {
                var shops = await _context.Shop.ToListAsync();
                return _mapper.Map<IList<ShopVM>>(shops);
            }
            catch
            {
                return null;
            }
        }
    }
}
