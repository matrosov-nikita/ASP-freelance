using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Abstract
{
   public interface IFile
    {
       IQueryable<File> Files { get; }
       void Add(File file);
    }
}
