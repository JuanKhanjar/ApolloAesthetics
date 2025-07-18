# Apollo Aesthetics - Deployment Guide

## 🚀 Production Deployment

### Prerequisites
- Windows Server 2019+ or Linux Server
- IIS (Windows) or Nginx/Apache (Linux)
- SQL Server 2019+ or PostgreSQL
- .NET 8.0 Runtime

### Database Setup

#### SQL Server Configuration
1. **Create Database**
   ```sql
   CREATE DATABASE ApolloAesthetics;
   ```

2. **Update Connection String**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=ApolloAesthetics;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
     }
   }
   ```

3. **Run Migrations**
   ```bash
   dotnet ef database update --project src/ApolloAesthetics.Infrastructure --startup-project src/ApolloAesthetics.Web
   ```

### Application Configuration

#### appsettings.Production.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-Production-Connection-String"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "your-domain.com",
  "Identity": {
    "RequireConfirmedEmail": true,
    "RequireConfirmedPhoneNumber": false,
    "LockoutTimeSpan": "00:30:00",
    "MaxFailedAccessAttempts": 5
  },
  "EmailSettings": {
    "SmtpServer": "your-smtp-server",
    "SmtpPort": 587,
    "SmtpUsername": "your-email@domain.com",
    "SmtpPassword": "your-password",
    "FromEmail": "noreply@apolloaesthetics.com",
    "FromName": "Apollo Aesthetics"
  }
}
```

### IIS Deployment (Windows)

1. **Install Prerequisites**
   - .NET 8.0 Hosting Bundle
   - URL Rewrite Module
   - Application Request Routing (ARR)

2. **Publish Application**
   ```bash
   dotnet publish src/ApolloAesthetics.Web -c Release -o ./publish
   ```

3. **Create IIS Site**
   - Create new site in IIS Manager
   - Point to published folder
   - Set application pool to "No Managed Code"
   - Enable 32-bit applications if needed

4. **Configure SSL**
   - Install SSL certificate
   - Bind HTTPS to port 443
   - Redirect HTTP to HTTPS

### Linux Deployment (Ubuntu/CentOS)

1. **Install .NET Runtime**
   ```bash
   wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   sudo dpkg -i packages-microsoft-prod.deb
   sudo apt-get update
   sudo apt-get install -y aspnetcore-runtime-8.0
   ```

2. **Create Service User**
   ```bash
   sudo useradd -r -s /bin/false apolloaesthetics
   ```

3. **Deploy Application**
   ```bash
   sudo mkdir /var/www/apolloaesthetics
   sudo chown apolloaesthetics:apolloaesthetics /var/www/apolloaesthetics
   # Copy published files to /var/www/apolloaesthetics
   ```

4. **Create Systemd Service**
   ```bash
   sudo nano /etc/systemd/system/apolloaesthetics.service
   ```

   ```ini
   [Unit]
   Description=Apollo Aesthetics Web Application
   After=network.target

   [Service]
   Type=notify
   User=apolloaesthetics
   WorkingDirectory=/var/www/apolloaesthetics
   ExecStart=/usr/bin/dotnet /var/www/apolloaesthetics/ApolloAesthetics.Web.dll
   Restart=always
   RestartSec=10
   KillSignal=SIGINT
   SyslogIdentifier=apolloaesthetics
   Environment=ASPNETCORE_ENVIRONMENT=Production
   Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

   [Install]
   WantedBy=multi-user.target
   ```

5. **Configure Nginx**
   ```nginx
   server {
       listen 80;
       server_name your-domain.com;
       return 301 https://$server_name$request_uri;
   }

   server {
       listen 443 ssl http2;
       server_name your-domain.com;

       ssl_certificate /path/to/certificate.crt;
       ssl_certificate_key /path/to/private.key;

       location / {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_set_header X-Real-IP $remote_addr;
           proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
           proxy_set_header X-Forwarded-Proto $scheme;
           proxy_cache_bypass $http_upgrade;
       }
   }
   ```

### Docker Deployment

#### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ApolloAesthetics.Web/ApolloAesthetics.Web.csproj", "src/ApolloAesthetics.Web/"]
COPY ["src/ApolloAesthetics.Application/ApolloAesthetics.Application.csproj", "src/ApolloAesthetics.Application/"]
COPY ["src/ApolloAesthetics.Domain/ApolloAesthetics.Domain.csproj", "src/ApolloAesthetics.Domain/"]
COPY ["src/ApolloAesthetics.Infrastructure/ApolloAesthetics.Infrastructure.csproj", "src/ApolloAesthetics.Infrastructure/"]
RUN dotnet restore "src/ApolloAesthetics.Web/ApolloAesthetics.Web.csproj"
COPY . .
WORKDIR "/src/src/ApolloAesthetics.Web"
RUN dotnet build "ApolloAesthetics.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApolloAesthetics.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApolloAesthetics.Web.dll"]
```

#### docker-compose.yml
```yaml
version: '3.8'

services:
  apolloaesthetics:
    build: .
    ports:
      - "80:80"
      - "443:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=db;Database=ApolloAesthetics;User=sa;Password=YourPassword123!;TrustServerCertificate=true
    depends_on:
      - db
    volumes:
      - ./uploads:/app/wwwroot/uploads

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword123!
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

### Security Considerations

1. **HTTPS Configuration**
   - Always use HTTPS in production
   - Configure HSTS headers
   - Use strong SSL/TLS settings

2. **Database Security**
   - Use strong passwords
   - Enable encryption at rest
   - Configure firewall rules
   - Regular backups

3. **Application Security**
   - Keep .NET runtime updated
   - Configure security headers
   - Enable request validation
   - Use secure session configuration

4. **Monitoring & Logging**
   - Configure application insights
   - Set up health checks
   - Monitor performance metrics
   - Configure log aggregation

### Performance Optimization

1. **Caching**
   - Enable response caching
   - Configure Redis for distributed caching
   - Implement output caching

2. **Database Optimization**
   - Add proper indexes
   - Optimize queries
   - Configure connection pooling

3. **Static Files**
   - Use CDN for static assets
   - Enable compression
   - Configure browser caching

### Backup Strategy

1. **Database Backups**
   ```sql
   BACKUP DATABASE ApolloAesthetics 
   TO DISK = 'C:\Backups\ApolloAesthetics_Full.bak'
   WITH FORMAT, INIT;
   ```

2. **Application Files**
   - Backup uploaded files
   - Backup configuration files
   - Version control for code

### Monitoring & Maintenance

1. **Health Checks**
   - Database connectivity
   - External service availability
   - Application performance

2. **Log Monitoring**
   - Error tracking
   - Performance monitoring
   - Security event logging

3. **Regular Updates**
   - Security patches
   - Framework updates
   - Dependency updates

### Troubleshooting

#### Common Issues

1. **Database Connection Issues**
   - Check connection string
   - Verify SQL Server service
   - Check firewall settings

2. **Authentication Problems**
   - Verify Identity configuration
   - Check cookie settings
   - Validate SSL certificate

3. **Performance Issues**
   - Monitor database queries
   - Check memory usage
   - Analyze request patterns

#### Logs Location
- **Windows**: `C:\inetpub\logs\LogFiles`
- **Linux**: `/var/log/apolloaesthetics`
- **Docker**: `docker logs container-name`

### Support & Maintenance

For production support:
- Monitor application health
- Regular security updates
- Performance optimization
- Database maintenance
- Backup verification

---

**Note**: Always test deployment procedures in a staging environment before applying to production.

