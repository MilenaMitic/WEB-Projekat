export class Koncert{
    constructor(nazivKoncerta, nazivHale, nazivIzvodjaca, datum, vreme, cena){
        this.nazivKoncerta=nazivKoncerta;
        this.nazivHale=nazivHale;
        this.nazivIzvodjaca=nazivIzvodjaca;
        this.datum=datum;
        this.vreme=vreme;
        this.cena=cena;
    }

    crtajRed(host){
        let red = document.createElement("div");//plavo
        red.className="Red";
        host.appendChild(red);
        return red;
    }

    crtajInfo(host){

        //Podaci
        var tr = document.createElement("tr");
        tr.className="RedPodaciInfo";
        host.appendChild(tr);

        var el  =document.createElement("td");
        el.innerHTML=this.nazivIzvodjaca;
        el.className="tdInfo";
        tr.appendChild(el);

        el  =document.createElement("td");
        el.innerHTML=this.datum;
        el.className="tdInfo";
        tr.appendChild(el);

        el  =document.createElement("td");
        el.innerHTML=this.vreme;
        el.className="tdInfo";
        tr.appendChild(el);

        el  =document.createElement("td");
        el.innerHTML=this.cena;
        el.className="tdInfo";
        tr.appendChild(el);
        
    }
}