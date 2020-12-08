﻿using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Store.Domain.Models;
using Store.Tests.Unit.Framework;

namespace Store.Tests.Unit.DomainTests.RepositoryTests.AddressRepositoryTests
{
    [TestFixture]
    public class When_adding_an_Address_range : Given_an_AddressRepository
    {
        private List<Address> _models;
        int _originalCount;

        protected override void Given()
        {
            base.Given();

            _models = new List<Address>
            {
                new Address
                {
                    Line1 = GetRandom.String(),
                    Line2 = GetRandom.String(),
                    City = GetRandom.String(),
                    StateId = 1,
                    PostalCode = GetRandom.String(1,10)
                },
                new Address
                {
                    Line1 = GetRandom.String(),
                    Line2 = GetRandom.String(),
                    City = GetRandom.String(),
                    StateId = 2,
                    PostalCode = GetRandom.String(1,10)
                }
            };

            _originalCount = SUT.CountAsync().Result;
        }

        protected override void When()
        {
            base.When();

            SUT.AddRangeAsync(AdminUserId, _models).Wait();
        }

        [Test]
        public void Then_the_new_addresses_should_have_an_Id()
        {
            _models.ForEach(x => x.Id.ShouldBeGreaterThan(0));
        }

        [Test]
        public void Then_the_new_addresses_should_be_added_to_the_table()
        {
            SUT.CountAsync().Result.ShouldBe(_originalCount + _models.Count);
        }
    }
}
