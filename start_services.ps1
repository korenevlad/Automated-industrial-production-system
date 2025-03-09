# Запуск Zookeeper и Kafka
docker-compose up -d zookeeper kafka

# Ожидание запуска Zookeeper и Kafka
Start-Sleep -Seconds 5

# Запуск консюмера в фоновом режиме
docker-compose up -d consumer

# Запуск первого продюсера в фоновом режиме
docker-compose up -d mixing-components-producer

# Открытие нового окна PowerShell для просмотра логов консюмера и первого продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f mixing-components-producer consumer"

# Время работы первого продюсера 
Start-Sleep -Seconds 30

# Время переключения на второй продюсер
Start-Sleep -Seconds 5

# Запуск второго продюсера в фоновом режиме
docker-compose up -d molding-and-initial-exposure-producer

# Открытие нового окна PowerShell для просмотра логов консюмера и второго продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f molding-and-initial-exposure-producer consumer"

# Время работы второго продюсера
Start-Sleep -Seconds 30

# Время переключения на третий продюсер
Start-Sleep -Seconds 5

# Запуск третьего продюсера в фоновом режиме
docker-compose up -d cutting-array-producer

# Открытие нового окна PowerShell для просмотра логов консюмера и третьего продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f cutting-array-producer consumer"

# Время работы третьего продюсера
Start-Sleep -Seconds 30

# Время переключения на третий продюсер
Start-Sleep -Seconds 5

# Запуск четвёртого продюсера в фоновом режиме
docker-compose up -d autoclaving-producer

# Открытие нового окна PowerShell для просмотра логов консюмера и четвёртого продюсера
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "docker-compose logs -f autoclaving-producer consumer"

# Время работы четвёртого продюсера
Start-Sleep -Seconds 30

# Остановка первого продюсера
docker-compose stop mixing-components-producer

# Остановка второго продюсера
docker-compose stop molding-and-initial-exposure-producer

# Остановка третьего продюсера
docker-compose stop cutting-array-producer

# Остановка четвёртого продюсера и консюмера
docker-compose stop autoclaving-producer consumer

# Остановка Kafka и Zookeeper
docker-compose stop kafka zookeeper

