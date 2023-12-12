## Consultas

### Listar Todos los empleados de la empresa de seguridad
```
http://localhost:5271/api/persona/GetAllEmployees
```

### Listar Todos los empleados que sean Vigilantes
```
http://localhost:5271/api/persona/GetWatchMen
```

### Listar Los numeros de contacto de un empleado que sea vigilante
```
http://localhost:5271/api/persona/GetWatchMenWithContact
```

### Listar Los Clientes que vivan en la ciudad de bucaramanga
```
http://localhost:5271/api/persona/GetClientFromBucaramanga
```

### Listar Los Empleados que vivan en la ciudad de giron y pidecuesta
```
http://localhost:5271/api/persona/GetEmployeeFromPidecuestaOrGiron
```

### Listar Los Clientes que tengan mas de 5 años de antiguedad
```
http://localhost:5271/api/persona/GetClients5YearOlder
```
### Listar Todos los contratos cuyo estado es activo. se debe mostrar el nro de contrato, el nombre del cliente y el empleado que registro elc contrato
```
http://localhost:5271/api/persona/GetContratsWithClientAndEmployee
```