using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CpfValidator
{
    public class CpfValidationFunction
    {
        private readonly ILogger _logger;

        public CpfValidationFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CpfValidationFunction>();
        }

        // --------------------------------------------------------------------------------
        // 1. HANDLER HTTP (Ponto de Entrada da Azure Function)
        // --------------------------------------------------------------------------------
        [Function("ValidateCpf")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cpf/{cpfNumber}")] HttpRequestData req,
            string cpfNumber)
        {
            _logger.LogInformation($"Requisição HTTP recebida para validar o CPF: {cpfNumber}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            string cpfLimpo = Regex.Replace(cpfNumber, "[^0-9]", "");

            // 2. Chama a Lógica de Validação
            bool isValid = IsValidCpf(cpfLimpo);

            // 3. Constrói a Resposta
            var responseData = new 
            {
                cpf = cpfNumber,
                cpf_limpo = cpfLimpo,
                valido = isValid,
                mensagem = isValid ? "CPF válido." : "CPF inválido."
            };
            
            await response.WriteStringAsync(JsonConvert.SerializeObject(responseData));
            response.Headers.Add("Content-Type", "application/json");

            return response;
        }

        // --------------------------------------------------------------------------------
        // 2. LÓGICA DE VALIDAÇÃO DO CPF (MÉTODO ESTÁTICO)
        // --------------------------------------------------------------------------------
        private static bool IsValidCpf(string cpf)
        {
            // Validação básica de tamanho e dígitos repetidos
            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
            {
                return false;
            }

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            // Cálculo do 1º Dígito Verificador
            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            // Cálculo do 2º Dígito Verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            // Comparação final
            return cpf.EndsWith(digito);
        }
    }
}
