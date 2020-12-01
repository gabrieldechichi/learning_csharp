using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEngine.Tests.DataDrivenTests
{
    class TestData_HealthDamageCSV
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                var csvLines = File.ReadAllLines("DataDrivenTests/TestData_HealthDamage.csv");

                return csvLines
                    .Select(l =>
                    {
                        return l.Split(",").Select(int.Parse).Cast<object>().ToArray();
                    });
            }
        }
    }
}
