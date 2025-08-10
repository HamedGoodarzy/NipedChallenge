namespace NipedWebApi.mappings
{
    using AutoMapper;
    using NipedModel;
    using NipedWebApi.Data.Model;
    using System.Linq;

    public static class Mappings
    {
        public static Action<IMapperConfigurationExpression> MappingConfiguration = cfg =>
        {
            #region guideline mappings
            // Map the main GuidelineTO class to Guideline
            cfg.CreateMap<GuidelineTO, Guideline>()
                .ForMember(dest => dest.Cholesterol, opt => opt.MapFrom(src => src.Cholesterol));

            // Map the CholesterolGuidelineTO to CholesterolGuideline
            cfg.CreateMap<CholesterolGuidelineTO, CholesterolGuideline>()
                .ForMember(dest => dest.ValueGuidelines, opt => opt.MapFrom(src => new List<ValueGuideline>
                {
                //Map total  Guideline
                new ValueGuideline
                {
                    Label = "Total",
                    Optimal = src.Total.Optimal,
                    NeedsAttention = src.Total.NeedsAttention,
                    SeriousIssue = src.Total.SeriousIssue
                },
                //Map hdl Guideline
                new ValueGuideline
                {
                    Label = "Hdl",
                    Optimal = src.Hdl.Optimal,
                    NeedsAttention = src.Hdl.NeedsAttention,
                    SeriousIssue = src.Hdl.SeriousIssue
                },
                //Map ldl Guideline
                new ValueGuideline
                {
                    Label = "Ldl",
                    Optimal = src.Ldl.Optimal,
                    NeedsAttention = src.Ldl.NeedsAttention,
                    SeriousIssue = src.Ldl.SeriousIssue
                }
                }));

            // Map individual ValueGuidelineTO to ValueGuideline
            cfg.CreateMap<ValueGuidelineTO, ValueGuideline>()
                .ForMember(dest => dest.Label, opt => opt.Ignore()); // Label is set manually above

            // Map the main Guideline class to GuidelineTO
            cfg.CreateMap<Guideline, GuidelineTO>()
                .ForMember(dest => dest.Cholesterol, opt => opt.MapFrom(src => src.Cholesterol));

            // Map CholesterolGuideline to CholesterolGuidelineTO
            cfg.CreateMap<CholesterolGuideline, CholesterolGuidelineTO>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.ValueGuidelines.FirstOrDefault(vg => vg.Label == "Total")))
                .ForMember(dest => dest.Hdl, opt => opt.MapFrom(src => src.ValueGuidelines.FirstOrDefault(vg => vg.Label == "Hdl")))
                .ForMember(dest => dest.Ldl, opt => opt.MapFrom(src => src.ValueGuidelines.FirstOrDefault(vg => vg.Label == "Ldl")));

            // Map ValueGuideline to ValueGuidelineTO
            cfg.CreateMap<ValueGuideline, ValueGuidelineTO>();
            #endregion

            #region clinet mappings
            cfg.CreateMap<Client, ClientTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId))

            .ForMember(dest => dest.MedicalData, opt => opt.MapFrom(src => new MedicalDataTO
            {
                Bloodwork = new BloodworkTO
                {
                    Cholesterol = new CholesterolTO
                    {
                        Total = src.Bloodwork.CholesterolTotal,
                        Hdl = src.Bloodwork.CholesterolHdl,
                        Ldl = src.Bloodwork.CholesterolLdl
                    },
                    BloodSugar = src.Bloodwork.BloodSugar,
                    BloodPressure = new BloodPressureTO
                    {
                        Systolic = src.Bloodwork.BloodPressureSystolic,
                        Diastolic = src.Bloodwork.BloodPressureDiastolic
                    }
                },
                Questionnaire = new QuestionnaireTO
                {
                    ExerciseWeeklyMinutes = src.Questionnaire.ExerciseWeeklyMinutes,
                    SleepQuality = src.Questionnaire.SleepQuality,
                    StressLevels = src.Questionnaire.StressLevels,
                    DietQuality = src.Questionnaire.DietQuality
                }
            }));


            cfg.CreateMap<ClientTO, Client>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Bloodwork, opt => opt.MapFrom(src => new Bloodwork
                  {
                      CholesterolTotal = src.MedicalData.Bloodwork.Cholesterol.Total,
                      CholesterolHdl = src.MedicalData.Bloodwork.Cholesterol.Hdl,
                      CholesterolLdl = src.MedicalData.Bloodwork.Cholesterol.Ldl,
                      BloodSugar = src.MedicalData.Bloodwork.BloodSugar,
                      BloodPressureSystolic = src.MedicalData.Bloodwork.BloodPressure.Systolic,
                      BloodPressureDiastolic = src.MedicalData.Bloodwork.BloodPressure.Diastolic
                  }))
                  .ForMember(dest => dest.Questionnaire, opt => opt.MapFrom(src => new Questionnaire
                  {
                      ExerciseWeeklyMinutes = src.MedicalData.Questionnaire.ExerciseWeeklyMinutes,
                      SleepQuality = src.MedicalData.Questionnaire.SleepQuality,
                      StressLevels = src.MedicalData.Questionnaire.StressLevels,
                      DietQuality = src.MedicalData.Questionnaire.DietQuality
                  }));
            #endregion
        };
    }
}
