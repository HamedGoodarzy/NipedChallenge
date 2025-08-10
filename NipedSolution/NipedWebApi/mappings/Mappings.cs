using AutoMapper;
using NipedModel;
using NipedWebApi.Data.Model;
using System.Linq;
namespace NipedWebApi.mappings
{
    public static class Mappings
    {
        public static Action<IMapperConfigurationExpression> MappingConfiguration = cfg =>
        {
            #region guideline mappings
            // Map the main GuidelineTO class to Guideline
            cfg.CreateMap<GuidelineTO, Guideline>()
                .ForMember(dest => dest.ValueGuidelines, opt => opt.MapFrom(src => new List<ValueGuideline>
                {
                    new ValueGuideline
                    {
                        Category = "Cholesterol.Total",
                        Optimal = src.Cholesterol.Total.Optimal,
                        NeedsAttention = src.Cholesterol.Total.NeedsAttention,
                        SeriousIssue = src.Cholesterol.Total.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "Cholesterol.Hdl",
                        Optimal = src.Cholesterol.Hdl.Optimal,
                        NeedsAttention = src.Cholesterol.Hdl.NeedsAttention,
                        SeriousIssue = src.Cholesterol.Hdl.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "Cholesterol.Ldl",
                        Optimal = src.Cholesterol.Ldl.Optimal,
                        NeedsAttention = src.Cholesterol.Ldl.NeedsAttention,
                        SeriousIssue = src.Cholesterol.Ldl.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "BloodSugar",
                        Optimal = src.BloodSugar.Optimal,
                        NeedsAttention = src.BloodSugar.NeedsAttention,
                        SeriousIssue = src.BloodSugar.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "ExerciseWeeklyMinutes",
                        Optimal = src.ExerciseWeeklyMinutes.Optimal,
                        NeedsAttention = src.ExerciseWeeklyMinutes.NeedsAttention,
                        SeriousIssue = src.ExerciseWeeklyMinutes.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "SleepQuality",
                        Optimal = src.SleepQuality.Optimal,
                        NeedsAttention = src.SleepQuality.NeedsAttention,
                        SeriousIssue = src.SleepQuality.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "StressLevels",
                        Optimal = src.StressLevels.Optimal,
                        NeedsAttention = src.StressLevels.NeedsAttention,
                        SeriousIssue = src.StressLevels.SeriousIssue
                    },
                    new ValueGuideline
                    {
                        Category = "DietQuality",
                        Optimal = src.DietQuality.Optimal,
                        NeedsAttention = src.DietQuality.NeedsAttention,
                        SeriousIssue = src.DietQuality.SeriousIssue
                    }
                }));

            // Map the CholesterolGuidelineTO to CholesterolGuideline
            //cfg.CreateMap<CholesterolGuidelineTO, ValueGuideline>()
            //    .ForMember(dest => dest.ValueGuidelines, opt => opt.MapFrom(src => new List<ValueGuideline>
            //    {
            //    //Map total  Guideline
            //    new ValueGuideline
            //    {
            //        Label = "Total",
            //        Optimal = src.Total.Optimal,
            //        NeedsAttention = src.Total.NeedsAttention,
            //        SeriousIssue = src.Total.SeriousIssue
            //    },
            //    //Map hdl Guideline
            //    new ValueGuideline
            //    {
            //        Label = "Hdl",
            //        Optimal = src.Hdl.Optimal,
            //        NeedsAttention = src.Hdl.NeedsAttention,
            //        SeriousIssue = src.Hdl.SeriousIssue
            //    },
            //    //Map ldl Guideline
            //    new ValueGuideline
            //    {
            //        Label = "Ldl",
            //        Optimal = src.Ldl.Optimal,
            //        NeedsAttention = src.Ldl.NeedsAttention,
            //        SeriousIssue = src.Ldl.SeriousIssue
            //    }
            //    }));

            //// Map individual ValueGuidelineTO to ValueGuideline
            //cfg.CreateMap<ValueGuidelineTO, ValueGuideline>()
            //    .ForMember(dest => dest.Label, opt => opt.Ignore()); // Label is set manually above

            //// Map the main Guideline class to GuidelineTO
            //cfg.CreateMap<Guideline, GuidelineTO>()
            //    .ForMember(dest => dest.Cholesterol, opt => opt.MapFrom(src => src.Cholesterol));

            //// Map CholesterolGuideline to CholesterolGuidelineTO
            //cfg.CreateMap<CholesterolGuideline, CholesterolGuidelineTO>()
            //    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.ValueGuidelines.FirstOrDefault(vg => vg.Label == "Total")))
            //    .ForMember(dest => dest.Hdl, opt => opt.MapFrom(src => src.ValueGuidelines.FirstOrDefault(vg => vg.Label == "Hdl")))
            //    .ForMember(dest => dest.Ldl, opt => opt.MapFrom(src => src.ValueGuidelines.FirstOrDefault(vg => vg.Label == "Ldl")));

            // Map ValueGuideline to ValueGuidelineTO

            cfg.CreateMap<Guideline, GuidelineTO>()
            .ForMember(dest => dest.Cholesterol, opt => opt.MapFrom(src => new CholesterolGuidelineTO
            {
                Total = MapValue(src, "Cholesterol.Total"),
                Hdl = MapValue(src, "Cholesterol.Hdl"),
                Ldl = MapValue(src, "Cholesterol.Ldl")
            }))
            .ForMember(dest => dest.BloodSugar, opt => opt.MapFrom(src => MapValue(src, "BloodSugar")))
            .ForMember(dest => dest.ExerciseWeeklyMinutes, opt => opt.MapFrom(src => MapValue(src, "ExerciseWeeklyMinutes")));
            //.ForMember(dest => dest.SleepQuality, opt => opt.MapFrom(src => MapValue(src, "SleepQuality")));
            //.ForMember(dest => dest.StressLevels, opt => opt.MapFrom(src => MapValue(src, "StressLevels")))
            //.ForMember(dest => dest.DietQuality, opt => opt.MapFrom(src => MapValue(src, "DietQuality")));




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

        private static ValueGuidelineTO MapValue(Guideline src, string category)
        {
            var v = src.ValueGuidelines.FirstOrDefault(x => x.Category == category);
            return new ValueGuidelineTO
            {
                Optimal = v?.Optimal ?? string.Empty,
                NeedsAttention = v?.NeedsAttention ?? string.Empty,
                SeriousIssue = v?.SeriousIssue ?? string.Empty
            };
        }
    }
}
