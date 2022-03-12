using Moq;
using Platform.SearchFight.Service.Engine.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Platform.SearchFight.Tests
{
    public class WinnerProcessorTest
    {
        private readonly Mock<IWinnerProcessor> _mock;
        public WinnerProcessorTest()
        {
            _mock = new Mock<IWinnerProcessor>();
        }

        [Fact]
        public void Should_process_search_report()
        {
            //Arrange
            //Act

        }
    }
}
