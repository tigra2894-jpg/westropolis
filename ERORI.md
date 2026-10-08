# Erori Westropolis

Fiecare eroare: ce s-a întâmplat, cauza reală, cum s-a reparat, cum se evită. Se citește la începutul fiecărei sesiuni.

## E1 (08.10.2026) – push-ul unei etichete Git refuzat din sesiunea cloud
- Ce: `git push origin <tag>` → „unexpected disconnect”; eticheta n-a ajuns pe GitHub.
- Cauza: proxy-ul sesiunii cloud acceptă doar ramuri, nu etichete.
- Reparat: nu a fost nevoie (istoria păstra commit-ul).
- Evitare: etichetele de fază (`v0.N-fazaN`) le pune utilizatorul din GitHub → Releases → „Create a new release”,
  sau Claude Code de pe Mac.

## E2 (08.10.2026) – push 403 după redenumirea repo-ului
- Ce: după `lawless-two-worlds` → `westropolis`, push-ul spre adresa nouă a dat 403.
- Cauza: accesul sesiunii era dat pe numele vechi al repo-ului.
- Reparat: repo-ul adăugat în sesiune sub numele nou (`add_repo`), clonat din nou.
- Evitare: după orice redenumire, sesiune nouă sau repo adăugat din nou sub numele nou.

## E3 (08.10.2026) – ștergerea unei ramuri și crearea unui repo refuzate din sesiunea cloud
- Ce: `git push --delete` pe o ramură și crearea unui repo prin GitHub → refuzate (403).
- Cauza: drepturile sesiunii cloud nu acoperă ștergerea ramurilor și crearea repo-urilor.
- Evitare: le face utilizatorul (butonul „Delete branch” din PR; github.com/new), cu pașii explicați.

## E4 (08.10.2026) – licența Unity pe GitHub: „Invalid Credential” (401)
- Ce: Unity `6000.3.25f1` s-a instalat (7 min), dar activarea licenței Personal a dat `Invalid Credential, 143.002 (401)`.
- Cauza: serverul Unity nu acceptă combinația din `UNITY_EMAIL` / `UNITY_PASSWORD` (greșeală de scriere, spațiu sau
  rând nou în plus, cont făcut cu Google / Apple fără parolă proprie, sau verificare în doi pași activă).
- Încercări (08.10.2026): secretele puse de 2 ori, apoi curățarea automată a spațiilor (pasul „Verifica secretele
  Unity”, fără avertismente: secretele nu aveau spații, e-mailul are forma corectă). Tot 401 → serverul Unity respinge
  chiar contul / parola (cont făcut cu Google / Apple fără parolă proprie, verificare în doi pași, alt e-mail).
- Regula celor 2 încercări: oprit; căile propuse utilizatorului: (A) parolă proprie Unity + fără verificarea în doi
  pași; (B) cont Unity nou, doar cu e-mail + parolă, pentru GitHub; (C) fișierul de licență de pe Mac (`UNITY_LICENSE`).
- Evitare: secretele se lipesc fără spații / rânduri în plus; contul Unity are parolă proprie, fără verificare în doi pași.
