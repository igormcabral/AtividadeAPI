using UnityEngine;

public class Teste : MonoBehaviour
{
    private GameAPI_Service apiService;

    async void Start()
    {
        apiService = new GameAPI_Service();

        Debug.Log("=== TESTE DA API ===");

        //Adicionar Jogadores
        Jogador novoJogador = new Jogador();
        novoJogador.Vida = 100;
        novoJogador.PosicaoX = 0;
        novoJogador.PosicaoY = 0;
        novoJogador.PosicaoZ = 0;
        //adicionar jogador na API
        Jogador criadoJogador = await apiService.CriarJogador(novoJogador);

        Debug.Log("=== FIM DOS TESTES ===");
    }


    void OnDestroy()
    {
        apiService?.Dispose();
    }
}
