import { Rezervacija } from "./Rezervacija.js";
import { Koncert } from "./Koncert.js";
import { Karta } from "./Karta.js";

export class Hala{
    constructor(listaKoncerata, listaKarata, nazivHale){
        this.listaKoncerata=listaKoncerata;
        this.listaKarata=listaKarata;
        this.listaRezervacija=[];
        this.nazivHale=nazivHale;
        this.container=null;
    }

    crtaj(host){
        

        this.container=document.createElement("div");//crno
        this.container.className="GlavnaForma";
        host.appendChild(this.container);

        let naslov = document.createElement("div");
        naslov.className="Naslov";
        naslov.innerHTML=this.nazivHale;
        this.container.appendChild(naslov);

        let forma = document.createElement("div");
        forma.className="Forma";
        this.container.appendChild(forma);

        let formaRezervacija = document.createElement("div");//braon
        formaRezervacija.className="FormaRezervacija";
        forma.appendChild(formaRezervacija);

        let formaHalaKarta = document.createElement("div");//vraon
        formaHalaKarta.className="FormaHalaKarta";
        forma.appendChild(formaHalaKarta);

        this.crtajRezervacija(formaRezervacija);
        this.crtajHalaKarta(formaHalaKarta);
    }

    
    crtajRed(host){
        let red = document.createElement("div");//plavo
        red.className="Red";
        host.appendChild(red);
        return red;
    }

    crtajRezervacija(host){
        //labelaRezervacija karata
        let red = this.crtajRed(host);
        let l =document.createElement("label");
        l.innerHTML="Rezervacija karata";
        l.className="Rezervacija_karata";
        red.appendChild(l);

        //Ime
        red = this.crtajRed(host);
        red.className="RedRezervacija";
        l =document.createElement("label");
        l.innerHTML="Ime: ";
        l.className="Podacilabela";
        red.appendChild(l);

        var poljeIme = document.createElement("input");
        poljeIme.type="text";
        poljeIme.className="ime";
        red.appendChild(poljeIme);

        //Prezime
        red = this.crtajRed(host);
        red.className="RedRezervacija";
        l =document.createElement("label");
        l.innerHTML="Prezime: ";
        l.className="Podacilabela";
        red.appendChild(l);

        var poljePrezime = document.createElement("input");
        poljePrezime.type="text";
        poljePrezime.className="prezime";
        red.appendChild(poljePrezime);

        //e-mail
        red = this.crtajRed(host);
        red.className="RedRezervacija";
        l =document.createElement("label");
        l.innerHTML="e-mail: ";
        l.className="Podacilabela";
        red.appendChild(l);

        var poljeEmail = document.createElement("input");
        poljeEmail.type="email";
        poljeEmail.className="email";
        red.appendChild(poljeEmail);

        //Koncert
        red = this.crtajRed(host);
        red.className="RedKoncert"
        l =document.createElement("label");
        l.innerHTML="Koncert: ";
        l.className="Podacilabela";
        red.appendChild(l);

        let se = document.createElement("select");
        red.appendChild(se);

        let op;
        this.listaKoncerata.forEach(koncert=>{
            op = document.createElement("option");
            op.innerHTML=koncert.nazivKoncerta;
            op.value=koncert.nazivKoncerta;
            se.appendChild(op);
        })

        //INFO dugme
        l = document.createElement("button");
        l.innerHTML="INFO";
        l.onclick=(ev=>this.nadjiKoncert());
        red.appendChild(l);

        //Potvrdi Rezervaciju
        red = this.crtajRed(host);
        red.className="RedPotvrdiRezervacijuButton";
        l = document.createElement("button");
        l.innerHTML="Potvrdi rezervaciju";
        l.onclick=(ev=>this.rezervisi(poljeIme.value, poljePrezime.value, poljeEmail.value));
        red.appendChild(l);

        //Promena/Otkazivanje
        red = this.crtajRed(host);
        l =document.createElement("label");
        l.innerHTML="Promena/Otkazivanje";
        red.className="Promena_Otkazivanje";
        red.appendChild(l);

        //SBR Rezervacije
        red = this.crtajRed(host);
        red.className="SBRRed";
        l =document.createElement("label");
        l.innerHTML="SBR Rezervacije: ";
        l.className="Podacilabela";
        red.appendChild(l);

        var sbrPolje = document.createElement("input");
        sbrPolje.type="number";
        sbrPolje.className="UpisSBR";
        red.appendChild(sbrPolje);

        //Promeni i Otkazi
        red = this.crtajRed(host);
        red.className="RedDugmad"
        l = document.createElement("button");
        l.innerHTML="Promeni";
        l.onclick=(ev=>this.promeni(sbrPolje.value));
        red.appendChild(l);
        l = document.createElement("button");
        l.innerHTML="Otkaži";
        l.onclick=(ev=>this.otkazi(sbrPolje.value));
        red.appendChild(l);
        l = document.createElement("button");
        l.innerHTML="Prikaži Kartu";
        l.onclick=(ev=>this.prikaziKartu(sbrPolje.value));
        red.appendChild(l);

    }

