using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace SolicitaTokenFormUrlEncoded
{
    public class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string usuario = "";  //Usuario
            string senhaUsuario = ""; //SenhaUsuario
            string clientId = ""; // senha ClientID
            string clientSecret = ""; //Senha ClientSecret
            string serviceToken = ""; //Senha serviceTolen
            string endpointRequestToken = "https://"; // Endpoint para solicitar token
            try
            {
                //Classe HttpClient
                var client = new HttpClient();
                //Parametros/Formulario para requisição do Token Access
                var requestContent = new FormUrlEncodedContent(new[] {
                        new KeyValuePair<string, string>("username", usuario),
                        new KeyValuePair<string, string>("password", senhaUsuario),
                        new KeyValuePair<string, string>("client_id", clientId),
                        new KeyValuePair<string, string>("client_secret", clientSecret),
                        new KeyValuePair<string, string>("grant_type", serviceToken),
                    });

                //Response para metodo Post da nossa requisição de Token
                HttpResponseMessage response = await client.PostAsync(endpointRequestToken, requestContent);
                HttpContent responseContent = response.Content;
                using (var reader = new StringReader(await responseContent.ReadAsStringAsync()))

                    //Validando se o status de retorno do endpoint for = a 200 -> OK
                    if ((int)response.StatusCode == 200)
                    {
                        //recebendo token - json inteiro
                        string output = reader.ReadLine();

                        //Para Tratar o retorno Json do Token
                        string tratamento = output.Substring(0, 130);
                        string token = tratamento.Replace("access_token", "Bearer ").Replace("{", "").Replace(":", "").Replace(@"""", "");

                        Console.WriteLine("Recebendo Json inteiro da Requisição do Token -> " + output);
                        Console.WriteLine("Recebendo Token tratado -> " + token);
                        Console.WriteLine("Response Status Code " + HttpStatusCode.OK);
                        //return token;
                    }
                    else
                    {
                        string output = "Falha não foi possicvel gerar Token ! ";
                        //return output;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception Catch: {e} ");
                Console.WriteLine("Http Status: " + HttpStatusCode.NotFound);
                throw new Exception($"Erro:" + "  " + e);
            }
        }
    }
}
