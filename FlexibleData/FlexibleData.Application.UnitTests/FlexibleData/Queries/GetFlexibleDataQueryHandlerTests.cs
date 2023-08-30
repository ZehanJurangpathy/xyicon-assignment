using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Application.Features.FlexibleData.Queries.GetFlexibleData;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using System.Linq.Expressions;

namespace FlexibleData.Application.UnitTests.FlexibleData.Queries
{
    public class GetFlexibleDataQueryHandlerTests : BaseUnitTest
    {
        #region Fields
        private readonly Mock<IFlexibleDataRepository> _mockFlexibleDataRepository;
        private readonly Mock<ILogger<GetFlexibleDataQueryHandler>> _mockLogger;
        #endregion

        #region Constructor
        public GetFlexibleDataQueryHandlerTests()
        {
            _mockFlexibleDataRepository = RepositoryMocks.GetFlexibleDataRepository();
            _mockLogger = new Mock<ILogger<GetFlexibleDataQueryHandler>>();
        }
        #endregion

        #region Test Methods
        [Fact]
        [Trait("Feature", "GetFlexibleData")]
        public async Task Should_ReturnAllFlexibleData_When_NoIdIsProvided()
        {
            //arrange
            _mockFlexibleDataRepository.Setup(x => x.GetAsync(
                It.IsAny<Expression<Func<Domain.Entities.FlexibleData, bool>>>()
                , It.IsAny<Func<IQueryable<Domain.Entities.FlexibleData>, IOrderedQueryable<Domain.Entities.FlexibleData>>>()
                , string.Empty
                ))
                .ReturnsAsync(new List<Domain.Entities.FlexibleData>
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

            //act
            var handler = new GetFlexibleDataQueryHandler(
                _mockFlexibleDataRepository.Object
                , _mockLogger.Object);

            var result = await handler.Handle(new GetFlexibleDataQuery(), CancellationToken.None);

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

            _mockFlexibleDataRepository.Setup(x => x.GetByIdAsync(guid))
                .ReturnsAsync(new Domain.Entities.FlexibleData
                {
                    Id = guid,
                    Data = "{\"Telephone\":\"TP23142\",\"Chair\":\"CHR1234\",\"Desk\":\"DSK2123\"}"
                });

            //act
            var handler = new GetFlexibleDataQueryHandler(
                _mockFlexibleDataRepository.Object
                , _mockLogger.Object);

            var result = await handler.Handle(new GetFlexibleDataQuery { Id = guid }, CancellationToken.None);

            //assert
            result.Count().ShouldBe(1);
            result.First().Data.ShouldBe(resultObject);
        }
        #endregion
    }
}
