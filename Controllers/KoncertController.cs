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
    public class KoncertController : ControllerBase
    {
        public KoncertneHaleContext Context{get;set;}
        public KoncertController(KoncertneHaleContext context)
        {
            Context=context;
        }

        [Route("PreuzmiKoncert/{nazivHale}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiKoncert(string nazivHale)
        {
            try
            {
                var hala = await Context.Hale.Where(p=>p.Naziv==nazivHale).FirstOrDefaultAsync();
                var format = await Context.Koncerti.Where(p=>p.Hala==hala)
                .Include(p=>p.Izvodjac)
                    .Select(p=>
                    new
                    {
                        NazivHale=p.Hala.Naziv,
                        NazivKoncerta=p.NazivKoncerta,
                        NazivIzvodjaca=p.Izvodjac.Naziv,
                        Datum=p.Datum,
                        Vreme=p.Vreme,
                        Cena=p.Cena

                    }).ToListAsync();

                return Ok(format);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("PreuzmiKoncert/{nazivKoncerta}/{nazivHale}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiKoncert(string nazivKoncerta, string nazivHale)
        {
            try
            {
                var hala = await Context.Hale.Where(p=>p.Naziv==nazivHale).FirstOrDefaultAsync();
                var format = await Context.Koncerti.Where(p=>p.NazivKoncerta==nazivKoncerta && p.Hala.Naziv==nazivHale)
                    .Include(p=>p.Hala)
                    .Include(p=>p.Izvodjac)
                    .Select(p=>
                    new
                    {
                        NazivHale=p.Hala.Naziv,
                        NazivKoncerta=p.NazivKoncerta,
                        NazivIzvodjaca=p.Izvodjac.Naziv,
                        Datum=p.Datum,
                        Vreme=p.Vreme,
                        Cena=p.Cena

                    }).ToListAsync();

                return Ok(format);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("UnesiKoncert/{nazivKoncerta}/{datum}/{vreme}/{cena}/{nazivIzvodjaca}/{nazivHale}")]
        [HttpPost]
        public async Task<ActionResult> Unesi(string nazivKoncerta, string datum, string vreme, int cena, string nazivIzvodjaca, string nazivHale)
        {
            

            if (string.IsNullOrWhiteSpace(nazivKoncerta) || nazivKoncerta.Length > 50)
            {
                return BadRequest("Unesi ispravan naziv koncerta!");
            }
            
            try
            {
                var koncert = Context.Koncerti.Where(h=> h.NazivKoncerta==nazivKoncerta && h.Datum==datum && h.Vreme==vreme && h.Cena==cena && h.Izvodjac.Naziv==nazivIzvodjaca && h.Hala.Naziv==nazivHale).FirstOrDefault();
                var izvodjac = Context.Izvodjaci.Where(h=> h.Naziv==nazivIzvodjaca).FirstOrDefault();
                var hala = Context.Hale.Where(h=> h.Naziv==nazivHale).FirstOrDefault();

                if(cena>3000 || cena<1000)
                {
                    return BadRequest("Cena nije u opsegu.");
                }

                if(!Context.Izvodjaci.Contains(izvodjac))
                {
                    return BadRequest("Izvodjac sa datim imenom ne postoji.");
                }
                if(!Context.Hale.Contains(hala))
                {
                    return BadRequest("Hala sa datim imenom ne postoji.");
                }

                if(!Context.Koncerti.Contains(koncert))
                {
                    Koncert nkoncert = new Koncert
                    {
                        NazivKoncerta=nazivKoncerta,
                        Datum=datum,
                        Vreme=vreme,
                        Cena=cena,
                        Izvodjac=izvodjac,
                        Hala=hala
                    };

                    await Context.Koncerti.AddAsync(nkoncert);
                    await Context.SaveChangesAsync();

                    var format = Context.Koncerti.Where(p=>p.NazivKoncerta==nazivKoncerta && p.Datum==datum && p.Vreme==vreme && p.Cena==cena && p.Izvodjac.Naziv==nazivIzvodjaca && p.Hala.Naziv==nazivHale)
                    .Select(p=>
                    new
                    {
                        NazivKoncerta=p.NazivKoncerta,
                        Datum=p.Datum,
                        Vreme=p.Vreme,
                        Cena=p.Cena,
                        NazivIzvodjaca=p.Izvodjac.Naziv,
                        NazivHale=p.Hala.Naziv
                    });

                    //Context.Rezervacije.Add(rezervacija);
                    
                    
                    return Ok(format);
                }
                else
                {
                    return BadRequest("Hala sa datim imenom vec postoji.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /* [Route("PreuzmiKoncerte")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                var format = await Context.Koncerti
                    .Include(p=>p.Izvodjac)
                    .Include(p=>p.Hala)
                    .Select(p=>
                    new
                    {
                        NazivHale=p.Hala.Naziv,
                        NazivKoncerta=p.NazivKoncerta,
                        NazivIzvodjaca=p.Izvodjac.Naziv,
                        Datum=p.Datum,
                        Vreme=p.Vreme,
                        Cena=p.Cena

                    }).ToListAsync();

                return Ok(format);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        } */
    }

}