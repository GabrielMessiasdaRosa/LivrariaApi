# Executando o Projeto

## Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Rodando o Projeto

1. Certifique-se de que o Docker e o Docker Compose estão instalados e em execução.

2. No diretório raiz do projeto, execute o comando abaixo para construir e rodar os containers:
   ```bash
   docker-compose up --build
   ```

3. O projeto estará disponível na porta \`5000\`. Abra o navegador e acesse:
   ```bash
   http://localhost:5000/
   ```
4. Para parar a execução do projeto, pressione \`Ctrl + C\` no terminal e execute o comando abaixo:
   ```bash
   docker-compose down
   ```
5. Para remover os containers e as imagens, execute o comando abaixo:
   ```bash
   docker-compose down --rmi all
   ```
6. Para remover os containers, as imagens e os volumes, execute o comando abaixo:
   ```bash
   docker-compose down --volumes --rmi all
   ```

7. Enjoy! 🚀

# Endpoints Disponíveis

## GET /livros

## GET /livros?id={item_id}

## POST /livros

## PATCH /livros?id={item_id}

## DELETE /livros?id={item_id}
