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
    public class IzvodjacController : ControllerBase
    {
        public KoncertneHaleContext Context{get;set;}
        public IzvodjacController(KoncertneHaleContext context)
        {
            Context=context;
        }

        [Route("UnesiIzvodjaca/{nazivIzvodjaca}")]
        [HttpPost]
        public async Task<ActionResult> Unesi(string nazivIzvodjaca)
        {
            

            if (string.IsNullOrWhiteSpace(nazivIzvodjaca) || nazivIzvodjaca.Length > 50)
            {
                return BadRequest("Unesi ispravan naziv Izvodjaca!");
            }
            
            try
            {
                var izvodjac = Context.Izvodjaci.Where(h=> h.Naziv==nazivIzvodjaca).FirstOrDefault();

                if(!Context.Izvodjaci.Contains(izvodjac))
                {
                    Izvodjac nizvodjac = new Izvodjac
                    {
                        Naziv=nazivIzvodjaca
                    };

                    await Context.Izvodjaci.AddAsync(nizvodjac);
                    await Context.SaveChangesAsync();

                    var format = Context.Izvodjaci.Where(p=> p.Naziv==nazivIzvodjaca)
                    .Select(p=>
                    new
                    {
                        Naziv=p.Naziv
                    });

                    //Context.Rezervacije.Add(rezervacija);
                    
                    
                    return Ok(format);
                }
                else
                {
                    return BadRequest("Izvodjac sa datim imenom vec postoji.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}