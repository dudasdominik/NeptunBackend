# Neptun Backend System

Ez a projekt a Neptun rendszer backend része, amely C# (.NET) nyelven íródott, PostgreSQL adatbázist használ, és Docker-konténerben fut.

## 🌐 Funkciók

* Hallgatók és oktatók kezelése
* Tárgyak kezelése
* Bejelentkezés / jelszó-módosítás
* EF Core-alapú migrációk és adattöltés
* Swagger UI a dokumentációhoz és API teszteléshez

---

## ⚡ Előfeltételek

Telepítve kell legyen a gépeden:

* [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Docker + Docker Compose)
* [Rider](https://www.jetbrains.com/rider/) vagy [Visual Studio 2022+](https://visualstudio.microsoft.com/)

---

## 📁 Projekt inicializálása

1. Készítsen egy `.env` fájlt a `.env.example` minta alapján.

   Tartalma kb. ilyesmi lesz:

   ```env
   PG_USER=root
   PG_PASSWORD=root
   PG_DATABASE=neptun
   PG_PORT=3254
   PG_HOST=localhost

   ConnectionStrings__DefaultConnection=Host=localhost;Port=3254;Database=neptun;Username=root;Password=root
   ```

2. Futtassa az `init.bat` scriptet.

   Ez:

   * Letörli a korábbi konténereket és volume-okat (ha vannak)
   * Elindítja a PostgreSQL konténert Dockerben
   * Végrehajtja az adatbázis migrációkat
   * Opcionálisan feltölti dummy adatokkal (populate.sql)

---

## ▶️ Az alkalmazás futtatása

1. Nyissa meg a projektet Riderben vagy Visual Studio-ban
2. Állítsa be az indító projektet (ha kell)
3. Indítsa el (F5 vagy Run)
4. A Swagger UI elérhető lesz a [http://localhost](http://localhost:PORT/swagger)[:PORT](http://localhost:PORT/swagger)[/swagger](http://localhost:PORT/swagger) címen (a PORT a projekted portja)

---

## 🛡️ .env biztonság

* A `.env` fájl NEM kerül be a verzókövetésbe (benne van a `.gitignore`-ban)
* A `.env.example` segít a paraméterek előkészítésében

---

## 🎓 Projekt felépítése

* `Models/` - EF Core entitás osztályok (Student, Teacher, Course)
* `Controllers/` - REST API kontrollerek
* `Data/` - DbContext, konfigurációk
* `Services/` - üzleti logika / jelszókezelés / beléptetés

---

## ✨ További tervek

* Token alapú hitelesítés
* Jogosultságkezelés
* Előrehaladás nyomon követése
* Frontend kapcsolódás (React)

---

Ha bármilyen hibába ütközik, nyugodtan nyisson issue-t vagy dobjon egy üzenetet. 🙂
