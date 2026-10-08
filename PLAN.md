# PLAN – Westropolis (Unity)

Ultima actualizare a planului: 8 octombrie 2026 (versiunea 2: repo public, reparațiile și completările analizei)
Dezvoltator: Tigra2805

Acest fișier e **ghidul complet și planul de construire** al jocului: ce s-a hotărât, regulile fixe, cum se lucrează
o etapă și lista tuturor etapelor, de la instalarea Unity până la Google Play. Stă în rădăcina repo-ului
și se bifează pe măsură ce etapele sunt gata.

---

## Cuprins

1. Deciziile luate
2. Repo-urile (public + privat) și limitele gratuite care rămân
3. Reguli fixe pentru fiecare etapă (performanță, modele și licențe, mărimi, cod, testare)
4. Cum lucrăm o etapă (Mac / telefon)
5. Instrucțiuni pentru Claude Code
6. Protecții anti-blocaj
7. Întâi gândit, apoi construit
8. Fazele 0–15 (lista etapelor)
9. Decizii lăsate pentru faza lor
10. Estimarea de timp

---

## 1. Deciziile luate

Construim „Westropolis” în Unity, pe Mac-ul M2, cu Claude Code: un joc open-world realist pentru Android
și macOS, în 16 faze (0–15). Fiecare etapă e gândită pentru 1–2 zile de lucru, la circa 5 ore pe zi.

**Principiul de bază: puțin și foarte bine, fără grabă.** Fiecare bucată se verifică și se termină corect înainte de
următoarea. O etapă poate dura oricât e nevoie, dar odată terminată nu mai trebuie refăcută.

| Subiect | Decizie |
| --- | --- |
| Nume joc | Westropolis (ales la etapa 0.1; fostul nume de lucru: „Lawless: Two Worlds”) |
| Pachet Android | com.tigra2805.westropolis (nu se mai poate schimba după publicare) |
| Proiecte vechi | `Ardeal-City` și `Orasul` se șterg complet (cu tot cu istorie, ramuri și release-uri); proiectul pornește într-un repo nou, curat, `westropolis` (08.10.2026) |
| Repo-uri | **Public** `westropolis` (codul, scenele, modelele CC0/CC-BY) + **privat** `westropolis-privat` (doar ce nu are voie să fie public: animațiile Mixamo, pachetele din Asset Store) |
| Calculator | Mac cu M2 (Unity + Claude Code + Blender) |
| Experiență Unity | Deloc: fiecare pas explicat detaliat |
| Lucru de pe telefon | Da: scripturi și logică prin Claude Code pe GitHub; partea vizuală doar pe Mac |
| Lume | Amestec: oraș modern (GTA) + Vest Sălbatic (Red Dead); legătura dintre ele se hotărăște la 0.19 |
| Ce face jucătorul | Merge/aleargă, conduce mașini, călărește, trage, luptă corp la corp, misiuni cu poveste, poliție/șerif, cumpărături |
| Prima hartă | Mică: un cartier de oraș + o bucată de Vest Sălbatic, mărită ulterior |
| Cameră | Persoana 1 și a 3-a, buton de schimbare, stil cinematic (Cinemachine 3) |
| Grafică | Realistă, optimizată pentru telefon (URP) |
| Modele | Gratuite: Sketchfab CC-BY / CC0, Poly Haven, ambientCG, Mixamo; toate în `CREDITS.md` |
| Unealtă de modelare | Blender (gratuit): scară, orientare, LOD, roți separate, ștergerea siglelor |
| Platforme | Android (test pe Galaxy S21 Ultra) și macOS (publicarea pe Mac se hotărăște la 13.6) |
| Scop | Google Play, cu bani din reclame și cumpărături în joc |
| Online | Offline acum, cod pregătit pentru online mai târziu |
| Comenzi telefon | Joystick sau săgeți (alegere din setări), butoane în dreapta, telefon pe orizontală |
| Mouse + tastatură | Pe Mac și pe telefon prin cablu OTG, cu taste schimbabile din setări |
| Limbi | Voci în engleză și/sau italiană; subtitrări și meniuri în limbile principale, inclusiv română |
| Timp | Circa 5 ore pe zi |
| Durată estimată | ~110 etape; realist **12–20 de luni** până la Google Play (vezi capitolul 10) |

---

## 2. Repo-urile și limitele gratuite care rămân

### 2.1 De ce public + privat

Repo-ul **public** are minute GitHub Actions nelimitate pe mașinile standard (Linux, Windows, macOS), deci APK-urile
și verificările nu mai consumă dintr-o cotă lunară. Dar **tot ce e în el poate fi văzut și descărcat de oricine**, așa că:

| Ce | Unde stă | De ce |
| --- | --- | --- |
| Cod C#, scene, prefab-uri, setări | Public | E munca ta; fără fișier de licență, legal nimeni nu-l poate refolosi |
| Modele Sketchfab CC-BY / CC0, Poly Haven, ambientCG | Public | Licențele permit redistribuirea (cu credit la CC-BY) |
| Fonturi Google Fonts (OFL), sunete CC0 / CC-BY | Public | Licențele permit redistribuirea |
| Animații Mixamo (fișierele FBX) | **Privat** | Termenii Adobe permit folosirea în joc, nu împărțirea fișierelor brute |
| Pachete din Unity Asset Store | **Privat** | Licența Asset Store interzice redistribuirea fișierelor sursă |
| Cheia de semnare, parole, licența Unity (`.ulf`), token-uri | **Doar GitHub Secrets** + stick USB + manager de parole | Niciodată în niciun repo |

Workflow-ul de construire din repo-ul public descarcă repo-ul privat în `Assets/_Privat/` cu un token salvat ca secret
(`PRIVAT_TOKEN`). Pe Mac, `Assets/_Privat/` e o copie (clone) a repo-ului privat, trecută în `.gitignore`-ul celui public.

### 2.2 Limitele care rămân și la un repo public

| Limită | Valoare | Regula noastră |
| --- | --- | --- |
| Fișier în repo | Avertisment la 50 MB, blocat la 100 MB | Fiecare fișier sub 50 MB; modelele intră deja simplificate |
| Mărimea repo-ului | Recomandat sub 1–5 GB | Originalele mari de pe Sketchfab NU intră în repo (rămân pe Mac, în `~/WestropolisSurse/`) |
| Git LFS | ~1 GB spațiu + ~1 GB trafic pe lună, și la public | **Fără Git LFS** (fiecare construire ar consuma traficul) |
| Memoria Actions (cache) | 10 GB pe repo | Doar folderul `Library`; cache-urile vechi se șterg singure |
| Artefacte Actions | Păstrate maxim 90 de zile, greu de descărcat de pe telefon (zip, cont) | APK-ul se pune în **GitHub Releases**, nu ca artefact |
| Fișier în Releases | Sub 2 GB fiecare | APK-ul de test: o singură versiune „test”, înlocuită la fiecare construire |
| Repo-ul privat | Minute Actions limitate (2.000/lună) | Pe repo-ul privat nu rulează nicio construire; doar e descărcat de cel public |

### 2.3 Reguli de siguranță pentru repo-ul public

1. Niciun secret, nicio parolă, nicio adresă de e-mail personală în cod, commit-uri, `JURNAL.md` sau capturi.
2. `.gitignore` blochează din start: `*.keystore`, `*.jks`, `*.ulf`, `*.p12`, `.env`, `Assets/_Privat/`.
3. Ramura `main` protejată (gratuit la repo public): se schimbă doar prin Pull Request, cu verificarea rapidă „verde”.
4. Workflow-urile nu primesc secrete pe Pull Request-uri venite din fork-uri (setarea implicită GitHub; nu se schimbă).
5. Dacă un secret ajunge din greșeală pe GitHub, se consideră furat: se schimbă imediat (parolă / cheie nouă), nu doar se șterge commit-ul.

