
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Madyan.Repo.Abstract;
using Madyan.Data;

namespace SampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    ////[Authorize]
    public class BasicController : Controller
    {

        private IEntityBaseRepository<Basic> BasicRepository;
        public BasicController(IEntityBaseRepository<Basic> _BasicRepository)
        {
            BasicRepository = _BasicRepository;
        }

        [HttpGet]
        ////[Authorize(Policy = "Basic.view")]
        public IActionResult Get()
        {

            IEnumerable<Basic> objBasic = BasicRepository.GetAll().ToList();
            return new OkObjectResult(objBasic);
        }

        [HttpGet("{id}", Name = "GetBasic")]
        ////[Authorize(Policy = "Basic.view")]

        public IActionResult Get(Int64 id)

        {
            //Basic  objBasic  = BasicRepository.GetSingle(u => u.PKColumn == id);

            Basic objBasic = BasicRepository.GetSingle(u => u.BasicID == id);
            if (objBasic != null)
            {
                return new OkObjectResult(objBasic);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        ////[Authorize(Policy = "Basic.create")]
        public IActionResult Create([FromBody]Basic objBasic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            BasicRepository.Add(objBasic);
            BasicRepository.Commit();

            return Ok(Json(objBasic));
            //CreatedAtRouteResult result = CreatedAtRoute("Get Basic", new { controller = " Basic", id =  objBasic.Id }, objBasic);
            //return result;
        }

        [HttpPut("{id}")]
        ////[Authorize(Policy = "Basic.update")]
        public IActionResult Put(int id, [FromBody]Basic objBasic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Basic BasicDb;
            BasicDb = BasicRepository.GetSingle(c => c.BasicID == id);//.GetSingle(id);		


            //Basic BasicDb = BasicRepository.GetSingle(c => c.KeyBoardID == id);//.GetSingle(id);
            if (BasicDb == null)
            {
                return NotFound();
            }
            else
            {

                BasicDb.BasicID = objBasic.BasicID;

                BasicDb.PasNumber = objBasic.PasNumber;

                BasicDb.Forenames = objBasic.Forenames;

                BasicDb.Surname = objBasic.Surname;

                BasicDb.SexCode = objBasic.SexCode;

                BasicDb.HomeTelephoneNumber = objBasic.HomeTelephoneNumber;

                BasicDb.FkPatientID = objBasic.FkPatientID;

                BasicRepository.Update(BasicDb);
                BasicRepository.Commit();
            }
            return Ok(Json(BasicDb));
            //return new NoContentResult();
        }
        //[HttpPatch]
        //public IActionResult Patch(int id, [FromBody]Basic objBasic)
        //{
        //    Basic BasicDb  = menuRepository.GetSingle(id);
        //    var patched = BasicDb.Copy();
        //    patch.ApplyTo(patched, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return new BadRequestObjectResult(ModelState);
        //    }

        //    var model = new
        //    {
        //        original = BasicDb,
        //        patched = patched
        //    };

        //    return Ok(model);
        //}
        [HttpDelete("{id}")]
        ////[Authorize(Policy = "Basic.delete")]
        public IActionResult Delete(int id)
        {
            Basic BasicDb;
            BasicDb = BasicRepository.GetSingle(c => c.BasicID == id);//.GetSingle(id);		

            //Basic BasicDb = BasicRepository.GetSingle(id);
            if (BasicDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                BasicRepository.Delete(BasicDb);
                BasicRepository.Commit();
                return new NoContentResult();
            }
        }

    }
}
