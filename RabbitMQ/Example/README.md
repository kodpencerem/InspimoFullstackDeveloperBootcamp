# Gerekli kurulumlar

## PostgreSql Docker Kurulum Kodu
```
docker run -d --name postgres -e POSTGRES_PASSWORD=1 -e POSTGRES_DB=customerdb -p 5432:5432 postgres
```