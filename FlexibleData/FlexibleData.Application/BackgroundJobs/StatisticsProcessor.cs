using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                    SetUniqueValues(insertedData, key, existingKeyDetails);

                    await _statisticsRepository.UpdateAsync(existingKeyDetails);
                }
                else
                {
                    //key not found. first time the key is inserted to the table
                    var uniqueValues = new HashSet<string>() { insertedData[key] };
                    var statistics = new Statistics
                    {
                        Key = key,
                        Count = 1,
                        UniqueCount = JsonConvert.SerializeObject(uniqueValues)
                    };
                    SetUniqueValues(insertedData, key, statistics);

                    await _statisticsRepository.CreateAsync(statistics);
                }
            }
        }

        private static void SetUniqueValues(Dictionary<string, string> insertedData, string key, Statistics statistics)
        {
            //add existing values to a HashSet
            var uniqueValues = JsonConvert.DeserializeObject<HashSet<string>>(statistics.UniqueCount);
            //add the new values for the key to the HashSet. value will be added only when the value 
            //is not present in the HashSet
            uniqueValues.Add(insertedData[key]);
            statistics.UniqueCount = JsonConvert.SerializeObject(uniqueValues);
        }
        #endregion
    }
}
