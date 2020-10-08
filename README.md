# Eventi

[![Build Status](https://dev.azure.com/Eventi-App/Eventi/_apis/build/status/EnisMulic.Eventi?branchName=master)](https://dev.azure.com/Eventi-App/Eventi/_build/latest?definitionId=1&branchName=master)


## Build

#### Download Docker
#### Start Docker

#### Clone the repository and navigate to the application

```
git clone https://github.com/EnisMulic/Eventi.git
cd Eventi
```

#### Build the Docker Image

```
docker-compose build
```

#### Start the Docker Container
```
docker-compose up
```

## Project Structure

```
.
├───Eventi.Common             ## Enumerations used accross the solution                             
├───Eventi.Contracts          # WebAPI Contracts                 
│   └───V1                    # Api Version folder     
│       ├───Requests          # Api request models                
│       └───Responses         # Api response models                 
├───Eventi.Core               ## The Api infrastructure         
│   ├───Helpers               #          
│   ├───Interfaces            # Service Interfaces             
│   ├───Mappings              # Contracts and Domain mapping profiles             
│   └───Settings              #              
├───Eventi.Database           ## Api Database project (EF Core context, database migrations and database seed)             
│   ├───Migrations            # Database migrations             
├───Eventi.Domain             ## Api Domain Model (Database Entity)             
├───Eventi.Sdk                ## Rest Client Interfaces for Refit         
├───Eventi.Services           ## Concrete Api Services implementations             
├───Eventi.Web                ## MVC Web application project         
│   ├───Areas                 #          
│   ├───Controllers           #              
│   ├───Helper                #          
│   ├───Models                #                                   
│   ├───ViewModels            #              
│   ├───Views                 #          
│   └───wwwroot               # Frontend Assets         
│       ├───css               #          
│       ├───images            #              
│       ├───js                #          
│       └───lib               #          
└───Eventi.WebAPI             ## Rest api project             
    ├───Controllers           # Api Controllers            
    │   └───V1                # Api Controllers Version folder         
    └───Installers            # Service registry, Swagger configuration, Automapper profile installation                        
```

## Contributors

* [Azra Bećirević](https://github.com/AzraBecirevic)
* [Zinedin Mezit]()
* [Enis Mulić](https://github.com/EnisMulic)