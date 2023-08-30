using FlexibleData.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FlexibleData.Application.Features.FlexibleData.Queries.GetFlexibleData
{
    public class GetFlexibleDataQueryHandler : IRequestHandler<GetFlexibleDataQuery, IEnumerable<GetFlexibleDataQueryVm>>
    {
        #region Fields
        private readonly IFlexibleDataRepository _flexibleDataRepository;
        private readonly ILogger<GetFlexibleDataQueryHandler> _logger;
        #endregion

        #region Constructor
        public GetFlexibleDataQueryHandler(IFlexibleDataRepository flexibleDataRepository, ILogger<GetFlexibleDataQueryHandler> logger)
        {
            _flexibleDataRepository = flexibleDataRepository;
            _logger = logger;
        }
        #endregion

        #region Handler
        public async Task<IEnumerable<GetFlexibleDataQueryVm>> Handle(GetFlexibleDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Flexible Data Id: {id}", request.Id);

            var flexibleDataList = new List<Domain.Entities.FlexibleData>();

            if (request.Id is null)
            {
                //no id is passed. get all flexible objects
                flexibleDataList.AddRange(await _flexibleDataRepository.GetAsync());
            }
            else
            {
                //id passed
                flexibleDataList.Add(await _flexibleDataRepository.GetByIdAsync(request.Id));
            }

            _logger.LogInformation("Flexible Data count retrieved from database: {count}", flexibleDataList.Count());
            //map the data objects
            var result = new List<GetFlexibleDataQueryVm>();
            foreach (var item in flexibleDataList)
            {
                result.Add(new GetFlexibleDataQueryVm
                {
                    Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.Data)
                });
            }

            return result.AsEnumerable();
        }
        #endregion
    }
}
