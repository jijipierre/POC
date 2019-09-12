using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.WebApi.Models;
using POC.WebApi.Services;

namespace POC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesService _service;

        public ValuesController(IValuesService service)
        {
            _service = service;
        } 

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ValuesItem>> Get()
        {
            var items = _service.GetAllItems();
            return Ok(items);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ValuesItem> Get(int id)
        {
            var item = _service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ValuesItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(value);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ValuesItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.GetById(id);
            //_service.Update(value);

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingItem = _service.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }

    }
}
