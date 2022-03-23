using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace KoncertneHale.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KartaController : ControllerBase
    {
        public KoncertneHaleContext Context{get;set;}

        public KartaController(KoncertneHaleContext context)
        {
            Context=context;
        }

        [Route("PreuzmiKarte/{nazivHale}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(string nazivHale)
        {
            try
            {
                var karte = await Context.Karte.Where(p=>p.Hala.Naziv==nazivHale)
                .Include(p=>p.Rezervacija)
                .Include(p=>p.Koncert)
                .ThenInclude(p=>p.Izvodjac)
                .Include(p=>p.Hala)
                .Select(p=>
                new
                {
                    SBR=p.Rezervacija.SBR,
                    NazivHale=p.Hala.Naziv,
                    NazivKoncerta=p.Koncert.NazivKoncerta,
                    NazivIzvodjaca=p.Koncert.Izvodjac.Naziv,
                    BrojSedista=p.BrojSedista

                }).ToListAsync();

                return Ok(karte);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("PrikaziKartu/{sbr}/{nazivHale}")]
        [HttpGet]
        public async Task<ActionResult> Prikazi(int sbr, string nazivHale)
        {
            var karta = Context.Karte.Where(p=>p.Rezervacija.SBR==sbr && p.Hala.Naziv==nazivHale).FirstOrDefault();

            if(!Context.Karte.Contains(karta))
            {
                return BadRequest("Karta ne postoji");
            }
            try
            {
                var format = Context.Karte.Where(p=>p.Rezervacija.SBR==sbr && p.Hala.Naziv==nazivHale)
                .Include(p=>p.Rezervacija)
                .Include(p=>p.Hala)
                .Include(p=>p.Koncert)
                .ThenInclude(p=>p.Izvodjac)
                .Select(p=>
                new
                {
                    Sbr=p.Rezervacija.SBR,
                    Ime=p.Rezervacija.Ime,
                    Prezime=p.Rezervacija.Prezime,
                    email=p.Rezervacija.email,
                    NazivHale=p.Hala.Naziv,
                    NazivKoncerta=p.Koncert.NazivKoncerta,
                    NazivIzvodjaca=p.Koncert.Izvodjac.Naziv,
                    Datum=p.Koncert.Datum,
                    Vreme=p.Koncert.Vreme,
                    BrojSedista=p.BrojSedista,
                    Cena=p.Koncert.Cena
                    
                });

                await format.ToListAsync();
                return Ok(format);
                

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("PreuzmiKarteKoncert/{nazivKoncerta}/{nazivHale}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziSedista(string nazivKoncerta, string nazivHale)
        {
            try
            {
                var karta = Context.Karte.Where(r=>r.Hala.Naziv==nazivHale && r.Koncert.NazivKoncerta==nazivKoncerta).FirstOrDefault();
                if(!Context.Karte.Contains(karta))
                {
                    return BadRequest("Karta ne postoji");
                }

                var format = Context.Karte.Where(p=>p.Koncert.NazivKoncerta==nazivKoncerta && p.Hala.Naziv==nazivHale)
                .Include(p=>p.Rezervacija)
                .Include(p=>p.Hala)
                .Include(p=>p.Koncert)
                .ThenInclude(p=>p.Izvodjac)
                .Select(p=>
                new
                {
                    Sbr=p.Rezervacija.SBR,
                    Ime=p.Rezervacija.Ime,
                    Prezime=p.Rezervacija.Prezime,
                    email=p.Rezervacija.email,
                    NazivHale=p.Hala.Naziv,
                    NazivKoncerta=p.Koncert.NazivKoncerta,
                    NazivIzvodjaca=p.Koncert.Izvodjac.Naziv,
                    Datum=p.Koncert.Datum,
                    Vreme=p.Koncert.Vreme,
                    BrojSedista=p.BrojSedista,
                    Cena=p.Koncert.Cena
                    
                });

                await format.ToListAsync();
                return Ok(format);
                

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        
    }
}