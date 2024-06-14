<h1 style= font-size: 26px>GameDream API</h1>
<p>Gamedream es una API que te permite hacer operaciones con videojuegos. Puedes registrarte o iniciar sesión, depositar dinero, retirar dinero, comprar videojuegos y ver tus operaciones y videeojuegos comprados.</p>
<p>Los usuarios con un rol de admin podrán hacer pruebas con los videojuegos, como crearlos, actualizarlos y eliminarlos, así como ver la lista de todos los usuarios registrados y poder actualizar la información de cualquier usuario.</p>

<h2 style= font-size: 24px>Docker</h2>
<p>La API está dockerizada y la puedes levantar con docker-compose mediante el comando docker-compose up --build --force-recreate -d. En el docker-compose levantamos la Aplicación por el puerto 7018 y la base de datos SQL por el 8107 donde almacenaremos la información</p>
<p>Si quieres conseguir la aplicación por consola puedes descargarte la imagen mediante docker pull alber965/gamedream:latest</p>

<h2 style= font-size: 24px>Azure</h2>
<p>Además la aplicación está subida a la nube de Azure y puedes utilizarla en estos enlaces:</p>
<p><strong>-https://gamedreamapipro.azurewebsites.net/swagger/index.html</strong></p>
<p><strong>-https://gamedreamapi.azurewebsites.net/swagger/index.html</strong></p>

