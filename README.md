# Developer Evaluation Project
[![wakatime](https://wakatime.com/badge/user/953a6bcd-5627-41d7-b03d-6e07c7380424/project/d1d03baf-4107-4e64-871a-40b9acc30246.svg)](https://wakatime.com/badge/user/953a6bcd-5627-41d7-b03d-6e07c7380424/project/d1d03baf-4107-4e64-871a-40b9acc30246)


## Instruções para execução

O projeto está preparado apra ser executado com o docker-compose disponibilizado:

 ```bash
cd  template\backend
docker-compose up
 ```

O projeto também pode ser inicializado pelo visual studio executando o debug no projeto docker-compose.

Para executar os testes:

 ```bash
cd  template\backend\tests\Ambev.DeveloperEvaluation.Unit
dotnet test
 ```

Todos os endpoints estão com autenticação e autorização, deve-se pegar um token em /auth e adicionar um header Authorization: Bearer {token} para acessar os endpoints. 

### Comandos úteis

#### Criar usuário (cliente)
```curl
curl -X 'POST' \ 'https://localhost:64926/api/Users' \ -H 'accept: text/plain' \ -H 'Content-Type: application/json' \ -d '{ "username": "João", "password": "João@123", "phone": "111111111", "email": "joao@gmail.com", "status": 1, "role": 1 }'
```
#### Criar usuário (gerente)
```curl
curl -X 'POST' \ 'https://localhost:64926/api/Users' \ -H 'accept: text/plain' \ -H 'Content-Type: application/json' \ -d '{ "username": "João", "password": "João@123", "phone": "111111111", "email": "joao_gerente@gmail.com", "status": 1, "role": 2 }'
```

#### Obter token
```curl
curl -X 'POST' \ 'https://localhost:64926/api/Auth' \ -H 'accept: text/plain' \ -H 'Content-Type: application/json' \ -d '{ "email": "joao@gmail.com", "password": "João@123" }'
```

#### Criar venda
```curl
curl -X 'POST' \ 'https://localhost:64926/api/Sales' \ -H 'accept: text/plain' \ -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJiMTc4YzZmMC1iZTU5LTQ4ODEtODExMS04MjI3Y2NjMTViYTIiLCJ1bmlxdWVfbmFtZSI6Ikpvw6NvIiwicm9sZSI6IkN1c3RvbWVyIiwibmJmIjoxNzM3Njg2NzQ3LCJleHAiOjE3Mzc3MTU1NDcsImlhdCI6MTczNzY4Njc0N30.0nUPH07AKmpv4TM0evicKg0XF5TmalONZDgRCZmg9Y4' \ -H 'Content-Type: application/json' \ -d '{ "saleNumber": "150", "saleDate": "2025-01-24T02:46:05.100Z", "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "customerName": "string", "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "branchName": "string", "items": [ { "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", "productName": "Doril", "quantity": 15, "unitPrice": 14.99 } ] }'
```
#### Cancelar item
```curl
curl -X 'PATCH' \ 'https://localhost:64926/api/Sales/6d2d448a-8b13-47d0-80ff-b5e867764bc6/satus/cancel' \ -H 'accept: */*' \ -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZTE4YWJjMS1mY2Y3LTQ1OWItODkyNy1iN2QxOGM1NTU5YzUiLCJ1bmlxdWVfbmFtZSI6Ikpvw6NvIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE3Mzc2ODY4NjIsImV4cCI6MTczNzcxNTY2MiwiaWF0IjoxNzM3Njg2ODYyfQ.LNyoijTHeX1gwKJynKuU4u3_PDdbpQ7bWrkNQYzYqXw'
```
#### Cancelar venda
```curl
curl -X 'PATCH' \ 'https://localhost:64926/api/Sales/6d2d448a-8b13-47d0-80ff-b5e867764bc6/products/3fa85f64-5717-4562-b3fc-2c963f66afa6/status/cancel' \ -H 'accept: */*' \ -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZTE4YWJjMS1mY2Y3LTQ1OWItODkyNy1iN2QxOGM1NTU5YzUiLCJ1bmlxdWVfbmFtZSI6Ikpvw6NvIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE3Mzc2ODY4NjIsImV4cCI6MTczNzcxNTY2MiwiaWF0IjoxNzM3Njg2ODYyfQ.LNyoijTHeX1gwKJynKuU4u3_PDdbpQ7bWrkNQYzYqXw'
```
 #### Consultar vendas paginada
```curl
curl -X 'GET' \ 'https://localhost:64926/api/Sales?page=1&pageSize=10' \ -H 'accept: */*' \ -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZTE4YWJjMS1mY2Y3LTQ1OWItODkyNy1iN2QxOGM1NTU5YzUiLCJ1bmlxdWVfbmFtZSI6Ikpvw6NvIiwicm9sZSI6Ik1hbmFnZXIiLCJuYmYiOjE3Mzc2ODY4NjIsImV4cCI6MTczNzcxNTY2MiwiaWF0IjoxNzM3Njg2ODYyfQ.LNyoijTHeX1gwKJynKuU4u3_PDdbpQ7bWrkNQYzYqXw'
```

## Casos de uso
- Criação de vendas seguindo as regras definidas nas instruções;
- Quando existem dois itens com o mesmo produto, eles são agrupados;
- As regras de desconto e quantidade devem ser aplicadas por produto;
- Autenticação e autorização
-- Clientes podem criar vendas, usuários e ler os dados;
-- Administradores e gerentes privilégios de cancelamento e deleções;
- É possivel cancelar todos os itens da venda, um a um, e quando isso acontecer a venda também ficará marcada como cancelada;
- Pesquisar todas as vendas de forma paginada, passando filtros opcionais de: Cliente, Filial e Numero da venda.

### Criação dos usuários
![Criação de usuário gerente e cliente](.docs/images/criacao-usuarios.gif)

### Geração dos tokens
![Obtenção de tokens por perfil](.docs/images/tokens.gif)
### Criação de vendas
![Criação de vendas](.docs/images/criacao-vendas.gif)
### Agrupamento de itens do mesmo produto
![Agrupamento de itens](.docs/images/agrupamento-itens.gif)
### Cancelamento de itens com cancelamento de venda
![cancelamentos](.docs/images/cancelamento-de-itens.gif)

## Instruções recebidas para resolução do desafio
O projeto foi desenvolvido seguindo essas [instruções](/README.md).
