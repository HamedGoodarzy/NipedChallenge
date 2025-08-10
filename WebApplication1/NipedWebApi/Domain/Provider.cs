using AutoMapper;
using Microsoft.Extensions.Logging;
using NipedModel;
using NipedWebApi.Data;
using NipedWebApi.Data.Model;
using System.Text.Json;
using WebApplication1.Helpers;
using WebApplication1.Services;
using WebApplication1.ViewModels;

namespace NipedWebApi.Domain
{
    public class Provider(INipedDbService dbService, IMapper mapper) : IProvider
    {
        private NipedDbContext _dbContext
        {
            get
            {
                return dbService.DbContext();
            }
        }
        public GuidelineTO GetGuideline()
        {
            var guideline = _dbContext.Guidelines.FirstOrDefault();
            //TODO exception handling
            if (guideline == null) throw new Exception("gudeline not found");
            var guideLineTO = mapper.Map<GuidelineTO>(guideline);
            return guideLineTO;
        }
        public string RegisterGuideline(string guidelineAsJson)
        {
            var guidelineTO = JsonLoader.LoadJson<Dictionary<string, GuidelineTO>>(guidelineAsJson)["guidelines"];
            Guideline guideline = mapper.Map<Guideline>(guidelineTO);
            _dbContext.Guidelines.Add(guideline);
            _dbContext.SaveChanges();
            return "Ok";
        }
        public string RegisterClientList(string clientsAsJson)
        {
            var clientListTO = JsonLoader.LoadJson<ClientListTO>(clientsAsJson);
            //TODO exception handling
            if (clientListTO == null) throw new Exception("clientList not found");
            foreach (var clinetTO in clientListTO.Clients)
            {
                Client client = mapper.Map<Client>(clinetTO);
                _dbContext.Clients.AddRange(client);
            }
            _dbContext.SaveChanges();
            return "Ok";
        }

    }
    public interface IProvider
    {
        public GuidelineTO GetGuideline();
        public string RegisterGuideline(string guidelineAsJson);
        public string RegisterClientList(string clientsAsJson);
    }
}
