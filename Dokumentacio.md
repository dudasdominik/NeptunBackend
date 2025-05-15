# Neptun Backend Dokumentáció

Ez a projekt egy egyszerű Neptun-rendszer backend implementációja ASP.NET Core technológiával, PostgreSQL adatbázissal és Docker konténerizálással.

---

## Tartalom

* [Követelmények](#követelmények)
* [Telepítés és futtatás](#telepítés-és-futtatás)
* [Főbb funkciók](#főbb-funkciók)
* [Authentikáció](#authentikáció)
* [Szerepkörök](#szerepkörök)
* [Adatmodell](#adatmodell)

---

## Követelmények

* [.NET 8 SDK](https://dotnet.microsoft.com/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [Visual Studio / Rider](https://visualstudio.microsoft.com/) (vagy bármilyen IDE)
* [PostgreSQL Docker image](https://hub.docker.com/_/postgres)

---

## Telepítés és futtatás

1. **.env beállítás**: Készíts egy `.env` fájlt a `.env.example` alapján.

2. **Konténer indítás**:

```bash
./init.bat
```

Ez elindítja a PostgreSQL-t a `docker-compose` alapján, és létrehozza a volume-ot is.

3. **Migrációk futtatása**:

```bash
dotnet ef database update
```

4. **Backend futtatása**:

```bash
dotnet run
```

Swagger elérhető a [http://localhost:5259/swagger](http://localhost:5259/swagger) címen.

---

## Főbb funkciók

* Hallgató, Oktató és Admin entitások CRUD
* Tantárgyak kezelése (Course)
* Vizsga kezelés (Exam)
* Vizsgára jelentkezés és jegy adás (ExamRegistration + Grade)
* JWT token alapú authentikáció

---

## Authentikáció

A rendszer JWT tokennel működik. Login után egy JWT token generálódik, mely tartalmazza a `NeptunCode`-ot és a `Role`-t.
Ezt a tokent a SwaggerUI jobb felső sarkában lévő zöld lakatra kattintva be kell másolni, hogy az authorizációval ellátott endpointok kipróbálása lehetséges legyen.

### Header beállítás Swaggerben:

```
Authorization: Bearer <token>
```

A token kulcsot az `.env`-ből olvassa:

```
Jwt__Key=szupertitkoskod
```

---

## Szerepkörök

```csharp
public enum Role
{
    Student,
    Teacher,
    Admin
}
```

A `Person` absztrakt osztály rendelkezik egy `Role` mezővel, amely meghatározza a felhasználó szerepét.

---

## Adatmodell

### 👨‍🎓 Person (abstract)

* NeptunCode (kulcs)
* Név, Email, Telefon, Cím
* Jelszó (hash-elve)
* Születési dátum
* Role

### 🎓 Student : Person

* State (aktív/inaktív)
* List<Course> Courses
* List<ExamRegistration> ExamRegistrations

### 🏫 Teacher : Person

* University, Department
* List<Course> TeacherCourses

### 👨‍💼 Administrator : Person

* Műveleteket végezhet (pl. admin felügyelet)

### 📖 Course

* Id, Name, Description, Credits, Semester
* Teacher (NeptunCode alapún kapcsolódik)
* List<Student>

### ✍️ Exam

* Id, Location, Date, Type, MaxScore
* Course kapcsolat
* List<ExamRegistration>

### 📅 ExamRegistration

* Id
* StudentNeptunCode, ExamId
* Grade (null, ha még nincs)

---

## Extra

* Adatok serializálásához a JSON ciklikus referenciákat `ReferenceHandler.Preserve` kezeli.
* Token generálása TokenService-ben történik.
* Vizsga pontozás csak tanár által, token alapán validálva.
* A jelszavak encyrptálva vannak tárolva az adatbázisban, amit a `Microsoft.PasswordHasher` által oldottunk meg.

---

Készítette: Dudás Dominik PR2UEQ, Bálint Tibor {neptunkód}
