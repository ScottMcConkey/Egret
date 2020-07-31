﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Egret.Services;
using Egret.DataAccess;
using Egret.Models;
using System.Linq;
using FluentAssertions;

namespace Egret.IntegrationTests.Tests.Services
{
    public class SelectListFactoryServiceTests : IClassFixture<EgretDbContextFixture>
    {
        private SelectListFactoryService _selectListService;

        private EgretDbContext _context;

        public SelectListFactoryServiceTests(EgretDbContextFixture fixture)
        {
            _selectListService = new SelectListFactoryService(fixture.Context);
            _context = fixture.Context;
        }

        [Fact]
        public void Categories_all_returns_all()
        {
            var sut = _selectListService.CategoriesAll();
            sut.Should().NotBeEmpty();
            var cats = _context.InventoryCategories;
            sut.Count().Should().Be(cats.Count());
        }

        [Fact]
        public void Categories_active_returns_only_actives()
        {
            var sut = _selectListService.CategoriesActive();
            sut.Should().NotBeEmpty();
            bool inactiveExists = false;
            foreach (var item in sut.Items)
            {
                if (!((InventoryCategory)item).Active)
                {
                    inactiveExists = true;
                }
            }
            inactiveExists.Should().BeFalse();
        }

        [Fact]
        public void Units_all_returns_all()
        {
            var sut = _selectListService.UnitsAll();
            sut.Should().NotBeEmpty();
            var units = _context.Units;
            sut.Count().Should().Be(units.Count());
        }

        [Fact]
        public void Units_active_returns_only_actives()
        {
            var sut = _selectListService.UnitsAll();
            sut.Should().NotBeEmpty();
            bool inactiveExists = false;
            foreach (var item in sut.Items)
            {
                if (!((Unit)item).Active)
                {
                    inactiveExists = true;
                }
            }
            inactiveExists.Should().BeFalse();
        }
    }
}
