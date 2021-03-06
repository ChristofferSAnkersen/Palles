﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly PalleContext _context;

        public GiftsController(PalleContext context)
        {
            _context = context;
        }

        // GET: api/Gifts
        [HttpGet]
        public IEnumerable<Gift> GetGifts()
        {
            return _context.Gifts;
        }

        [HttpGet("Girl")]
        public async Task<IActionResult> GirlIndex()
        {
            var data = await _context.Gifts.Where(c => c.GirlGift == true).ToListAsync();
            return Ok(data);
        }


        [HttpGet("Boy")]
        public async Task<IActionResult> BoyIndex()
        {
            var data = await _context.Gifts.Where(c => c.BoyGift == true).ToListAsync();
            return Ok(data);
        }

        // GET: api/Gifts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGift([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gift = await _context.Gifts.FindAsync(id);

            if (gift == null)
            {
                return NotFound();
            }

            return Ok(gift);
        }

        //// PUT: api/Gifts/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGift([FromRoute] int id, [FromBody] Gift gift)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != gift.GiftNumber)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(gift).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GiftExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Gifts
        [HttpPost]
        public async Task<IActionResult> PostGift([FromBody] Gift gift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            gift.CreationDate = DateTime.Now;
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGift", new { id = gift.GiftNumber }, gift);
        }

        //// DELETE: api/Gifts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGift([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var gift = await _context.Gifts.FindAsync(id);
        //    if (gift == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Gifts.Remove(gift);
        //    await _context.SaveChangesAsync();

        //    return Ok(gift);
        //}

        private bool GiftExists(int id)
        {
            return _context.Gifts.Any(e => e.GiftNumber == id);
        }
    }
}