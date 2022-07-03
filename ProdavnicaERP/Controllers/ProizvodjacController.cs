using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/proizvodjaci")]
    [Produces("application/json", "application/xml")]
    public class ProizvodjacController : ControllerBase
    {
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public ProizvodjacController(IProizvodjacRepository proizvodjacRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.proizvodjacRepository = proizvodjacRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<ProizvodjacDto>> GetProizvodjacs()
        {

            List<Proizvodjac> proizvodjaci = proizvodjacRepository.GetProizvodjac();
            if (proizvodjaci == null || proizvodjaci.Count == 0)
            {
                return NoContent();
            }
            List<ProizvodjacDto> proizvodjaciDto = new List<ProizvodjacDto>();
            foreach (Proizvodjac k in proizvodjaci)
            {
                ProizvodjacDto ProizvodjacDto = mapper.Map<ProizvodjacDto>(k);

                proizvodjaciDto.Add(ProizvodjacDto);
            }
            return Ok(proizvodjaciDto);
        }

        [HttpGet("{ProizvodjacID}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ProizvodjacDto> GetProizvodjac(int proizvodjacID)
        {

            Proizvodjac proizvodjac = proizvodjacRepository.GetProizvodjacById(proizvodjacID);


            if (proizvodjac == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProizvodjacDto>(proizvodjac));
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ProizvodjacDto> CreateProizvodjac([FromBody] ProizvodjacCreationDto proizvodjac)
        {
            try
            {
                Proizvodjac obj = mapper.Map<Proizvodjac>(proizvodjac);
                Proizvodjac k = proizvodjacRepository.CreateProizvodjac(obj);
                proizvodjacRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetProizvodjac", "Proizvodjac", new { ProizvodjacID = k.ProizvodjacId });


                return Created(location, mapper.Map<ProizvodjacDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{ProizvodjacID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteProizvodjac(int proizvodjacID)
        {
            try
            {
                Proizvodjac Proizvodjac = proizvodjacRepository.GetProizvodjacById(proizvodjacID);
                if (Proizvodjac == null)
                {
                    return NotFound();
                }
                proizvodjacRepository.DeleteProizvodjac(proizvodjacID);
                proizvodjacRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ProizvodjacDto> UpdateProizvodjac(ProizvodjacUpdateDto proizvodjac)
        {
            try
            {
                var oldProizvodjac = proizvodjacRepository.GetProizvodjacById(proizvodjac.ProizvodjacId);
                if (oldProizvodjac == null)
                {

                    return NotFound();
                }
                Proizvodjac proizvodjacEntity = mapper.Map<Proizvodjac>(proizvodjac);
                mapper.Map(proizvodjacEntity, oldProizvodjac); //Update objekta koji treba da sačuvamo u bazi                


                proizvodjacRepository.SaveChanges(); //Perzistiramo promene


                return Ok(mapper.Map<ProizvodjacDto>(proizvodjacEntity));
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