---

## 3. Reguli fixe pentru fiecare etapă

Aceste reguli se scriu o singură dată în `CLAUDE.md` (etapa 0.10). Claude Code îl citește automat la fiecare sesiune.

### 3.1 Performanță (grafică realistă pe telefon)

1. Doar URP, cu un profil de calitate pentru Android și altul pentru Mac.
2. Fiecare model din lume are LOD (niveluri de detaliu), făcut în Blender sau cu generatorul de LOD din Unity, dacă versiunea îl are.
3. Texturi comprimate ASTC pe Android, maxim 2048 px; 1024 px pentru obiecte mici.
4. Țintă: 30 FPS stabil pe Galaxy S21 Ultra și 60 FPS pe Mac. Contorul de FPS e vizibil în versiunile de test.
5. Lumina pe cât posibil „coaptă” (baked), cu puține lumini în timp real (decizia finală la 3.5a).
6. **Buget pentru tot ecranul** pe S21 Ultra, la 30 FPS: orientativ maximum ~1–1,5 milioane de triunghiuri vizibile
   și ~300–500 de apeluri de desenare (draw calls).
7. **Buget de memorie:** sub ~2 GB în total pe S21 Ultra, din care texturile sub ~1 GB. Peste asta Android poate închide jocul.
8. **Rezoluția de randare (Render Scale):** ecranul S21 Ultra are 3200×1440; randat întreg, 30 FPS e practic imposibil.
   Pornim de la Render Scale ~0,6–0,75 pe telefon (valoarea finală la 3.7), reglabilă din setări (11.6). Interfața rămâne la rezoluție întreagă.
9. Panoul de test arată la fiecare etapă: FPS, memorie, memoria texturilor, triunghiuri și apeluri de desenare.
   Dacă un buget e depășit, etapa nu e gata.
10. **Mărimea jocului:** Google Play acceptă ~200 MB în pachetul principal (AAB). De la faza 3, zonele hărții și modelele
    mari stau în Addressables; la publicare merg prin Play Asset Delivery. În APK-urile de test sunt incluse local.
11. **Harta în bucăți:** fiecare zonă e o scenă separată, încărcată aditiv în fundal, cu bară de încărcare, fără înghețarea
    ecranului. Dacă harta trece de ~5 km, se adaugă „floating origin” (altfel la margini personajul tremură).

### 3.2 Modele și licențe

1. De pe Sketchfab se descarcă doar modele CC-BY sau CC0. Niciodată CC-BY-NC, CC-BY-ND, CC-BY-SA sau „Editorial”.
2. **Modele „furate” interzise:** nu se folosesc modele cu „ripped”, „from GTA”, „game asset”, numele unui joc în titlu
   sau descriere, sau care arată evident extrase dintr-un joc (licența afișată poate fi falsă).
3. **Mărci reale interzise:** licența unui model acoperă modelul 3D, nu marca. Siglele (mașini, firme, magazine, arme)
   se șterg în Blender, iar obiectele primesc **nume inventate, clar diferite**.
4. Fiecare resursă descărcată (model, textură, HDRI, sunet, muzică, font) se trece în `CREDITS.md`: nume, autor, link,
   licență, data. Din el se face ecranul Credite.
5. Surse permise:
   - **Sketchfab** (CC-BY / CC0);
   - **Poly Haven** și **ambientCG** (CC0: cer HDRI, texturi de asfalt, nisip, pământ, piatră);
   - **Mixamo** (animații, gratuit și în jocuri comerciale; fișierele doar în repo-ul privat);
   - **Freesound** (doar CC0 sau CC-BY, niciodată NC) pentru sunete;
   - **Google Fonts** (licența OFL) pentru fonturi;
   - pachetele **gratuite** din Unity Asset Store (doar în repo-ul privat) și pachetele oficiale Unity.
6. Claude Code nu poate intra pe Mixamo (cere cont Adobe în browser): îi dă utilizatorului lista exactă de animații și
   setările de descărcare (format FBX for Unity, „Without Skin” pentru animații, „In Place” sau nu, după decizia de la 2.2),
   iar utilizatorul le descarcă pe Mac în `Assets/_Privat/Mixamo/`.
7. **Limite de mărime la import** (orientativ): obiecte mici sub 10.000 de triunghiuri, clădiri sub 50.000, personaje și
   vehicule sub 40.000. Un model mai mare se simplifică în Blender sau se caută altul.

### 3.3 Mărimi (standardul de scară)

1. În Unity, 1 unitate = 1 metru.
2. Mărimi fixe: personaj 1,80 m, ușă 2,10 m înălțime, mașină obișnuită ~4,5 m lungime, cal ~1,60 m la greabăn,
   etaj de clădire ~3 m (parter 3,5–4,5 m), fereastră ~1,1 × 1,6 m.
3. Fiecare model se aduce la scară **în Blender, înainte de import**, și se verifică în Unity lângă un personaj-reper de 1,80 m.
4. **Fața modelului spre +Z:** la fiecare vehicul, cal sau personaj se verifică și orientarea. Captura de verificare îl
   arată din față și din lateral, lângă reper. Un model întors se corectează în Blender (sau cu un obiect-părinte în
   prefab), niciodată în cod.
5. **Nimic nu plutește și nimic nu intră în alt obiect:** totul se sprijină pe ceva.

### 3.4 Cod

1. Logica jocului (viață, bani, misiuni, inventar) e separată de comenzi și de afișare, ca să putem adăuga online mai târziu.
2. Toate comenzile trec prin Input System (tastatură, mouse, touch, OTG).
3. Niciun text scris direct în cod: toate trec prin pachetul Localization (configurat la 0.11).
4. Straturile (layers), etichetele (tags) și matricea de coliziuni stabilite la 0.11 nu se schimbă fără notă în `DECIZII.md`.
5. Cod scris doar pentru versiunile de pachete notate în `CLAUDE.md` (ex.: Cinemachine 3, nu 2).
6. Când îi dă utilizatorului cod de copiat manual, Claude Code dă fișierul complet; când lucrează direct în proiect,
   modifică doar ce trebuie.

### 3.5 Testare și salvare

1. O etapă e gata doar când funcționează în editor pe Mac **și** în APK pe Galaxy S21 Ultra.
2. După fiecare etapă reușită: Pull Request → unire în `main` → etapa bifată în `PLAN.md`.
3. Nu trecem la etapa următoare până nu e gata cea curentă.
4. Pentru tot ce se mișcă (mers, condus, călărit, NPC, trafic), verificarea se face cu o **înregistrare video**
   de 20–40 de secunde (pe Mac: pachetul Recorder; pe S21: înregistrarea de ecran din meniul rapid), nu doar cu poze.

---

## 4. Cum lucrăm o etapă

### 4.1 Acasă, pe Mac

1. Aduci ultima versiune: `git pull` în ambele repo-uri (public și `Assets/_Privat/`).
2. Deschizi Unity și terminalul cu Claude Code în folderul proiectului.
3. Îi scrii: „Citește CLAUDE.md și PLAN.md. Fă etapa X.Y. Explică-mi pas cu pas ce trebuie să fac eu în Unity.”
4. Claude Code face întâi planul scurt al etapei (capitolul 7.2), apoi creează ramura `etapa-X.Y` și construiește.
5. Apeși Play în Unity și verifici lista „Gata când” a etapei.
6. Testezi pe telefon **prin cablu** (Build And Run, etapa 0.15): cel mai rapid drum.
7. Dacă merge: scrii „merge”. Claude Code face commit, deschide Pull Request-ul, tu îl unești (sau îi spui lui să-l unească),
   apoi bifează etapa. Dacă nu merge: îi spui exact ce vezi (captură + textul erorii din Console).

