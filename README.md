# Bem vindo ao TechChallenge!

O software de **AutomotiveMechanics** otimiza seus processos com eficiência

Gerenciar uma oficina mecânica é uma tarefa desafiadora que requer organização, controle e eficiência. Manter registros precisos de clientes, veículos, ordens de serviço, estoque de peças e despesas é essencial para o sucesso do negócio. Nesse contexto, um software de gerenciamento de **AutomotiveMechanics** se torna uma ferramenta fundamental para simplificar e melhorar todos esses aspectos.


# Tecnologias implementadas

 - DOT NET 7
	 - ASP.NET WebApi
	 - ASP.NET Identity Core
	 - Entity Framework 7
	 
 - Component/Service
	 - Swagger UI
	 - Redoc
	 - Flunt Validation
	 - Automapper
 - Hosting
	 - Docker Compose


Acesso a Documentação/Levantamento de requisitos em-> https://1drv.ms/p/s!AsMmi27yHbR4h9QpVBUetNwSHhjm4w?e=ob7epU	

## Instalação
Você pode executar o projeto **AutomotiveMechanics** em qualquer sistema operacional. Certifique-se de ter instalado o docker e o Visual Studio em seu ambiente. 

(Obter instalação do Docker) --> https://www.docker.com/products/docker-desktop/

Clone o repositório **AutomotiveMechanics** --> git clone https://github.com/eliterickytech/Techchallenge.AutomotiveMechanics.git

Na solução TechChallenge.AutomotiveMechanics selecione Clean Solution --> Build Solution --> Restore NuGetPackages

Vá para o Package Manager Console na solução Presentation.API.

Execute os seguintes comandos

    docker-compose up -d
    update-database

