# Control de organigramas

_Este proyecto permite subir organigramas y administrar los archivos que suben los usuarios + control de cuentas de usuarios y roles._


## Comenzando 🚀

_Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas._


### Pre-requisitos 📋

_Que cosas necesitas para instalar el software y como instalarlas_

```
Habilitar IIS y los componentes requeridos de IIS en Windows (opcional)
```


### Instalación 🔧

_Cambia la cadena de conexión de la base de datos en el archivo Web.config_

```
<add name="DefaultConnection" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-EstadiasUTTN-20201126063537.mdf;Initial Catalog=aspnet-EstadiasUTTN-20201126063537;Integrated Security=True" providerName="System.Data.SqlClient" />
<add name="EstadiasUTTNEntities" connectionString="metadata=res://*/Models.DB.csdl|res://*/Models.DB.ssdl|res://*/Models.DB.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDb)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\aspnet-EstadiasUTTN-20201126063537.mdf;initial catalog=aspnet-EstadiasUTTN-20201126063537;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
```

_Cambia las credenciales de acceso para la cuenta que envia correos en el archivo Web.config (tiene que ser Gmail, permitir acceso a apps menos seguras en el apartado de seguridad de la cuenta y en el correo en el apartado de Reenvío y correo POP/IMAP habilitar el acceso IMAP_
_Introducir credenciales en las siguientes lineas de codigo:_
```
<!--Credenciales de acceso para correo electrónico.-->
<add key="Email" value="Aquí va el correo xd" />
<add key="Password" value="Aquí va la contraseña" />
```

_Ejecuta el script sql que se encuentra en la carpeta "Base de datos"_

_Para instalar este proyecto inicia Visual Studio como administrador y publica el proyecto en la ruta C:inetpub\wwwroot_

_Tambien puedes ejecutarlo con IIS Express desde Visual Studio o publicar los archivos del proyecto en un servidor web_


## Ejecutando el proyecto por primera vez ⚙️

_Al ejecutar el proyecto por primera vez, deberas registrar una cuenta con un correo electronico real, pues se te enviara un correo de confirmación y esa cuenta tendra el rol de administrador por defecto, para los siguientes registros el administrador debera de otorgar un rol_


## Construido con 🛠️

_Menciona las herramientas que utilizaste para crear tu proyecto_

* [ASP.NET MVC](https://dotnet.microsoft.com/apps/aspnet/mvc) - El framework web usado
* [Microsoft Visual Studio](https://visualstudio.microsoft.com/es/) - Entorno de desarrollo


## Autor ✒️

_Menciona a todos aquellos que ayudaron a levantar el proyecto desde sus inicios_

* **Homero Francisco Resendiz Garcia** - *Trabajo Inicial* - [homeroresendizg](https://github.com/homeroresendizg)


---
⌨️ con ☕ por [homeroresendizg](https://github.com/homeroresendizg) 😊
