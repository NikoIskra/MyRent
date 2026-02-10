# Object browser

Web aplikacija koja koristi dostupne API metode i prikazuje podatke o objektima

## Lokalno pokretanje

Potrebno je dodati vrijednosti za token i guid u MyRent/appsettings.json i MyRent/appsettings.Development.json ovisno o environmentu.

```bash
"MyRentSettings": {
    "BaseUrl": "https://apit.my-rent.net/",
    "Guid": "xxxxx", <-- Zamijeniti xxxxx sa vrijednosti guid headera
    "Token": "yyyyy" <-- Zamijeniti yyyyy sa vrijednosti token headera
  },
```
