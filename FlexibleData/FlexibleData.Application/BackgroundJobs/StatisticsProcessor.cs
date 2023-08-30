using FlexibleData.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleData.Application.BackgroundJobs
{
    public class StatisticsProcessor
    {
        #region Fields
        private readonly IFlexibleDataRepository _flexibleDatarepository;
        private readonly ILogger<StatisticsProcessor> _logger;
        #endregion

        #region Constructor
        public StatisticsProcessor(IFlexibleDataRepository flexibleDatarepository, ILogger<StatisticsProcessor> logger)
        {
            _flexibleDatarepository = flexibleDatarepository;
            _logger = logger;
        }
        #endregion

        #region Methods
        public void Process(Dictionary<string, string> insertedData)
        {
            foreach (var key in insertedData)
            {

            }
        }
        #endregion
    }
}
