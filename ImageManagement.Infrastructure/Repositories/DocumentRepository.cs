using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using ImageManagement.Domain.SeedWork;

namespace ImageManagement.Infrastructure.Repositories
{
    public class DocumentRepository : BaseFileRepository<Document>, IDocumentRepository
    {
        private readonly AppDbContext _context;
        public DocumentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
