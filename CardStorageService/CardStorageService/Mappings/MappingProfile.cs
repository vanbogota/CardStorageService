using AutoMapper;
using CardStorageService.DAL;
using CardStorageService.Models;
using CardStorageService.Models.Requests;

namespace CardStorageService.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<CreateCardRequest, Card>();
            CreateMap<CreateClientRequest, Client>();
            CreateMap<Account, AccountDto>();
        }
    }
}
