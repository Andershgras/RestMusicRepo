# RestMusic API – Backend

## Overview

RestMusic API er en ASP.NET Core Web API, der håndterer CRUD-operationer for musikrecords.
API’et er deployet på Azure og anvender en Azure SQL Database til persistence.

---

## Arkitekturvalg

### Lagopdeling (Separation of Concerns)

Projektet er opdelt i tre hoveddele:

* **RestMusic.Api**

  * Indeholder controllers og konfiguration
  * Håndterer HTTP requests/responses
  * Konfigurerer JWT authentication og dependency injection

* **RestMusic.Domain**

  * Indeholder modeller (`MusicRecord`)
  * Indeholder repository interfaces og implementeringer
  * Indeholder forretningslogik

* **RestMusic.Test**

  * Indeholder unit tests (xUnit)
  * Tester repository logik isoleret

Dette sikrer, at ansvar er opdelt og gør systemet lettere at vedligeholde og teste.

---

### Repository Pattern

Vi bruger Repository Pattern til at abstrahere dataadgang:

* `IMusicRepoList` → interface
* `MusicRepoDb` → database implementation (EF Core)
* `MusicRepoList` → in-memory implementation (til test og udvikling)

Fordele:

* Let at skifte mellem database og in-memory
* Lettere at teste (mocking / isolation)
* Mindre afhængighed til database i resten af systemet

---

### Dependency Injection

Repositories injiceres via DI i `Program.cs`:

```csharp
builder.Services.AddScoped<IMusicRepoList, MusicRepoDb>();
```

Gør systemet fleksibelt og testbart.

---

### Database & Persistence

* **Entity Framework Core** bruges som ORM
* **Azure SQL Database** bruges i produktion
* **InMemory Database** bruges i tests

EF Core gør det nemt at håndtere data uden manuel SQL.

---

### Authentication & Authorization

* JWT (JSON Web Tokens) bruges til login
* Roller (Admin / User) er implementeret via claims

Eksempel:

```csharp
[Authorize(Roles = "Admin")]
```

Kun admins kan:

* Oprette records
* Opdatere records
* Slette records

---

### Validation

Backend bruger Data Annotations:

* `[Required]`
* `[Range]`
* `[StringLength]`

Sikrer at ugyldig data ikke gemmes i databasen.

---

### API Design

RESTful endpoints:

* `GET /api/records`
* `GET /api/records/{id}`
* `POST /api/records`
* `PUT /api/records/{id}`
* `DELETE /api/records/{id}`

Følger standard REST principper.

---

### Testing

* xUnit bruges til unit tests
* EF Core InMemory bruges til database tests

Tester repository logik uden rigtig database.

---

## Deployment

* Backend er deployet på **Azure App Service**
* Database er hostet på **Azure SQL**

---

## Konklusion

Arkitekturen er valgt med fokus på:

* Simpel struktur
* Testbarhed
* Udvidelsesmuligheder
* Klar separation af ansvar

Systemet er designet som en funktionel prototype med realistiske backend-principper.
