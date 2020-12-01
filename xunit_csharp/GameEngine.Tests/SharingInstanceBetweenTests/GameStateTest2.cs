using GameEngine.Tests.Collections;
using GameEngine.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests.SharingInstanceBetweenTests
{
    [Collection(GameStateCollection.GameStateCollectionName)]
    [Trait("Category", "SharedInstance")]
    public class GameStateTest2
    {
        private readonly GameStateFixture gameStateFixture;
        private readonly ITestOutputHelper output;

        public GameStateTest2(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            this.gameStateFixture = gameStateFixture;
            this.output = output;
        }

        [Fact]
        public void Test3()
        {
            output.WriteLine($"Test 3 GameState ID = {gameStateFixture.Instance.Id}");
        }

        [Fact]
        public void Test4()
        {
            output.WriteLine($"Test 4 GameState ID = {gameStateFixture.Instance.Id}");
        }
    }
}
