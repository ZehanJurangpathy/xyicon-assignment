using AutoMapper;
using FlexibleData.Application.Profiles;
using FlexibleData.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FlexibleData.Infrastructure.IntegrationTests
{
    public class BaseUnitTest
    {
        #region Fields
        protected readonly FlexibleDataContext _flexibleDataContext;
        #endregion

        #region Constructor
        public BaseUnitTest()
        {
            //set the db context options
            var dbContextOptions = new DbContextOptionsBuilder<FlexibleDataContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .ConfigureWarnings(x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                            .EnableSensitiveDataLogging()
                            .Options;

            _flexibleDataContext = new FlexibleDataContext(dbContextOptions);
        }
        #endregion
    }
}
