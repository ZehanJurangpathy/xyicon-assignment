using FlexibleData.Application.Contracts.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexibleData.Application.UnitTests
{
    public class RepositoryMocks
    {
        public static Mock<IFlexibleDataRepository> GetFlexibleDataRepository()
        {
            return new Mock<IFlexibleDataRepository>();
        }
    }
}
