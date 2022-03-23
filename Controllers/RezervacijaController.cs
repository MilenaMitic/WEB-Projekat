using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace KoncertneHale.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervacijaController : ControllerBase
    {
        public KoncertneHaleContext Context{get;set;}
        public RezervacijaController(KoncertneHaleContext context)
        {
            Context=context;
        }

        [Route("PreuzmiRezervacije/{nazivHale}")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi(string nazivHale)
        {
            try
            {
                var format = await Context.Rezervacije.Where(p=>p.Karta.Hala.Naziv==nazivHale)
                .Include(p=>p.Karta)
                .ThenInclude(p=>p.Hala)
                .Include(p=>p.Karta)
                .ThenInclude(p=>p.Koncert)
                .ThenInclude(p=>p.Izvodjac)
                .Select(p=>
                new
                {
                    Sbr=p.SBR,
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    email=p.email,
                    NazivHale=p.Karta.Hala.Naziv,
                    NazivKoncerta=p.Karta.Koncert.NazivKoncerta,
                    NazivIzvodjaca=p.Karta.Koncert.Izvodjac.Naziv,
                    Datum=p.Karta.Koncert.Datum,
                    Vreme=p.Karta.Koncert.Vreme,
                    BrojSedista=p.Karta.BrojSedista,
                    Cena=p.Karta.Koncert.Cena
                        

                }).ToListAsync();

                return Ok(format);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("NapraviRezervaciju/{ime}/{prezime}/{email}/{nazivHale}/{nazivKoncerta}/{brojSedista}")]
        [HttpPost]
        public async Task<ActionResult> Rezervisi(string ime, string prezime, string email, string nazivHale, string nazivKoncerta, int brojSedista)
        {
            

            if (string.IsNullOrWhiteSpace(ime) || ime.Length > 25)
            {
                return BadRequest("Unesi ispravno ime!");
            }

            if (string.IsNullOrWhiteSpace(prezime) || prezime.Length > 25)
            {
                return BadRequest("Unesi ispravno prezime!");
            }
            
            try
            {
                var koncert = Context.Koncerti.Where(k=> k.NazivKoncerta==nazivKoncerta).FirstOrDefault();
                var hala = Context.Hale.Where(h=> h.Naziv==nazivHale).FirstOrDefault();
                var karta = Context.Karte.Where(s=> s.BrojSedista==brojSedista && s.Koncert==koncert && s.Hala==hala).FirstOrDefault();

                if(!Context.Koncerti.Contains(koncert))
                {
                    return BadRequest("Koncert ne postoji");
                }
                if(!Context.Hale.Contains(hala))
                {
                    return BadRequest("Hala ne postoji");
                }

                if(!Context.Karte.Contains(karta))
                {
                    Random rs = new Random();
                    int randbr = rs.Next(100000, 999999);
                    var rv = Context.Rezervacije.Where(r=> r.SBR==randbr).FirstOrDefault();
                    while(Context.Rezervacije.Contains(rv))
                    {
                        randbr = rs.Next(100000, 999999);
                        rv = Context.Rezervacije.Where(r=> r.SBR==randbr).FirstOrDefault();
                    }
                    Rezervacija rezervacija = new Rezervacija
                    {
                        SBR=randbr,
                        Ime=ime,
                        Prezime=prezime,
                        email=email
                    };

                    await Context.Rezervacije.AddAsync(rezervacija);
                    await Context.SaveChangesAsync();

                    Karta nkarta = new Karta
                    {
                        BrojSedista=brojSedista,
                        Koncert=koncert,
                        Rezervacija=rezervacija,
                        Hala=hala
                    };

                    await Context.Karte.AddAsync(nkarta);
                    await Context.SaveChangesAsync();

                    var format = Context.Rezervacije.Where(p=>p.SBR==randbr)
                    .Include(p=>p.Karta)
                    .ThenInclude(p=>p.Hala)
                    .Include(p=>p.Karta)
                    .ThenInclude(p=>p.Koncert)
                    .ThenInclude(p=>p.Izvodjac)
                    .Select(p=>
                    new
                    {
                        SBR=p.SBR,
                        Ime=p.Ime,
                        Prezime=p.Prezime,
                        email=p.email,
                        NazivHale=p.Karta.Hala.Naziv,
                        NazivKoncerta=p.Karta.Koncert.NazivKoncerta,
                        NazivIzvodjaca=p.Karta.Koncert.Izvodjac.Naziv,
                        Datum=p.Karta.Koncert.Datum,
                        Vreme=p.Karta.Koncert.Vreme,
                        BrojSedista=p.Karta.BrojSedista,
                        Cena=p.Karta.Koncert.Cena
                    });

                    //Context.Rezervacije.Add(rezervacija);
                    
                    
                    return Ok(format);
                }
                else
                {
                    return BadRequest("Sediste je zauzeto");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromeniRezervaciju/{sbrRezervacije}/{brojSedista}/{nazivHale}")]
        [HttpPut]
        public async Task<ActionResult> Promeni(int sbrRezervacije, int brojSedista, string nazivHale)
        {
            try
            {
                var rezervacija = Context.Rezervacije.Where(r=> r.SBR==sbrRezervacije).FirstOrDefault();
                var karta = Context.Karte.Where(p=>p.Rezervacija==rezervacija && p.Hala.Naziv==nazivHale).Include(p=>p.Koncert).Include(p=>p.Hala).FirstOrDefault();
                var pkarta = Context.Karte.Where(p=>p.BrojSedista==brojSedista && p.Koncert==karta.Koncert && p.Hala==karta.Hala).FirstOrDefault();

                if(!Context.Rezervacije.Contains(rezervacija))
                {
                    return BadRequest("Rezervacija ne postoji");
                }
                if(!Context.Karte.Contains(pkarta))
                {
                    karta.BrojSedista=brojSedista;
                    rezervacija.Karta=karta;

                    await Context.SaveChangesAsync();

                    var format = Context.Rezervacije.Where(p=>p.ID==rezervacija.ID)
                    .Include(p=>p.Karta)
                    .ThenInclude(p=>p.Koncert)
                    .ThenInclude(p=>p.Izvodjac)
                    .Include(p=>p.Karta)
                    .ThenInclude(p=>p.Hala)
                    .Select(p=>
                    new
                    {
                        SBR=p.SBR,
                        Ime=p.Ime,
                        Prezime=p.Prezime,
                        email=p.email,
                        NazivHale=p.Karta.Hala.Naziv,
                        NazivKoncerta=p.Karta.Koncert.NazivKoncerta,
                        NazivIzvodjaca=p.Karta.Koncert.Izvodjac.Naziv,
                        Datum=p.Karta.Koncert.Datum,
                        Vreme=p.Karta.Koncert.Vreme,
                        BrojSedista=p.Karta.BrojSedista,
                        Cena=p.Karta.Koncert.Cena
                    }); 

                    return Ok(format);
                }
                else
                {
                    return BadRequest("Sediste je zauzeto.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("OtkaziRezervaciju/{sbrRezervacije}/{nazivHale}")]
        [HttpDelete]
        public async Task<ActionResult> Otkazi(int sbrRezervacije, string nazivHale)
        {
            try
            {
                var rezervacija = await Context.Rezervacije.Where(r=> r.SBR==sbrRezervacije).FirstOrDefaultAsync();
                var format = await Context.Rezervacije.Where(r=> r.SBR==sbrRezervacije)
                .Include(p=>p.Karta)
                .ThenInclude(p=>p.Koncert)
                .ThenInclude(p=>p.Izvodjac)
                .Include(p=>p.Karta)
                .ThenInclude(p=>p.Hala)
                .Select(p=>
                new
                {
                    SBR=p.SBR,
                    Ime=p.Ime,
                    Prezime=p.Prezime,
                    email=p.email,
                    NazivHale=p.Karta.Hala.Naziv,
                    NazivKoncerta=p.Karta.Koncert.NazivKoncerta,
                    NazivIzvodjaca=p.Karta.Koncert.Izvodjac.Naziv,
                    Datum=p.Karta.Koncert.Datum,
                    Vreme=p.Karta.Koncert.Vreme,
                    BrojSedista=p.Karta.BrojSedista,
                    Cena=p.Karta.Koncert.Cena
                }).FirstOrDefaultAsync();
                var karta = Context.Karte.Where(p=>p.Rezervacija==rezervacija && p.Hala.Naziv==nazivHale).FirstOrDefault();
                
                if(Context.Rezervacije.Contains(rezervacija))
                {
                    Context.Rezervacije.Remove(rezervacija);
                    Context.Karte.Remove(karta);

                    await Context.SaveChangesAsync();

                    return Ok(format);
                }
                else
                {
                    return BadRequest("Rezervacija nije pronadjena.");
                }
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
