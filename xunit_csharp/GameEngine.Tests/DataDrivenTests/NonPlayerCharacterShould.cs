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
        [MemberData(nameof(TestData_HealthDamage.TestData), MemberType = typeof(TestData_HealthDamage))]
        public void TakeDamage(int damage, int expectedHealth)
        {
            var player = new PlayerCharacter();
            player.TakeDamage(damage);
            Assert.Equal(expectedHealth, player.Health);
        }
    }
}
