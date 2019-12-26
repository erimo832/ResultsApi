using NUnit.Framework;
using ResultManager.Model;
using ResultManager.Rules;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Model.ObjectMother;

namespace Tests.Rules
{
    public class HcpAvgThirdCeiledTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Avg score tests

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_NoRounds()
        {
            var rounds = new List<Round>();

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 0.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_NotAll_Scenario_1_0()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59)
            });
            
            var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_NotAll_Scenario_1_1()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61)
            });

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_NotAll_Scenario_1_2()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62)
            });

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 59.5);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_NotAll_Scenario_1_3()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-05"), 62)
            });

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 59.5);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_NotAll_Scenario_1_4()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-05"), 63),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-06"), 64)
            });

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 59.5);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_All_Scenario_1_0()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61)
            });

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 3 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 59.0);

            Assert.Pass();
        }


        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateAvgScore_All_Scenario_1_1()
        {
            var rounds = RoundObjectMother.GetRounds(new List<Tuple<DateTime, int>>
            {
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-01"), 59),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-02"), 60),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-03"), 61),
                new Tuple<DateTime, int>(DateTime.Parse("2020-01-04"), 62)
            });

            var rule = new RuleAvgThirdCeiled() { TotalRounds = 3 };

            var value = rule.CalculateAvgScore(rounds);

            Assert.IsTrue(value == 60.0);

            Assert.Pass();
        }

        #endregion

        #region Hcp tests

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateHcp_NoRounds()
        {
            var avgScore = 0;
            var rule = new RuleAvgThirdCeiled();

            var value = rule.CalculateHcp(avgScore);

            Assert.IsTrue(value == 0.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateHcp_Scenario_1_0()
        {
            var avgScore = 48;
            var rule = new RuleAvgThirdCeiled();

            var value = rule.CalculateHcp(avgScore);

            Assert.IsTrue(value == 0.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateHcp_Scenario_1_1()
        {
            var avgScore = 58;
            var rule = new RuleAvgThirdCeiled();

            var value = rule.CalculateHcp(avgScore);

            Assert.IsTrue(value == 8.0);

            Assert.Pass();
        }

        [Test]
        public void Rules_HcpAvgThirdCeiled_Should_CalculateHcp_Scenario_1_2()
        {
            var avgScore = 50;
            var rule = new RuleAvgThirdCeiled();

            var value = rule.CalculateHcp(avgScore);

            Assert.IsTrue(value == 1.6);

            Assert.Pass();
        }

        #endregion
    }
}

