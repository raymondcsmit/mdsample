
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
    public class GpDetailController : Controller
    {
        private IEntityBaseRepository<GpDetail> GpDetailRepository;
       
        public GpDetailController(IEntityBaseRepository<GpDetail> _GpDetailRepository)
        {
            GpDetailRepository = _GpDetailRepository;
        }

		[HttpGet]
        //[Authorize(Policy = "GpDetail.view")]
        public IActionResult Get()
        {
            IEnumerable<GpDetail> objGpDetail = GpDetailRepository.GetAll().ToList();
            return new OkObjectResult(objGpDetail);
        }

		[HttpGet("{id}", Name = "GetGpDetail")]
        //[Authorize(Policy = "GpDetail.view")]
		public IActionResult Get(Int64 id)    
			 
        {           
			GpDetail  objGpDetail  = GpDetailRepository.GetSingle(u => u.GpDetailID == id); 	
            if (objGpDetail  != null)
            {
                return new OkObjectResult(objGpDetail );
            }
            else
            {
                return NotFound();
            }
        }


		[HttpPost]
		//[Authorize(Policy = "GpDetail.create")]
        public IActionResult Create([FromBody]GpDetail objGpDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            GpDetailRepository.Add(objGpDetail);
            GpDetailRepository.Commit();

			return Ok(Json(objGpDetail));
            //CreatedAtRouteResult result = CreatedAtRoute("Get GpDetail", new { controller = " GpDetail", id =  objGpDetail.Id }, objGpDetail);
            //return result;
        }

		[HttpPut("{id}")]
		//[Authorize(Policy = "GpDetail.update")]
        public IActionResult Put(int id, [FromBody]GpDetail objGpDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			GpDetail GpDetailDb;
						 GpDetailDb = GpDetailRepository.GetSingle(c => c.GpDetailID == id);//.GetSingle(id);		
			

            //GpDetail GpDetailDb = GpDetailRepository.GetSingle(c => c.KeyBoardID == id);//.GetSingle(id);
            if (GpDetailDb == null)
            {
                return NotFound();
            }
            else
            {
			
			GpDetailDb.GpDetailID = objGpDetail.GpDetailID;			
			
			GpDetailDb.GpCode = objGpDetail.GpCode;			
			
			GpDetailDb.GpSurname = objGpDetail.GpSurname;			
			
			GpDetailDb.GpInitials = objGpDetail.GpInitials;			
			
			GpDetailDb.GpPhone = objGpDetail.GpPhone;			
			
			GpDetailDb.FkPatientID = objGpDetail.FkPatientID;			
			
			GpDetailRepository.Update(GpDetailDb);
                GpDetailRepository.Commit();
            }
			return Ok(Json(GpDetailDb));
            //return new NoContentResult();
        }
		//[HttpPatch]
        //public IActionResult Patch(int id, [FromBody]GpDetail objGpDetail)
        //{
        //    GpDetail GpDetailDb  = menuRepository.GetSingle(id);
        //    var patched = GpDetailDb.Copy();
        //    patch.ApplyTo(patched, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return new BadRequestObjectResult(ModelState);
        //    }

        //    var model = new
        //    {
        //        original = GpDetailDb,
        //        patched = patched
        //    };

        //    return Ok(model);
        //}
		[HttpDelete("{id}")]
		//[Authorize(Policy = "GpDetail.delete")]
        public IActionResult Delete(int id)
        {
			GpDetail GpDetailDb;
						 GpDetailDb = GpDetailRepository.GetSingle(c => c.GpDetailID == id);//.GetSingle(id);		
			
            //GpDetail GpDetailDb = GpDetailRepository.GetSingle(id);
            if (GpDetailDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                GpDetailRepository.Delete(GpDetailDb);
                GpDetailRepository.Commit();
                return new NoContentResult();
            }
        }

	}
}
