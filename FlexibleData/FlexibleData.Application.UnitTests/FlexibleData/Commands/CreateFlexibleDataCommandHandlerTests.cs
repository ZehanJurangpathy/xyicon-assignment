using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace FlexibleData.Application.UnitTests.FlexibleData.Commands
{
    public class CreateFlexibleDataCommandHandlerTests : BaseUnitTest
    {
        #region Fields
        private readonly Mock<IFlexibleDataRepository> _mockFlexibleDataRepository;
        private readonly Mock<ILogger<CreateFlexibleDataCommandHandler>> _mockLogger;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="CreateFlexibleDataCommandHandlerTests" /> class.</summary>
        public CreateFlexibleDataCommandHandlerTests()
        {
            _mockFlexibleDataRepository = RepositoryMocks.GetFlexibleDataRepository();
            _mockLogger = new Mock<ILogger<CreateFlexibleDataCommandHandler>>();
        }
        #endregion

        #region TestMethods
        [Fact]
        [Trait("Feature", "CreateFlexibleData")]
        public async Task Should_SaveFlexibleData_When_FlexibleDataIsProvided()
        {
            //arrange
            //create test data
            var testData = new Dictionary<string, string>
            {
                { "Telephone", "TP23142" },
                { "Chair", "CHR1234" },
                { "Desk", "DSK2123" }
            };

            _mockFlexibleDataRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.FlexibleData>()))
                .Returns(Task.CompletedTask);

            //act
            var handler = new CreateFlexibleDataCommandHandler(
                _mockFlexibleDataRepository.Object
                , _mockLogger.Object
                , _mapper);

            var result = await handler.Handle(new CreateFlexibleDataCommand
            {
                Data = testData
            }, CancellationToken.None);

            //assert
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(Guid.Empty);

        }
        #endregion
    }
}
