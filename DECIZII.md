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
