# Bem vindo ao TechChallenge!

O software de **AutomotiveMechanics** otimiza seus processos com eficiência

Gerenciar uma oficina mecânica é uma tarefa desafiadora que requer organização, controle e eficiência. Manter registros precisos de clientes, veículos, ordens de serviço, estoque de peças e despesas é essencial para o sucesso do negócio. Nesse contexto, um software de gerenciamento de **AutomotiveMechanics** se torna uma ferramenta fundamental para simplificar e melhorar todos esses aspectos.


# Tecnologias implementadas

 - DOT NET 7
	 - ASP.NET WebApi
	 - ASP.NET Identity Core
	 - Entity Framework 7
	 - JWT Beare Authentication      
	 
 - Component/Service
	 - Swagger UI
	 - Redoc
	 - Flunt Validation
	 - Automapper
 - Hosting
	 - Docker Compose

# Participantes do projeto
	- João Paulo Marques 	  RM 351763
	- Isabella Kratchei       RM 351575
	- Isaias Silva            RM 352364
	- Ricardo Perdigão        RM 351514
	- Michel Balarin Claro    RM 351165
 
# Documentação

Acesso a documentação de requisitos online
	
 	https://1drv.ms/p/s!AsMmi27yHbR4h9QpVBUetNwSHhjm4w?e=ob7epU	
  
Acesso a documentação de requisitos offline

	documentation/AutomotiveMechanics.pdf

 ## Documentação da Api online
 
https://automotivemechanics.azurewebsites.net/api-docs

https://automotivemechanics.azurewebsites.net/swagger

## Documentação da Api offline

https://localhost:7116/api-docs

https://localhost:7116/swagger/index.html

## Instalação
Você pode executar o projeto **AutomotiveMechanics** em qualquer sistema operacional. Certifique-se de ter instalado o docker e o Visual Studio em seu ambiente. 

(Obter instalação do Docker)

https://www.docker.com/products/docker-desktop/

Clone o repositório **AutomotiveMechanics**

	git clone https://github.com/eliterickytech/Techchallenge.AutomotiveMechanics.git

Na solução TechChallenge.AutomotiveMechanics selecione:
- Clean Solution
- Build Solution
- Restore NuGetPackages

Vá para o Package Manager Console na solução Presentation.API.

Execute os seguintes comandos

    docker-compose up -d

Criar tabelas e dados iniciais
     
     update-database
    
