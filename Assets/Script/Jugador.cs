using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    [Header("Movimiento")]
    private float movimientoX;
    public float velocidad = 2;
    private Rigidbody2D rb2d;

    [Header("************ Salto ************")]
    public float fuerzaSalto = 2;

    [Header("******* CompruebaSuelo *******")]
    private bool estaEnSuelo = false;
    public LayerMask layerSuelo;
    private float radioEsferaTocaSuelo = 0.1f;
    public Transform compruebaSuelo;

    [Header("******* Animaciones *******")]
    private Animator animator;

    [Header("******** Sonido ********")]
    public AudioSource audioSource;
    public AudioClip clipManzana;
    public AudioClip clipMuerte;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        rb2d.linearVelocity = new Vector2(movimientoX * velocidad, rb2d.linearVelocity.y);

        
        animator.SetBool("estaCorriendo", movimientoX != 0);

        
        animator.SetBool("estaSaltando", !estaEnSuelo);
    }


    void FixedUpdate()
    {
        estaEnSuelo = Physics2D.OverlapCircle(compruebaSuelo.position, radioEsferaTocaSuelo, layerSuelo);
    }

    private void OnMove(InputValue inputMovimiento)
    {
        movimientoX = inputMovimiento.Get<Vector2>().x;

        if (movimientoX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movimientoX), 1, 1);
            animator.SetBool("estaCorriendo", true);
        }
    }

    private void OnJump(InputValue inputSalto)
    {
        if (estaEnSuelo)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, fuerzaSalto);
            animator.SetBool("estaSaltando", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Manzana") || collision.transform.CompareTag("Platano") || collision.transform.CompareTag("Kiwi") || collision.transform.CompareTag("Melon"))
        {
            FindObjectOfType<GameManager>().SumarPuntos();
            audioSource.PlayOneShot(clipManzana);
            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("SueloMuerte") || collision.transform.CompareTag("Enemigo"))
        {
            ReproducirSonidoMuerte();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (collision.transform.CompareTag("Meta"))
        {
            SceneManager.LoadScene(2);
        }
    }

    void ReproducirSonidoMuerte()
    {
        GameObject go = new GameObject("SonidoMuerteTemp");
        AudioSource tempAudio = go.AddComponent<AudioSource>();
        tempAudio.clip = clipMuerte;
        tempAudio.Play();

        DontDestroyOnLoad(go);

        Destroy(go, clipMuerte.length);
    }


}
