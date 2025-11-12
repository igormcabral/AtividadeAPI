using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Jogador jogador;
    private GameAPI_Service apiService;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private InputActionAsset inputAsset;

    private InputAction moveAction;
    public Vector2 moveAmt;

    private CharacterController characterController;

    [SerializeField] private float speed;

    void Start()
    {
        jogador = new Jogador();
        jogador.id = "1";
        jogador.Vida = 10;

        apiService = new GameAPI_Service();

        characterController = GetComponent<CharacterController>();
        moveAction = inputAsset.FindAction("Move");
    }

    void Update()
    {
        jogador.PosicaoX = (int)transform.position.x;
        jogador.PosicaoY = (int)transform.position.y;
        jogador.PosicaoZ = (int)transform.position.z;
        
        if(uiManager.panel.activeSelf == false)
        {
            moveAmt = moveAction.ReadValue<Vector2>();

            Vector3 move = transform.forward * moveAmt.y + transform.right * moveAmt.x;
            move = move * speed * Time.deltaTime;

            characterController.Move(move);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            jogador.QuantidadeDeItens++;
            Destroy(other.gameObject);

            StartCoroutine(uiManager.SaveText());

            apiService.AtualizarJogador(jogador.id, jogador);
        }

        if (other.gameObject.CompareTag("Obstáculo"))
        {
            Debug.Log("Colisão");
            jogador.Vida--; 
            Destroy(other.gameObject);
        }

    }
}

