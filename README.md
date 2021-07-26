# PostOffice  
## Don't have DB  
Download Docker Desktop, might need to set it to linux  
cd Setup  
docker-compose up -d  



At the start of .NET app, it automatically migrates changes, so don't have to do anything else  
## Have DB  
Change connection string in appsettings.json  

## FE  
cd PostOfficeFE  
npm i  
npm start  


If your API is in another port, might need to change that in PostOfficeFE/.env
