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
    [Route("api/stavkeKorpe")]
    [Route("api/stavkeKorpe/[action]")]
    [Produces("application/json", "application/xml")]
    public class StavkaKorpeController : ControllerBase
    {
        private readonly IStavkaKorpeRepository stavkaKorpeRepository;
        private readonly IKorpaRepository korpaRepository;
        private readonly IProizvodRepository proizvodRepository;
        private readonly IKorisnikRepository korisnikRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IVrstaProizvodaRepository vrstaProizvodaRepository;
        private readonly ITipProizvodaRepository tipProizvodaRepository;
        
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public StavkaKorpeController(IStavkaKorpeRepository stavkaKorpeRepository,
            IKorpaRepository korpaRepository, IProizvodRepository proizvodRepository, 
           ITipProizvodaRepository tipProizvodaRepository, IProizvodjacRepository proizvodjacRepository,
            IVrstaProizvodaRepository vrstaProizvodaRepository, IKorisnikRepository korisnikRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.stavkaKorpeRepository = stavkaKorpeRepository;
            this.korpaRepository = korpaRepository;
            this.proizvodRepository = proizvodRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.proizvodjacRepository = proizvodjacRepository;
            this.vrstaProizvodaRepository = vrstaProizvodaRepository;
            this.tipProizvodaRepository = tipProizvodaRepository;
            this.korisnikRepository = korisnikRepository;
           
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<StavkaKorpeDto>> GetStavkaKorpe()
        {

            List<StavkaKorpe> stavkaKorpi = stavkaKorpeRepository.GetStavkaKorpe();
            if (stavkaKorpi == null || stavkaKorpi.Count == 0)
            {
                return NoContent();
            }
            List<StavkaKorpeDto> stavkaKorpiDto = new List<StavkaKorpeDto>();
            foreach (StavkaKorpe k in stavkaKorpi)
            {
                StavkaKorpeDto stavkaKorpeDto = mapper.Map<StavkaKorpeDto>(k);

                stavkaKorpeDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(k.ProizvodId));
                stavkaKorpeDto.Korpa = mapper.Map<KorpaDto>(korpaRepository.GetKorpaById(k.KorpaId));
                stavkaKorpeDto.Korpa.Korisnik = mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(k.Korpa.KorisnikId));
                stavkaKorpeDto.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(k.Proizvod.ProizvodjacId));
                stavkaKorpeDto.Proizvod.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(k.Proizvod.TipProizvodaId));
                stavkaKorpeDto.Proizvod.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(k.Proizvod.VrstaProizvodaId));

                stavkaKorpiDto.Add(stavkaKorpeDto);
            }
            return Ok(stavkaKorpiDto);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{stavkaKorpeID}")]
        [ActionName("stavkaById")]
        public ActionResult<StavkaKorpeDto> GetStavkaKorpe(int stavkaKorpeID)
        {

            StavkaKorpe stavkaKorpe = stavkaKorpeRepository.GetStavkaKorpeById(stavkaKorpeID);


            if (stavkaKorpe == null)
            {
                return NotFound();
            }
            StavkaKorpeDto stavkaKorpeDto = mapper.Map<StavkaKorpeDto>(stavkaKorpe);
            stavkaKorpeDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(stavkaKorpe.ProizvodId));
            stavkaKorpeDto.Korpa = mapper.Map<KorpaDto>(korpaRepository.GetKorpaById(stavkaKorpe.KorpaId));
            stavkaKorpeDto.Korpa.Korisnik = mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(stavkaKorpe.Korpa.KorisnikId));
            stavkaKorpeDto.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(stavkaKorpe.Proizvod.ProizvodjacId));
            stavkaKorpeDto.Proizvod.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(stavkaKorpe.Proizvod.TipProizvodaId));
            stavkaKorpeDto.Proizvod.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(stavkaKorpe.Proizvod.VrstaProizvodaId));

            return Ok(mapper.Map<StavkaKorpeDto>(stavkaKorpe));
        }
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ActionName("create")]
        public ActionResult<StavkaKorpeDto> CreateStavkaKorpe([FromBody] StavkaKorpeCreationDto stavkaKorpe)
        {
            try
            {
                StavkaKorpe obj = mapper.Map<StavkaKorpe>(stavkaKorpe);
                StavkaKorpe k = stavkaKorpeRepository.CreateStavkaKorpe(obj);
                stavkaKorpeRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetStavkaKorpe", "StavkaKorpe", new { stavkaKorpeID = k.StavkaKorpeId });

                StavkaKorpeDto stavkaKorpeDto = mapper.Map<StavkaKorpeDto>(k);
                stavkaKorpeDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(k.ProizvodId));
                stavkaKorpeDto.Korpa = mapper.Map<KorpaDto>(korpaRepository.GetKorpaById(k.KorpaId));
                stavkaKorpeDto.Korpa.Korisnik = mapper.Map<KorisnikKorpaDto>(korisnikRepository.GetKorisnikById(k.Korpa.KorisnikId));
                stavkaKorpeDto.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(k.Proizvod.ProizvodjacId));
                stavkaKorpeDto.Proizvod.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(k.Proizvod.TipProizvodaId));
                stavkaKorpeDto.Proizvod.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(k.Proizvod.VrstaProizvodaId));
                return Created(location, mapper.Map<StavkaKorpeDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ActionName("stavkaKorpe")]
        [HttpGet("{korpaId}")]
        public ActionResult<List<StavkaKorpeDto>> GetStavkaKorpeByKorpa(int korpaId)
        {
            List<StavkaKorpe> stavkeKorpe = stavkaKorpeRepository.GetStavkaKorpeByKorpa(korpaId);
            if (stavkeKorpe == null || stavkeKorpe.Count == 0)
            {
                return NoContent();
            }

            List<StavkaKorpeDto> stavkeKorpeDto = new List<StavkaKorpeDto>();
            foreach (StavkaKorpe sk in stavkeKorpe)
            {
                StavkaKorpeDto stavkaKorpeDto = mapper.Map<StavkaKorpeDto>(sk);
                stavkaKorpeDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(sk.ProizvodId));
                stavkaKorpeDto.Korpa = mapper.Map<KorpaDto>(korpaRepository.GetKorpaById(sk.KorpaId));
                stavkaKorpeDto.Proizvod.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(sk.Proizvod.ProizvodjacId));
                stavkaKorpeDto.Proizvod.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(sk.Proizvod.TipProizvodaId));
                stavkaKorpeDto.Proizvod.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(sk.Proizvod.VrstaProizvodaId));

                stavkeKorpeDto.Add(stavkaKorpeDto);
            }
            return Ok(stavkeKorpeDto);
        }

        [HttpDelete("{stavkaKorpeID}")]
        [ActionName("delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteStavkaKorpe(int stavkaKorpeID)
        {
            try
            {
                StavkaKorpe stavkaKorpe = stavkaKorpeRepository.GetStavkaKorpeById(stavkaKorpeID);
                if (stavkaKorpe == null)
                {
                    return NotFound();
                }
                stavkaKorpeRepository.DeleteStavkaKorpe(stavkaKorpeID);
                stavkaKorpeRepository.SaveChanges();
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
        [ActionName("update")]
        public ActionResult<StavkaKorpeDto> UpdateStavkaKorpe(StavkaKorpeUpdateDto stavkaKorpe)
        {

            try
            {
                var stariStavkaKorpe = stavkaKorpeRepository.GetStavkaKorpeById(stavkaKorpe.StavkaKorpeId);
                if (stariStavkaKorpe == null)
                {

                    return NotFound();
                }
                StavkaKorpe stavkaKorpeEntity = mapper.Map<StavkaKorpe>(stavkaKorpe);
                mapper.Map(stavkaKorpeEntity, stariStavkaKorpe);
                stavkaKorpeRepository.SaveChanges();
                StavkaKorpeDto stavkaKorpeDto = mapper.Map<StavkaKorpeDto>(stavkaKorpeEntity);

                stavkaKorpeDto.Proizvod = mapper.Map<ProizvodDto>(proizvodRepository.GetProizvodById(stavkaKorpe.ProizvodId));
                stavkaKorpeDto.Korpa = mapper.Map<KorpaDto>(korpaRepository.GetKorpaById(stavkaKorpe.KorpaId));

                return Ok(stavkaKorpeDto);
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
