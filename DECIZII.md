# Decizii Westropolis

Fiecare decizie importantă și de ce s-a luat. Deciziile din `PLAN.md` cap. 9 le ia utilizatorul.

| Data | Decizie | De ce |
| --- | --- | --- |
| 08.10.2026 | Proiect nou de la zero; nimic din Ardeal City / Orasul nu se păstrează (repo-urile șterse complet) | Cererea utilizatorului |
| 08.10.2026 | Unity + URP, Android (S21 Ultra) + macOS, construit pe Mac M2 cu Claude Code | Planul utilizatorului |
| 08.10.2026 | Repo **public** `westropolis` + repo **privat** `westropolis-privat` | Public: minute Actions nelimitate. Privat: Mixamo și Asset Store nu au voie să fie redistribuite |
| 08.10.2026 | Fără Git LFS; fișiere sub 50 MB | LFS are ~1 GB trafic/lună și la repo public; fiecare construire l-ar consuma |
| 08.10.2026 | APK-ul în GitHub Releases („test”), nu ca artefact | Se descarcă ușor de pe telefon; artefactele expiră și sunt arhivate |
| 08.10.2026 | O ramură pe etapă, unire în `main` prin Pull Request | `main` conține doar etape verificate |
| 08.10.2026 | Numele **Westropolis**, pachet `com.tigra2805.westropolis` | „Lawless” e folosit de multe jocuri, „Two Worlds” e seria TopWare; „Westropolis” = West + Metropolis, niciun joc cu acest nume găsit pe web. Rezervă: „Duskline” |
| 08.10.2026 | Unity **6.3 LTS `6000.3.25f1`**, fix pe tot proiectul | 6.0 LTS iese din suport în oct. 2026, 6.6 nu e LTS, 6.7 LTS nu a apărut; 25f1 e cel mai nou patch 6.3 cu imagine GameCI Android (același Unity pe Mac și pe GitHub). O eventuală trecere la 6.7 LTS doar cu acordul utilizatorului |
| 08.10.2026 | Etapa 0.10 făcută înaintea etapelor 0.4–0.9 | Utilizatorul nu e la Mac; 0.10 nu depinde de Unity. Acordul lui: „fă doar că Unity pe Mac îl voi instala mai târziu” |
| 08.10.2026 | Lucru continuu fără Mac: proiectul Unity scris ca și cum Unity ar fi instalat, verificat pe GitHub Actions | Cererea utilizatorului („continuă să lucrezi … când îl voi instala te anunț”) |
| 08.10.2026 | Setările proiectului (jucător, Android, URP, straturi, Localization) aplicate prin cod (`Configurare.cs`), nu scrise de mână în ProjectSettings | Aceleași setări pe Mac și pe GitHub, fără fișiere YAML scrise orbește; se pot reaplica oricând |
| 08.10.2026 | Toate pachetele din tabelul 7.1 adăugate deodată (nu câte unul), verificate într-o singură construire | Fără Mac, „câte unul” ar însemna 13 construiri; dacă apare o eroare, pachetul vinovat se găsește scoțându-le pe rând |
| 08.10.2026 | Versiunile pachetelor: URP 17.3.0, Test Framework 1.6.0, uGUI 2.0.0 (incluse în Unity 6.3); Input System 1.20.1, Cinemachine 3.1.7, Timeline 1.8.13, Localization 1.5.13, AI Navigation 2.0.16, ProBuilder 6.1.2, Animation Rigging 1.4.1, Splines 2.9.1, Terrain Tools 5.3.3, glTFast 6.20.0, Addressables 4.1.1, Recorder 5.1.7, Memory Profiler 1.1.12 | Cele mai noi versiuni stabile compatibile cu Unity 6000.3 din registrul Unity (08.10.2026) |
| 08.10.2026 | Starter Assets nu se folosește; mișcarea se scrie de la zero la 1.2 | Pachetul vine din Asset Store (nu poate sta în repo-ul public) și e scris pentru versiuni mai vechi de Cinemachine |
| 08.10.2026 | Licența Unity pe GitHub: activare Personal cu contul (`UNITY_EMAIL`, `UNITY_PASSWORD`), fără fișier `.ulf` | Merge fără Mac; metoda a funcționat deja în proiectul vechi |
| 08.10.2026 | Mod test = Development Build (`Debug.isDebugBuild`) | Un singur comutator: APK-urile de test au panoul, versiunea publică nu |
| 08.10.2026 | Textele panoului de test nu trec prin Localization | Sunt tehnice, pentru dezvoltator, și nu apar în versiunea publică |
| 08.10.2026 | Render Scale 0,7 și 30 FPS pe telefon, aplicate la pornire (`SetariGrafice.cs`); 60 FPS și scara 1 pe Mac | Bugetul de performanță; valorile finale la 3.7 |
| 08.10.2026 | Straturi de la indexul 8: Player, Vehicul, Cal, NPC, Teren, Cladiri, Apa, Interactiune, Proiectil; Interactiune atinge doar Player; Proiectil nu atinge Interactiune și Proiectil | Fundația 0.11 |
| 08.10.2026 | MacBook M2 cu 8 GB RAM: Mac-ul doar pentru editare și verificare; APK, teste și coacerea luminii pe GitHub Actions | Cu 8 GB, Unity + Android + IL2CPP umplu memoria și Mac-ul se blochează în swap |
