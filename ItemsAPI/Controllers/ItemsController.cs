using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItemsAPI.DAL;
using ItemsAPI.Model;
using ItemsAPI.Repositories;
using ItemsAPI.Validators;
using Swashbuckle.AspNetCore;

namespace ItemsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _repository;
        public ItemsController(IItemRepository r)
        {
            _repository = r;
        }
        /// <summary>
        /// Retrieves all Items data.
        /// </summary>
        /// <returns>The Items data.</returns>
        // GET: api/Items
        [HttpGet]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "impactlevel", "pii" })]
        public async Task<ActionResult<IEnumerable<Items>>> GetItem()
        {
            return await _repository.GetAllItemsAsync();
        }

        /// <summary>
        /// Creates an Item
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// 
        ///     POST api/item/create
        ///     {        
        ///       "ItemName" : "Item4"
        ///         "Price" :"100" 
        ///     }
        /// </remarks>
 

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> Create([FromBody]Items item)
        {
            if (item.IsValid(out IEnumerable<string> errors))
            {
                var result = await _repository.Create(item);

                return CreatedAtAction(
                    nameof(GetItembyId),
                    new { id = result.ItemId}, result);
            }
            else
            {
                return BadRequest(errors);
            }
        }

        /// <summary>
        /// Retrieves an Item based on ID
        /// </summary>
  

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
  public IActionResult GetItembyId(int id)
        {
            
                var result = _repository.GetItembyId(id);
            if(result != null)
            { 
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an Item
        /// </summary>
  
       
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
           
                var result = await _repository.Delete(id);

            if (result )
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }


        }

        /// <summary>
        /// Updates an Item
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// 
        ///     POST api/item/update
        ///     {
        ///         "ItemId" : "1" 
        ///       "ItemName" : "Item4"
        ///         "Price" :"100" 
        ///     }
        /// </remarks>

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public async Task<IActionResult> Update([FromBody]Items  item)
        {

            if (item.IsValid(out IEnumerable<string> errors))
            {
                var result = await _repository.Update(item);

                if(result) 
                   
                    return Ok();
               
            }

            return BadRequest(errors);
        }


        /// <summary>
        /// Returns max price for an Item
        /// </summary>
  

         [HttpGet("price/{itemName}/maxPrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MaxPrice(String itemName)
        {

            var result = await _repository.MaxPrice(itemName);

                if (result!= null)

                    return Ok(result.Price);

                return NotFound();
        }


    }
}
