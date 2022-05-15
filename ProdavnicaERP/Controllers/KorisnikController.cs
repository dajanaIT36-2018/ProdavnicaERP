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
    [Route("api/korisnici")]
    [Produces("application/json", "application/xml")]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikRepository korisnikRepository;
        private readonly ITipKorisnikaRepository tipKorisnikaRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public KorisnikController(IKorisnikRepository korisnikRepository, ITipKorisnikaRepository tipKorisnikaRepository,
            IMapper mapper, LinkGenerator linkGenerator) 
        {
            this.korisnikRepository = korisnikRepository;
            this.tipKorisnikaRepository = tipKorisnikaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<KorisnikDto>> GetKorisnik()
        {

            List<Korisnik> korisnici = korisnikRepository.GetKorisnik();
            if (korisnici == null || korisnici.Count == 0)
            {
                return NoContent();
            }
            List<KorisnikDto> korisniciDto = new List<KorisnikDto>();
            foreach (Korisnik k in korisnici)
            {
                KorisnikDto korisnikDto = mapper.Map<KorisnikDto>(k);

                korisnikDto.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(k.TipKorisnikaId)); 

                korisniciDto.Add(korisnikDto);
            }
            return Ok(korisniciDto);
        }

            [HttpGet("{korisnikID}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public ActionResult<KorisnikDto> GetKorisnik(int korisnikID)
            {

            Korisnik korisnik = korisnikRepository.GetKorisnikById(korisnikID);
            

            if (korisnik == null)
                {
                    return NotFound();
                }
            KorisnikDto korisnikDto = mapper.Map<KorisnikDto>(korisnik);
            korisnikDto.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(korisnik.TipKorisnikaId));
            return Ok(mapper.Map<KorisnikDto>(korisnik));
            }
            [HttpPost]
            [Consumes("application/json")]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public ActionResult<KorisnikDto> CreateKorisnik([FromBody] KorisnikDto korisnik)
            {
                try
                {
                    Korisnik obj = mapper.Map<Korisnik>(korisnik);
                    Korisnik k = korisnikRepository.CreateKorisnik(obj);
                    korisnikRepository.SaveChanges();
                    string location = linkGenerator.GetPathByAction("GetKorisnik", "Korisnik", new { korisnikID = k.KorisnikId });

                    KorisnikDto korisnikDto = mapper.Map<KorisnikDto>(k);
                    korisnikDto.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(k.TipKorisnikaId));
                    return Created(location, mapper.Map<KorisnikDto>(k));

                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
                }
            }

            [HttpDelete("{korisnikID}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            [ProducesResponseType(StatusCodes.Status401Unauthorized)]
            public IActionResult DeleteKorisnik(int korisnikID)
            {
                try
                {
                    Korisnik korisnik = korisnikRepository.GetKorisnikById(korisnikID);
                    if (korisnik == null)
                    {
                        return NotFound();
                    }
                    korisnikRepository.DeleteKorisnik(korisnikID);
                    korisnikRepository.SaveChanges();
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
        public ActionResult<KorisnikDto> UpdateKorisnik(KorisnikUpdateDto korisnik)
        { 
            try
            {
                var oldKorisnik = korisnikRepository.GetKorisnikById(korisnik.KorisnikId);
                if (oldKorisnik == null)
                {
                   
                    return NotFound();
                }
                Korisnik korisnikEntity = mapper.Map<Korisnik>(korisnik);
                mapper.Map(korisnikEntity, oldKorisnik); //Update objekta koji treba da sačuvamo u bazi                


                korisnikRepository.SaveChanges(); //Perzistiramo promene



                KorisnikDto korisnikDto = mapper.Map<KorisnikDto>(korisnikEntity);
                korisnikDto.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(korisnikEntity.TipKorisnikaId));

                return Ok(korisnikDto);
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
