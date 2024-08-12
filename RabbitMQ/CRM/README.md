# Gerekli kurumlar

## RabbitMQ Docker kurulum kodu
```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.13-management
```

- Eðer kullanýcý adý þifre vermezsek o zaman
Kullanýcý Adý: guest
Þifre: guest