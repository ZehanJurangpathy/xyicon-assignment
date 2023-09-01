using AutoMapper;
using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Application.Extensions;
using FlexibleData.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlexibleData.Application.Features.FlexibleData.Queries.GetKeyCount
{
    public class GetKeyCountQueryHandler : IRequestHandler<GetKeyCountQuery, IEnumerable<GetKeyCountQueryVm>>
    {
        #region Fields
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<GetKeyCountQueryHandler> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public GetKeyCountQueryHandler(IStatisticsRepository statisticsRepository, ILogger<GetKeyCountQueryHandler> logger, IMapper mapper)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Handler
        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task<IEnumerable<GetKeyCountQueryVm>> Handle(GetKeyCountQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get count for key: {key}", request.Key);

            var statisticsList = new List<Statistics>();

            if (request.Key is null)
            {
                //no id is passed. get all flexible objects
                statisticsList.AddIfNotNull(await _statisticsRepository.GetAsync());
            }
            else
            {
                //id passed
                statisticsList.AddIfNotNull(await _statisticsRepository.GetByIdAsync(request.Key));
            }

            return _mapper.Map<IEnumerable<GetKeyCountQueryVm>>(statisticsList);
        }
        #endregion

    }
}
