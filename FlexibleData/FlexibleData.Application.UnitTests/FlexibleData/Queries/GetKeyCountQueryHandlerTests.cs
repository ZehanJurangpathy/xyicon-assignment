using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Application.Features.FlexibleData.Queries.GetFlexibleData;
using FlexibleData.Application.Features.FlexibleData.Queries.GetKeyCount;
using FlexibleData.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Linq.Expressions;

namespace FlexibleData.Application.UnitTests.FlexibleData.Queries
{
    public class GetKeyCountQueryHandlerTests : BaseUnitTest
    {
        #region Fields
        private readonly Mock<IStatisticsRepository> _mockStatisticsRepository;
        private readonly Mock<ILogger<GetKeyCountQueryHandler>> _mockLogger;
        #endregion

        #region Constructor
        public GetKeyCountQueryHandlerTests()
        {
            _mockStatisticsRepository = RepositoryMocks.GetStatisticsRepository();
            _mockLogger = new Mock<ILogger<GetKeyCountQueryHandler>>();
        }
        #endregion

        #region Test Methods
        [Fact]
        [Trait("Feature", "GetKeyCount")]
        public async Task Should_ReturnCount_When_KeyIsInDatabase()
        {
            //arrange
            var key = "Telephone";

            _mockStatisticsRepository.Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new Statistics
                {
                    Key = key,
                    Count = 3
                });

            //act
            var handler = new GetKeyCountQueryHandler(
                _mockStatisticsRepository.Object
                , _mockLogger.Object
                , _mapper);

            var result = await handler.Handle(new GetKeyCountQuery { Key = key }, CancellationToken.None);

            //assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(1);
            result.First().Count.ShouldBe(3);
        }

        [Fact]
        [Trait("Feature", "GetKeyCount")]
        public async Task Should_ReturnEmptyList_When_KeyIsNotInDatabase()
        {
            //arrange
            var key = "Telephone";

            _mockStatisticsRepository.Setup(s => s.GetByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<Statistics>(null));

            //act
            var handler = new GetKeyCountQueryHandler(
                _mockStatisticsRepository.Object
                , _mockLogger.Object
                , _mapper);

            var result = await handler.Handle(new GetKeyCountQuery { Key = key }, CancellationToken.None);

            //assert
            result.Count().ShouldBe(0);
        }

        [Fact]
        [Trait("Feature", "GetKeyCount")]
        public async Task Should_ReturnAllKeysInDatabase_When_KeyIsNotProvided()
        {
            //arrange
            _mockStatisticsRepository.Setup(s => s.GetAsync(
                It.IsAny<Expression<Func<Statistics, bool>>>()
                , It.IsAny<Func<IQueryable<Statistics>, IOrderedQueryable<Statistics>>>()
                , string.Empty))
                .ReturnsAsync(new List<Statistics>
                {
                    new Statistics
                    {
                        Key = "Telephone",
                        Count = 1
                    },
                    new Statistics
                    {
                        Key = "Desk",
                        Count = 10
                    }
                });

            //act
            var handler = new GetKeyCountQueryHandler(
                _mockStatisticsRepository.Object
                , _mockLogger.Object
                , _mapper);

            var result = await handler.Handle(new GetKeyCountQuery { Key = null }, CancellationToken.None);

            //assert
            result.Count().ShouldBe(2);
            result.First().Key.ShouldBe("Telephone");
            result.First().Count.ShouldBe(1);
        }
        #endregion
    }
}
