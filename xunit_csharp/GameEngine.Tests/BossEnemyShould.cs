using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    [Trait("Category", "Enemy")]
    public class BossEnemyShould
    {
        ITestOutputHelper outputHelper;

        public BossEnemyShould(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void HaveCorrectPower()
        {
            outputHelper.WriteLine("Creating Enemy");
            BossEnemy enemy = new BossEnemy();

            Assert.Equal(166.667, enemy.TotalSpecialAttackPower, 3);
        }


    }
}
