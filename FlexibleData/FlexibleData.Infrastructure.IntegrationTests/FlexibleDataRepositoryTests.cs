using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Application.Features.FlexibleData.Queries.GetFlexibleData;
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

        [Fact]
        [Trait("Feature", "GetFlexibleData")]
        public async Task Should_ReturnAllFlexibleData_When_NoIdIsProvided()
        {
            //arrange
            await _flexibleDataContext.FlexibleData.AddRangeAsync(new List<Domain.Entities.FlexibleData>
            {
                new Domain.Entities.FlexibleData
                {
                    Id = Guid.NewGuid(),
                    Data = "{\"Telephone\":\"TP23142\",\"Chair\":\"CHR1234\",\"Desk\":\"DSK2123\"}"
                },
                new Domain.Entities.FlexibleData
                {
                    Id = Guid.NewGuid(),
                    Data = "{\"Telephone\":\"TP2333ww\",\"Chair\":\"CHR1434\",\"Desk\":\"DSK21553\"}"
                }
            });
            await _flexibleDataContext.SaveChangesAsync();

            //act
            var result = await _flexibleDataRepository.GetAsync();

            //assert
            result.Count().ShouldBe(2);
        }

        [Fact]
        [Trait("Feature", "GetFlexibleData")]
        public async Task Should_ReturnSingleFlexibleData_When_IdIsProvided()
        {
            //arrange
            var guid = Guid.NewGuid();
            var resultObject = JsonConvert.DeserializeObject<Dictionary<string, string>>("{\"Telephone\":\"TP23142\",\"Chair\":\"CHR1234\",\"Desk\":\"DSK2123\"}");

            await _flexibleDataContext.FlexibleData.AddRangeAsync(new List<Domain.Entities.FlexibleData>
            {
                new Domain.Entities.FlexibleData
                {
                    Id = guid,
                    Data = "{\"Telephone\":\"TP23142\",\"Chair\":\"CHR1234\",\"Desk\":\"DSK2123\"}"
                },
                new Domain.Entities.FlexibleData
                {
                    Id = Guid.NewGuid(),
                    Data = "{\"Telephone\":\"TP2333ww\",\"Chair\":\"CHR1434\",\"Desk\":\"DSK21553\"}"
                }
            });
            await _flexibleDataContext.SaveChangesAsync();

            //act
            var result = await _flexibleDataRepository.GetByIdAsync(guid);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(guid);
        }
        #endregion
    }
}
