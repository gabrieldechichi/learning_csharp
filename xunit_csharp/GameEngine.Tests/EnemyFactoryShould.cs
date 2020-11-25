using Xunit;

namespace GameEngine.Tests
{
    [Trait("Category", "Factories")]
    public class EnemyFactoryShould
    {
        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            var factory = new EnemyFactory();

            var enemy = factory.Create("Zombie");

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact(Skip = "This is some skip reason")]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            var factory = new EnemyFactory();

            var enemy = factory.Create("Zombie King", isBoss: true);

            var boss = Assert.IsType<BossEnemy>(enemy);

            Assert.Equal("Zombie King", boss.Name);
        }

        [Fact]
        public void CreateBossEnemy_CreateAssignableTypes()
        {
            var factory = new EnemyFactory();

            var enemy = factory.Create("Zombie King", isBoss: true);

            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            var factory = new EnemyFactory();

            var enemy1 = factory.Create("Zombie");
            var enemy2 = factory.Create("Zombie");

            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            var factory = new EnemyFactory();

            var ex = Assert.Throws<System.ArgumentNullException>("name", () => factory.Create(null));

            //additional Asserts with exception
        }
    }
}
