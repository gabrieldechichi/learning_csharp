using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Tests.Fixtures
{
    public class FixtureSingletonBase<T> : IDisposable where T : new()
    {
        public T Instance { get; private set; }

        public FixtureSingletonBase()
        {
            Instance = new T();
        }

        public void Dispose()
        {
            //
        }
    }
}
