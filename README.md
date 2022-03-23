"# WEB-Projekat" 
WEB Projekat


  Aplikacija je napravljena za rezervisanje karata za koncerte u različitim koncertnim halama. Svaka od hala ima svoje koncerte, datume i vreme njihovog održavanja kao i izvođače, pri tom u različitim halama se održavaju različiti koncerti koji mogu da imaju iste izvodjače ili isti koncerti sa istim izvođačima. 
  
Korisnik može da:

 	Proveri informacije o koncertima – Izvođač, datum i vreme održavanja i cena karte
 	Rezerviše više sedišta za koncerte
 	Proveri informacije o svojoj karti
 	Promeni svoju rezervaciju
 	Otkaže rezervaciju svoje karte
  
Početni ekran

	Na početnom ekranu korisniku se nudi opcija da bira u kojoj koncertnoj hali želi da rezerviše kartu za koncert. Kreirane su dve koncertne hale: Tokyo Dome i Allegiant Stadium. Klikom na jednu od opcija korisnik dolazi do forme za rezervisanje karata u odgovarajućoj hali.

Forme za rezervisanje

Provera informacija o koncertu

	Kada izabere koncert iz Combobox-a, klikom na dugme INFO, korisnik može da vidi podatke o koncertu (izvođač, datum i vreme održavanja kao i cenu karte) u tabeli na vrhu i na slici se crvenom bojom oboje mesta koja su već zauzeta. U različitim halama korisnik može da vidi njihova zauzeta mesta za koncert koji izaberu u combobox-u.

Rezervisanje karata

  U formi za rezervaciju karata, korisnik mora da unese ime, prezime i e-mail, a u desnom delu da izabere sedište koje želi da rezerviše. Ako korisnik pokuša da selektuje više od jednog ili nijedno mesto kao i mesto koje je zauzeto, klikom na dugme „Potvrdi“ rezervaciju korisnik se obaveštava o grešci ili uspešnoj rezervaciji. Korisnik zna koje je sedište selektovao jer ono postane plave boje. Nakon uspešne rezervacije karte, korisnik može da vidi svoju kartu u donjem desnom uglu i obaveštava se da preko SBR karte može dalje da manipuliše svojom rezervacijom. Klikom na dugme “INFO”, promena u broju zauzetih sedišta je vidljiva.

Provera informacija o karti

	Korisnik može da proveri svoju kartu putem SBR karte koju je dobio prilikom rezervacije unosom SBR karte u odgovarajuće polje i klikom na dugme „Prikaži kartu“. Korisnik se obaveštava o bilo kakvom nepravilnom unosu za SBR.

Promena rezervacije putem SBR

	Korisnik može da promeni svoju rezervaciju za odgovarajući koncert odabirom novog sedišta. Ukoliko unese nevalidan SBR ili napravi grešku tokom biranja novog sedišta kada klikne na dugme “Promeni” javljaju mu se odgovarajuća upozorenja.

	Ako je rezervacija uspešno promenjena korisnik se obaveštava i ispisuje se nova karta na dnu sa novim sedištem (SBR i ostali podaci ostaju isti).
	Promena se može videti i klikom na dugme “INFO” koje sad boji sedišta koja su zauzeta nakon promene rezervacije.

Otkazivanje rezervacije putem SBR

	Korisnik može da otkaže svoju rezervaciju za odgovarajući koncert unosom svog SBR-a karte i klikom na dugme „Otkaži”. Ukoliko unese nevalidan SBR javljaju mu se odgovarajuća upozorenja i ukoliko pokuša dva puta da otkaže kartu korisnik se obaveštava da njegova rezervacija više ne postoji. Ako je rezervacija uspešno otkazana korisnik se obaveštava i briše se karta na dnu ukoliko je bila prikazana i sva sedišta se resetuju u zelenu boju. Otkazivanje se može videti i klikom na dugme “INFO” koje sad boji sedišta koja su zauzeta nakon otkazivanja rezervacije (jedno sedište manje će biti obojeno). 