### 4.2 Plecat, de pe telefon

1. Deschizi Claude Code din aplicația Claude, pe repo-ul `westropolis`.
2. Lucrezi doar la etapele marcate „merge și de pe telefon”: scripturi, logică, texte, traduceri, reparații.
3. Claude Code lucrează pe ramura etapei; verificarea rapidă arată în câteva minute dacă s-a stricat ceva.
4. Pentru APK: în aplicația GitHub → Actions → „APK Android” → „Run workflow” pe ramura etapei. APK-ul apare în Releases → „test”.
5. Ce ține de scenă (așezat obiecte, lumini, teren) rămâne pentru Mac.
6. **Niciodată nu lucrezi în același timp pe Mac și de pe telefon** pe aceeași ramură.

### 4.3 Drumul unei etape pe GitHub

```
ramura etapa-X.Y  →  commit-uri  →  verificarea rapidă (verde)  →  test pe S21  →  „merge”
→  Pull Request spre main  →  unire  →  APK automat din main (Releases „test”)  →  bifă în PLAN.md
→  la final de fază: etichetă v0.N-fazaN
```

---

## 5. Instrucțiuni pentru Claude Code (planul de acționare)

Secțiune scrisă direct pentru Claude Code, respectată la fiecare sesiune, fără excepție.

### 5.1 Cine face ce

- Claude Code construiește, câte o etapă pe rând, exact în ordinea din acest plan.
- Utilizatorul (Tigra2805) nu e programator și nu a mai folosit Unity. El verifică fiecare etapă și spune „merge” sau
  descrie ce nu merge.
- Ordinea etapelor e fixă. Nu se sare peste etape și nu se schimbă ordinea fără acordul utilizatorului.

### 5.2 Regulile de lucru, la fiecare etapă

1. Fă o singură etapă. Nu începe nimic din etapa următoare, nici „pregătiri” pentru ea.
2. Înainte să începi, spune pe scurt, în română, ce vei construi și arată planul scurt al etapei (capitolul 7.2).
3. Când termini, oprește-te și dă utilizatorului lista de verificare a etapei: ce să apese, ce trebuie să vadă,
   pas cu pas, ca pentru un începător. Lista cuprinde și reverificarea rapidă din `VERIFICARE.md`.
4. Așteaptă răspunsul. Treci la etapa următoare doar după ce utilizatorul scrie „merge”.
5. Dacă ceva nu merge, repari doar acel lucru, în aceeași etapă, și dai din nou lista de verificare.
6. Când utilizatorul are ceva de făcut manual în Unity sau Blender, explici fiecare click: în ce fereastră, ce buton,
   ce trebuie să apară.
7. Când dai cod utilizatorului, dai mereu fișierul complet.
8. După „merge”: commit cu numele etapei (ex: „Etapa 1.2 – capsula care se mișcă”), Pull Request spre `main`,
   bifă în `PLAN.md`, intrare în `JURNAL.md`, completare în `VERIFICARE.md`.
9. Respectă mereu regulile fixe, protecțiile anti-blocaj, regula „întâi gândit, apoi construit” și regula „doar se
   adaugă, nu se reconstruiește”.
10. Repo-ul e public: verifici înainte de fiecare commit că nu intră niciun secret și nimic din `Assets/_Privat/`.

### 5.3 De ce lucrăm așa

- **Pe bucăți mici:** o bucată mică se verifică în câteva minute; una mare ascunde greșeli.
- **Oprire după fiecare etapă:** o greșeală prinsă devreme se repară ușor.
- **Capsula înaintea personajului:** orice problemă de mișcare vine sigur din cod, nu din modele sau animații.
- **Cutii înaintea modelelor realiste:** forma hărții și distanțele se verifică repede cu cutii.
- **Test pe telefon la fiecare etapă:** ce merge pe Mac nu merge automat pe telefon.
- **Provizoriile planificate sunt permise** (capsula, cutiile, HUD-ul de bază de la 6.1, ecranul de setări de la 1.6).
  Interzise sunt doar provizoriile neplanificate, care obligă la rescrierea codului mai târziu.

### 5.4 Mesajul de pornire pentru fiecare sesiune

> Citește CLAUDE.md, PLAN.md, ERORI.md și ultimele intrări din JURNAL.md. Fă doar etapa X.Y. Nu face nimic din etapa
> următoare. Când termini, oprește-te și dă-mi lista cu ce trebuie să verific.

---

## 6. Protecții anti-blocaj

Se copiază și ele în `CLAUDE.md`.

### 6.1 Memoria lui Claude Code între sesiuni

| Fișier | Ce conține | Când se scrie |
| --- | --- | --- |
| `CLAUDE.md` | Regulile fixe și protecțiile | O dată, la 0.10; completat când o eroare cere o regulă nouă |
| `PLAN.md` | Acest plan, cu etapele bifate | La fiecare etapă terminată |
| `JURNAL.md` | Ce s-a făcut, ce e în lucru, probleme cunoscute, următorul pas | La finalul fiecărei sesiuni, obligatoriu |
| `DECIZII.md` | Fiecare decizie importantă și de ce | Când se ia o decizie |
| `CREDITS.md` | Toate resursele: autor, link, licență | La fiecare resursă descărcată |
| `ERORI.md` | Erorile: cauză, reparare, cum se evită | La fiecare eroare |
| `VERIFICARE.md` | Tot ce funcționează deja, de reverificat | La fiecare etapă terminată |
| `CONCEPT.md` | Conceptul jocului pe o pagină (0.19) | O dată; schimbat doar cu acordul utilizatorului |

Reguli:

1. La începutul fiecărei sesiuni: citește `CLAUDE.md`, `PLAN.md`, `ERORI.md` și ultimele intrări din `JURNAL.md`.
2. La finalul fiecărei sesiuni, chiar dacă etapa nu e gata, scrie în `JURNAL.md` unde a rămas.
3. O sesiune nouă pentru fiecare etapă: o sesiune foarte lungă pierde detalii de la început.

### 6.2 Unity

1. **Versiunea Unity se fixează** la 0.3 și nu se schimbă în timpul proiectului.
2. **Setări pentru Git** (0.5): Asset Serialization = Force Text, Version Control = Visible Meta Files.
3. **Ordine fixă a folderelor:** `Assets/_Game/` cu `Scripts`, `Scenes`, `Prefabs`, `Materials`, `Audio`, `Fonts`;
   modelele în `Assets/ThirdParty/Sketchfab/<nume model>/`, texturile în `Assets/ThirdParty/PolyHaven/`;
   ce nu are voie să fie public în `Assets/_Privat/`.
4. **Prefab-uri, nu totul în scenă:** fiecare obiect important e un prefab separat.
5. **Salvare înainte de orice modificare automată:** Cmd+S înainte ca Claude Code să lucreze în Unity.
6. **Zero erori în Console** la finalul fiecărei etape. Avertismentele galbene se notează în `JURNAL.md`.
7. **Versiunile pachetelor fixate** în `CLAUDE.md`.
8. **Fără „Reimport All”** și fără ștergerea folderului `Library` fără motiv.

### 6.3 GitHub

1. **O ramură pentru fiecare etapă** (`etapa-1.2`). `main` conține doar etape verificate cu „merge”, unite prin Pull Request.
   O etapă ieșită prost: ramura ei se aruncă, `main` rămâne curat.
2. **Pull înainte, push după.**
3. **Fără Git LFS** (capitolul 2.2).
4. **Etichetă la finalul fiecărei faze** (`v0.1-faza1`): punct sigur de întoarcere.
5. **Două lucrări separate pe GitHub Actions:**
   - **Verificarea rapidă** (compilare + teste automate) la fiecare push care schimbă fișiere `.cs`, `.asmdef`,
     `Packages/` sau `ProjectSettings/`, pe orice ramură. Durează 5–15 minute (Unity trebuie pornit).
   - **APK-ul** la unirea în `main` sau la butonul „Run workflow” pe orice ramură.