    nadjiKoncert(){

        let optionEl = this.container.querySelector("select");
        var nazivKoncerta = optionEl.options[optionEl.selectedIndex].value;

        var listaZS = [];

        //console.log(this.listaKarata);
        this.listaKarata.forEach(p=>{
            if(p.nazivKoncerta==nazivKoncerta && p.nazivHale==this.nazivHale){
                listaZS.push(p.brojSedista);
            }
        })
        var div = this.container.querySelectorAll(".Sediste");
        div.forEach(k=>{
            this.promenibojuZ(k);
        })

        listaZS.forEach(p=>{
            div.forEach(k=>{
                if(k.innerHTML==p){
                    this.promenibojuC(k);
                }
            })
        })

        fetch("https://localhost:5001/Koncert/PreuzmiKoncert/"+nazivKoncerta+"/"+this.nazivHale,
        {
            method:"GET"
        }).then(r=>{
            if(r.ok){
                var teloInfo = this.obrisiPrethodniSadrzajInfo();
                r.json().then(data=>{
                    data.forEach(d=>{
                        const koncert = new Koncert(d.nazivKoncerta, d.nazivHale, d.nazivIzvodjaca, d.datum, d.vreme, d.cena);
                        koncert.crtajInfo(teloInfo);
                    })
                        
                })
            }
        })
    }
    

    rezervisi(ime, prezime, email){

        var alrt=0;

        if(ime.length>25 || !ime)
        {
            alert("Neispravno ime.");
            alrt=1;
            return;
        }
        if(prezime.length>25 || !prezime)
        {
            alert("Neispravno prezime.");
            alrt=1;
            return;
        }
        if(!email)
        {
            alert("Unesite email.");
            alrt=1;
            return;
        }
        const sedis = this.container.querySelectorAll(".Sediste");
        var br=0;
        var pl=0;
        sedis.forEach(p=>{
            if(p.style.backgroundColor=='rgb(92, 92, 199)'){
                pl=p.innerHTML;
                br++;
            }
        })
        
        if(br>1)
        {
            alert("Možete da izaberete samo jedno sedište.");
            alrt=1;
            return;
        }
        
        if(br==0){
            alert("Morate da izaberete sedište.");
            alrt=1;
            return;
        }
        
        
        let optionEl = this.container.querySelector("select");
        const nazivKoncerta = optionEl.options[optionEl.selectedIndex].value;
        const brojSedista = pl;

        this.listaKarata.forEach(p=>{
            if(p.brojSedista==brojSedista && p.nazivKoncerta==p.nazivKoncerta && p.nazivHale==this.nazivHale){
                alrt=1;
                return;
            }

        })

        if(alrt==0){
            fetch("https://localhost:5001/Rezervacija/NapraviRezervaciju/"+ime+"/"+prezime+"/"+email+"/"+this.nazivHale+"/"+nazivKoncerta+"/"+brojSedista,
            {
                method:"POST"
            }).then(r=>{
                if(r.ok){
                    var teloKarte = this.obrisiPrethodniSadrzajKarta();
                    r.json().then(data=>{
                    data.forEach(r=>{
                        const rezervacija = new Rezervacija(r.sbr, r.ime, r.prezime, r.email);
                        const karta = new Karta(r.sbr, r.nazivHale, r.nazivKoncerta, r.brojSedista);
                        this.listaRezervacija.push(rezervacija);
                        this.listaKarata.push(karta);
                        this.crtajKarta(teloKarte, r.ime, r.prezime, r.email, r.nazivKoncerta, r.nazivIzvodjaca, r.datum, r.vreme, r.brojSedista, r.cena, r.sbr);
                        })
                    })
                    alert("Uspešna rezervacija. Za promenu/otkazivanje koristite SBR na karti.")
                    return;
                }
            })
        }
        else{
            alert("Sedište je zauzeto");
            return;
        }
            
    }

