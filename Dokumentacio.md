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

## Inicializáció
```bash
docker rm -f %CONTAINER_NAME% >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Removed existing container: %CONTAINER_NAME%
) else (
    echo No existing container to remove.
)

docker volume rm %VOLUME_NAME% >nul 2>&1
if %ERRORLEVEL% EQU 0 (
    echo Removed volume: %VOLUME_NAME%
) else (
    echo No volume to remove or already gone.
)
```
Letörli az előző container-t és volume-ot, hogy az újra inditáskor üres legyen az adatbázis

```bash
dotnet ef database update
   IF %ERRORLEVEL% NEQ 0 (
     echo ❌ EF Core migration failed.
     pause
     exit /b 1
 )
```
Elinditja az EntityFramework Core által készített legutolsó migrációt, és feltölti az adatbázist a megfelelő táblákkal.
## Authentikáció

A rendszer JWT tokennel működik. Login után egy JWT token generálódik, mely tartalmazza a `NeptunCode`-ot és a `Role`-t.
Ezt a tokent a SwaggerUI jobb felső sarkában lévő zöld lakatra kattintva be kell másolni, hogy az authorizációval ellátott endpointok kipróbálása lehetséges legyen.
```csharp
public class TokenService
{
    public string GenerateToken(Person user)
    {
        var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key");
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new Exception("JWT key is not set in the environment variables.");
        }
        var key = Encoding.UTF8.GetBytes(jwtKey);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.NeptunCode),
            new Claim(ClaimTypes.Role, user.GetType().Name)
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
```

```csharp
 var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key");
```
Ezzel a sorral beolvassa az .env file-ból a JWT tokent

```csharp
  var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.NeptunCode),
            new Claim(ClaimTypes.Role, user.GetType().Name)
        };
```
Ez a claims változó állitja ba a Role-okat amik a NeptunCode-ot és a Role-t tartalmazza.

```csharp
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

```

## Program.cs

```csharp
DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

builder.Services.AddDbContext<NeptunDbContext>(options =>
    options.UseNpgsql(connectionString));

```
Betölti az adatbázis connectionStringjét a .env fájlból és rácsatlakozik a docker által létrehozott PostreSQL-re.


```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IExamService, ExamService>();
builder.Services.AddTransient<IExamRegistrationService, ExamRegistrationService>();
builder.Services.AddTransient<NeptunService>();
builder.Services.AddScoped<TokenService>();
```
Ezek hozzáadják a Service-eket Transientként ami azt jelenti, hogy minden API hiváskor egy új instance jön létre. Az Scoped pedig csak egyetlen egy instance jön létre.
### Header beállítás Swaggerben:

```
Authorization: Bearer <token>
```

A token kulcsot az `.env`-ből olvassa:

```
Jwt__Key=szupertitkoskod
```


## Controllerek

```csharp
[AllowAnonymous]
  [HttpPost("login")]
  public async Task<IActionResult> LogIn([FromBody] LoginDetailsDTO loginDetails)
  {
    try
    {
      var token = await _studentService.LogIn(loginDetails.NeptunCode, loginDetails.Password);
      if (token == null)
      {
        return BadRequest("Failed to login");
      }
      return Ok(new {token});
    }
    catch (Exception ex)
    {
      return Unauthorized(new {message = ex.Message});
    }
  }
```
```
[AllowAnonymous]
```
Ez az Attribute beenged jelentkezni bárkit, attól függetlenül, hogy van JWT Tokenje vagy nincs, mivel a belépéskor generálja le. Ehhez a Controllerhez való StudentService a következő.

```csharp

    public async Task<string> LogIn(string neptunCode, string password)
    {
        var foundStudent = await GetStudentByNeptunCode(neptunCode);
        if (foundStudent == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        IPasswordHasher<Student> passwordHasher = new PasswordHasher<Student>();
        var result = passwordHasher.VerifyHashedPassword(foundStudent, foundStudent.Password, password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception($"Password is incorrect");
        }
        var token = _tokenService.GenerateToken(foundStudent);
        return token;
    }
```
Megkeresi, hogy létetik-e ilyen Neptuncode-al rendelkező Student és, ha igen a PasswordHasher interface segítségével megnézi, hogy azonos az adott jelszó az adatbázisban tárolt jelszóhoz képest, majd legenerálja a tokent és hozzáadja a Studenthez.

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
