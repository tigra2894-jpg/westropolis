# Jurnal Westropolis

Ce s-a făcut, ce e în lucru, probleme cunoscute, următorul pas. Se scrie la finalul fiecărei sesiuni.

## 08.10.2026 – pornirea proiectului
- Planul inițial al utilizatorului („Lawless: Two Worlds”) analizat și refăcut ca ghid complet (`PLAN.md`): repo
  public + privat, fără LFS, APK în Releases, ramură + Pull Request pe etapă, Blender, pachete noi, bugete de
  performanță, reguli de licență, etape noi de joc. 111 etape.
- Repo-urile vechi (`Ardeal-City`, `Orasul`) șterse complet la cererea utilizatorului; repo nou, public și curat,
  creat de utilizator, apoi redenumit `westropolis`.
- Etapa 0.1: numele **Westropolis** (pachet `com.tigra2805.westropolis`); PR #1 unit în `main`.
- Etapa 0.3: versiunea aleasă **Unity 6.3 LTS `6000.3.25f1`** (cel mai nou patch 6.3 cu imagine GameCI Android,
  24.09.2026). Instalarea pe Mac: mai târziu, utilizatorul anunță.
- Etapa 0.10 (cu acordul utilizatorului, înaintea lui 0.4–0.9): `CLAUDE.md` complet, `JURNAL.md`, `DECIZII.md`,
  `CREDITS.md`, `ERORI.md`, `VERIFICARE.md`.
- Probleme cunoscute: ramura `etapa-0.1` n-a putut fi ștearsă din sesiune (o șterge utilizatorul din PR #1).
- **Următorul pas:** „merge” pentru 0.10; apoi 0.3 când utilizatorul e la Mac (sau 0.19, conceptul, dacă vrea).

## 08.10.2026 – proiectul Unity fără Mac (ramura `etapa-0.5`)
- `ProjectSettings/ProjectVersion.txt`: `6000.3.25f1 (e1dba0a9aba4)`; `Packages/manifest.json` cu versiunile fixe.
- `Configurare.cs`: nume, pachet, IL2CPP + ARM64, Vulkan + GLES3, orizontală, Input System, Force Text, URP,
  straturi + coliziuni, Localization (en, ro, tabelul „Interfata”, limba salvată). Se aplică singur la prima
  deschidere pe Mac.
- `Construire.cs`: scena de test (podea + cub care se rotește), APK Development cu numărul versiunii din GitHub.
- `SetariGrafice.cs` (30 FPS + Render Scale 0,7 pe telefon), panoul de test (`PanouTest`, `JurnalErori`, `ModTest`).
- Teste EditMode (`TesteBaza`): pachet, IL2CPP/ARM64, straturi, formatarea din panou.
- Workflow-uri: `verificare.yml` (compilare + teste), `apk-android.yml` (APK în Releases → „test”), acțiunea comună
  `pregateste-unity`. `.gitignore` (cu liniile de siguranță), `.gitattributes`, `README.md`.
- Necompilat încă: aici nu există Unity; prima verificare reală e pe GitHub, după secretele `UNITY_EMAIL` și `UNITY_PASSWORD`.
- **Următorul pas:** utilizatorul pune cele 2 secrete → pornesc verificarea → repar ce iese → APK-ul pe telefon.
