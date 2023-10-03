using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TodoController : ControllerBase
    {
        private readonly ApiDbContext _context;

    public TodoController(ApiDbContext context)
        {
            _context = context;
        }



        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem(Item item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Items.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("AddItem", new { item.Id }, item);
                }catch(Exception ex)
                {
                    NotFound(ex);
                }
                
            }
            return new JsonResult("somthing went wrong"){StatusCode =500};

        }

        [HttpGet]
        [Route("GetAllItem")]
        public async Task<IActionResult> GetAllItem()
        {
           
            var data = await _context.Items.ToListAsync();
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }


        [HttpGet]
        [Route("GetItem/{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var data= await _context.Items.FirstOrDefaultAsync(x=>x.Id==id);
           
            if (data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem(int id,Item item)
        {
            if (id !=item.Id)
            {
                return NotFound();
            }
            var existItem= await _context.Items.FirstOrDefaultAsync(x=>x.Id==id);
           
            if (existItem == null)
            {
                return BadRequest();
            }
           
         
            existItem.Title =item.Title;   
            existItem.Description =item.Description;
            existItem.Done =item.Done;
            await _context.SaveChangesAsync();  

            return NoContent();
         
        }
        [HttpDelete]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeleteItem(int id)
        {
          
            var existItem = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (existItem == null)
            {
                return BadRequest();
            }


         _context.Items.Remove(existItem);
            await _context.SaveChangesAsync();
            

            return Ok(existItem);

        }


    }
   

}