6. **APK-ul în Releases**, versiunea „test”, înlocuită la fiecare construire (nu ca artefact).
7. **Construiri mai rapide:** GameCI păstrează `Library` în cache între construiri; construirea Android rulează pe Linux.
8. **Numărul versiunii crește automat** la fiecare APK (din numărul rulării GitHub), ca telefonul să instaleze peste versiunea veche.
9. **Cheia de semnare fixă** (0.13): toate APK-urile semnate cu ea. Păstrată și în afara GitHub (stick USB + manager de
   parole). Fără ea, după publicare jocul nu mai poate fi actualizat.
10. **Smart Merge (UnityYAMLMerge)** configurat în Git (0.7), ca scenele și prefab-urile să se unească fără stricăciuni.
11. **Testele rulează înainte de APK:** dacă pică, APK-ul nu se construiește.

### 6.4 Capturi și erori vizibile

1. **Captură automată la finalul fiecărei etape:** din fereastra Game, salvată în `Capturi/etapa-X.Y.png` și urcată pe GitHub.
2. **Erorile apar pe ecran în versiunile de test:** panoul de test (0.17) arată ultimele erori și FPS-ul.
3. **Jurnal de erori salvat într-un fișier** pe telefon, trimis prin butonul „Trimite jurnalul” (meniul de partajare Android).
4. **Un singur comutator „mod test”:** contorul de FPS, panoul de erori și butoanele de test apar doar în versiunile de test.
5. **Când ceva nu merge, trimiți:** captura, textul erorii și ce ai apăsat înainte.

### 6.5 Când apare un blocaj

1. **Regula celor 2 încercări:** dacă aceeași eroare rămâne după 2 încercări, Claude Code se oprește, scrie în `JURNAL.md`
   ce a încercat, propune 2–3 căi diferite și te întreabă pe care o alegi.
2. **Întoarcere la ultima versiune bună:** se revine la `main` și etapa se reia, mai mic împărțită.
3. **Etapă prea mare:** dacă durează peste 2 zile, se împarte (ex: 3.3a și 3.3b).
4. **Nicio improvizație:** dacă un model, un pachet sau o soluție nu se potrivește cu planul, Claude Code întreabă înainte.

---

## 7. Întâi gândit, apoi construit

Claude Code nu începe o etapă până nu are tot ce îi trebuie pregătit și verificat. Se copiază și aceasta în `CLAUDE.md`.

### 7.1 Pachetele instalate de la început (etapa 0.6), câte unul pe rând

1. Se instalează un singur pachet.
2. Unity compilează; se verifică zero erori în Console.
3. Commit cu numele pachetului (ex: „Pachet: Cinemachine”).
4. Abia apoi pachetul următor.

| # | Pachet | Pentru ce |
| --- | --- | --- |
| 1 | Input System | Toate comenzile: tastatură, mouse, touch, OTG |
| 2 | Cinemachine (3.x) | Camerele persoana 1 și 3, scene cinematice |
| 3 | Timeline | Scenele cinematice din misiuni |
| 4 | Localization | Meniuri și subtitrări în mai multe limbi |
| 5 | AI Navigation | Mersul pietonilor, poliției, inamicilor (NavMesh) |
| 6 | ProBuilder | Schița hărții din cutii (blockout) |
| 7 | Animation Rigging | Țintit, mâinile pe volan și pe frâu, picioarele pe trepte și pantă |
| 8 | Splines | Traseele traficului, drumuri |
| 9 | Terrain Tools | Dealuri, nisip, iarbă (3.2) |
| 10 | glTFast | Importul modelelor de pe Sketchfab (format glTF / GLB) |
| 11 | Addressables | Zonele hărții și modelele mari în pachete separate |
| 12 | Recorder | Înregistrări video din editor, pentru verificări |
| 13 | Memory Profiler | Măsurarea memoriei (3.7, 13.1) |
| — | UI + TextMeshPro | Deja inclus în Unity 6 (pachetul uGUI); se importă doar „TMP Essentials” |
| — | Test Framework | Deja inclus; pe el se sprijină testele automate |

**Starter Assets – Third Person** NU se instalează automat: la 0.6 se verifică întâi (fără instalare) dacă versiunea
curentă e scrisă pentru Cinemachine 3 și Unity 6 și ce licență are. Dacă e compatibil, se instalează ultimul
(în `Assets/_Privat/` dacă licența nu permite repo public). Dacă nu, mișcarea se scrie de la zero la 1.2.

Pachetele pentru reclame și cumpărături (faza 14) și **Addressables for Android** (14.5) se adaugă abia atunci.

### 7.2 Verificarea de dinainte (la fiecare etapă, înainte de orice cod)

Claude Code scrie un plan scurt al etapei, în modul Plan, și i-l arată utilizatorului:

1. **Ce fișiere creează** și ce fișiere existente modifică.
2. **De ce are nevoie:** pachete, setări, modele, animații, etape anterioare; verifică pe rând că fiecare există.
3. **Ce poate să meargă prost** și cum evită asta din start.
4. **Lista de verificare** pe care utilizatorul o va primi la final.

Dacă lipsește ceva, se rezolvă întâi lipsa, apoi începe construcția.

### 7.3 Construire dintr-o bucată

1. După verificare, etapa se construiește de la cap la coadă, fără opriri și reporniri.
2. Codul se scrie complet și curat de la început.
3. După fiecare fișier scris, Unity compilează; o eroare se repară imediat.
4. APK-ul se construiește doar după ce etapa merge fără erori în editor.

### 7.4 Erorile se notează și nu se repetă

1. Fiecare eroare în `ERORI.md`: ce s-a întâmplat, cauza reală, reparația, cum se evită.
2. Erorile se repară la cauză, nu se ascund.
3. O eroare care poate apărea și în altă parte devine o regulă nouă în `CLAUDE.md`.

### 7.5 Doar se adaugă, nu se reconstruiește

1. **Cod pe module:** fiecare sistem în fișierele lui; un lucru nou = de regulă un fișier nou.
2. **Modificări punctuale** în fișierele existente.
3. **Scenele nu se regenerează:** obiectele noi se adaugă în scena existentă.
4. **Etapele verificate sunt „închise”:** codul lor se atinge doar dacă e strict necesar, cu explicația înainte.
5. **Construiri care nu pornesc de la zero:** cache-ul GitHub Actions păstrează ce s-a procesat deja.

### 7.6 Nimic vechi nu se strică pe nevăzute

1. `VERIFICARE.md`: lista scurtă cu tot ce funcționează deja, completată la fiecare etapă.
2. Lista de verificare a fiecărei etape noi cuprinde și reverificarea lucrurilor de bază.
3. **Teste automate** (Test Runner) pentru bani, inventar, salvare, misiuni, nivelul de urmărire, economie.
4. Testele rulează și pe GitHub; dacă pică, APK-ul nu se construiește.

---

## 8. Fazele 0–15

### Faza 0 – Pregătirea uneltelor (în mare parte pe Mac)

La finalul fazei 0 ai pe Galaxy S21 Ultra un APK cu un cub și panoul de test, construit automat de GitHub, plus
conceptul jocului pe o pagină. Asta dovedește că tot lanțul funcționează înainte de a scrie jocul.

