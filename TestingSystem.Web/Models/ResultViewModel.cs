namespace TestingSystem.Web.Models
{
    using AutoMapper;

    using TestingSystem.Models;
    using TestingSystem.Web.Infrastructure.Mapping;
    
    public class ResultViewModel : IMapFrom<Result>, IHaveCustomMappings
    {
        public string CourseName { get; set; }

        public string TestName { get; set; }

        public double Points { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Result, ResultViewModel>()
                         .ForMember(rvm => rvm.CourseName, opt => opt.MapFrom(r => r.Test.Course.Name))
                         .ForMember(rvm => rvm.TestName, opt => opt.MapFrom(r => r.Test.Name))
                         .ReverseMap();
        }
    }
}