    promeni(sbr){

        var alrt=0;

        if(sbr==0){
            alert("Unesite SBR karte.");
            alrt=1;
            return;
        }
        
        if(sbr<100000 || sbr>999999)
        {
            alert("SBR je šestocifreni broj.");
            alrt=1;
            return;
        }
        const sedis = this.container.querySelectorAll(".Sediste");
        var br=0;
        var pl=0;
        sedis.forEach(p=>{
            if(p.style.backgroundColor=='rgb(92, 92, 199)'){
                pl=p.innerHTML;
                br++;
            }
        })
        if(br>1)
        {
            alert("Možete da izaberete samo jedno sedište.");
            alrt=1;
            return;
        }
        
        if(br==0){
            alert("Morate da izaberete sedište.");
            alrt=1;
            return;
        }
        var brojSedista = pl;

        var k='';
        this.listaKarata.forEach(p=>{
            if(p.sbr==sbr){
                k=p.nazivKoncerta;
            }
        })
        
        this.listaKarata.forEach(p=>{
            if(p.nazivKoncerta==k && p.nazivHale==this.nazivHale && p.brojSedista==brojSedista){
                alrt=1;
                return;
            }
        })
        if(alrt==1){
            alert("Sedište je zauzeto.");
            return;
        }
        this.listaKarata.forEach(p=>{
            if(p.sbr==sbr && p.nazivHale==this.nazivHale){
                alrt=0;
            }
            else{
                alrt=1;
                return;
            }
        })

        if(alrt==0){
            fetch("https://localhost:5001/Rezervacija/PromeniRezervaciju/"+sbr+"/"+brojSedista+"/"+this.nazivHale,
            {
                method:"PUT"
            }).then(r=>{
                if(r.ok){
                    var teloKarte = this.obrisiPrethodniSadrzajKarta();
                    r.json().then(data=>{
                        var karta;
                        data.forEach(r=>{
                            this.crtajKarta(teloKarte, r.ime, r.prezime, r.email, r.nazivKoncerta, r.nazivIzvodjaca, r.datum, r.vreme, r.brojSedista, r.cena, r.sbr);
                            this.listaKarata.forEach(p=>{
                                if(p.sbr==r.sbr && p.nazivKoncerta==r.nazivKoncerta && p.nazivHale==r.nazivHale){
                                    p.brojSedista=brojSedista
                                }
                            })
                        })
                    })
                    alert("Uspešna promena rezervacije.");
                    return;
                }
            })
        }
        else{
            alert("Rezervacija sa datim SBR-om ne postoji.");
            return;
        }
    }

