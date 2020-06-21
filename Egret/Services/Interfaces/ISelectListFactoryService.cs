using Egret.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Services
{
    public interface ISelectListFactoryService
    {
        SelectList CategoriesAll(int? selected = null);

        SelectList CategoriesActive(int? selected = null);

        SelectList CategoriesActivePlusCurrent(InventoryCategory selected);

        SelectList UnitsAll(string selected = null);

        SelectList UnitsActive(int? selected = null);

        SelectList UnitsActivePlusCurrent(Unit selected);

        SelectList CurrencyTypesAll(int? selected = null);

        SelectList CurrencyTypesActive(int? selected = null);

        SelectList CurrencyTypesPlusCurrent(CurrencyType selected);

        SelectList StorageLocationsAll(int? selected = null);

        SelectList StorageLocationsActive(int? selected = null);

        SelectList ResultsPerPage();
    }
}