- [x] **0.1 Numele jocului.** Hotărât de utilizator la 08.10.2026: **Westropolis** (West + Metropolis: cele două lumi
  într-un cuvânt), pachetul `com.tigra2805.westropolis`. Vechiul nume „Lawless: Two Worlds” a fost abandonat: „Lawless”
  e folosit de multe jocuri (Lawless West, The Lawless, Lawless de la DeNA, Lawless Lands), iar „Two Worlds” e seria
  TopWare Interactive. Căutarea pe web (08.10.2026): niciun joc „Westropolis”; nume apropiate fără risc real: Westopia
  (păcănele iOS), Destropolis (shooter Xbox/PC), Retropolis (joc de masă). Rezervă: „Duskline”. *Merge și de pe telefon.*
  *Gata când:* utilizatorul a verificat „WESTROPOLIS” în TMview (clasele 9 și 41) și nu există nicio marcă.
- [x] **0.2 Repo-urile vechi.** Hotărât de utilizator (08.10.2026): din `Ardeal-City` și `Orasul` nu se păstrează nimic;
  se șterg complet din GitHub → repo → Settings → Danger Zone → Delete this repository (dispar și ramurile, istoria,
  release-urile și memoria Actions). Workflow-ul de construire și verificarea cu Roslyn se scriu din nou la 0.12 și 0.14.
- [ ] **0.3 Unity Hub și Unity.** Instalezi Unity Hub, apoi ultima versiune **LTS a Unity 6**, cu modulele „Android Build
  Support” (cu OpenJDK și SDK/NDK) și „Mac Build Support”. Versiunea trebuie să suporte paginile de memorie de 16 KB
  (cerință Google Play). Licență: Unity Personal, gratuită. *Gata când:* Unity Hub arată versiunea cu cele două module;
  versiunea exactă e notată.
  *Decizie (08.10.2026, aprobată de utilizator):* **Unity 6.3 LTS, patch-ul fix `6000.3.25f1`**, cu suport până în
  decembrie 2027. Ales pentru că e cel mai nou patch 6.3 pentru care GameCI are deja imaginea de construire Android
  (`unityci/editor:ubuntu-6000.3.25f1-android-3`, publicată la 24.09.2026): același Unity pe Mac și pe GitHub.
  Se instalează exact acesta (Unity Hub → arhiva de versiuni), nu „cel mai nou”. Unity 6.0 LTS iese din suport în octombrie
  2026, Unity 6.6 nu e LTS, Unity 6.7 LTS nu a apărut încă. O eventuală trecere la 6.7 LTS se face o singură dată,
  doar cu acordul utilizatorului și notată în `DECIZII.md`. Patch-ul nu se schimbă în timpul proiectului.
- [ ] **0.4 Blender.** Instalezi Blender (ultima versiune stabilă, gratuit). Claude Code verifică că îl poate porni din
  terminal (scripturi Python pentru scară, orientare, LOD, export FBX). *Gata când:* un cub exportat din Blender intră în
  Unity la 1 × 1 × 1 m, cu fața spre +Z.
- [ ] **0.5 Proiectul nou.** Șablon „Universal 3D” (URP). Setări:
  - platforma Android, orientarea pe orizontală (Landscape Left + Right);
  - numele jocului, pachetul `com.tigra2805.westropolis`, dezvoltator Tigra2805;
  - Scripting Backend **IL2CPP**, arhitectura **ARM64** (obligatorii pentru Google Play);
  - grafica **Vulkan**, cu OpenGL ES 3 ca rezervă;
  - Active Input Handling = Input System (cere repornirea Unity);
  - Asset Serialization = Force Text, Version Control = Visible Meta Files;
  - Target API Level = cel mai nou instalat (cerința exactă Google Play se verifică la 14.5).
  *Gata când:* proiectul se deschide fără erori în Console.
- [ ] **0.6 Pachetele, câte unul.** În ordinea din tabelul 7.1, cu commit după fiecare. Plus verificarea Starter Assets
  (fără instalare dacă nu e compatibil). *Gata când:* toate pachetele sunt instalate, zero erori, versiunile notate.
- [ ] **0.7 Repo-urile GitHub.**
  - Repo **public** `westropolis` (creat curat la 08.10.2026, fără istorie veche). Conținut: `.gitignore` pentru Unity + liniile de siguranță (capitolul 2.3),
    `.gitattributes` (fișierele Unity ca text, fără LFS), Smart Merge (UnityYAMLMerge) configurat în Git pe Mac,
    `README.md` scurt cu „Toate drepturile rezervate” pentru cod și trimitere la `CREDITS.md` pentru resurse.
  - Repo **privat** `westropolis-privat`, clonat pe Mac în `Assets/_Privat/`.
  - Ramura `main` protejată: doar prin Pull Request.
  *Gata când:* primul commit apare pe GitHub în ambele repo-uri și `Assets/_Privat/` nu apare în cel public.
- [ ] **0.8 Claude Code pe Mac.** Instalare, autentificare, pornire în folderul proiectului. *Gata când:* Claude Code vede
  fișierele proiectului.
- [ ] **0.9 Conexiunile MCP.** Conectezi Unity și Sketchfab la Claude Code (cheia API Sketchfab o pui tu, nu în chat și
  nu în repo). Opțional: conexiunea Blender. *Gata când:* Claude Code poate crea un cub în scenă și poate căuta un model
  pe Sketchfab filtrat pe licență. Conexiunea Unity aleasă (nume + versiune) se notează în `DECIZII.md`; funcționează
  doar cu Unity deschis pe Mac.
- [ ] **0.10 CLAUDE.md și fișierele de memorie.** Claude Code creează `CLAUDE.md` (regulile fixe, protecțiile, „întâi
  gândit”, regulile repo-ului public), pune acest `PLAN.md` în proiect și creează `JURNAL.md`, `DECIZII.md`,
  `CREDITS.md`, `ERORI.md`, `VERIFICARE.md`. *Merge și de pe telefon.* *Gata când:* toate fișierele sunt pe GitHub.
  *Făcută la 08.10.2026, înaintea etapelor 0.4–0.9, cu acordul utilizatorului (nu era la Mac).*
- [ ] **0.11 Fundația proiectului.** Folderele din 6.2; straturile (Player, Vehicul, Cal, NPC, Teren, Clădiri, Apă,
  Interacțiune, Proiectil) și matricea de coliziuni; Localization configurat (limbile en și ro, primul tabel de texte,
  limba aleasă salvată). *Gata când:* totul e notat în `DECIZII.md`, zero erori.
- [ ] **0.12 APK automat.** GitHub Actions cu GameCI. Secretele: `UNITY_LICENSE` (conținutul fișierului `.ulf` de pe Mac,
  după activarea licenței în Unity Hub), `UNITY_EMAIL`, `UNITY_PASSWORD`, `PRIVAT_TOKEN`. Workflow-ul: descarcă repo-ul
  privat în `Assets/_Privat/`, păstrează `Library` în cache, construiește Addressables înainte de joc (din faza 3),
  crește automat numărul versiunii, pune APK-ul în Releases → „test” (înlocuit). *Gata când:* butonul „Run workflow”
  produce un APK descărcabil din Releases, de pe telefon.
- [ ] **0.13 Cheia de semnare fixă.** Claude Code creează cheia (keystore) pe Mac și explică pas cu pas cum o pui ca
  secrete (`ANDROID_KEYSTORE_BASE64`, `ANDROID_KEYSTORE_PASS`, `ANDROID_KEYALIAS_NAME`, `ANDROID_KEYALIAS_PASS`) și unde o
  păstrezi (stick USB + manager de parole). La publicare devine „cheia de încărcare” pentru Play App Signing.
  *Gata când:* două APK-uri construite unul după altul se instalează unul peste altul, fără dezinstalare.
- [ ] **0.14 Verificarea rapidă.** Lucrarea scurtă din GitHub Actions (compilare + teste), separată de APK, pornită doar
  când se schimbă cod sau setări (6.3.5). Plus verificarea codului cu Roslyn în sesiunile de pe telefon, fără Unity.
  *Gata când:* un push pe o ramură arată „verde” sau „roșu” în câteva minute.
