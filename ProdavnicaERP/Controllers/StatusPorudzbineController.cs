using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ProdavnicaERP.Data;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdavnicaERP.Controllers
{
    [ApiController]
    [Route("api/statusi")]
    [Produces("application/json", "application/xml")]
    public class StatusPorudzbineController : ControllerBase
    {
        private readonly IStatusPorudzbineRepository statusPorudzbineRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public StatusPorudzbineController(IStatusPorudzbineRepository statusPorudzbineRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.statusPorudzbineRepository = statusPorudzbineRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<StatusPorudzbineDto>> GetStatusPorudzbine()
        {

            List<StatusPorudzbine> statusPorudzbini = statusPorudzbineRepository.GetStatusPorudzbine();
            if (statusPorudzbini == null || statusPorudzbini.Count == 0)
            {
                return NoContent();
            }
            List<StatusPorudzbineDto> statusPorudzbiniDto = new List<StatusPorudzbineDto>();
            foreach (StatusPorudzbine k in statusPorudzbini)
            {
                StatusPorudzbineDto statusPorudzbineDto = mapper.Map<StatusPorudzbineDto>(k);

                statusPorudzbiniDto.Add(statusPorudzbineDto);
            }
            return Ok(statusPorudzbiniDto);
        }

        [HttpGet("{StatusPorudzbineID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<StatusPorudzbineDto> GetStatusPorudzbine(int statusPorudzbineID)
        {

            StatusPorudzbine statusPorudzbine = statusPorudzbineRepository.GetStatusPorudzbineById(statusPorudzbineID);


            if (statusPorudzbine == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StatusPorudzbineDto>(statusPorudzbine));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<StatusPorudzbineDto> CreateStatusPorudzbine([FromBody] StatusPorudzbineCreationDto statusPorudzbine)
        {
            try
            {
                StatusPorudzbine obj = mapper.Map<StatusPorudzbine>(statusPorudzbine);
                StatusPorudzbine k = statusPorudzbineRepository.CreateStatusPorudzbine(obj);
                statusPorudzbineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStatusPorudzbine", "StatusPorudzbine", new { StatusPorudzbineID = k.StatusPorudzbineId });


                return Created(location, mapper.Map<StatusPorudzbineDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{StatusPorudzbineID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteStatusPorudzbine(int statusPorudzbineID)
        {
            try
            {
                StatusPorudzbine StatusPorudzbine = statusPorudzbineRepository.GetStatusPorudzbineById(statusPorudzbineID);
                if (StatusPorudzbine == null)
                {
                    return NotFound();
                }
                statusPorudzbineRepository.DeleteStatusPorudzbine(statusPorudzbineID);
                statusPorudzbineRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<StatusPorudzbineDto> UpdateStatusPorudzbine(StatusPorudzbineUpdateDto statusPorudzbine)
        {
            try
            {
                var oldStatusPorudzbine = statusPorudzbineRepository.GetStatusPorudzbineById(statusPorudzbine.StatusPorudzbineId);
                if (oldStatusPorudzbine == null)
                {

                    return NotFound();
                }
                StatusPorudzbine statusPorudzbineEntity = mapper.Map<StatusPorudzbine>(statusPorudzbine);
                mapper.Map(statusPorudzbineEntity, oldStatusPorudzbine); //Update objekta koji treba da sačuvamo u bazi                


                statusPorudzbineRepository.SaveChanges(); //Perzistiramo promene


                return Ok(mapper.Map<StatusPorudzbineDto>(statusPorudzbineEntity));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }


        }
        /// <summary>
        /// Vraca opcije za rad sa javnim nadmetanjima
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
