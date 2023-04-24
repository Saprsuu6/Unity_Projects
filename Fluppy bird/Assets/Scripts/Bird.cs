using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    public GameManager gameManager;
    [SerializeField]
    public GameObject gameOverCanvas;

    private AudioSource wingSource;
    private new Rigidbody2D rigidbody2D;
    private float force = 3;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        AudioSource[] sources = GetComponents<AudioSource>();
        wingSource = sources[0];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameOverCanvas.activeInHierarchy)
        {
            rigidbody2D.velocity = Vector2.up * force;
            Audio.Play(wingSource);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") ||
            collision.gameObject.CompareTag("Border"))
        {
            gameManager.GameOver();
        }
    }
}
