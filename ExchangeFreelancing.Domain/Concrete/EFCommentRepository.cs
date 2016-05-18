using ExchangeFreelancing.Domain.Abstract;
using ExchangeFreelancing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFCommentRepository : IComment
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<Comment> Comments
        {
            get
            {
                return context.Comments;
            }
        }

        public void Add(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
        }
    }
}
