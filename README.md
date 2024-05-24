# Projeto Anamnese e Encaminhamento Médico API

Este projeto é uma API desenvolvida em .NET 6 e Docker para gerenciar anamneses e encaminhamentos médicos.

## Tecnologias Utilizadas

- **.NET 6**
- **EntityFramework**
- **Docker**

## Requisitos

Antes de iniciar, certifique-se de ter os seguintes requisitos instalados:

- **Visual Studio**
- **.NET 6 SDK**
- **Docker Desktop** (opcional)

## Inicialização

### Utilizando Docker

Para inicializar o projeto utilizando Docker, siga os passos abaixo:

1. **Certifique-se de ter o Docker instalado em sua máquina.**
   - [Instalar Docker Desktop](https://www.docker.com/products/docker-desktop)

2. **Configurar e inicializar com Docker Compose:**
   - Altere a inicialização (startup projects) do projeto para utilizar o `docker-compose`.
   - Inicialize o projeto. Durante a primeira inicialização, pode ocorrer um erro devido à criação do banco de dados MySQL. Isso é esperado.
   - Inicialize o projeto novamente para garantir que o banco de dados e os serviços estejam configurados corretamente.

3. **Iniciar os contêineres:**
   - Execute o seguinte comando na raiz do projeto para iniciar os contêineres:
     ```bash
     docker-compose up
     ```

### Rodando Localmente

Se você preferir rodar o projeto localmente sem Docker, siga os passos abaixo:

1. **Configurar as variáveis de ambiente:**
   - Abra o arquivo `appsettings.json`.
   - Altere a senha do banco de dados nas variáveis de controle para corresponder às suas configurações locais.

2. **Inicializar o projeto:**
   - Abra o projeto no Visual Studio.
   - Execute o projeto usando o Visual Studio (`F5` ou `Ctrl+F5`).

