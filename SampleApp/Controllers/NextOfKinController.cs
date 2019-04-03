
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
    //[Authorize]
    public class NextOfKinController : Controller
    {
        private IEntityBaseRepository<NextOfKin> NextOfKinRepository;
       
        public NextOfKinController(IEntityBaseRepository<NextOfKin> _NextOfKinRepository)
        {
            NextOfKinRepository = _NextOfKinRepository;
        }

		[HttpGet]
        //[Authorize(Policy = "NextOfKin.view")]
        public IActionResult Get()
        {
            IEnumerable<NextOfKin> objNextOfKin = NextOfKinRepository.GetAll().ToList();

            return new OkObjectResult(objNextOfKin);
        }

		[HttpGet("{id}", Name = "GetNextOfKin")]
        //[Authorize(Policy = "NextOfKin.view")]
        ////[Authorize("View NextOfKin ")]
		public IActionResult Get(Int64 id)    
			 
        {
            //NextOfKin  objNextOfKin  = NextOfKinRepository.GetSingle(u => u.PKColumn == id);

			NextOfKin  objNextOfKin  = NextOfKinRepository.GetSingle(u => u.NextOfKinID == id); 	
            if (objNextOfKin  != null)
            {
                return new OkObjectResult(objNextOfKin );
            }
            else
            {
                return NotFound();
            }
        }


		[HttpPost]
		//[Authorize(Policy = "NextOfKin.create")]
        public IActionResult Create([FromBody]NextOfKin objNextOfKin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            NextOfKinRepository.Add(objNextOfKin);
            NextOfKinRepository.Commit();

			return Ok(Json(objNextOfKin));
            //CreatedAtRouteResult result = CreatedAtRoute("Get NextOfKin", new { controller = " NextOfKin", id =  objNextOfKin.Id }, objNextOfKin);
            //return result;
        }

		[HttpPut("{id}")]
		//[Authorize(Policy = "NextOfKin.update")]
        public IActionResult Put(int id, [FromBody]NextOfKin objNextOfKin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			NextOfKin NextOfKinDb;
						 NextOfKinDb = NextOfKinRepository.GetSingle(c => c.NextOfKinID == id);//.GetSingle(id);		
			

            //NextOfKin NextOfKinDb = NextOfKinRepository.GetSingle(c => c.KeyBoardID == id);//.GetSingle(id);
            if (NextOfKinDb == null)
            {
                return NotFound();
            }
            else
            {
			
			NextOfKinDb.NextOfKinID = objNextOfKin.NextOfKinID;			
			
			NextOfKinDb.NokName = objNextOfKin.NokName;			
			
			NextOfKinDb.NokRelationshipCode = objNextOfKin.NokRelationshipCode;			
			
			NextOfKinDb.NokAddressLine1 = objNextOfKin.NokAddressLine1;			
			
			NextOfKinDb.NokAddressLine2 = objNextOfKin.NokAddressLine2;			
			
			NextOfKinDb.NokAddressLine3 = objNextOfKin.NokAddressLine3;			
			
			NextOfKinDb.NokAddressLine4 = objNextOfKin.NokAddressLine4;			
			
			NextOfKinDb.NokPostcode = objNextOfKin.NokPostcode;			
			
			NextOfKinDb.FkPatientID = objNextOfKin.FkPatientID;			
			
			NextOfKinRepository.Update(NextOfKinDb);
                NextOfKinRepository.Commit();
            }
			return Ok(Json(NextOfKinDb));
            //return new NoContentResult();
        }
		//[HttpPatch]
        //public IActionResult Patch(int id, [FromBody]NextOfKin objNextOfKin)
        //{
        //    NextOfKin NextOfKinDb  = menuRepository.GetSingle(id);
        //    var patched = NextOfKinDb.Copy();
        //    patch.ApplyTo(patched, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return new BadRequestObjectResult(ModelState);
        //    }

        //    var model = new
        //    {
        //        original = NextOfKinDb,
        //        patched = patched
        //    };

        //    return Ok(model);
        //}
		[HttpDelete("{id}")]
		//[Authorize(Policy = "NextOfKin.delete")]
        public IActionResult Delete(int id)
        {
			NextOfKin NextOfKinDb;
						 NextOfKinDb = NextOfKinRepository.GetSingle(c => c.NextOfKinID == id);//.GetSingle(id);		
			
            //NextOfKin NextOfKinDb = NextOfKinRepository.GetSingle(id);
            if (NextOfKinDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                NextOfKinRepository.Delete(NextOfKinDb);
                NextOfKinRepository.Commit();
                return new NoContentResult();
            }
        }

	}
}
