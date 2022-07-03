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
    [Route("api/proizvodi")]
    [Produces("application/json", "application/xml")]
    public class ProizvodController : ControllerBase
    {
        private readonly IProizvodRepository proizvodRepository;
        private readonly ITipProizvodaRepository tipProizvodaRepository;
        private readonly IVrstaProizvodaRepository vrstaProizvodaRepository;
        private readonly IProizvodjacRepository proizvodjacRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public ProizvodController(IProizvodRepository proizvodRepository, IVrstaProizvodaRepository vrstaProizvodaRepository,
            IProizvodjacRepository proizvodjacRepository, ITipProizvodaRepository tipProizvodaRepository,
            IMapper mapper, LinkGenerator linkGenerator)
        {
            this.proizvodRepository = proizvodRepository;
            this.tipProizvodaRepository = tipProizvodaRepository;
            this.vrstaProizvodaRepository = vrstaProizvodaRepository;
            this.proizvodjacRepository = proizvodjacRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<ProizvodDto>> GetProizvods()
        {

            List<Proizvod> proizvodi = proizvodRepository.GetProizvod();
            if (proizvodi == null || proizvodi.Count == 0)
            {
                return NoContent();
            }
            List<ProizvodDto> proizvodiDto = new List<ProizvodDto>();
            foreach (Proizvod k in proizvodi)
            {
                ProizvodDto proizvodDto = mapper.Map<ProizvodDto>(k);


                proizvodDto.VrstaProizvoda= mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(k.VrstaProizvodaId));
                proizvodDto.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(k.TipProizvodaId));
                proizvodDto.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(k.ProizvodjacId));

                proizvodiDto.Add(proizvodDto);
            }
            return Ok(proizvodiDto);
        }


        [HttpGet("{proizvodID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ProizvodDto> GetProizvod(int proizvodID)
        {

            Proizvod proizvod = proizvodRepository.GetProizvodById(proizvodID);


            if (proizvod == null)
            {
                return NotFound();
            }
            ProizvodDto proizvodDto = mapper.Map<ProizvodDto>(proizvod);
            proizvodDto.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(proizvod.VrstaProizvodaId));
            proizvodDto.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(proizvod.TipProizvodaId));
            proizvodDto.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(proizvod.ProizvodjacId));
            return Ok(mapper.Map<ProizvodDto>(proizvod));
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ProizvodDto> CreateProizvod([FromBody] ProizvodCreationDto proizvod)
        {
            try
            {
                Proizvod obj = mapper.Map<Proizvod>(proizvod);
                Proizvod k = proizvodRepository.CreateProizvod(obj);
                proizvodRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetProizvod", "Proizvod", new { proizvodID = k.ProizvodId });

                ProizvodDto proizvodDto = mapper.Map<ProizvodDto>(k);
                proizvodDto.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(k.VrstaProizvodaId));
                proizvodDto.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(k.TipProizvodaId));
                proizvodDto.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(k.ProizvodjacId));
                return Created(location, mapper.Map<ProizvodDto>(k));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{proizvodID}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteProizvod(int proizvodID)
        {
            try
            {
                Proizvod proizvod = proizvodRepository.GetProizvodById(proizvodID);
                if (proizvod == null)
                {
                    return NotFound();
                }
                proizvodRepository.DeleteProizvod(proizvodID);
                proizvodRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
        [HttpPut]
        [Consumes("application/json")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ProizvodDto> UpdateProizvod(ProizvodUpdateDto proizvod)
        {

            try
            {
                var stariProizvod = proizvodRepository.GetProizvodById(proizvod.ProizvodId);
                if (stariProizvod == null)
                {
                    
                    return NotFound();
                }
                Proizvod proizvodEntity = mapper.Map<Proizvod>(proizvod);
                mapper.Map(proizvodEntity, stariProizvod);
                proizvodRepository.SaveChanges();
                ProizvodDto proizvodDto = mapper.Map<ProizvodDto>(proizvodEntity);

                proizvodDto.VrstaProizvoda = mapper.Map<VrstaProizvodumDto>(vrstaProizvodaRepository.GetVrstaProizvodaById(proizvod.VrstaProizvodaId));
                proizvodDto.TipProizvoda = mapper.Map<TipProizvodumDto>(tipProizvodaRepository.GetTipProizvodaById(proizvod.TipProizvodaId));
                proizvodDto.Proizvodjac = mapper.Map<ProizvodjacDto>(proizvodjacRepository.GetProizvodjacById(proizvod.ProizvodjacId));

                return Ok(proizvodDto);
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
