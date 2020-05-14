using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using testapp_api.Models;

namespace testapp_api.Services
{
    public class ProductService
    {
        private StoreDbContext _context;
        private IMapper _mapper;

        public ProductService(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public (IList<ProductView>, int) GetAll(ProductFilter filter)
        {
            try
            {
                var skip = (filter.Page - 1) * filter.Take;
                var take = filter.Take;
                var order = filter.SortOrder == SortOrderEnum.ASC ? "" : "desc";

                var products = _context.Products
                    .Where(w => String.IsNullOrEmpty(filter.Title) || filter.Title == "null" ? true : w.Title.Contains(filter.Title))
                    .Skip(skip).Take(take).OrderBy(filter.SortField, order).ToList();
                var qty = _context.Products
                    .Where(w => String.IsNullOrEmpty(filter.Title) || filter.Title == "null" ? true : w.Title.Contains(filter.Title)).Count();
                return (_mapper.Map<IList<ProductView>>(products), qty);
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public int getQuantity()
        {
            try
            {
                return _context.Products.Count();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
