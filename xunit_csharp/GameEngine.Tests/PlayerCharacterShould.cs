using GameEngine.Tests.DataDrivenTests;
using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould : IDisposable
    {
        private readonly PlayerCharacter player;
        ITestOutputHelper outputHelper;
        public PlayerCharacterShould(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            player = new PlayerCharacter();
        }

        public void Dispose()
        {
            outputHelper.WriteLine("Disposing!");
        }

        [Fact]
        public void BeInexperiencedWhenNew()
        {
            Assert.True(player.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            var firstName = "Sarah";
            var lastName = "Smith";
            player.FirstName = firstName;
            player.LastName = lastName;

            Assert.StartsWith(firstName, player.FullName);
            Assert.EndsWith(lastName, player.FullName);
            Assert.Equal(firstName + " " + lastName, player.FullName, ignoreCase: true);
        }

        [Fact]
        public void StartWithDefaultHealth()
        {
            Assert.Equal(100, player.Health);
        }

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            player.Sleep();

            Assert.InRange(player.Health, 101, 200);
        }

        [Fact]
        public void NotHaveNickNameByDefault()
        {
            Assert.Null(player.Nickname);
        }

        #region Weapons
        [Fact]
        public void HaveALongBown()
        {
            Assert.Contains("Long Bow", player.Weapons);
        }

        [Fact]
        public void HaveAtLeastOneSword()
        {
            Assert.Contains(player.Weapons, w => w.Contains("Sword"));
        }

        [Fact]
        public void HaveNoEmptyDefaultWeapons()
        {
            Assert.All(player.Weapons, w => Assert.False(string.IsNullOrEmpty(w)));
        }
        #endregion

        #region Events
        [Fact]
        public void RaiseSleptEvent()
        {
            Assert.Raises<EventArgs>(
                handler => player.PlayerSlept += handler,
                handler => player.PlayerSlept -= handler,
                () => player.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            Assert.PropertyChanged(player, "Health", () => player.TakeDamage(10));
        }
        #endregion

        #region DataDrivenTests
        [Fact]
        [Trait("Category", "Player_DataDriven")]
        public void TakeZeroDamage()
        {
            player.TakeDamage(0);
            Assert.Equal(100, player.Health);
        }

        [Fact]
        [Trait("Category", "Player_DataDriven")]
        public void TakeSmallAmoundOfDamage()
        {
            player.TakeDamage(1);
            Assert.Equal(99, player.Health);
        }

        [Fact]
        [Trait("Category", "Player_DataDriven")]
        public void TakeMediumAmounOfDamage()
        {
            player.TakeDamage(50);
            Assert.Equal(50, player.Health);
        }

        [Fact]
        [Trait("Category", "Player_DataDriven")]
        public void HaveMinimum1Health()
        {
            player.TakeDamage(1000);
            Assert.Equal(1, player.Health);
        }

        [Theory]
        [MemberData(nameof(TestData_HealthDamageInternal.TestData), MemberType = typeof(TestData_HealthDamageInternal))]
        public void TakeDamage(int damage, int expectedHealth)
        {
            player.TakeDamage(damage);
            Assert.Equal(expectedHealth, player.Health);
        }
        #endregion

    }
}
