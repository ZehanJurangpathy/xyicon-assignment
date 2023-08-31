using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FlexibleData.Application.BackgroundJobs
{
    public class StatisticsProcessor
    {
        #region Fields
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<StatisticsProcessor> _logger;
        #endregion

        #region Constructor
        public StatisticsProcessor(IStatisticsRepository statisticsRepository, ILogger<StatisticsProcessor> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task Process(Dictionary<string, string> insertedData)
        {
            //loop through the inserted keys
            foreach (var key in insertedData.Keys)
            {
                //get key details from the statistics table
                var existingKeyDetails = await _statisticsRepository.GetByIdAsync(key);

                if (existingKeyDetails != null)
                {
                    //key already exist. assuming that for one key only one value will be inserted for one API call
                    //hence incrementing the count by one
                    existingKeyDetails.Count += 1;

                    await _statisticsRepository.UpdateAsync(existingKeyDetails);
                }
                else
                {
                    //key not found. first time the key is inserted to the table
                    await _statisticsRepository.CreateAsync(new Statistics
                    {
                        Key = key,
                        Count = 1
                    });
                }
            }
        }
        #endregion
    }
}
