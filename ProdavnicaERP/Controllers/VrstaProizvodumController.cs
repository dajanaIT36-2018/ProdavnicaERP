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
    [Route("api/vrsteProizvoda")]
    [Produces("application/json", "application/xml")]
    public class VrstaProizvodumController : ControllerBase
    {
        private readonly IVrstaProizvodaRepository vrstaProizvodaRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public VrstaProizvodumController(IVrstaProizvodaRepository vrstaProizvodaRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.vrstaProizvodaRepository = vrstaProizvodaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<VrstaProizvodumDto>> GetVrstaProizvoda()
        {

            List<VrstaProizvodum> vrsteProizvoda = vrstaProizvodaRepository.GetVrstaProizvoda();

            if (vrsteProizvoda == null || vrsteProizvoda.Count == 0)
            {
                return NoContent();
            }

            List<VrstaProizvodumDto> vrsteProizvodaDto = new List<VrstaProizvodumDto>();
            foreach (VrstaProizvodum k in vrsteProizvoda)
            {

                VrstaProizvodumDto vrstaProizvodaDto = mapper.Map<VrstaProizvodumDto>(k);
                vrsteProizvodaDto.Add(vrstaProizvodaDto);
            }
            return Ok(vrsteProizvodaDto);

        }

        [HttpGet("{vrstaProizvodaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<VrstaProizvodumDto> GetVrstaProizvoda(int vrstaProizvodaID)
        {

            VrstaProizvodum vrstaProizvoda = vrstaProizvodaRepository.GetVrstaProizvodaById(vrstaProizvodaID);


            if (vrstaProizvoda == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VrstaProizvodumDto>(vrstaProizvoda));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipKorisnikaDto> CreateVrstaProizvoda([FromBody] VrstaProizvodumDto vrstaProizvoda)
        {
            try
            {
                VrstaProizvodum obj = mapper.Map<VrstaProizvodum>(vrstaProizvoda);
                VrstaProizvodum k = vrstaProizvodaRepository.CreateVrstaProizvoda(obj);
                vrstaProizvodaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetVrstaProizvoda", "VrstaProizvodum", new { TipKorisnikaID = k.VrstaProizvodaId });


                return Created(location, mapper.Map<VrstaProizvodumDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{vrstaProizvodaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeletevrstaProizvoda(int vrstaProizvodaID)
        {
            try
            {
                VrstaProizvodum vrstaProizvoda = vrstaProizvodaRepository.GetVrstaProizvodaById(vrstaProizvodaID);
                if (vrstaProizvoda == null)
                {
                    return NotFound();
                }
                vrstaProizvodaRepository.DeleteVrstaProizvoda(vrstaProizvodaID);
                vrstaProizvodaRepository.SaveChanges();
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
        public ActionResult<VrstaProizvodumDto> UpdateVrstaProizvoda(VrstaProizvodumUpdateDto vrstaProizvoda)
        {
            try
            {
                var oldVrstaProizvoda = vrstaProizvodaRepository.GetVrstaProizvodaById(vrstaProizvoda.VrstaProizvodaId);
                if (oldVrstaProizvoda == null)
                {

                    return NotFound();
                }
                VrstaProizvodum vrstaProizvodaEntity = mapper.Map<VrstaProizvodum>(vrstaProizvoda);
                mapper.Map(vrstaProizvodaEntity, oldVrstaProizvoda); //Update objekta koji treba da sačuvamo u bazi                


                vrstaProizvodaRepository.SaveChanges(); //Perzistiramo promene


                return Ok(mapper.Map<VrstaProizvodumDto>(vrstaProizvodaEntity));
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