- [ ] **0.15 Testare prin cablu.** Pe S21: Mod dezvoltator + Depanare USB. Pe Mac: „Build And Run” cu telefonul conectat.
  *Gata când:* APK-ul ajunge pe telefon în câteva minute, fără GitHub.
- [ ] **0.16 Primul test pe telefon.** *Gata când:* APK-ul se deschide pe S21 cu un cub rotindu-se, pe orizontală.
- [ ] **0.17 Panoul de test.** FPS, memorie (totală și texturi), triunghiuri, apeluri de desenare, ultimele erori și butonul
  „Trimite jurnalul”; ascuns de comutatorul „mod test”. *Gata când:* se vede pe telefon și dispare când comutatorul e oprit.
- [ ] **0.18 Claude Code de pe telefon.** Din aplicația Claude dai o sarcină mică pe repo. *Gata când:* modificarea ajunge
  pe o ramură, verificarea rapidă e verde, iar butonul „Run workflow” pe acea ramură produce APK.
- [ ] **0.19 Conceptul pe o pagină (`CONCEPT.md`).** Scris împreună cu Claude, aprobat de utilizator:
  - **legătura dintre cele două lumi:** aceeași epocă sau două epoci (portal, călătorie în timp etc.); ce trece dintr-o
    lume în alta (mașini, cai, arme, bani); unde e granița pe hartă;
  - tonul jocului și vârsta-țintă (cu arme și violență: probabil 16+ sau 18+);
  - bucla de joc: ce faci în 5 minute, într-o oră, într-o săptămână;
  - personajul principal în 3 rânduri.
  *Merge și de pe telefon.* *Gata când:* `CONCEPT.md` e aprobat și urcat. Harta (3.0), legea (8), magazinele (9) și
  povestea (10) se fac după el.

### Faza 1 – Mișcare și cameră (cu o capsulă în loc de personaj)

- [ ] **1.1 Teren de test.** Plan de 200 × 200 m, cutii de înălțimi diferite, o rampă, trepte (înălțime reală 17 cm),
  pe straturile de la 0.11. *Gata când:* scena se vede în Play, nimic nu plutește și nimic nu e îngropat.
- [ ] **1.2 Mișcare cu tastatura.** Fișierul de acțiuni Input System (Input Actions) pentru toate comenzile viitoare.
  Dacă Starter Assets a trecut verificarea de la 0.6, se folosește; altfel se scrie de la zero. Mers, alergat, sărit cu
  WASD/săgeți, Shift și Space, cu gravitație. *Merge și de pe telefon.* *Gata când:* capsula merge în toate direcțiile,
  aleargă clar mai repede, sare normal, urcă rampa și treptele, nu trece prin cutii și cade de pe margine.
- [ ] **1.3 Camera persoana a 3-a.** Cinemachine 3, rotire cu mouse-ul, nu intră în pereți. *Gata când:* camera urmărește lin capsula.
- [ ] **1.4 Camera persoana 1 și butonul de schimbare.** Tranziție lină, tasta V pe Mac, buton pe ecran.
  *Gata când:* comuți fără sacadări.
- [ ] **1.5 Comenzi tactile.** Joystick stânga, butoane dreapta, rotire cameră cu degetul; totul în **Safe Area**
  (în afara găurii camerei frontale). *Gata când:* pe S21 te miști și rotești camera, iar niciun buton nu e acoperit.
- [ ] **1.6 Ecranul de setări (baza) și săgețile ca variantă.** Ecranul de setări se face acum și doar se completează mai
  târziu (11.6). Alegere joystick/săgeți, salvată. *Merge și de pe telefon.* *Gata când:* alegerea rămâne după repornire.
- [ ] **1.7 Mouse și tastatură prin OTG.** Detectare automată; butoanele de pe ecran dispar când e conectată tastatura.
  *Gata când:* pe telefon cu OTG joci ca pe Mac.
- [ ] **1.8 Taste schimbabile.** Schimbi tasta pentru fiecare acțiune, în ecranul de setări. *Merge și de pe telefon.*
  *Gata când:* o tastă schimbată funcționează și după repornire.

Capsula se înlocuiește cu personajul realist abia după ce toate cele 8 etape merg.

### Faza 2 – Personajul

- [ ] **2.1 Model de personaj.** Realist, CC-BY/CC0, sub 40.000 de triunghiuri, cu **schelet umanoid** sau potrivit pentru
  rigging-ul automat Mixamo (poziție în T). Adus la scară în Blender, trecut în `CREDITS.md`. *Gata când:* stă în scenă
  la 1,80 m, cu fața spre +Z, și Unity îl acceptă ca „Humanoid” fără erori.
- [ ] **2.2 Animații de bază.** De la Mixamo: stat, mers, alergat, sărit, căzut, aterizat. Se decide o singură dată
  (notat în `DECIZII.md`): „In Place” cu viteza animației potrivită vitezei reale, sau „root motion”. *Gata când:*
  animațiile trec lin una în alta după viteză; picioarele nu alunecă și personajul nu plutește, verificat cu video.
- [ ] **2.3 Înlocuirea capsulei.** Personajul folosește comenzile din faza 1. *Gata când:* tot ce mergea cu capsula merge cu personajul.
- [ ] **2.4 Ghemuit și mers încet.** *Gata când:* funcționează pe Mac și pe telefon.
- [ ] **2.5 Picioarele pe teren (foot IK).** Animation Rigging: picioarele stau pe trepte și pe pantă, nu în aer și nu
  în sol. *Gata când:* video pe rampă și pe trepte, fără picioare îngropate sau plutind.
- [ ] **2.6 Sistemul de interacțiune.** Un singur sistem pentru tot ce se „folosește”: buton „Interacționează” pe ecran,
  tasta F pe Mac, text localizat („Urcă”, „Deschide”, „Cumpără”). Testat pe o ușă de probă. *Merge și de pe telefon.*
  *Gata când:* butonul apare doar lângă obiect și ușa se deschide.

### Faza 3 – Lumea mică

- [ ] **3.0 Planul hărții, văzut de sus.** După `CONCEPT.md`: cartierul de oraș, orășelul western, granița / drumul
  dintre ele, zonele pentru misiuni, magazine, garaj, grajd, poliție și șerif. Salvat ca imagine în proiect și notat în
  `DECIZII.md`. *Gata când:* utilizatorul aprobă schița.
- [ ] **3.1 Schița hărții din cutii (blockout).** Fiecare zonă e de la început o scenă separată, încărcată aditiv, ca
  pachet Addressables; workflow-ul construiește Addressables înainte de APK. *Gata când:* poți merge prin toată harta din
  cutii, pe Mac și pe telefon, iar zonele se încarcă fără înghețare.
- [ ] **3.2 Terenul.** Dealuri, nisip, iarbă, drumuri (Terrain Tools, texturi Poly Haven / ambientCG). *Gata când:*
  terenul arată natural și rulează la 30 FPS pe telefon.
- [ ] **3.3a Clădirile de oraș – primul model.** Un singur model de clădire adus complet: Blender (scară, LOD, fără
  sigle), import, prefab, captură de aproape. *Gata când:* clădirea arată realist de aproape și de departe, cu LOD.
- [ ] **3.3b Clădirile de oraș – cartierul.** Restul clădirilor, după același drum. *Gata când:* cartierul arată
  realist, cu LOD pe fiecare clădire și în bugetele de performanță.
- [ ] **3.4 Orășelul western.** Saloon, șerif, grajd, magazin. *Gata când:* zona western arată realist.
- [ ] **3.5 Detalii.** Felinare, garduri, copaci, cactuși, semne (cu nume inventate). *Gata când:* lumea nu mai pare goală.
- [ ] **3.5a Decizia despre lumină și zi–noapte.** a) câteva momente fixe (zi, apus, noapte), coapte separat; sau
  b) soare în timp real + Adaptive Probe Volumes / light probes, felinare „false” noaptea și doar câteva lumini reale
  lângă jucător. Ferestrele luminate noaptea cu o mască pe fiecare fereastră. *Gata când:* decizia e în `DECIZII.md`.
