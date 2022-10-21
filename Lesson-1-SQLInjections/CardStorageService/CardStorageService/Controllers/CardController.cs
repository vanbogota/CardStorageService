using CardStorageService.DAL;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;
        public CardController(ILogger<CardController> logger, ICardRepositoryService cardRepositoryService)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            try
            {
                var cardId = _cardRepositoryService.Create(new Card
                {
                    ClientId = request.ClientId,
                    CardNo = request.CardNo,
                    ExpDate = request.ExpDate,
                    CVV2 = request.CVV2
                });
                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("getByClientId")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] int clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = cards.Select(card => new CardDto
                    {
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        Name = card.Name,
                        ExpDate = card.ExpDate.ToString("MM/yy")
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }

        [HttpGet("getById")]        
        public IActionResult GetById([FromQuery] string cardId)
        {
            try
            {
                var card = _cardRepositoryService.GetById(cardId);
                return Ok(new CardDto
                {
                    CardNo = card.CardNo,
                    CVV2 = card.CVV2,
                    ExpDate = card.ExpDate.ToString("MM/yy"),
                    Name = card.Name,
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get card error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1014,
                    ErrorMessage = "Get card error."
                });
            }
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var cards = _cardRepositoryService.GetAll();
                return Ok(new GetCardsResponse
                {
                    Cards = cards.Select(card => new CardDto
                    {
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        Name = card.Name,
                        ExpDate = card.ExpDate.ToString("MM/yy")
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }

        [HttpPut("Update")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] Card newCard)
        {
            try
            {
                int responce = _cardRepositoryService.Update(newCard);
                return Ok(responce);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update card error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1015,
                    ErrorMessage = "Update card error."
                });
            }
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult Delete([FromQuery] string cardId)
        {
            try
            {
                int responce = _cardRepositoryService.Delete(cardId);
                return Ok(responce);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete card error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1016,
                    ErrorMessage = "Delete card error."
                });
            }
        }
    }
}
