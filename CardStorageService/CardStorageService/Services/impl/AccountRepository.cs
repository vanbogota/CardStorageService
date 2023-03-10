using CardStorageService.DAL;

namespace CardStorageService.Services.impl
{
    public class AccountRepository : IAccountRepositoryService
    {
        private readonly ILogger<AccountRepository> _logger;
        private readonly CardStorageServiceDbContext _dbContext;

        public AccountRepository(ILogger<AccountRepository> logger, CardStorageServiceDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public int Create(Account data)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Account data)
        {
            throw new NotImplementedException();
        }
    }
}