    otkazi(sbr){

        var alrt=0;
        if(sbr==0){
            alert("Unesite SBR karte.");
            alrt=1;
            return;
        }

        if(sbr<100000 || sbr>999999)
        {
            alert("SBR je šestocifreni broj.");
            alrt=1;
            return;
        }

        var div = document.querySelectorAll(".Sediste");
        div.forEach(k=>{
                this.promenibojuZ(k);
        })

        var alrt=0;
        this.listaKarata.forEach(p=>{
            if(p.sbr==sbr && p.nazivHale==this.nazivHale){
                alrt=0;
            }
            else{
                alrt=1;
                return;
            }
        })
        
        if(alrt==0){
            fetch("https://localhost:5001/Rezervacija/OtkaziRezervaciju/"+sbr+"/"+this.nazivHale,
            {
                method:"DELETE"
            }).then(r=>{
                if(r.ok){
                    r.json().then(data=>{
                        var indeks;
                        for(let i=0; i<this.listaKarata.length; i++){
                            if(this.listaKarata[i].sbr===data.sbr){
                                indeks=i;
                            }
                        }
                        for(let i=indeks; i<this.listaKarata.length;i++){
                            this.listaKarata[i]=this.listaKarata[i+1];
                        }
                        //console.log(this.listaKarata);
                        this.listaKarata.pop();
                        //console.log(this.listaKarata);
                        this.obrisiPrethodniSadrzajKarta();
                    })
                    alert("Rezervacija uspešno otkazana.");
                    return;
                }
            })
        }
        else{
            alert("Rezervacija sa datim SBR-om ne postoji.");
            return;
        }
    }  
    
    prikaziKartu(sbr){

        var alrt=1;
        if(sbr==0){
            alert("Unesite SBR karte.");
            alrt=1;
            return;
        }
        if(sbr>999999 || sbr<100000){
            alert("SBR je šestocifren broj.");
            alrt=1;
            return;
        }

        this.listaKarata.forEach(p=>{
            if(p.sbr==sbr && p.nazivHale==this.nazivHale){
                alrt=0;
            }
        })

        if(alrt==0){
            fetch("https://localhost:5001/Karta/PrikaziKartu/"+sbr+"/"+this.nazivHale,
            {
                method:"GET"
            }).then(r=>{
                if(r.ok){
                    var teloKarte = this.obrisiPrethodniSadrzajKarta();
                    r.json().then(data=>{
                        data.forEach(d=>{
                            const karta = new Karta(d.sbr, d.nazivHale, d.nazivKoncerta, d.brojSedista);
                            this.listaKarata.push(karta);
                            this.crtajKarta(teloKarte, d.ime, d.prezime, d.email, d.nazivKoncerta, d.nazivIzvodjaca, d.datum, d.vreme, d.brojSedista, d.cena, d.sbr);
                        })    
                    })
                }
            })
        }
        else{
            alert("Rezervacija ne postoji.");
            return;
        }
    }

    obrisiPrethodniSadrzajKarta(){
        var teloKarte = this.container.querySelector(".Karta");
        var roditelj = teloKarte.parentNode;
        roditelj.removeChild(teloKarte);

        teloKarte = document.createElement("div");
        teloKarte.className="Karta";
        roditelj.appendChild(teloKarte);
        return teloKarte;
    }
    obrisiPrethodniSadrzajInfo(){
        var teloInfo = this.container.querySelector(".TabelaPodaciInfo");
        var roditelj = teloInfo.parentNode;
        roditelj.removeChild(teloInfo);

        teloInfo = document.createElement("tbody");
        teloInfo.className="TabelaPodaciInfo";
        roditelj.appendChild(teloInfo);
        return teloInfo;
    }

    crtajHalaKarta(host){
        let hala = document.createElement("div");//nema na slici
        hala.className="Hala";
        host.appendChild(hala);

        let karta = document.createElement("div");//nema na slici
        karta.className="Karta";
        host.appendChild(karta);

        this.crtajHala(hala);
    }

