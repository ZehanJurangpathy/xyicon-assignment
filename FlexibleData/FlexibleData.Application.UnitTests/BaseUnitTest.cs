using AutoMapper;
using FlexibleData.Application.Profiles;
using Hangfire;
using Hangfire.MemoryStorage;

namespace FlexibleData.Application.UnitTests
{
    public class BaseUnitTest
    {
        #region Fields
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="BaseUnitTest" /> class.</summary>
        public BaseUnitTest()
        {
            //set the auto mapper profile
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            // Use in-memory storage for testing
            GlobalConfiguration.Configuration.UseMemoryStorage(); 
        }
        #endregion
    }
}
