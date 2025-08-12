using AutoMapper;
using NipedModel;
using NipedWebApi.Data;
using NipedWebApi.Data.Model;
using WebApplication1.Helpers;

namespace NipedWebApi.Domain
{
    public class BaseInfoProvider(INipedDbService dbService, IMapper mapper) : IBaseInfoProvider
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
    }
        public interface IBaseInfoProvider
        {
            public GuidelineTO GetGuideline();
            public string RegisterGuideline(string guidelineAsJson);
        }
}
