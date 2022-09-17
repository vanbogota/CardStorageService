using CardStorageService.DAL;

namespace CardStorageService.Services.impl
{
    public class ClientRepository : IClientRepositoryService
    {
        private readonly CardStorageServiceDbContext _dbContext;

        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(ILogger<ClientRepository> logger, CardStorageServiceDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public int Create(Client data)
        {
            _dbContext.Clients.Add(data);
            _dbContext.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == id);
            if (client == null)
                throw new Exception("Card not found.");

            _dbContext.Clients.Remove(client);
            _dbContext.SaveChanges();
            return 1;
        }

        public IList<Client> GetAll()
        {
            return _dbContext.Clients.ToList();
        }

        public Client GetById(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == id);
            if (client == null)
                throw new Exception("Card not found.");

            return client;
        }

        public int Update(Client data)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Card not found.");

            _dbContext.Clients.Update(client);
            return 1;
        }
    }
}
