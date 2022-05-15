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
    [Route("api/tipoviProizvoda")]
    [Produces("application/json", "application/xml")]
    public class TipProizvodumController : ControllerBase
    {
        private readonly ITipProizvodaRepository tipProizvodaRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public TipProizvodumController(ITipProizvodaRepository tipProizvodaRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.tipProizvodaRepository = tipProizvodaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<TipProizvodumDto>> GetTipProizvoda()
        {

            List<TipProizvodum> tipoviProizvoda = tipProizvodaRepository.GetTipProizvoda();

            if (tipoviProizvoda == null || tipoviProizvoda.Count == 0)
            {
                return NoContent();
            }

            List<TipProizvodumDto> tipoviProizvodaDto = new List<TipProizvodumDto>();
            foreach (TipProizvodum k in tipoviProizvoda)
            {

                TipProizvodumDto tipProizvodaDto = mapper.Map<TipProizvodumDto>(k);
                tipoviProizvodaDto.Add(tipProizvodaDto);
            }
            return Ok(tipoviProizvodaDto);

        }
        [HttpGet("{tipProizvodaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipProizvodumDto> GetTipProizvoda(int tipProizvodaID)
        {

            TipProizvodum tipProizvoda = tipProizvodaRepository.GetTipProizvodaById(tipProizvodaID);


            if (tipProizvoda == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TipProizvodumDto>(tipProizvoda));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipKorisnikaDto> CreateTipProizvoda([FromBody] TipProizvodumDto tipProizvoda)
        {
            try
            {
                TipProizvodum obj = mapper.Map<TipProizvodum>(tipProizvoda);
                TipProizvodum k = tipProizvodaRepository.CreateTipProizvoda(obj);
                tipProizvodaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetTipProizvoda", "TipProizvodum", new { TipKorisnikaID = k.TipProizvodaId});


                return Created(location, mapper.Map<TipProizvodumDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{tipProizvodaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeletetipProizvoda(int tipProizvodaID)
        {
            try
            {
                TipProizvodum tipProizvoda = tipProizvodaRepository.GetTipProizvodaById(tipProizvodaID);
                if (tipProizvoda == null)
                {
                    return NotFound();
                }
                tipProizvodaRepository.DeleteTipProizvoda(tipProizvodaID);
                tipProizvodaRepository.SaveChanges();
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
        public ActionResult<TipProizvodumDto> UpdatetipProizvoda(TipProizvodumUpdateDto tipProizvoda)
        {
            try
            {
                var oldtipProizvoda = tipProizvodaRepository.GetTipProizvodaById(tipProizvoda.TipProizvodaId);
                if (oldtipProizvoda == null)
                {

                    return NotFound();
                }
                TipProizvodum tipProizvodaEntity = mapper.Map<TipProizvodum>(tipProizvoda);
                mapper.Map(tipProizvodaEntity, oldtipProizvoda); //Update objekta koji treba da sačuvamo u bazi                


                tipProizvodaRepository.SaveChanges(); //Perzistiramo promene


                return Ok(mapper.Map<TipProizvodumDto>(tipProizvodaEntity));
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
