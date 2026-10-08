# Westropolis – reguli de lucru (se citesc la fiecare sesiune)

Joc open-world realist (oraș modern + Vest Sălbatic) în Unity, pentru Android (test pe Galaxy S21 Ultra) și macOS.
Pornit de la zero la 08.10.2026. Nimic din proiectele vechi (Ardeal City, Orasul) nu se păstrează și nu se aplică.
Detaliile complete sunt în `PLAN.md`; aici sunt regulile care se respectă mereu.

## Datele fixe ale proiectului
- Nume: **Westropolis** · pachet Android `com.tigra2805.westropolis` · dezvoltator Tigra2805.
- Unity **6.3 LTS, patch fix `6000.3.25f1`** (imagine GameCI `unityci/editor:ubuntu-6000.3.25f1-android-3`). Nu se schimbă.
- Repo public `tigra2894-jpg/westropolis`; repo privat `westropolis-privat` (Mixamo, Asset Store) în `Assets/_Privat/`.
- Calculatorul: **MacBook M2 (Apple Silicon), macOS 15 Sequoia** – Unity, Blender, Android SDK în variantele Apple
  Silicon; pentru construirea macOS cu IL2CPP trebuie Xcode (sau Command Line Tools).
- Versiunile pachetelor: se notează aici la etapa 0.6 (Cinemachine 3, nu 2).

## La începutul fiecărei sesiuni
1. Citește `CLAUDE.md`, `PLAN.md`, `ERORI.md` și ultimele intrări din `JURNAL.md`, înainte de orice.
2. O sesiune nouă pentru fiecare etapă.

## Cum lucrez cu utilizatorul
- Utilizatorul nu e programator și n-a folosit Unity; scrie des de pe telefon. Răspunsuri în **română**, detaliate,
  strict la întrebare. Fiecare click explicat (fereastră, buton, ce trebuie să apară).
- **O singură etapă odată**, în ordinea din `PLAN.md`. Nimic din etapa următoare, nici „pregătiri”.
- Înainte de construire: spun pe scurt ce fac și arăt planul scurt al etapei (fișiere create / modificate, de ce am
  nevoie și verific că există, ce poate merge prost, lista de verificare).
- La final: lista de verificare pas cu pas (inclusiv reverificarea din `VERIFICARE.md`), apoi **mă opresc până la „merge”**.
- Ce nu merge se repară în aceeași etapă; apoi lista de verificare din nou.
- Cod dat utilizatorului de copiat: fișierul complet. Cod scris direct în proiect: doar rândurile necesare.
- Ordinea etapelor se schimbă doar cu acordul utilizatorului. Deciziile din `PLAN.md` cap. 9 le ia el, nu eu.

## Git și GitHub
- O ramură pe etapă (`etapa-X.Y`); după „merge”: Pull Request → unire în `main` → bifă în `PLAN.md`, intrare în
  `JURNAL.md`, completare în `VERIFICARE.md`; ramura se șterge. Etichetă `v0.N-fazaN` la final de fază.
- Pull înainte, push după. Nu se lucrează simultan pe Mac și de pe telefon.
- **Fără Git LFS.** Fiecare fișier sub 50 MB; originalele mari rămân pe Mac (`~/WestropolisSurse/`).
- APK-ul în **Releases → „test”** (înlocuit la fiecare construire), nu ca artefact.
- Verificarea rapidă (compilare + teste) la push cu schimbări de cod / setări; APK la unirea în `main` sau la buton.
- Testele pică → APK-ul nu se construiește.

## Repo public – siguranță (verific înainte de FIECARE commit)
- Niciun secret, parolă, token, cheie, fișier `.ulf` / `.keystore`, adresă de e-mail personală în fișiere, commit-uri
  sau capturi. Secretele stau doar în GitHub Secrets (+ copia utilizatorului pe stick USB și în managerul de parole).
- Nimic din `Assets/_Privat/` în repo-ul public (Mixamo, Asset Store).
- Un secret scăpat pe GitHub se consideră furat: se schimbă imediat, nu doar se șterge.

## Performanță (S21 Ultra: 30 FPS stabil; Mac: 60 FPS)
- Doar URP, profil Android + profil Mac. LOD pe fiecare model. Texturi ASTC, max 2048 px (1024 la obiecte mici).
- Buget pe ecran: ~1–1,5 mil. triunghiuri, ~300–500 draw calls; memorie sub ~2 GB (texturi sub ~1 GB).
- Render Scale pe telefon ~0,6–0,75 (final la 3.7), reglabil din setări; interfața la rezoluție întreagă.
- Lumina cât mai „coaptă”, puține lumini reale. Zonele hărții = scene separate, încărcate aditiv, prin Addressables.
- Un buget depășit = etapa nu e gata.

## Modele și licențe
- Sketchfab doar **CC-BY / CC0** (niciodată NC, ND, SA, „Editorial”); Poly Haven, ambientCG (CC0); Mixamo (în repo-ul
  privat); Freesound doar CC0 / CC-BY; fonturi Google Fonts (OFL); Asset Store doar gratuite și doar în repo-ul privat.
