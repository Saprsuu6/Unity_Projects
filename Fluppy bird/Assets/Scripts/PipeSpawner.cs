using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    private float timer = 0;

    public float maxTime = 3;
    public GameObject pipe;
    public const float height = 2;

    private void Start()
    {
        GameObject newPipe = GameObject.Find("Pipes");
        Destroy(newPipe, 5);
    }

    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newPipe = Instantiate(pipe);
            newPipe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(newPipe, 5);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
