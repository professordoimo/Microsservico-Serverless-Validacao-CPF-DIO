# Microsservico-Serverless-Validacao-CPF-DIO

# Desafio DIO: Microsserviço Serverless para Validação de CPF

[![Status](https://img.shields.io/badge/Status-Concluído-brightgreen)]() 
[![Plataforma](https://img.shields.io/badge/DIO-Desafio%20de%20Projeto-blueviolet)]()
> Repositório criado para cumprir o Desafio de Projeto "Criando um Microsserviço Serverless para Validação de CPF" do Bootcamp [Nome do Bootcamp, ex: Global Experience AWS/Azure/etc.].
> O objetivo deste projeto foi desenvolver um microsserviço com arquitetura **Serverless** capaz de validar a autenticidade de um número de CPF (Cadastro de Pessoa Física) brasileiro, utilizando seu algoritmo matemático de cálculo dos dígitos verificadores (DV1 e DV2).
Esta solução demonstra a capacidade de:
1.  Implementar uma lógica de negócio essencial (Validação de CPF).
2.  Expor esta lógica como uma API leve e de alta disponibilidade.
3.  Utilizar recursos de Cloud Computing (FaaS) para criar uma aplicação escalável e de baixo custo.
4.  * **Plataforma Cloud:** [AWS Lambda / Azure Functions / Google Cloud Functions]
* **Linguagem:** [Python / Node.js / Go]
* **Framework/CLI:** [Serverless Framework / AWS SAM / Azure CLI]
* **Gateway:** [AWS API Gateway / Azure API Management]
* **Gerenciador de Pacotes:** [npm / pip]
* 1.  O cliente envia uma requisição HTTP (GET ou POST) para o **API Gateway**.
2.  O API Gateway aciona a função Serverless (**[Nome da Plataforma] Functions**).
3.  A função extrai o número do CPF da requisição.
4.  O código de validação processa o CPF (limpeza, cálculo dos DVs).
5.  A função retorna a resposta JSON (`valido: true/false`) ao API Gateway.
6.  O API Gateway envia a resposta ao cliente.
7.  **URL Pública:** `[COLE SUA URL DE DEPLOY AQUI]`

**Método Recomendado:** `GET` (passando o CPF como parâmetro de rota)

**Exemplo com cURL:**

```bash
# CPF Válido (Substitua a URL pela sua!)
$ curl -X GET "SUA_URL_DA_API/validar/99999999999"

# Resposta JSON Esperada:
# {
#   "cpf_formatado": "999.999.999-99",
#   "valido": true,
#   "mensagem": "CPF válido."
# }

# CPF Inválido
$ curl -X GET "SUA_URL_DA_API/validar/11111111111" 

# Resposta JSON Esperada:
# {
#   "cpf_formatado": "111.111.111-11",
#   "valido": false,
#   "mensagem": "CPF inválido por conter dígitos repetidos."
# }
