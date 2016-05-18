using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFFileRepository : IFile
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<File> Files
        {
            get
            {
                return context.Files;
            }
        }

        public void Add(File file)
        {
            context.Files.Add(file);
            context.SaveChanges();
        }
    }
}
