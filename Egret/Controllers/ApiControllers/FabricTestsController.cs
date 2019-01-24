using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Egret.DataAccess;
using Egret.Models;

namespace Egret.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Item_Read")]
    [ApiController]
    public class FabricTestsController : ControllerBase
    {
        private readonly EgretContext _context;

        public FabricTestsController(EgretContext context)
        {
            _context = context;
        }

        // GET: api/FabricTests
        [HttpGet]
        public IEnumerable<FabricTest> GetFabricTests()
        {
            return _context.FabricTests;
        }

        // GET: api/FabricTests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFabricTest([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fabricTest = await _context.FabricTests.FindAsync(id);

            if (fabricTest == null)
            {
                return NotFound();
            }

            return Ok(fabricTest);
        }

        // PUT: api/FabricTests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFabricTest([FromRoute] string id, [FromBody] FabricTest fabricTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fabricTest.Id)
            {
                return BadRequest();
            }

            _context.Entry(fabricTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricTestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FabricTests
        [HttpPost]
        public async Task<IActionResult> PostFabricTest([FromBody] FabricTest fabricTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FabricTests.Add(fabricTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFabricTest", new { id = fabricTest.Id }, fabricTest);
        }

        // DELETE: api/FabricTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFabricTest([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fabricTest = await _context.FabricTests.FindAsync(id);
            if (fabricTest == null)
            {
                return NotFound();
            }

            _context.FabricTests.Remove(fabricTest);
            await _context.SaveChangesAsync();

            return Ok(fabricTest);
        }

        private bool FabricTestExists(string id)
        {
            return _context.FabricTests.Any(e => e.Id == id);
        }
    }
}