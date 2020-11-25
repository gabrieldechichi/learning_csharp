using GameEngine.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    [Trait("Category", "GameState")]
    public class GameStateShould : IClassFixture<GameStateFixture>
    {
        private readonly GameStateFixture gameStateFixture;
        private readonly ITestOutputHelper output;

        public GameStateShould(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            this.gameStateFixture = gameStateFixture;
            this.output = output;
        }

        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            gameStateFixture.Instance.Players.Add(player1);
            gameStateFixture.Instance.Players.Add(player2);

            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

            gameStateFixture.Instance.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact]
        public void Reset()
        {
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            gameStateFixture.Instance.Players.Add(player1);
            gameStateFixture.Instance.Players.Add(player2);

            gameStateFixture.Instance.Reset();

            Assert.Empty(gameStateFixture.Instance.Players);            
        }
    }
}
