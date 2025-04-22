# GAME JAM ITB API

Este repositorio contiene un backend construido con una API REST y un frontend utilizando Razor Pages. El backend está compuesto por tres controladores:

- **AuthController**: Gestiona la autenticación y registro de usuarios.
- **GamesController**: Permite interactuar con los juegos (crear, editar, eliminar, votar).
- **UsersController**: Permite consultar y gestionar los perfiles de usuario.

## START

Para iniciar esta aplicacion debes buscar primero la solucion del proyecto, la cual se encuentra dentro de la carpeta Server y se llama **GameJamITB_SantiagoVergaraRodriguez.sln**
SE DEBE DE TENER UNA BASE DE DATOS SQL LLAMADA "GamesDatabase" o de lo contrario, modificar la ruta desde el appsettings.json

## 📚 Estructura de la Base de Datos

La base de datos se compone de tres entidades principales:

### 1. `Games`

Contiene la información esencial de cada videojuego:

- `Id`: Identificador único del juego.
- `Titulo`: Nombre del juego.
- `Descripcion`: Descripción del contenido o historia.
- `Desarrollador`: Nombre del estudio o creador.
- `Fecha_Lanzamiento`: Fecha de publicación.
- `Imagen`: URL o ruta de la imagen asociada.
- `Numero_Votos`: Total acumulado de votos (opcional, puede calcularse dinámicamente).

---

### 2. `ApplicationUser`

Clase que extiende `IdentityUser` de ASP.NET Core Identity, e incluye:

- Propiedades estándar de usuario (correo, contraseña, etc.).
- Campos personalizados como `NombreCompleto`, `Avatar`, etc.
- Asociaciones con las acciones del usuario (votos, favoritos, reviews futuras).

Esto permite autenticación segura y control de roles para funcionalidades exclusivas (como acceso a páginas de administrador).

---

### 3. `UserGameVotes` (Tabla intermedia)

Gestiona la relación muchos-a-muchos entre usuarios y juegos votados:

- `UserId`: Clave foránea a `ApplicationUser`.
- `GameId`: Clave foránea a `Game`.
- `ValorVoto`: Valor del voto emitido (por ejemplo, de 1 a 5 estrellas o like/dislike).

Esta tabla:

- Garantiza que un usuario solo pueda votar una vez por juego.
- Permite futuras ampliaciones como timestamps o comentarios.

## Estructura del Backend

### 1. **AuthController** (Autenticación y Registro)

Este controlador maneja la autenticación y los registros de usuarios y administradores.

- **POST `/api/auth/registre`**: Registra un nuevo usuario.
- **POST `/api/auth/admin/registre`**: Registra un nuevo usuario administrador.
- **POST `/api/auth/login`**: Inicia sesión y devuelve un token JWT.
- **GET `/api/auth/prova`**: Devuelve información básica del usuario autenticado.

### 2. **GamesController** (Gestion de juegos)
Este controlador permite crear, leer, actualizar, eliminar y votar juegos.

- **GET `/api/games`**: Obtiene todos los juegos de la base de datos.
- **GET `/api/games/{id}`**: Obtiene un juego específico por su ID.
- **GET `/api/games/search`**: Busca juegos por título.
- **POST `/api/games/insert`**: Crea un nuevo juego (solo admins).
- **PUT `/api/games/{id}`**: Actualiza un juego (solo admins).
- **DELETE `/api/games/{id}`**: Elimina un juego (solo admins).
- **POST `/api/games/vote/{id}`**: Permite a un usuario votar un juego.
- **POST `/api/games/unvote/{id}`**: Permite a un usuario desvotar un juego.

### 3. **UsersController** (Perfil de Usuario)
Este controlador permite consultar el perfil del usuario autenticado, incluyendo los juegos que ha votado.

- **GET `/api/users/perfil`**: Devuelve el perfil del usuario autenticado.

## Estructura Frontend

Disenyada con Razor Pages, su estructura basica cuenta con distintas paginas para una comoda navegacion por parte del usuario

### Index
Pagina donde se mostraran los primeros 10 juegos ordenados segun las votaciones de cada usuario

## ALL Games
PAgina donde se encuentra el total de los juegos disponibles en la GameJam ITB

### **(Sin autentificarse)**
## Signin
Pagina donde se podran registrar los usuarios corrientes
## Login
Pagina donde se podran logear tanto los usuarios corrientes como administradores, los roles se les asignan de acuerdo a como fueron registrados

### **(Autentificados)**
## Chat
Pagina que contiene un chat en tiempo real usando websockets y SignalR
## Perfil
Pagina donde se mostrara la informacion basica del usuario
## Logout
Pagina para poderse deslogear

Como extra, un administrador puede votar, desvotar y eliminar juegos de la base de datos
Un usuario corriente puede solo votar y desvotar los juegos

## 📚 Bibliografia

- **Amazon Web Services.** (n.d.). *What is RESTful API?* Recuperat el 21 d’abril de 2025 de [https://aws.amazon.com/es/what-is/restful-api/](https://aws.amazon.com/es/what-is/restful-api/)

- **Walther, S.** (n.d.). *Creating a Controller (C#)*. Microsoft Learn. Recuperat el 21 d’abril de 2025 de [https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/creating-a-controller-cs](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/creating-a-controller-cs)

- **Anderson, R., Brock, D., & Larkin, K.** (2024, 6 de novembre). *Introducción a Razor Pages en ASP.NET Core*. Microsoft Learn. Recuperat el 21 d’abril de 2025 de [https://learn.microsoft.com/es-es/aspnet/core/razor-pages/?view=aspnetcore-9.0&tabs=visual-studio](https://learn.microsoft.com/es-es/aspnet/core/razor-pages/?view=aspnetcore-9.0&tabs=visual-studio)

- **Refsnes Data.** (n.d.). *ASP.NET Web Pages - Examples in C# and VB*. W3Schools. Recuperat el 21 d’abril de 2025 de [https://www.w3schools.com/asp/webpages_examples.asp](https://www.w3schools.com/asp/webpages_examples.asp)

