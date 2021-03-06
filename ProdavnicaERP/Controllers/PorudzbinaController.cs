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
    [Route("api/porudzbine")]
    [Route("api/porudzbine/[action]")]
    [Produces("application/json", "application/xml")]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IStatusPorudzbineRepository statusPorudzbineRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public PorudzbinaController(IPorudzbinaRepository porudzbinaRepository, IKorisnikRepository korisnikRepository,
            IStatusPorudzbineRepository statusPorudzbineRepository ,IMapper mapper, LinkGenerator linkGenerator)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.korisnikRepository = korisnikRepository;
            this.statusPorudzbineRepository = statusPorudzbineRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<PorudzbinaDto>> GetPorudzbina()
        {

            List<Porudzbina> porudzbine = porudzbinaRepository.GetPorudzbina();
            if (porudzbine == null || porudzbine.Count == 0)
            {
                return NoContent();
            }
            List<PorudzbinaDto> porudzbineDto = new List<PorudzbinaDto>();
            foreach (Porudzbina k in porudzbine)
            {
                PorudzbinaDto porudzbinaDto = mapper.Map<PorudzbinaDto>(k);

                porudzbinaDto.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(k.KorisnikId));
                porudzbinaDto.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(k.StatusPorudzbineId));

                porudzbineDto.Add(porudzbinaDto);
            }
            return Ok(porudzbineDto);
        }

        [HttpGet("{porudzbinaID}")]
        [ActionName("porudzbinaById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PorudzbinaDto> GetPorudzbina(int porudzbinaID)
        {

            Porudzbina porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaID);


            if (porudzbina == null)
            {
                return NotFound();
            }
            PorudzbinaDto porudzbinaDto = mapper.Map<PorudzbinaDto>(porudzbina);
            porudzbinaDto.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(porudzbina.KorisnikId));
            porudzbinaDto.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(porudzbina.StatusPorudzbineId));
            return Ok(mapper.Map<PorudzbinaDto>(porudzbina));
        }

        /// <summary>
        /// Vraća jednu korpu na osnovu ID-ja korpe.
        /// </summary>
        /// <param name="korpaId">ID korpe</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženy Korpu</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ActionName("porudzbineKorisnika")]
        [HttpGet("{korisnikId}")]
        public ActionResult<PorudzbinaDto> GetPorudzbinaByKorisnik(int korisnikId)
        {


            Porudzbina porudzbina = porudzbinaRepository.GetPorudzbineByKorisnik(korisnikId);
            if (porudzbina == null)
            {

                return NotFound();
            }

            PorudzbinaDto porudzbinaDto = mapper.Map<PorudzbinaDto>(porudzbina);
            porudzbinaDto.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(porudzbina.KorisnikId));
            porudzbinaDto.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(porudzbina.StatusPorudzbineId));

            return Ok(porudzbinaDto);
        }

        /// <summary>
        /// Vraća jednu korpu na osnovu ID-ja korpe.
        /// </summary>
        /// <param name="korpaId">ID korpe</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženy Korpu</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ActionName("porudzbineByStatus")]
        [HttpGet("{statusPorudzbineId}")]
        public ActionResult<PorudzbinaDto> GetPorudzbinaByStatus(int statusPorudzbineId)
        {


            Porudzbina porudzbina = porudzbinaRepository.GetPorudzbineByStatus(statusPorudzbineId);
            if (porudzbina == null)
            {

                return NotFound();
            }

            PorudzbinaDto porudzbinaDto = mapper.Map<PorudzbinaDto>(porudzbina);
            porudzbinaDto.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(porudzbina.KorisnikId));
            porudzbinaDto.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(porudzbina.StatusPorudzbineId));

            return Ok(porudzbinaDto);
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<PorudzbinaDto> CreatePorudzbina([FromBody] PorudzbinaCreationDto porudzbina)
        {
            try
            {
                Porudzbina obj = mapper.Map<Porudzbina>(porudzbina);
                Porudzbina k = porudzbinaRepository.CreatePorudzbina(obj);
                porudzbinaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetPorudzbina", "Porudzbina", new { porudzbinaID = k.PorudzbinaId });

                PorudzbinaDto porudzbinaDto = mapper.Map<PorudzbinaDto>(k);
                porudzbinaDto.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(k.KorisnikId));
                porudzbinaDto.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(k.StatusPorudzbineId));
                return Created(location, mapper.Map<PorudzbinaDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{porudzbinaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeletePorudzbina(int porudzbinaID)
        {
            try
            {
                Porudzbina porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaID);
                if (porudzbina == null)
                {
                    return NotFound();
                }
                porudzbinaRepository.DeletePorudzbina(porudzbinaID);
                porudzbinaRepository.SaveChanges();
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
        public ActionResult<PorudzbinaDto> UpdatePorudzbina(PorudzbinaUpdateDto porudzbina)
        {

            try
            {
                var stariPorudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbina.PorudzbinaId);
                if (stariPorudzbina == null)
                {

                    return NotFound();
                }
                Porudzbina porudzbinaEntity = mapper.Map<Porudzbina>(porudzbina);
                mapper.Map(porudzbinaEntity, stariPorudzbina);
                porudzbinaRepository.SaveChanges();
                PorudzbinaDto porudzbinaDto = mapper.Map<PorudzbinaDto>(porudzbinaEntity);

                porudzbinaDto.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(porudzbina.KorisnikId));
                porudzbinaDto.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(porudzbina.StatusPorudzbineId));


                return Ok(porudzbinaDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }


        }
        [HttpOptions]
        public IActionResult GetSluzbeniListOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
