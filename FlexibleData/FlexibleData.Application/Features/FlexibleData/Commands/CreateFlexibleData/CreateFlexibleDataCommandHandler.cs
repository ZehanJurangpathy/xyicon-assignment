using AutoMapper;
using FlexibleData.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData
{
    public class CreateFlexibleDataCommandHandler : IRequestHandler<CreateFlexibleDataCommand, CreateFlexibleDataCommandVm>
    {
        #region Fields
        private readonly IFlexibleDataRepository _flexibleDataRepository;
        private readonly ILogger<CreateFlexibleDataCommandHandler> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="CreateFlexibleDataCommandHandler" /> class.</summary>
        /// <param name="flexibleDataRepository">The flexible data repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">mapper</param>
        public CreateFlexibleDataCommandHandler(IFlexibleDataRepository flexibleDataRepository
            , ILogger<CreateFlexibleDataCommandHandler> logger
            , IMapper mapper)
        {
            _flexibleDataRepository = flexibleDataRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Handler
        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<CreateFlexibleDataCommandVm> Handle(CreateFlexibleDataCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received flexible data to save: {data}", JsonConvert.SerializeObject(request.Data));

            var dataToSave = new Domain.Entities.FlexibleData
            {
                Id = new Guid(),
                Data = JsonConvert.SerializeObject(request.Data)
            };

            //save the details to the database
            await _flexibleDataRepository.CreateAsync(dataToSave);

            return _mapper.Map<CreateFlexibleDataCommandVm>(dataToSave);
        }
        #endregion
    }
}
