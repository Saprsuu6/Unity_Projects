using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text scoreText;
    public static int score = 0;

    void Start()
    {
        score = 0;
    }

    private void LateUpdate()
    {
        scoreText.text = score.ToString();
    }
}
