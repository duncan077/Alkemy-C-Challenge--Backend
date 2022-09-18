
# Disney Finder

La forma mas facil de buscar personajes o peliculas de Disney.

Se utilizo AspCore, EntityFrameworkCore, AutoMapper, Identity, JWT Auth y
para diseñar la base de datos se uso Code First.

## Modelos
| Genero |
|:--:|
| Id : int |
| Nombre : string |
| Imagen : string |

| Personaje |
|:--:|
| Id : int |
| Nombre : string |
| Imagen : string |
| Edad : int |
| Peso : float |
| Historia : string |
| Peliculas : PeliculaSimpleDTO |

| Pelicula |
|:---:|
| Id : int |
| Titulo : string |
| Imagen : string |
| Clasificacion : int |
| FechaCreacion : DateTime |
| Generos : GeneroDTO |
| Personajes : PersonajeSimpleDTO |


## Environment Variables

To run this project, you will need to add the following environment variables to your .env file

`SendGridApiKey` SendGrid ApiKey

`ConnectionString` Database ConnectionString of a MariaDB.

