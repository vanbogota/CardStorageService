using CardStorageService.DAL;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace CardStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly IMapper _mapper;

        public ClientController(ILogger<ClientController> logger, IClientRepositoryService clientRepositoryService, IMapper mapper)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(_mapper.Map<Client>(request));
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateClientResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create clinet error."
                });
            }
        }
        [HttpGet("getById")]
        public IActionResult GetById([FromQuery] int clientId)
        {
            try
            {
                var client = _clientRepositoryService.GetById(clientId);
                return Ok(new ClientDto
                {
                    FirstName = client.FirstName,
                    Patronymic = client.Patronymic,
                    Surname =  client.Surname
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get client error.");
                return Ok(new 
                {
                    ErrorCode = 913,
                    ErrorMessage = "Get client error."
                });
            }
        }

        [HttpGet("getAll")]
        
        public IActionResult GetAll()
        {
            try
            {
                var clients = _clientRepositoryService.GetAll();
                return Ok(clients);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get client error.");
                return Ok(new 
                {
                    ErrorCode = 914,
                    ErrorMessage = "Get client error."
                });
            }
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Client newClient)
        {
            try
            {
                int responce = _clientRepositoryService.Update(newClient);
                return Ok(responce);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update client error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 915,
                    ErrorMessage = "Update client error."
                });
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteCard([FromQuery] int clientId)
        {
            try
            {
                int responce = _clientRepositoryService.Delete(clientId);
                return Ok(responce);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete client error.");
                return Ok(new 
                {
                    ErrorCode = 916,
                    ErrorMessage = "Delete client error."
                });
            }
        }


    }
}
