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

_Cambie las credenciales de acceso para la cuenta que envía correos electrónicos en el archivo Web.config (debe ser Gmail, permitE el acceso a aplicaciones menos seguras en la sección de seguridad de la cuenta y en el correo en la sección Reenvío y correo POP/IMAP, habilita el acceso IMAP_

_Introducir credenciales en las siguientes lineas de codigo:_
```
<!--Credenciales de acceso para correo electrónico.-->
<add key="Email" value="Aquí va el correo" />
<add key="Password" value="Aquí va la contraseña" />
```

_Ejecuta el script sql que se encuentra en la carpeta "Base de datos" (MSSQL)_

_Para instalar este proyecto inicia Visual Studio como administrador y publica el proyecto en la ruta C:inetpub\wwwroot_

_Tambien puedes ejecutarlo con IIS Express desde Visual Studio o publicar los archivos del proyecto en un servidor web_


## Ejecutando el proyecto por primera vez ⚙️

_Al ejecutar el proyecto por primera vez, debe registrar una cuenta con una dirección de correo electrónico real, ya que se le enviará un correo electrónico de confirmación y esa cuenta tendrá el rol de administrador predeterminado, para los siguientes registros el administrador debe otorgar un rol_


## Construido con 🛠️

* [ASP.NET MVC](https://dotnet.microsoft.com/apps/aspnet/mvc) - El framework web usado
* [Microsoft Visual Studio](https://visualstudio.microsoft.com/es/) - Entorno de desarrollo


## Autor ✒️

_Menciona a todos aquellos que ayudaron a levantar el proyecto desde sus inicios_

* **Homero Francisco Resendiz Garcia** - *Trabajo Inicial* - [homeroresendizg](https://github.com/homeroresendizg)


---
⌨️ con ☕ por [homeroresendizg](https://github.com/homeroresendizg) 😊
