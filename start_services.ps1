# Запуск Zookeeper и Kafka
docker-compose up -d zookeeper kafka

# Ожидание запуска Zookeeper и Kafka
Start-Sleep -Seconds 10

# Запуск первого продюсера в фоновом режиме
docker-compose up -d mixing-components-producer

# Запуск консюмера в фоновом режиме
docker-compose up -d consumer

# Открытие нового окна PowerShell для просмотра логов консюмера и первого продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f mixing-components-producer consumer"

# Время работы первого продюсера 
Start-Sleep -Seconds 20

# Останавливаем первый продюсер
docker-compose stop mixing-components-producer

# Время переключения на второго продюсера
Start-Sleep -Seconds 10

# Запуск второго продюсера в фоновом режиме
docker-compose up -d molding_and_initial_exposure_producer

# Открытие нового окна PowerShell для просмотра логов консюмера и первого продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f molding_and_initial_exposure_producer consumer"

# Время работы второго продюсера
Start-Sleep -Seconds 20

# Останавливаем второго продюсера
docker-compose stop molding_and_initial_exposure_producer

# Остановка всех контейнеров
docker-compose down