    crtajHala(host){

        let red = this.crtajRed(host);
        red.className="TeloInfo";
        var tabela = document.createElement("table");
        tabela.className="tabelaInfo";
        red.appendChild(tabela);
    
        red = this.crtajRed(tabela);
        red.className="TabelaRed";
        var tabelahead = document.createElement("thead");
        tabelahead.innerHTML="";
        red.appendChild(tabelahead);
    
        var tr = document.createElement("tr");
        tr.className="HederInfo";
        tabelahead.appendChild(tr);

        let th;
        var zag=["Izvođač: ", "Datum: ", "Vreme: ", "Cena: "];
        zag.forEach(el=>{
            th = document.createElement("th");
            th.innerHTML=el;
            th.className="thInfo";
            tr.appendChild(th);
        })

        var tabelabody = document.createElement("tbody");
        tabelabody.className="TabelaPodaciInfo";
        red.appendChild(tabelabody);

        tr = document.createElement("tr");
        tabelabody.appendChild(tr);
        
        red = this.crtajRed(host);
        let l =document.createElement("label");
        l.innerHTML="Bina";
        red.className="Binalabela";
        red.appendChild(l);

        //Sedista
        red = this.crtajRed(host);//plavi sa sedistima
        red.className="Sedista";

        l =document.createElement("div");//crveno
        l.className="Loza1";
        red.appendChild(l);

        this.crtajRedSedista(l);

        l =document.createElement("div");//crveno
        l.className="Loza2";
        red.appendChild(l);

        this.crtajRedSedista(l);

        l =document.createElement("div");//crveno
        l.className="Loza3";
        red.appendChild(l);

        this.crtajRedSedista(l);
        this.dodeliIdSedista();
        
    }

    crtajRedSedista(host){
        
        for(let i=0; i<2; i++){
            let l = document.createElement("div");
            host.appendChild(l);
            if(i==0 && host.className=="Loza1")
            {
                this.crtajSedista(l, 5);
                l.className="PrviRed";
            }
            else if(host.className=="Loza1")
            {
                this.crtajSedista(l, 4);
                l.className="DrugiRed";
            }
            if(i==0 && host.className=="Loza2")
            {
                this.crtajSedista(l, 8);
                l.className="TreciRed";
            }
            else if(host.className=="Loza2")
            {
                this.crtajSedista(l, 8);
                l.className="CetvrtiRed";
            }
            if(i==0 && host.className=="Loza3")
            {
                this.crtajSedista(l, 4);
                l.className="PetiRed";
            }
            else if(host.className=="Loza3")
            {
                this.crtajSedista(l, 5);
                l.className="SestiRed";
            }
        }
    }

    crtajSedista(host, b){
        for(let i=0; i<b; i++){
            let l = document.createElement("div");
            l.innerHTML="SD";
            l.className="Sediste";
            l.onclick=(ev=>this.promenibojuP(l));
            host.appendChild(l);
        }
    }
    dodeliIdSedista()
    {
        let sed = this.container.querySelectorAll(".Sediste");
        for(let i=0; i<34; i++){
            sed[i].value=i+1;
            sed[i].innerHTML=i+1;
        }
    }
    promenibojuC(host)
    {
        if(host.style.backgroundColor != 'rgb(238, 58, 58)'){
            host.style.backgroundColor = 'rgb(238, 58, 58)';
        }
    }
    promenibojuZ(host)
    {
        if(host.style.backgroundColor != 'rgb(109, 255, 109)'){
            host.style.backgroundColor = 'rgb(109, 255, 109)';
        }
    }
    promenibojuP(host)
    {
        if(host.style.backgroundColor !='rgb(92, 92, 199)'){
            host.style.backgroundColor = 'rgb(92, 92, 199)';
        }
        else
        {
            host.style.backgroundColor = 'rgb(109, 255, 109)';
        }
        
    }
    crtajInfo(host){
         //INFO tabela
         var red = this.crtajRed(host);
         var tabela = document.createElement("table");
         tabela.className="tabelaKoncert";
         red.appendChild(tabela);
 
         red = this.crtajRed(tabela);
         red.className="TabelaRed";
         var tabelahead = document.createElement("thead");
         tabelahead.innerHTML="INFO";
         red.appendChild(tabelahead);
 
         var tr = document.createElement("tr");
         tr.className="HederKoncert";
         tabelahead.appendChild(tr);
 
         var tabelabody = document.createElement("tbody");
         tabelabody.className="TabelaPodaciKoncert";
         red.appendChild(tabelabody);
 
         let th;
         var zag=["Koncert", "Datum", "Vreme", "Cena"];
         zag.forEach(el=>{
             th = document.createElement("th");
             th.innerHTML=el;
             tr.appendChild(th);
         })
    }

