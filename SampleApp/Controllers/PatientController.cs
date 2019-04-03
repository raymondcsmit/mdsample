
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Madyan.Repo.Abstract;
using Madyan.Data;
using SampleApp;
using SampleApp.Filters;
using Newtonsoft.Json;
using Madyan.Repo;
using Newtonsoft.Json.Linq;

namespace SampleApp.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class PatientController : Controller
    {
        private IEntityBaseRepository<Patient> PatientRepository;
        int page = 1;
        int pageSize = 10;
        public PatientController(IEntityBaseRepository<Patient> _PatientRepository)
        {
            PatientRepository = _PatientRepository;
        }
        //[HttpPost("{page,pagesize}", Name = "getpatientpaged")]
        
        //public IActionResult GetPatientPaged(int page, int pagesize )
        //{
            
        //    //throw new Exception("Please check username or password");
        //    int currentPage = page;
        //    int currentPageSize = pageSize;
        //    //var a = DateTime.Parse("asdfasd");
           
        //    IEnumerable <Patient> objPatient = PatientRepository
        //        .AllIncluding(c=>c.FkBasic,c=>c.FkNextOfKin, c=>c.GpDetailList).OrderBy(u => u.PatientID)
        //        .Skip((currentPage - 1) * currentPageSize)
        //        .Take(currentPageSize)
        //        .ToList();

        //    return new OkObjectResult(objPatient);
        //}
        [HttpPost("{prop}",Name = "getsearch")]

        public IActionResult GetSearch([FromBody] List<Property> prop)
        {
            IQueryable<Patient> objPatient=   PatientRepository
               .BuildDynamicExpression(prop, c => c.FkBasic, c => c.FkNextOfKin, c => c.GpDetailList);

            return new OkObjectResult(objPatient);
        }
        [HttpGet]
        //[Authorize(Policy = "Patient.view")]
        public IActionResult Get()
        {
            IEnumerable<Patient> objPatient = PatientRepository.AllIncluding(c => c.FkBasic, c => c.FkNextOfKin, c => c.GpDetailList).OrderBy(u => u.PatientID).ToList();
            return new OkObjectResult(objPatient);
        }

        [HttpGet("{id}", Name = "GetPatient")]
        //[Authorize(Policy = "Patient.view")]
        ////[Authorize("View Patient ")]
        public IActionResult Get(Int64 id)

        {
            //Patient  objPatient  = PatientRepository.GetSingle(u => u.PKColumn == id);

            Patient objPatient = PatientRepository.GetSingle(u => u.PatientID == id);
            if (objPatient != null)
            {
                return new OkObjectResult(objPatient);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        //[Authorize(Policy = "Patient.create")]
        public IActionResult Create([FromBody]Patient objPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            PatientRepository.Add(objPatient);
            PatientRepository.Commit();

            return Ok(Json(objPatient));
            //CreatedAtRouteResult result = CreatedAtRoute("Get Patient", new { controller = " Patient", id =  objPatient.Id }, objPatient);
            //return result;
        }
      
        [HttpPut("{id}")]
        //[Authorize(Policy = "Patient.update")]
        public IActionResult Put(int id, [FromBody]Patient objPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Patient PatientDb;
            PatientDb = PatientRepository.GetSingle(c => c.PatientID == id,t=>t.FkBasic,t=>t.FkNextOfKin, t => t.GpDetailList);//.GetSingle(id);		


            //Patient PatientDb = PatientRepository.GetSingle(c => c.KeyBoardID == id);//.GetSingle(id);
            if (PatientDb == null)
            {
                return NotFound();
            }
            else
            {

                #region Basic
                PatientDb.FkBasic.PasNumber = objPatient.FkBasic.PasNumber;
                PatientDb.FkBasic.Forenames = objPatient.FkBasic.Forenames;
                PatientDb.FkBasic.Surname = objPatient.FkBasic.Surname;
                PatientDb.FkBasic.SexCode = objPatient.FkBasic.SexCode;
                PatientDb.FkBasic.HomeTelephoneNumber = objPatient.FkBasic.HomeTelephoneNumber;
                PatientDb.FkBasic.FkPatientID = objPatient.FkBasic.FkPatientID;
                #endregion Basic
                #region Patient
                PatientDb.FkNextOfKin.NokName = objPatient.FkNextOfKin.NokName;
                PatientDb.FkNextOfKin.NokRelationshipCode = objPatient.FkNextOfKin.NokRelationshipCode;
                PatientDb.FkNextOfKin.NokAddressLine1 = objPatient.FkNextOfKin.NokAddressLine1;
                PatientDb.FkNextOfKin.NokAddressLine2 = objPatient.FkNextOfKin.NokAddressLine2;
                PatientDb.FkNextOfKin.NokAddressLine3 = objPatient.FkNextOfKin.NokAddressLine3;
                PatientDb.FkNextOfKin.NokAddressLine4 = objPatient.FkNextOfKin.NokAddressLine4;
                PatientDb.FkNextOfKin.NokPostcode = objPatient.FkNextOfKin.NokPostcode;
                PatientDb.FkNextOfKin.FkPatientID = objPatient.FkNextOfKin.FkPatientID;
                #endregion Patient
                foreach(GpDetail objGp in objPatient.GpDetailList)
                {
                    if (objGp.GpDetailID == 0)
                    {
                        PatientDb.GpDetailList.Add(objGp);
                    }
                    else
                    {
                        GpDetail objGpDetail = objPatient.GpDetailList.Where(c => c.GpDetailID == objGp.GpDetailID).FirstOrDefault();
                        if (objGpDetail != null)
                        {
                            objGp.GpDetailID = objGpDetail.GpDetailID;
                            objGp.GpCode = objGpDetail.GpCode;
                            objGp.GpSurname = objGpDetail.GpSurname;
                            objGp.GpInitials = objGpDetail.GpInitials;
                            objGp.GpPhone = objGpDetail.GpPhone;
                            objGp.FkPatientID = objGpDetail.FkPatientID;
                        }
                    }
                }

                PatientRepository.Update(objPatient);
                PatientRepository.Commit();
            }
            return Ok(Json(PatientDb));
            //return new NoContentResult();
        }
        //[HttpPatch]
        //public IActionResult Patch(int id, [FromBody]Patient objPatient)
        //{
        //    Patient PatientDb  = menuRepository.GetSingle(id);
        //    var patched = PatientDb.Copy();
        //    patch.ApplyTo(patched, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return new BadRequestObjectResult(ModelState);
        //    }

        //    var model = new
        //    {
        //        original = PatientDb,
        //        patched = patched
        //    };

        //    return Ok(model);
        //}
        [HttpDelete("{id}")]
        //[Authorize(Policy = "Patient.delete")]
        public IActionResult Delete(int id)
        {
            Patient PatientDb;
            PatientDb = PatientRepository.GetSingle(c => c.PatientID == id,t=>t.FkBasic,t=>t.FkNextOfKin, t=>t.GpDetailList);//.GetSingle(id);		

            //Patient PatientDb = PatientRepository.GetSingle(id);
            if (PatientDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                PatientRepository.Delete(PatientDb);
                PatientRepository.Commit();
                return new NoContentResult();
            }
        }

    }
    
}
