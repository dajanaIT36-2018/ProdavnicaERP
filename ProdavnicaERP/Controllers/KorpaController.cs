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
    [Route("api/korpe")]
    [Produces("application/json", "application/xml")]
    public class KorpaController : ControllerBase
    {
        private readonly IKorpaRepository korpaRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
       
        public KorpaController(IKorpaRepository korpaRepository, IKorisnikRepository korisnikRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.korpaRepository = korpaRepository;
            this.korisnikRepository = korisnikRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<KorpaDto>> GetKorpa()
        {

            List<Korpa> korpe = korpaRepository.GetKorpa();
            if (korpe == null || korpe.Count == 0)
            {
                return NoContent();
            }
            List<KorpaDto> korpeDto = new List<KorpaDto>();
            foreach (Korpa k in korpe)
            {
                KorpaDto korpaDto = mapper.Map<KorpaDto>(k);

                korpaDto.Korisnik = mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(k.KorisnikId));
               

                korpeDto.Add(korpaDto);
            }
            return Ok(korpeDto);
        }

        [HttpGet("{korpaID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KorpaDto> GetKorpa(int korpaID)
        {

            Korpa korpa = korpaRepository.GetKorpaById(korpaID);


            if (korpa == null)
            {
                return NotFound();
            }
            KorpaDto korpaDto = mapper.Map<KorpaDto>(korpa);
            korpaDto.Korisnik= mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(korpa.KorisnikId));
            return Ok(mapper.Map<KorpaDto>(korpa));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<KorpaDto> CreateKorpa([FromBody] KorpaCreationDto korpa)
        {
            try
            {
                Korpa korpaEntity = mapper.Map<Korpa>(korpa);
                Korpa korpaCreate = korpaRepository.CreateKorpa(korpaEntity);
                korpaRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetKorpa", "Korpa", new { korpaId = korpaCreate.KorpaId });

                KorpaDto korpaDto = mapper.Map<KorpaDto>(korpaCreate);
                korpaDto.Korisnik = mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(korpa.KorisnikId));

                return Created(location, korpaDto);

            }
            catch
            {

                
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{korpaID}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteKorpa(int korpaID)
        {
            try
            {
                Korpa korpa = korpaRepository.GetKorpaById(korpaID);
                if (korpa == null)
                {
                    return NotFound();
                }
                korpaRepository.DeleteKorpa(korpaID);
                korpaRepository.SaveChanges();
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
        public ActionResult<KorpaDto> UpdateKorpa(KorpaUpdateDto korpa)
        {
            try
            {
                var oldKorpa = korpaRepository.GetKorpaById(korpa.KorpaId);
                if (oldKorpa == null)
                {

                    return NotFound();
                }
                Korpa korpaEntity = mapper.Map<Korpa>(korpa);
                mapper.Map(korpaEntity, oldKorpa); //Update objekta koji treba da sačuvamo u bazi                


                korpaRepository.SaveChanges(); //Perzistiramo promene



                KorpaDto korpaDto = mapper.Map<KorpaDto>(korpaEntity);
                korpaDto.Korisnik = mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(korpa.KorisnikId));

                return Ok(korpaDto);
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