    crtajKarta(host, ime, prezime, email, nazivKoncerta, nazivIzvodjaca, datum, vreme, brojSedista, cena, sbr){

        //Karta
        let red = this.crtajRed(host);
        let l =document.createElement("label");
        l.innerHTML="Karta: ";
        l.className="Kartalabela";
        red.appendChild(l);
    
        //Karta tabela1
         var div = document.createElement("div");
         div.className="Tabele";
         host.appendChild(div);
         red = this.crtajRed(div);
         red.className="RedTabela1";
         var tabela = document.createElement("table");
         tabela.className="tabelaHKarta";
         red.appendChild(tabela);
 
         red = this.crtajRed(tabela);
         red.className="TabelaHRed";
         var tabelahead = document.createElement("thead");
         tabelahead.innerHTML="";
         red.appendChild(tabelahead);
 
         var tr = document.createElement("tr");
         tr.className="HederKarta";
         tabelahead.appendChild(tr);
 
         let th;
         var zag=["Ime: ", "Prezime: ", "e-mail: "];
         zag.forEach(el=>{
             th = document.createElement("th");
             th.className="thKarta";
             th.innerHTML=el;
             tr.appendChild(th);
         })
        
        //Podaci
        var tabelabody = document.createElement("tbody");
        tabelabody.className="TabelaPodaciKarta";
        red.appendChild(tabelabody);

        tr = document.createElement("tr");
        tabelabody.appendChild(tr);

        var el  =document.createElement("td");
        el.innerHTML=ime;
        el.className="tdKarta";
        tr.appendChild(el);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=prezime;
        tr.appendChild(el);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=email;
        tr.appendChild(el);
    
        
    
        //Karta tabela2
        red = this.crtajRed(div);
        red.className="RedTabela2";
        var tabela = document.createElement("table");
        tabela.className="tabelaHKarta";
        red.appendChild(tabela);
 
        red = this.crtajRed(tabela);
        red.className="TabelaHRed";
        var tabelahead = document.createElement("thead");
        tabelahead.innerHTML="";
        red.appendChild(tabelahead);
 
        var tr = document.createElement("tr");
        tr.className="HederKarta";
        tabelahead.appendChild(tr);
 
        th;
        var zag=["Koncert: ", "Izvođač: ", "Datum: ", "Vreme: ", "Sedište: "];
        zag.forEach(el=>{
            th = document.createElement("th");
            th.className="thKarta";
            th.innerHTML=el;
            tr.appendChild(th);
        })

        //Podaci
        tabelabody = document.createElement("tbody");
        tabelabody.className="TabelaPodaciKarta";
        red.appendChild(tabelabody);

        tr = document.createElement("tr");
        tabelabody.appendChild(tr);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=nazivKoncerta;
        tr.appendChild(el);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=nazivIzvodjaca;
        tr.appendChild(el);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=datum;
        tr.appendChild(el);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=vreme;
        tr.appendChild(el);

        el  =document.createElement("td");
        el.className="tdKarta";
        el.innerHTML=brojSedista;
        tr.appendChild(el);

       
    
        //Karta tabela3
        red = this.crtajRed(div);
        red.className="RedTabela3";
        var tabela = document.createElement("table");
        tabela.className="tabelaHKarta";
        red.appendChild(tabela);
 
        red = this.crtajRed(tabela);
        red.className="TabelaHRed";
        var tabelahead = document.createElement("thead");
        tabelahead.innerHTML="";
        red.appendChild(tabelahead);
 
        var tr = document.createElement("tr");
        tr.className="HederKarta";
        tabelahead.appendChild(tr);
 
        th;
        var zag=["Cena: ", "SBR: "];
        zag.forEach(el=>{
            th = document.createElement("th");
            th.className="thKarta";
            th.innerHTML=el;
            tr.appendChild(th);
        })
        
        //Podaci
        tabelabody = document.createElement("tbody");
        tabelabody.className="TabelaPodaciKarta";
        red.appendChild(tabelabody);

        tr = document.createElement("tr");
        tabelabody.appendChild(tr);

        el  =document.createElement("td");
        el.innerHTML=cena;
        tr.appendChild(el);

        el  =document.createElement("td");
        el.innerHTML=sbr;
        tr.appendChild(el); 
    
        
    
    }
}