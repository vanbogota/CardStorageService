using CardStorageService.DAL;
using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.ClientService;

namespace CardStorageService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        private readonly IClientRepositoryService _clientRepositoryService;

        public ClientService(IClientRepositoryService clientRepositoryService)
        {
            _clientRepositoryService = clientRepositoryService;
        }

        public override Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
        {
            var clientId = _clientRepositoryService.Create(new Client
            {
                FirstName = request.FirstName,
                Surname = request.SurName,
                Patronymic = request.Patronymic
            });

            var response = new CreateClientResponse
            {
                ClientId = clientId,
                ErrorCode = 0,
                ErrorMessage = String.Empty,                           
            };

            return Task.FromResult(response);
        }
    }
}
