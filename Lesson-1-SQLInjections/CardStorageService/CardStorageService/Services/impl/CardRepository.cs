using CardStorageService.DAL;
using CardStorageService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;


namespace CardStorageService.Services.impl
{
    public class CardRepository : ICardRepositoryService
    {
        private readonly CardStorageServiceDbContext _dbContext;
        
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(ILogger<CardRepository> logger, CardStorageServiceDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public string Create(Card data)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");

            _dbContext.Cards.Add(data);

            _dbContext.SaveChanges();

            return data.CardId.ToString();
        }

        public int Delete(string id)
        {
            var card = _dbContext.Cards.FirstOrDefault(card=> card.CardId.ToString()==id);
            if(card == null)
                throw new Exception("Card not found.");

            _dbContext.Cards.Remove(card);
            _dbContext.SaveChanges();
            return 1;
        }

        public IList<Card> GetAll()
        {
            return _dbContext.Cards.ToList();
        }

        public IList<Card> GetByClientId(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(client => client.ClientId == id);
            if (client == null)
                throw new Exception("Client not found.");

            return _dbContext.Cards.Where(card => card.ClientId == id).ToList();
        }
        
        public Card GetById(string id)
        {
            var card = _dbContext.Cards.FirstOrDefault(card => card.CardId.ToString() == id);
            if (card == null)
                throw new Exception("Card not found.");

            return card;
        }

        public int Update(Card data)
        {
            var card = _dbContext.Cards.FirstOrDefault(card => card.CardId == data.CardId);
            if (card == null)
                throw new Exception("Card not found.");

            _dbContext.Cards.Update(data);
            return 1;
        }
    }
}
