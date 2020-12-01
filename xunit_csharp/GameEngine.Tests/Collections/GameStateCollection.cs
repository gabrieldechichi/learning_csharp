using GameEngine.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameEngine.Tests.Collections
{
    [CollectionDefinition(GameStateCollection.GameStateCollectionName)]
    public class GameStateCollection : ICollectionFixture<GameStateFixture>
    {
        public const string GameStateCollectionName = "GameState Collection";
    }
}
