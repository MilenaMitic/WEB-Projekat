import { Hala } from "./Hala.js";
import { Koncert } from "./Koncert.js";
import { Rezervacija } from "./Rezervacija.js";
import { Karta } from "./Karta.js";

var listaHala=[];
var listaKoncerata=[];
var listaKarata=[];

fetch("https://localhost:5001/Hala/PreuzmiHale")
.then(r=>{
    if(r.ok){
        r.json().then(hale=>{
            hale.forEach(hala=>{
                listaHala.push(hala);
            })
            crtajPocetnuStranu();
        
        function crtajPocetnuStranu(){
        let glavna=document.createElement("div");
        glavna.className="GlavnaFormaPocetna";
        document.body.appendChild(glavna);

        let red=document.createElement("div");
         red.className="PocetnaNaslov";
         red.innerHTML="Koncertne Hale";
         glavna.appendChild(red);

         let izbor=document.createElement("div");
         izbor.className="PocetnaIzbor";
         glavna.appendChild(izbor);

         listaHala.forEach(p=>{
            red=document.createElement("div");
            red.className="IzborHala";
            red.innerHTML=p.nazivHale;
            red.onclick=(ev => izaberi())
            izbor.appendChild(red);

            function izaberi(){

                var naziv = p.nazivHale;

                fetch("https:\\localhost:5001/Koncert/PreuzmiKoncert/"+naziv)
                .then(p=>{
                    p.json().then(koncerti=>{
                        koncerti.forEach(koncert=>{
                            var k = new Koncert(koncert.nazivKoncerta, koncert.nazivHale, koncert.nazivIzvodjaca, koncert.datum, koncert.vreme, koncert.cena);
                            listaKoncerata.push(k);
                        })

                        fetch("https:\\localhost:5001/Karta/PreuzmiKarte/"+naziv)
                        .then(p=>{
                            p.json().then(karte=>{
                                karte.forEach(karta=>{
                                    var k = new Karta(karta.sbr, karta.nazivHale, karta.nazivKoncerta, karta.brojSedista);
                                    listaKarata.push(k);
                                })



                                var hala = new Hala(listaKoncerata, listaKarata, naziv);
                                var pozadina = document.body.querySelector(".GlavnaFormaPocetna");
                                if (pozadina != null){
                                    var rod = pozadina.parentNode;
                                    rod.removeChild(pozadina);
                                }
                                var izborHala = document.body.querySelectorAll(".izborHala");

                                izborHala.forEach(hala => {
                                    let rod = hala.parentNode;
                                rod.removeChild(hala);
                                });

                                hala.crtaj(document.body);
                                return;
                            })
                        })

                    })
                })
                
            }
         })
  
        }
        })
    }
        
})
        
/* var listaKoncerata2=[];
var listaRezervacija2=[];
var listaKarata2=[];
fetch("https://localhost:5001/Koncert/PreuzmiKoncert/"+"Allegiant Stadium")
.then(p=>{
    p.json().then(koncerti=>{
        koncerti.forEach(koncert => {
            var k = new Koncert(koncert.nazivKoncerta, koncert.nazivHale, koncert.nazivIzvodjaca, koncert.datum, koncert.vreme, koncert.cena);
            listaKoncerata2.push(k);
        });
    fetch("https://localhost:5001/Karta/PreuzmiKarte/"+"Allegiant Stadium")
    .then(p=>{
        p.json().then(karte=>{
            karte.forEach(karta => {
                var k = new Karta(karta.sbr, karta.nazivHale, karta.nazivKoncerta, karta.brojSedista);
                listaKarata2.push(k);
            });
        })
    })
    fetch("https://localhost:5001/Rezervacija/PreuzmiRezervacije/"+"Allegiant Stadium")
    .then(p=>{
        p.json().then(rezervacije=>{
            rezervacije.forEach(rezervacija => {
                var r = new Rezervacija(rezervacija.sbr, rezervacija.ime, rezervacija.prezime, rezervacija.email, rezervacija.nazivKoncerta, rezervacija.nazivIzvodjaca, rezervacija.datum, rezervacija.vreme, rezervacija.brojSedista, rezervacija.cena);
                listaRezervacija2.push(r);
            });
        })
    })
        var hala2 = new Hala(listaKoncerata2, listaRezervacija2, listaKarata2, "Allegiant Stadium");
        hala2.crtaj(document.body);
    })
}) */


