using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shouldly;

namespace FlexibleData.Infrastructure.IntegrationTests
{
    public class FlexibleDataRepositoryTests : BaseUnitTest
    {
        #region Fields
        private readonly IFlexibleDataRepository _flexibleDataRepository;
        #endregion

        #region Constructor
        public FlexibleDataRepositoryTests()
        {
            _flexibleDataRepository = new FlexibleDataRepository(_flexibleDataContext);
        }
        #endregion

        #region Test Methods
        [Fact]
        [Trait("Feature", "CreateFlexibleData")]
        public async Task Should_SaveFlexibleData_When_FlexibleDataIsProvided()
        {
            //arrange
            var beforeCount = await _flexibleDataContext.FlexibleData.CountAsync();

            var testData = new Dictionary<string, string>
            {
                { "Telephone", "TP23142" },
                { "Chair", "CHR1234" },
                { "Desk", "DSK2123" }
            };

            var flexibleDate = new Domain.Entities.FlexibleData
            {
                Id = Guid.NewGuid(),
                Data = JsonConvert.SerializeObject(testData)
            };

            //act
            await _flexibleDataRepository.CreateAsync(flexibleDate);

            //assert
            var afterCount = await _flexibleDataContext.FlexibleData.CountAsync();
            afterCount.ShouldBe(beforeCount + 1);
        }
        #endregion
    }
}
