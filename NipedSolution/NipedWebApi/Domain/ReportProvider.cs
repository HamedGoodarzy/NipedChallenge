using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NipedModel;
using NipedWebApi.Data;
using NipedWebApi.Data.Model;
using WebApplication1.Services;

namespace NipedWebApi.Domain
{
    public class ReportProvider(INipedDbService dbService, IMapper mapper, IRuleEvaluator ruleEvaluator) : IReportProvider
    {
        private NipedDbContext _dbContext
        {
            get
            {
                return dbService.DbContext();
            }
        }
        public List<ClientReportTO> GetClinetsReportV1()
        {
            throw new NotImplementedException("This method is deprecated. Use GetClinetsReport instead.");
        }
        public List<ClientReportTO> GetClinetsReportV2()
        {
            List<Client> dbClinets = _dbContext.Clients.Include(c=> c.Bloodwork).Include(c=>c.Questionnaire).ToList();
            var dbGuideline = _dbContext.Guidelines.Include(g=> g.ValueGuidelines).FirstOrDefault(); 
            var guidelneTO = mapper.Map<GuidelineTO>(dbGuideline);
            IReportGenerator reportGenerator = new ReportGenerator(guidelneTO, ruleEvaluator);
            var clientsReportTO = new List<ClientReportTO>();
            foreach (var dbClient in dbClinets)
            {
                ClientTO clientTO = mapper.Map<ClientTO>(dbClient);
                clientsReportTO.Add(new ClientReportTO()
                {
                    Client = clientTO,
                    ReportEntries = reportGenerator.GenerateReportEntries(clientTO),
                });
            }
            return clientsReportTO;
        }
    }
    public interface IReportProvider
    {
        public List<ClientReportTO> GetClinetsReportV1();
        public List<ClientReportTO> GetClinetsReportV2();
    }
}
