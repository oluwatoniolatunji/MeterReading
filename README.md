# MeterReading
Meter Reading Microservice

## Database Set Up
- Create an MSSQL Database with a choice name
- Open SqlScript
- Run database-set-up-script.sql to create the tables required by the microservice

## Creating Migration if Necessary
- Clone the repo
- Open command prompt and cd to MeterReading/EnergyMeterReading.DataAccess
- Run the script
  dotnet ef --startup-project ../EnergyMeterReading.Api migrations add CreateMeterReadingTable -c MeterReadingDbContext
  dotnet ef --startup-project ../EnergyMeterReading.Api database update
  
## API Configuration
- Update appsettings.json and appsettings.Development.json EnergyMeterReading.Api
