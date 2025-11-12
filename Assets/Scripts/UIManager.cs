using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameAPI_Service apiService;

    [SerializeField] private GameObject saveText;

    [SerializeField] private InputActionAsset inputAsset;


    private InputAction menuAction;

    public GameObject panel;

    [SerializeField] private TextMeshProUGUI vidaText;
    [SerializeField] private TextMeshProUGUI itensText;
    [SerializeField] private TextMeshProUGUI posicaoXText;
    [SerializeField] private TextMeshProUGUI posicaoYText;
    [SerializeField] private TextMeshProUGUI posicaoZText;

    [SerializeField] private Player playerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        apiService = new GameAPI_Service();

        menuAction = inputAsset.FindAction("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (menuAction.WasReleasedThisFrame())
            ActivatePanel();
    }

    private void ActivatePanel()
    {
        panel.SetActive(!panel.activeSelf);

        vidaText.text = "Vida: " + playerScript.jogador.Vida;
        itensText.text = "Itens: " + playerScript.jogador.QuantidadeDeItens;
        posicaoXText.text = "PosicaoX: " + playerScript.jogador.PosicaoX;
        posicaoYText.text = "PosicaoY: " + playerScript.jogador.PosicaoY;
        posicaoZText.text = "PosicaoZ: " + playerScript.jogador.PosicaoZ;
    }
    public IEnumerator SaveText()
    {
        saveText.SetActive(true);
        yield return new WaitForSeconds(2);
        saveText.SetActive(false);
    }

    public void Recarregar()
    {
        SceneManager.LoadScene(0);
    }

}
