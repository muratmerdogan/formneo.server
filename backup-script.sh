#!/bin/bash
# Veritabanı yedeği alma scripti

echo "Veritabanı yedeği alınıyor..."

# Container çalışıyor mu kontrol et
if docker ps --format "table {{.Names}}" | grep -q formneo-postgres; then
    echo "PostgreSQL container çalışıyor, yedek alınıyor..."

    # Yedek al
    docker exec formneo-postgres pg_dump -U 74268c194be9 -d GTlLeHqTYbNT > backup_$(date +%Y%m%d_%H%M%S).sql

    echo "Yedek başarıyla alındı: backup_$(date +%Y%m%d_%H%M%S).sql"
else
    echo "PostgreSQL container çalışmıyor. Önce 'docker-compose up -d postgres' çalıştırın."
fi
