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
        private readonly IVrstaProizvodaRepository VrstaProizvodaRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public VrstaProizvodumController(IVrstaProizvodaRepository VrstaProizvodaRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.VrstaProizvodaRepository = VrstaProizvodaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<VrstaProizvodumDto>> GetVrstaProizvodas()
        {

            List<VrstaProizvodum> vrsteProizvoda = VrstaProizvodaRepository.GetVrstaProizvoda();

            if (vrsteProizvoda == null || vrsteProizvoda.Count == 0)
            {
                return NoContent();
            }

            List<VrstaProizvodumDto> vrsteProizvodaDto = new List<VrstaProizvodumDto>();
            foreach (VrstaProizvodum k in vrsteProizvoda)
            {

                VrstaProizvodumDto VrstaProizvodaDto = mapper.Map<VrstaProizvodumDto>(k);
                vrsteProizvodaDto.Add(VrstaProizvodaDto);
            }
            return Ok(vrsteProizvodaDto);

        }
        [HttpGet("{VrstaProizvodaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<VrstaProizvodumDto> GetVrstaProizvoda(int VrstaProizvodaID)
        {

            VrstaProizvodum VrstaProizvoda = VrstaProizvodaRepository.GetVrstaProizvodaById(VrstaProizvodaID);


            if (VrstaProizvoda == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<VrstaProizvodumDto>(VrstaProizvoda));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipKorisnikaDto> CreateVrstaProizvoda([FromBody] VrstaProizvodumDto VrstaProizvoda)
        {
            try
            {
                VrstaProizvodum obj = mapper.Map<VrstaProizvodum>(VrstaProizvoda);
                VrstaProizvodum k = VrstaProizvodaRepository.CreateVrstaProizvoda(obj);
                VrstaProizvodaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetVrstaProizvoda", "VrstaProizvodum", new { TipKorisnikaID = k.VrstaProizvodaId });


                return Created(location, mapper.Map<VrstaProizvodumDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Uneli ste nepostojecu vrstu proizvoda");
            }
        }

        [HttpDelete("{VrstaProizvodaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteVrstaProizvoda(int VrstaProizvodaID)
        {
            try
            {
                VrstaProizvodum VrstaProizvoda = VrstaProizvodaRepository.GetVrstaProizvodaById(VrstaProizvodaID);
                if (VrstaProizvoda == null)
                {
                    return NotFound();
                }
                VrstaProizvodaRepository.DeleteVrstaProizvoda(VrstaProizvodaID);
                VrstaProizvodaRepository.SaveChanges();
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
        public ActionResult<VrstaProizvodumDto> UpdateVrstaProizvoda(VrstaProizvodumUpdateDto VrstaProizvoda)
        {
            try
            {
                var oldVrstaProizvoda = VrstaProizvodaRepository.GetVrstaProizvodaById(VrstaProizvoda.VrstaProizvodaId);
                if (oldVrstaProizvoda == null)
                {

                    return NotFound();
                }
                VrstaProizvodum VrstaProizvodaEntity = mapper.Map<VrstaProizvodum>(VrstaProizvoda);
                mapper.Map(VrstaProizvodaEntity, oldVrstaProizvoda); //Update objekta koji treba da sačuvamo u bazi                


                VrstaProizvodaRepository.SaveChanges(); //Perzistiramo promene


                return Ok(mapper.Map<VrstaProizvodumDto>(VrstaProizvodaEntity));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Uneli ste nepostojecu vrstu proizvoda");
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
