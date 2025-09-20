using System.Data;
using ImageManagement.Domain.AggregatesModel.DocumentAggregate;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using ImageManagement.Domain.SeedWork;
using ImageManagement.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ImageManagement.Infrastructure
{
    public class AppDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<Uploader> Uploaders { get; set; }
        public DbSet<Document> Documents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("AppDbContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UploaderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentEntityTypeConfiguration());

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            _ = await base.SaveChangesAsync(cancellationToken);

            return true;

        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (HasActiveTransaction)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (HasActiveTransaction)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
