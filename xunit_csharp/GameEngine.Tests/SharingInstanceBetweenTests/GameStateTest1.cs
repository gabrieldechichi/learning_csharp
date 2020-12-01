using GameEngine.Tests.Collections;
using GameEngine.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests.SharingInstanceBetweenTests
{
    [Collection(GameStateCollection.GameStateCollectionName)]
    [Trait("Category", "SharedInstance")]
    public class GameStateTest1
    {
        private readonly GameStateFixture gameStateFixture;
        private readonly ITestOutputHelper output;

        public GameStateTest1(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            this.gameStateFixture = gameStateFixture;
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            output.WriteLine($"Test 1 GameState ID = {gameStateFixture.Instance.Id}");
        }

        [Fact]
        public void Test2()
        {
            output.WriteLine($"Test 2 GameState ID = {gameStateFixture.Instance.Id}");
        }
    }
}
