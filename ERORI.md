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
