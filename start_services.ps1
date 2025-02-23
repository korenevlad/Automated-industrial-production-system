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

# Остановка первого продюсера
docker-compose stop mixing-components-producer

# Время переключения на второй продюсер
Start-Sleep -Seconds 5

# Запуск второго продюсера в фоновом режиме
docker-compose up -d molding_and_initial_exposure_producer

# Открытие нового окна PowerShell для просмотра логов консюмера и второго продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f molding_and_initial_exposure_producer consumer"

# Время работы второго продюсера
Start-Sleep -Seconds 20

# Остановка второго продюсера
docker-compose stop molding_and_initial_exposure_producer

# Время переключения на третий продюсер
Start-Sleep -Seconds 5

# Запуск третьего продюсера в фоновом режиме
docker-compose up -d autoclaving-producer

# Открытие нового окна PowerShell для просмотра логов консюмера и третьего продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f autoclaving-producer consumer"

# Время работы третьего продюсера
Start-Sleep -Seconds 20

# Остановка третьего продюсера и консюмера
docker-compose stop autoclaving-producer consumer

# Остановка всех контейнеров
docker-compose down