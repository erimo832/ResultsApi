using NUnit.Framework;
using ResultManager.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model.ObjectMother;

namespace Tests.Rules
{
    public class HcpAvgThridOf18Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Rules_HcpAvgThridOf18_Should_CalculateHcp_NotAll18_Scenario_1_0()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(0, 60, 1);

            var rule = new RuleAvgTopThirdOf18();

            var value = rule.Calculate(player);

            Assert.IsTrue(value == 0.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThridOf18_Should_CalculateHcp_NotAll18_Scenario_1_1()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(2, 60, 1);

            var rule = new RuleAvgTopThirdOf18();

            var value = rule.Calculate(player);

            Assert.IsTrue(value == 60.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThridOf18_Should_CalculateHcp_NotAll18_Scenario_1_2()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(10, 60, 1);

            var rule = new RuleAvgTopThirdOf18();

            var value = rule.Calculate(player);

            Assert.IsTrue(value == 61.0);
            
            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThridOf18_Should_CalculateHcp_All18_Scenario_1_0()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(18, 60, 1);

            var rule = new RuleAvgTopThirdOf18();

            var value = rule.Calculate(player);

            Assert.IsTrue(value == 62.5);

            Assert.Pass();
        }


        [Test]
        public void Rules_HcpAvgThridOf18_Should_CalculateHcp_All18_Scenario_1_1()
        {
            var player = PlayerObjectMother.GetPlayer();
            player.Rounds = RoundObjectMother.GetRounds(30, 60, 1);

            var rule = new RuleAvgTopThirdOf18();

            var value = rule.Calculate(player);

            Assert.IsTrue(value == 62.5);

            Assert.Pass();
        }

        //TODO order by date
    }
}
