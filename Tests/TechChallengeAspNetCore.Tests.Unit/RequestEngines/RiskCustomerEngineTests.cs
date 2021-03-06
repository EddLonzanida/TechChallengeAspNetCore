using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Shouldly;
using TechChallengeAspNetCore.Business.Common.Entities;
using TechChallengeAspNetCore.Business.RequestEngines;
using TechChallengeAspNetCore.Business.Requests;
using TechChallengeAspNetCore.Business.Responses;
using TechChallengeAspNetCore.Tests.Unit.BaseClasses;
using Xunit;

namespace TechChallengeAspNetCore.Tests.Unit.RequestEngines
{
    public class RiskCustomerEngineTests : EngineTestBase<RiskCustomerRequest, RiskCustomerResponse>
    {
        public RiskCustomerEngineTests()
        {
            engine = new RiskCustomerEngine(customerRepository, betRepository);
        }

        [Fact]
        public async Task Engine_ShouldHandleNullData()
        {
            List<Bet> nullBetData = null;
            betRepository.GetAllAsync(Arg.Any<int>()).Returns(nullBetData);
            var request = new RiskCustomerRequest(1);

            var response = await engine.GetAsync(request);

            await betRepository.Received(1).GetAllAsync(Arg.Any<int>());
        }

        [Fact]
        public async Task Engine_ShouldReturnRiskCustomers()
        {
            betRepository.GetAllAsync(Arg.Any<int>()).Returns(betStub);
            customerRepository.GetAllAsync(Arg.Any<int>()).Returns(customerStub);
            var request = new RiskCustomerRequest(1);

            var response = await engine.GetAsync(request);

            response.RiskCustomers.Count().ShouldBe(7);
            var c1 = response.RiskCustomers.First(r => r.Id == 1);
            var c2 = response.RiskCustomers.First(r => r.Id == 2);
            var c3 = response.RiskCustomers.First(r => r.Id == 3);
            var c4 = response.RiskCustomers.First(r => r.Id == 4);
            var c5 = response.RiskCustomers.First(r => r.Id == 5);
            var c6 = response.RiskCustomers.First(r => r.Id == 6);
            var c7 = response.RiskCustomers.First(r => r.Id == 7);
            c1.Bets.Count.ShouldBe(1);
            c2.Bets.Count.ShouldBe(1);
            c3.Bets.Count.ShouldBe(2);
            c4.Bets.Count.ShouldBe(1);
            c5.Bets.Count.ShouldBe(3);
            c6.Bets.Count.ShouldBe(3);
            c7.Bets.Count.ShouldBe(3);
        }
    }
}
