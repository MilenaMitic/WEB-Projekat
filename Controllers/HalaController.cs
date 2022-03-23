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
    public class HalaController : ControllerBase
    {
        public KoncertneHaleContext Context{get;set;}

        public HalaController(KoncertneHaleContext context)
        {
            Context=context;
        }

        [Route("PreuzmiHale")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                var hale = await Context.Hale
                .Select(p=>
                new
                {
                    NazivHale=p.Naziv

                }).ToListAsync();

                return Ok(hale);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("UnesiHalu/{nazivHale}")]
        [HttpPost]
        public async Task<ActionResult> Unesi(string nazivHale)
        {
            

            if (string.IsNullOrWhiteSpace(nazivHale) || nazivHale.Length > 50)
            {
                return BadRequest("Unesi ispravan naziv hale!");
            }
            
            try
            {
                var hala = Context.Hale.Where(h=> h.Naziv==nazivHale).FirstOrDefault();

                if(!Context.Hale.Contains(hala))
                {
                    Hala nhala = new Hala
                    {
                        Naziv=nazivHale
                    };

                    await Context.Hale.AddAsync(nhala);
                    await Context.SaveChangesAsync();

                    var format = Context.Hale.Where(p=>p.Naziv==nazivHale)
                    .Select(p=>
                    new
                    {
                        NazivHale=p.Naziv
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
    }
}