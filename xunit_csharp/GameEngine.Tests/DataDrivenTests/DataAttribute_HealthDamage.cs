using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace GameEngine.Tests.DataDrivenTests
{
    class DataAttribute_HealthDamage : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return TestData_HealthDamageCSV.TestData;
        }
    }
}