- Interzise: modele „ripped” / extrase din alte jocuri; mărci reale (sigle șterse în Blender, nume inventate).
- Fiecare resursă în `CREDITS.md` (nume, autor, link, licență, data).
- Limite: obiecte mici < 10.000 triunghiuri, clădiri < 50.000, personaje / vehicule < 40.000.

## Scară și orientare
- 1 unitate = 1 m. Personaj 1,80 m, ușă 2,10 m, mașină ~4,5 m, cal ~1,60 m la greabăn, etaj ~3 m (parter 3,5–4,5 m).
- Scara și fața spre **+Z** se corectează în Blender (sau cu obiect-părinte în prefab), niciodată în cod; verificat
  lângă reperul de 1,80 m, din față și din lateral. Nimic nu plutește, nimic nu intră în alt obiect.

## Cod
- Logica jocului separată de comenzi și de afișare (pregătit pentru online).
- Toate comenzile prin Input System. Niciun text în cod: totul prin Localization.
- Straturile și matricea de coliziuni (0.11) nu se schimbă fără notă în `DECIZII.md`.
- Cod pe module: un sistem nou = de regulă fișiere noi. Etapele verificate sunt „închise”: le ating doar dacă e strict
  necesar, cu explicația înainte. Scenele nu se regenerează. Fără „Reimport All”, fără ștergerea `Library`.
- Fără provizorii neplanificate (provizoriile din plan sunt permise: capsula, cutiile, HUD-ul de bază).

## Unity
- Asset Serialization = Force Text, Visible Meta Files. Foldere: `Assets/_Game/` (Scripts, Scenes, Prefabs, Materials,
  Audio, Fonts), `Assets/ThirdParty/Sketchfab/<model>/`, `Assets/ThirdParty/PolyHaven/`, `Assets/_Privat/`.
- Prefab-uri pentru tot ce e important. Cmd+S înainte de orice modificare automată.
- Zero erori în Console la finalul fiecărei etape; avertismentele în `JURNAL.md`.
- Unity compilează după fiecare fișier scris; o eroare se repară imediat.

## Testare
- Etapă gata = merge în editor pe Mac **și** în APK pe S21 Ultra.
- Ce se mișcă se verifică cu video de 20–40 s, nu doar cu poze. Captură la final în `Capturi/etapa-X.Y.png`.
- Teste automate (Test Runner) pentru bani, inventar, salvare, misiuni, urmărire, economie.
- Panoul de test (mod test): FPS, memorie, triunghiuri, draw calls, erori, „Trimite jurnalul”.

## Blocaje
- **Regula celor 2 încercări:** aceeași eroare după 2 reparații → mă opresc, scriu în `JURNAL.md` ce am încercat,
  propun 2–3 căi și întreb.
- Etapă peste 2 zile → se împarte (3.3a / 3.3b). Etapă încurcată → înapoi la `main`, reluată mai mică.
- Nicio improvizație: dacă ceva nu se potrivește cu planul, întreb înainte.
- Fiecare eroare în `ERORI.md` (cauza reală, reparația, cum se evită), reparată la cauză. O eroare care poate
  reapărea în altă parte devine regulă nouă aici.

## Fișierele de memorie
| Fișier | Când se scrie |
| --- | --- |
| `PLAN.md` | Bifă la fiecare etapă terminată |
| `JURNAL.md` | La finalul fiecărei sesiuni, obligatoriu (și dacă etapa nu e gata) |
| `DECIZII.md` | La fiecare decizie importantă |
| `CREDITS.md` | La fiecare resursă descărcată |
| `ERORI.md` | La fiecare eroare |
| `VERIFICARE.md` | La fiecare etapă terminată |
| `CONCEPT.md` | La etapa 0.19 |

## Lucrul fără Mac (cerut 08.10.2026)
- Utilizatorul lucrează doar prin Claude Code. Lucrez continuu, ca și cum Unity `6000.3.25f1` ar fi instalat pe Mac;
  verificarea o face GitHub Actions (compilare, teste, APK). Când instalează Unity pe Mac, mă anunță.
- Nu-i cer „merge” pentru fiecare pas tehnic; îi dau doar ce are de verificat el (APK-ul pe telefon, capturi).

## Stadiul
- Gata: 0.1 (numele), 0.2 (repo-urile vechi), 0.10 (fișierele de memorie).
- 0.3: Unity `6000.3.25f1` ales; instalarea pe Mac mai târziu (o anunță utilizatorul).
- Scrise pe ramura `etapa-0.5`, de verificat pe GitHub: 0.5 (configurarea proiectului prin cod), 0.6 (pachetele),
  0.7 (fișierele repo-ului), 0.11 (straturi, coliziuni, Localization), 0.12 (APK automat), 0.14 (verificarea rapidă),
  0.16 (scena de test cu cubul), 0.17 (panoul de test). Așteaptă secretele `UNITY_EMAIL` și `UNITY_PASSWORD`.
- Rămân pentru Mac: 0.4 (Blender), 0.8–0.9 (Claude Code + MCP pe Mac), 0.13 (cheia de semnare), 0.15 (cablu).
