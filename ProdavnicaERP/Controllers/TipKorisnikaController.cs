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
    [Route("api/tipovi")]
    [Produces("application/json", "application/xml")]
    public class TipKorisnikaController : ControllerBase
    {
        private readonly ITipKorisnikaRepository tipKorisnikaRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public TipKorisnikaController(ITipKorisnikaRepository tipKorisnikaRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.tipKorisnikaRepository = tipKorisnikaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<TipKorisnikaDto>> GetTipKorisnika()
        {

            List<TipKorisnika> tipoviKorisnika = tipKorisnikaRepository.GetTipKorisnika();
            if (tipoviKorisnika == null || tipoviKorisnika.Count == 0)
            {
                return NoContent();
            }
            List<TipKorisnikaDto> tipoviKorisnikaDto = new List<TipKorisnikaDto>();
            foreach (TipKorisnika k in tipoviKorisnika)
            {
                TipKorisnikaDto tipKorisnikaDto = mapper.Map<TipKorisnikaDto>(k);

                tipoviKorisnikaDto.Add(tipKorisnikaDto);
            }
            return Ok(tipoviKorisnikaDto);
        }
        [HttpGet("{TipKorisnikaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipKorisnikaDto> GetTipKorisnika(int tipKorisnikaID)
        {

            TipKorisnika tipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnikaID);


            if (tipKorisnika == null)
            {
                return NotFound();
            }
            
            return Ok(mapper.Map<TipKorisnikaDto>(tipKorisnika));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<TipKorisnikaDto> CreateTipKorisnika([FromBody] TipKorisnikaCreationDto tipKorisnika)
        {
            try
            {
                TipKorisnika obj = mapper.Map<TipKorisnika>(tipKorisnika);
                TipKorisnika k = tipKorisnikaRepository.CreateTipKorisnika(obj);
                tipKorisnikaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetTipKorisnika", "TipKorisnika", new { TipKorisnikaID = k.TipKorisnikaId });

               
                return Created(location, mapper.Map<TipKorisnikaDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{TipKorisnikaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteTipKorisnika(int tipKorisnikaID)
        {
            try
            {
                TipKorisnika TipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnikaID);
                if (TipKorisnika == null)
                {
                    return NotFound();
                }
                tipKorisnikaRepository.DeleteTipKorisnika(tipKorisnikaID);
                tipKorisnikaRepository.SaveChanges();
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
        public ActionResult<TipKorisnikaDto> UpdateTipKorisnika(TipKorisnikaUpdateDto tipKorisnika)
        {
            try
            {
                var oldTipKorisnika = tipKorisnikaRepository.GetTipKorisnikaById(tipKorisnika.TipKorisnikaId);
                if (oldTipKorisnika == null)
                {

                    return NotFound();
                }
                TipKorisnika tipKorisnikaEntity = mapper.Map<TipKorisnika>(tipKorisnika);
                mapper.Map(tipKorisnikaEntity, oldTipKorisnika); //Update objekta koji treba da sačuvamo u bazi                


                tipKorisnikaRepository.SaveChanges(); //Perzistiramo promene


                return Ok(mapper.Map<TipKorisnikaDto>(tipKorisnikaEntity));
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
