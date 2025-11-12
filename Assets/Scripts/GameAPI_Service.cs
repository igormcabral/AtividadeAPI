using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameAPI_Service 
{
    private readonly HttpClient httpClient;
    private const string BASE_URL = "https://68f95ab5deff18f212b951dd.mockapi.io/Jogador";

    public GameAPI_Service()
    {
        httpClient = new HttpClient();
    }

    #region Jogador Operations

    /// <summary>
    /// Busca um jogador específico
    /// </summary>
    public async Task<Jogador> GetJogador(string id)
    {
        try
        {
            string url = $"{BASE_URL}/Jogador/{id}";
            Debug.Log($"GET: {url}");

            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador recebido: {json}");

            Jogador jogador = JsonUtility.FromJson<Jogador>(json);
            return jogador;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao buscar jogador {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Atualiza dados do jogador
    /// </summary>
    public async Task<Jogador> AtualizarJogador(string id, Jogador jogador)
    {
        try
        {
            string url = $"{BASE_URL}/Jogador/{id}";
            Debug.Log($"PUT: {url}");

            string json = JsonUtility.ToJson(jogador);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador atualizado: {responseJson}");

            return JsonUtility.FromJson<Jogador>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao atualizar jogador {id}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Cria novo jogador
    /// </summary>
    public async Task<Jogador> CriarJogador(Jogador jogador)
    {
        try
        {
            string url = $"{BASE_URL}/Jogador";
            Debug.Log($"POST: {url}");

            string json = JsonUtility.ToJson(jogador);
            Debug.Log($"JSON sendo enviado: {json}");

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseJson = await response.Content.ReadAsStringAsync();
            Debug.Log($"Jogador criado: {responseJson}");

            return JsonUtility.FromJson<Jogador>(responseJson);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao criar jogador: {ex.Message}");
            return null;
        }
    }

    #endregion

    public void Dispose()
    {
        httpClient?.Dispose();
    }
}

// Classes auxiliares para deserialização de arrays
[System.Serializable]
public class JogadorArray
{
    public Jogador[] jogadores;
}

