using AutoMapper;
using NipedModel;
using NipedWebApi.Data;
using NipedWebApi.Data.Model;
using NipedWebApi.Domain.Validations;
using WebApplication1.Helpers;

namespace NipedWebApi.Domain
{
    public class ClientProvider(INipedDbService dbService, IMapper mapper , IClientValidator clientValidator) : IClientProvider
    {
        private NipedDbContext _dbContext
        {
            get
            {
                return dbService.DbContext();
            }
        }
        public string RegisterClientList(string clientsAsJson)
        {
            try
            {
                var clientListTO = JsonLoader.LoadJson<ClientListTO>(clientsAsJson);
                //TODO exception handling
                if (clientListTO == null) throw new Exception("clientList not found");
                foreach (var clinetTO in clientListTO.Clients)
                {
                    Client client = mapper.Map<Client>(clinetTO);
                    var validationResult = clientValidator.Validate(client);
                    if(!validationResult.IsValid) continue;
                    //if (!cv.Validate(client).IsValid) continue;
                    _dbContext.Clients.AddRange(client);
                }
                _dbContext.SaveChanges();
                return "Ok";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public interface IClientProvider
    {
        public string RegisterClientList(string clientsAsJson);
    }
}
