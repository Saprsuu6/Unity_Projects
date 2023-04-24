using UnityEngine;

public class AddScore : MonoBehaviour
{
    private AudioSource scoreSource;

    private void Start()
    {
        scoreSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            Score.score++;
            Audio.Play(scoreSource);
        }
    }
}