- [ ] **3.6 Lumină și cer.** După 3.5a: cer HDRI, ceață ușoară în depărtare. *Gata când:* arată cinematic, ține 30 FPS;
  verificat și noaptea (drumurile și personajele se văd, ferestrele nu sunt pete arse).
- [ ] **3.7 Prima optimizare.** Occlusion culling, verificare LOD, compresie texturi, Render Scale final pentru S21,
  măsurare memorie (Memory Profiler). *Gata când:* 30 FPS stabil pe toată harta, în bugetele 3.1.6–3.1.8.

### Faza 4 – Mașini

- [ ] **4.1 Prima mașină din cutii.** Fizică cu WheelCollider: accelerat, frânat, virat, frână de mână; pasul fizicii
  (Fixed Timestep) reglat pentru vehicule și notat. *Gata când:* mașina se conduce stabil pe drum.
- [ ] **4.2 Model realist de mașină.** Sketchfab CC-BY/CC0; în Blender: roți separate, sigle șterse, scară 4,5 m, fața
  spre +Z; nume inventat. *Gata când:* arată realist și roțile se învârt și virează corect.
- [ ] **4.3 Urcat și coborât.** Prin sistemul de interacțiune (2.6), cu animațiile Mixamo de intrat/ieșit și de condus
  (mâinile pe volan cu Animation Rigging). *Gata când:* intri și ieși fără să te blochezi și fără să treci prin portieră.
- [ ] **4.4 Camera de mașină.** Persoana 3 și persoana 1 (din cabină), cu efect cinematic la viteză. *Gata când:* butonul
  de cameră merge și în mașină.
- [ ] **4.5 Comenzi de condus pe telefon.** Pedale și volan sau săgeți, după setări. *Gata când:* conduci confortabil pe S21.
- [ ] **4.6 Sunete de motor și tamponări.** Sunete CC0/CC-BY, trecute în `CREDITS.md`. *Gata când:* motorul se aude după turație.
- [ ] **4.7 Încă 2–3 mașini diferite.** *Gata când:* fiecare se conduce diferit (rapidă, grea, pick-up).

### Faza 5 – Cai

- [ ] **5.1 Model de cal cu animații.** Cel mai greu de găsit gratuit; dacă nu există, căutăm alternativă (întâi în
  `DECIZII.md`). *Gata când:* calul are cel puțin stat, pas, trap, galop, la 1,60 m greabăn, cu fața spre +Z.
- [ ] **5.2 Urcat și coborât de pe cal.** Prin sistemul de interacțiune, cu animațiile de călăreț (lista Mixamo dată
  înainte; dacă lipsesc, alternativă hotărâtă cu utilizatorul). *Gata când:* personajul stă corect în șa, cu mâinile pe frâu.
- [ ] **5.3 Călărit.** Viteze diferite, întoarcere, oprire. *Gata când:* călărești prin deșert pe Mac și pe telefon.
- [ ] **5.4 Chemat calul.** Fluier, iar calul vine la tine. *Merge și de pe telefon.*

### Faza 6 – Arme și luptă

- [ ] **6.1 Viață, moarte și ragdoll.** Bară de viață (HUD de bază), deces cu ragdoll (corpul cade natural), reapariție.
  *Merge și de pe telefon* (partea de logică).
- [ ] **6.2 Primul pistol.** Țintit (animații Mixamo + Animation Rigging), tras, reîncărcat, muniție. *Gata când:* tragi
  în ținte de test.
- [ ] **6.3 Țintire pe telefon.** Asistență la țintire pentru touch. *Gata când:* nimerești ușor pe S21.
- [ ] **6.4 Mai multe arme.** Revolver western, pușcă, armă modernă (nume inventate); roată de arme. *Gata când:* schimbi
  arma din roată.
- [ ] **6.5 Luptă corp la corp.** Pumni, lovituri, blocare, animații de lovit. *Gata când:* lupta merge pe Mac și pe telefon.
- [ ] **6.6 Tras din mașină și de pe cal.** *Gata când:* funcționează în ambele situații.
- [ ] **6.7 Inamici cu AI.** Inamici care se adăpostesc, trag, se apropie, fug când sunt răniți. *Gata când:* o luptă cu
  3–5 inamici e grea dar câștigabilă, pe telefon la 30 FPS.

### Faza 7 – Oameni și trafic (NPC)

- [ ] **7.1 Pietoni.** Merg pe trotuare (NavMesh). *Gata când:* 10–20 pietoni se plimbă fără să se blocheze.
- [ ] **7.2 Reacții.** Fug când tragi, se feresc de mașini. *Merge și de pe telefon.*
- [ ] **7.3 Trafic simplu în oraș.** Mașini pe trasee Splines, oprire la intersecții. *Gata când:* traficul circulă și ține 30 FPS.
- [ ] **7.4 Furtul de mașini și reacția șoferilor.** Scoți șoferul din mașină; șoferul fuge, se ceartă sau cheamă poliția.
  *Gata când:* furtul merge fără blocări, iar nivelul de urmărire (8.1) primește fapta.
- [ ] **7.5 Oameni în zona western.** Călăreți și localnici. *Gata când:* orășelul pare locuit.

### Faza 8 – Poliție și șerif

- [ ] **8.1 Nivel de urmărire.** Stele care cresc după fapte și scad dacă te ascunzi. *Merge și de pe telefon.*
- [ ] **8.2 Poliția în oraș.** Mașini de poliție (fără însemne reale) care te urmăresc și polițiști care trag.
  *Gata când:* poți scăpa de urmărire.
- [ ] **8.3 Șeriful în zona western.** Șerif și ajutoare călare. *Gata când:* zona western are propria lege, după `CONCEPT.md`.
- [ ] **8.4 Arestare sau moarte.** Ce pierzi și unde reapari. *Merge și de pe telefon.*

### Faza 9 – Bani, cumpărături, salvare

- [ ] **9.0 Economia jocului.** Tabel cu prețuri, recompense pe misiune, câștig pe oră de joc, amenzi. *Merge și de pe
  telefon.* *Gata când:* tabelul e aprobat și pus într-un fișier de date (nu în cod).
- [ ] **9.1 Bani și inventar.** *Merge și de pe telefon.* *Gata când:* banii și obiectele apar în interfață.
- [ ] **9.2 Salvare și încărcare.** Poziție, bani, arme, misiuni. Fișierul de salvare are număr de versiune; salvările
  vechi se convertesc automat la actualizări. Salvare automată când jocul trece în fundal (apel, ieșire din aplicație),
  cu pauză automată. *Merge și de pe telefon.* *Gata când:* jocul continuă exact de unde ai rămas, și după un apel telefonic.
- [ ] **9.3 Magazin de arme.** Unul în oraș, unul în zona western. *Gata când:* cumperi și primești arma.
- [ ] **9.4 Haine.** Schimbi ținuta personajului. *Gata când:* ținuta rămâne după salvare.
- [ ] **9.5 Mașini și cai de cumpărat.** Garaj și grajd. *Gata când:* vehiculul cumpărat e al tău permanent.

### Faza 10 – Misiuni și poveste

- [ ] **10.1 Sistemul de misiuni.** Început, obiective, reușită/eșec, recompensă. *Merge și de pe telefon.*
- [ ] **10.2 Povestea pe hârtie.** Personaje, conflict, primele 5 misiuni (scrise împreună cu Claude, după `CONCEPT.md`).
  *Merge și de pe telefon.*
