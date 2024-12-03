
# StayHotel Projesi

StayHotel, otel oluşturma, rapor talep etme, otel yönetimi ve rapor oluşturma gibi işlevler sunan bir mikroservis tabanlı bir uygulamadır. Bu proje, **.NET Core 9**, **MSSQL**, **PostgreSQL**, ve **RabbitMQ** kullanılarak geliştirilmiştir.

## Proje Yapısı

Proje iki ana servisten oluşmaktadır:
1. **Hotel Servisi**: Otel yönetimi, iletişim bilgileri ekleme, otel oluşturma ve silme işlemlerini içerir.
2. **Rapor Servisi**: Rapor talebi oluşturur, raporlara erişim sağlar ve raporları yönetir.

Her iki servis de **RabbitMQ** üzerinden mesajlaşarak birbirleriyle asenkron olarak iletişim kurar. **Serilog** loglama için kullanılır.

---

## Kurulum

### Gereksinimler

- **.NET Core 9**: Projenin çalışabilmesi için .NET Core 9 gerekmektedir.
- **RabbitMQ**: Mesaj kuyruğu yönetimi için kullanılır.
- **PostgreSQL**: Rapor servisi için veritabanı olarak kullanılır.
- **MSSQL**: Hotel servisi için veritabanı olarak kullanılır.

### Adımlar

1. **CloudAMQP Üyeliği**:
    - [CloudAMQP](https://www.cloudamqp.com/) üzerinden ücretsiz bir hesap oluşturun.
    - Kullanıcı bilgilerinizi alıp aşağıdaki dosyalarda güncelleyin.

2. **appsettings.json Dosyası**:
    - **Hotel Servisi** ve **Rapor Servisi** için `appsettings.json` dosyalarında gerekli bilgileri aşağıdaki gibi güncelleyin:
    
#### HotelService appsettings.json

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "ConnectionStrings": {
        "HotelServiceDb": "Server=YOUR_SERVER_NAME; Database=HotelServiceDb; Integrated Security=True; Encrypt=False;"
    },
    "RabbitMqSettings": {
        "Host": "YOUR_RABBITMQ_HOST",
        "Port": 5672,
        "Username": "YOUR_RABBITMQ_USERNAME",
        "Password": "YOUR_RABBITMQ_PASSWORD",
        "VirtualHost": "/",
        "QueueName": "report-service-queue",
        "ReQueueName": "report-response"
    },
    "Serilog": {
        "MinimumLevel": {
            "Default": "Information",
            "Override": {
                "Microsoft": "Warning",
                "System": "Error"
            }
        },
        "WriteTo": [
            {
                "Name": "Console"
            },
            {
                "Name": "File",
                "Args": {
                    "path": "Logs/HotelService.log",
                    "rollingInterval": "Day"
                }
            }
        ]
    },
    "AllowedHosts": "*"
}
```

#### ReportService appsettings.json

```json
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "ConnectionStrings": {
        "ReportServiceDb": "Host=localhost;Port=5432;Database=ReportServiceDb;Username=postgres;Password=YOUR_POSTGRES_PASSWORD"
    },
    "RabbitMqSettings": {
        "Host": "YOUR_RABBITMQ_HOST",
        "Port": 5672,
        "Username": "YOUR_RABBITMQ_USERNAME",
        "Password": "YOUR_RABBITMQ_PASSWORD",
        "QueueName": "report-service-queue",
        "ReQueueName": "report-response"
    },
    "Serilog": {
        "MinimumLevel": {
            "Default": "Information",
            "Override": {
                "Microsoft": "Warning",
                "System": "Error"
            }
        },
        "WriteTo": [
            {
                "Name": "Console"
            },
            {
                "Name": "File",
                "Args": {
                    "path": "Logs/ReportService.log",
                    "rollingInterval": "Day"
                }
            }
        ]
    },
    "AllowedHosts": "*"
}
```

### Veritabanı

- **HotelServisi**: MSSQL veritabanı kullanır. Otel ve iletişim bilgilerini tutar.
- **ReportServisi**: PostgreSQL kullanır ve rapor bilgilerini saklar.

### Çalıştırma

Projeyi başlatmadan önce, gerekli bağımlılıkları yüklemek için şu komutları çalıştırın:

```bash
dotnet restore
```

Projenizi çalıştırmak için:

```bash
dotnet run
```

Her iki servis de başlatıldığında, gerekli bağlantılar sağlanacaktır.

---




## Loglama

Loglama için **Serilog** kullanılır ve loglar her iki serviste de belirtilen dosyaya kaydedilir. Örneğin, **Hotel Servisi** için loglar `Logs/HotelService.log` dosyasına kaydedilir.

---

## Lisans

Bu proje **MIT Lisansı** altında lisanslanmıştır.
