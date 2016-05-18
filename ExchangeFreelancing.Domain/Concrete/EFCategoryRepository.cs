using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFCategoryRepository: ICategory
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Category> Categories
        {
            get
            {
                return context.Categories;
            }
           
        }
    }
}