- [ ] **10.3 Sistemul de dialog.** Replici, alegeri simple, subtitrări. *Merge și de pe telefon.*
- [ ] **10.4 Voci în engleză și/sau italiană.** Înregistrate sau generate cu un instrument care permite uz comercial (planul
  plătit, dacă așa cer termenii); alegere din setări. *Gata când:* prima misiune are voci.
- [ ] **10.5 Scene cinematice.** Timeline + Cinemachine pentru începutul și finalul misiunilor. *Gata când:* prima scenă
  arată ca un film.
- [ ] **10.6a Misiunile 1–2 jucabile.** *Gata când:* le termini de la cap la coadă, pe telefon.
- [ ] **10.6b Misiunile 3–5 jucabile.** *Gata când:* le termini de la cap la coadă, pe telefon.

### Faza 11 – Interfață, meniuri, limbi

- [ ] **11.1 HUD complet.** Viață, muniție, bani, stele, minihartă, în Safe Area. *Gata când:* totul se citește bine pe telefon.
- [ ] **11.2 Harta mare și puncte de interes.** *Gata când:* pui un punct pe hartă și te ghidează spre el.
- [ ] **11.3 Meniul principal și pauza.** Joc nou, continuare, setări, credite, ieșire.
- [ ] **11.4 Localizare completă.** Meniuri și subtitrări în limbile principale, inclusiv română; fonturi Google Fonts
  cu toate diacriticele (ă, â, î, ș, ț și ale celorlalte limbi) în atlasul TextMeshPro. *Merge și de pe telefon.*
  *Gata când:* nicio literă nu apare ca pătrățel în nicio limbă.
- [ ] **11.5 Ecranul Credite.** Generat din `CREDITS.md`. *Merge și de pe telefon.*
- [ ] **11.6 Setări complete.** Grafică (scăzută / medie / ridicată + Render Scale), sunet, limbă, voce, comenzi,
  mărimea subtitrărilor. *Gata când:* fiecare setare se aplică imediat și rămâne după repornire.

### Faza 12 – Atmosferă și sunet

- [ ] **12.1 Zi și noapte.** După decizia de la 3.5a. *Gata când:* ciclul arată bine și ține 30 FPS.
- [ ] **12.2 Vreme.** Ploaie în oraș, furtună de nisip în deșert.
- [ ] **12.3 Sunete de ambianță.** Oraș, deșert, saloon, trafic, pași pe suprafețe diferite (CC0/CC-BY).
- [ ] **12.4 Muzică.** Diferită pentru oraș, western, urmăriri și misiuni (doar cu licență comercială, trecută în `CREDITS.md`).

### Faza 13 – Optimizare și testare finală

- [ ] **13.1 Măsurare cu Profiler și Memory Profiler pe S21 Ultra.** *Gata când:* știm ce consumă cel mai mult.
- [ ] **13.2 Reparații de performanță.** *Gata când:* 30 FPS stabil oriunde pe hartă.
- [ ] **13.3 Încălzire și baterie.** 30 de minute de joc continuu. *Gata când:* FPS-ul nu scade sub 30 după încălzire.
- [ ] **13.4 Test pe un telefon mai slab.** Împrumutat, cu setarea grafică scăzută.
- [ ] **13.5 Lista de bug-uri rezolvată.**
- [ ] **13.6 Versiunea macOS.** Construită **local pe Mac** (nu pe GitHub), testată cu mouse și tastatură. Se hotărăște
  aici: Mac doar pentru test, sau publicat (Mac App Store / Steam / itch.io: cer cont Apple Developer și notarizare).
- [ ] **13.7 Strângerea testerilor.** Contul Google Play personal nou cere o testare închisă (în 2024: 12 testeri,
  14 zile; regula actuală se verifică). Se încep strânși acum.

### Faza 14 – Bani și Google Play

- [ ] **14.0 Rapoarte de blocări.** Firebase Crashlytics sau Unity Cloud Diagnostics. *Gata când:* o blocare de test apare în raport.
- [ ] **14.1 Reclame.** Tipul se hotărăște atunci. Înainte de reclame apare formularul de consimțământ (Google UMP, GDPR).
  *Gata când:* reclamele de test apar.
- [ ] **14.2 Cumpărături în joc.** Prin Google Play Billing. *Gata când:* o cumpărătură de test funcționează.
- [ ] **14.3 Politică de confidențialitate, „Data safety” și vârsta (IARC).** Politica publicată gratuit pe GitHub Pages
  (din repo-ul public). Cu arme și violență, jocul va fi probabil 16+ sau 18+, ceea ce schimbă și reclamele permise.
- [ ] **14.4 Cont de dezvoltator și statut fiscal.** Cont Google Play (taxă unică, verificarea identității), profil de
  plăți, statutul legal în Franța pentru încasări (ex.: micro-entrepreneur). Regulile se verifică atunci.
- [ ] **14.5 Fișierul AAB semnat.** Construit de GitHub Actions; Target API Level cerut de Google Play în acel moment;
  paginile de 16 KB verificate; Play App Signing activat; zonele hărții prin Play Asset Delivery (pachetul
  Addressables for Android).
- [ ] **14.6 Pagina din magazin.** Icon, capturi de ecran, video, descrieri în mai multe limbi.
- [ ] **14.7 Testare închisă, apoi publicare.**

### Faza 15 (viitor) – Online și extinderi

- [ ] **15.1 Decizia despre online.** Jucători în aceeași lume, clasamente sau doar salvare în cloud.
- [ ] **15.2 Hartă mai mare.** Oraș extins, zonă western mai mare, drum între ele.
- [ ] **15.3 Misiuni noi.** Pe bucăți, ca actualizări.

**Cum ținem evidența:** fiecare etapă se bifează aici când e gata. O etapă prea mare se împarte (ex: 3.3a și 3.3b)
înainte să înceapă.

---

## 9. Decizii lăsate pentru faza lor

Claude Code nu le ia singur: îl întreabă pe utilizator când ajunge la faza respectivă.

| Decizie | Când se ia |
| --- | --- |
| Numele final al jocului | Etapa 0.1 |
| Folosim Starter Assets sau nu | Etapa 0.6 |
| Conexiunea Unity MCP | Etapa 0.9 |
| Legătura dintre cele două lumi, vârsta-țintă, bucla de joc | Etapa 0.19 |
| Modelul personajului principal | Etapa 2.1 |
| Animații „In Place” sau „root motion” | Etapa 2.2 |
| Lumina coaptă sau zi–noapte în timp real | Etapa 3.5a |
| Render Scale implicit pe S21 | Etapa 3.7 |
| Modelele de clădiri, mașini și cai | Etapele 3.3, 3.4, 4.2, 5.1 |
| Economia (prețuri, recompense) | Etapa 9.0 |
| Povestea, personajele, primele 5 misiuni | Etapa 10.2 |
| Vocile (înregistrate sau generate) | Etapa 10.4 |
| Muzica | Etapa 12.4 |
| Publicăm și pe Mac sau nu | Etapa 13.6 |
| Tipul de reclame | Etapa 14.1 |
| Ce cumpărături în joc oferim | Etapa 14.2 |
| Ce înseamnă „online” | Etapa 15.1 |

---

## 10. Estimarea de timp

- Planul are ~110 etape. La 1–2 zile fiecare, doar etapele simple ar însemna 6–10 luni.
- Câteva etape valorează cât multe etape mici: clădirile (3.3), orășelul western (3.4), calul (5.1), inamicii (6.7),
  misiunile (10.6), optimizarea (13.2), bug-urile (13.5). Ele se vor împărți pe parcurs.
- Estimarea sinceră, la ~5 ore pe zi: **12–20 de luni** până la Google Play.

---

**Următorul pas:** Faza 0, etapa 0.3 – instalarea Unity Hub și a Unity 6 LTS pe Mac.
