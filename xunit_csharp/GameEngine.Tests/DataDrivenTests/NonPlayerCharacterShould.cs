using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameEngine.Tests.DataDrivenTests
{
    [Trait("Category", "NonPlayerCharacterShould")]
    public class NonPlayerCharacterShould
    {
        [Theory]
        [MemberData(nameof(TestData_HealthDamageCSV.TestData), MemberType = typeof(TestData_HealthDamageCSV))]
        public void TakeDamage(int damage, int expectedHealth)
        {
            var player = new PlayerCharacter();
            player.TakeDamage(damage);
            Assert.Equal(expectedHealth, player.Health);
        }
    }
}
