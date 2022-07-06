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
    [Route("api/stavkePorudzbine")]
    [Route("api/stavkePorudzbine/[action]")]
    [Produces("application/json", "application/xml")]
    public class StavkaPorudzbineController : ControllerBase
    {
        private readonly IStavkaPorudzbineRepository stavkaPorudzbineRepository;
        private readonly IProizvodRepository proizvodRepository;
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IStatusPorudzbineRepository statusPorudzbineRepository;
        private readonly ITipKorisnikaRepository tipKorisnikaRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public StavkaPorudzbineController(IStavkaPorudzbineRepository stavkaPorudzbineRepository,
            IProizvodRepository proizvodRepository, IPorudzbinaRepository porudzbinaRepository, IKorisnikRepository korisnikRepository,
            IStatusPorudzbineRepository statusPorudzbineRepository, ITipKorisnikaRepository tipKorisnikaRepository,
            IProizvodjacRepository proizvodjacRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.stavkaPorudzbineRepository = stavkaPorudzbineRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.porudzbinaRepository = porudzbinaRepository;
            this.proizvodRepository = proizvodRepository;
            this.korisnikRepository = korisnikRepository;
            this.statusPorudzbineRepository = statusPorudzbineRepository;
            this.tipKorisnikaRepository = tipKorisnikaRepository;
            this.proizvodjacRepository = proizvodjacRepository;
        }

        [HttpGet]
        [HttpHead]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<StavkaPorudzbineDto>> GetStavkaPorudzbine()
        {

            List<StavkaPorudzbine> stavkaPorudzbini = stavkaPorudzbineRepository.GetStavkaPorudzbine();
            if (stavkaPorudzbini == null || stavkaPorudzbini.Count == 0)
            {
                return NoContent();
            }
            List<StavkaPorudzbineDto> stavkaPorudzbiniDto = new List<StavkaPorudzbineDto>();
            foreach (StavkaPorudzbine k in stavkaPorudzbini)
            {
                StavkaPorudzbineDto stavkaPorudzbineDto = mapper.Map<StavkaPorudzbineDto>(k);

                stavkaPorudzbineDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(k.ProizvodId));
                stavkaPorudzbineDto.Porudzbina = mapper.Map<PorudzbinaDto>(porudzbinaRepository.GetPorudzbinaById(k.PorudzbinaId));
                stavkaPorudzbineDto.Porudzbina.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(k.Porudzbina.KorisnikId));
                stavkaPorudzbineDto.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(k.Proizvod.ProizvodjacId));

                stavkaPorudzbineDto.Porudzbina.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(k.Porudzbina.StatusPorudzbineId));
                stavkaPorudzbiniDto.Add(stavkaPorudzbineDto);
            }
            return Ok(stavkaPorudzbiniDto);
        }

        [HttpGet("{stavkaPorudzbineID}")]
        [ActionName("stavkaById")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<StavkaPorudzbineDto> GetStavkaPorudzbine(int stavkaPorudzbineID)
        {

            StavkaPorudzbine stavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbineID);


            if (stavkaPorudzbine == null)
            {
                return NotFound();
            }
            StavkaPorudzbineDto stavkaPorudzbineDto = mapper.Map<StavkaPorudzbineDto>(stavkaPorudzbine);
            stavkaPorudzbineDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(stavkaPorudzbine.ProizvodId));
            stavkaPorudzbineDto.Porudzbina = mapper.Map<PorudzbinaDto>(porudzbinaRepository.GetPorudzbinaById(stavkaPorudzbine.PorudzbinaId));
            stavkaPorudzbineDto.Porudzbina.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(stavkaPorudzbine.Porudzbina.KorisnikId));
            stavkaPorudzbineDto.Porudzbina.Korisnik.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(stavkaPorudzbine.Porudzbina.Korisnik.TipKorisnikaId));
            stavkaPorudzbineDto.Porudzbina.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(stavkaPorudzbine.Porudzbina.StatusPorudzbineId));
            return Ok(mapper.Map<StavkaPorudzbineDto>(stavkaPorudzbine));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ActionName("stavkaPorudzbine")]
        [HttpGet("{porudzbinaId}")]
        public ActionResult<List<StavkaPorudzbineDto>> GetStavkaPorudzbineByPorudzbina(int porudzbinaId)
        {
            List<StavkaPorudzbine> stavkePorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineByPorudzbina(porudzbinaId);
            if (stavkePorudzbine == null || stavkePorudzbine.Count == 0)
            {
                return NoContent();
            }

            List<StavkaPorudzbineDto> stavkePorudzbineDto = new List<StavkaPorudzbineDto>();
            foreach (StavkaPorudzbine sk in stavkePorudzbine)
            {
                StavkaPorudzbineDto stavkaPorudzbineDto = mapper.Map<StavkaPorudzbineDto>(sk);
                stavkaPorudzbineDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(sk.ProizvodId));
                stavkaPorudzbineDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(sk.ProizvodId));
                stavkaPorudzbineDto.Porudzbina = mapper.Map<PorudzbinaDto>(porudzbinaRepository.GetPorudzbinaById(sk.PorudzbinaId));
                stavkaPorudzbineDto.Porudzbina.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(sk.Porudzbina.KorisnikId));
                stavkaPorudzbineDto.Porudzbina.Korisnik.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(sk.Porudzbina.Korisnik.TipKorisnikaId));
                stavkaPorudzbineDto.Porudzbina.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(sk.Porudzbina.StatusPorudzbineId));

                stavkePorudzbineDto.Add(stavkaPorudzbineDto);
            }
            return Ok(stavkePorudzbineDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ActionName("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<StavkaPorudzbineDto> CreateStavkaPorudzbine([FromBody] StavkaPorudzbineCreationDto stavkaPorudzbine)
        {
            try
            {
                StavkaPorudzbine obj = mapper.Map<StavkaPorudzbine>(stavkaPorudzbine);
                StavkaPorudzbine k = stavkaPorudzbineRepository.CreateStavkaPorudzbine(obj);
                stavkaPorudzbineRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStavkaPorudzbine", "StavkaPorudzbine", new { stavkaPorudzbineID = k.StavkaPorudzbineId });

                StavkaPorudzbineDto stavkaPorudzbineDto = mapper.Map<StavkaPorudzbineDto>(k);
                stavkaPorudzbineDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(k.ProizvodId));
                stavkaPorudzbineDto.Porudzbina = mapper.Map<PorudzbinaDto>(porudzbinaRepository.GetPorudzbinaById(k.PorudzbinaId));
                stavkaPorudzbineDto.Porudzbina.Korisnik = mapper.Map<KorisnikDto>(korisnikRepository.GetKorisnikById(k.Porudzbina.KorisnikId));
                stavkaPorudzbineDto.Porudzbina.Korisnik.TipKorisnika = mapper.Map<TipKorisnikaDto>(tipKorisnikaRepository.GetTipKorisnikaById(k.Porudzbina.Korisnik.TipKorisnikaId));
                stavkaPorudzbineDto.Porudzbina.StatusPorudzbine = mapper.Map<StatusPorudzbineDto>(statusPorudzbineRepository.GetStatusPorudzbineById(k.Porudzbina.StatusPorudzbineId));
                return Created(location, mapper.Map<StavkaPorudzbineDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{stavkaPorudzbineID}")]
        [ActionName("delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteStavkaPorudzbine(int stavkaPorudzbineID)
        {
            try
            {
                StavkaPorudzbine stavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbineID);
                if (stavkaPorudzbine == null)
                {
                    return NotFound();
                }
                stavkaPorudzbineRepository.DeleteStavkaPorudzbine(stavkaPorudzbineID);
                stavkaPorudzbineRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [Consumes("application/json")]
        [ActionName("update")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<StavkaPorudzbineDto> UpdateStavkaPorudzbine(StavkaPorudzbineUpdateDto stavkaPorudzbine)
        {

            try
            {
                var stariStavkaPorudzbine = stavkaPorudzbineRepository.GetStavkaPorudzbineById(stavkaPorudzbine.StavkaPorudzbineId);
                if (stariStavkaPorudzbine == null)
                {

                    return NotFound();
                }
                StavkaPorudzbine stavkaPorudzbineEntity = mapper.Map<StavkaPorudzbine>(stavkaPorudzbine);
                mapper.Map(stavkaPorudzbineEntity, stariStavkaPorudzbine);
                stavkaPorudzbineRepository.SaveChanges();
                StavkaPorudzbineDto stavkaPorudzbineDto = mapper.Map<StavkaPorudzbineDto>(stavkaPorudzbineEntity);

                stavkaPorudzbineDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(stavkaPorudzbine.ProizvodId));
                stavkaPorudzbineDto.Porudzbina = mapper.Map<PorudzbinaDto>(porudzbinaRepository.GetPorudzbinaById(stavkaPorudzbine.PorudzbinaId));


                return Ok(stavkaPorudzbineDto);
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

