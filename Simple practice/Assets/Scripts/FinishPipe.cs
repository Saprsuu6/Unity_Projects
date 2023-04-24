using UnityEngine;

public class FinishPipe : MonoBehaviour
{
    private GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}

//if (collision.gameObject.CompareTag("Tube"))
//{
//    collision.transform.position = spawnPoint.transform.position
//        + Vector3.up * Random.Range(-2f, 2f);
//}

//if (collision.gameObject.CompareTag("Pipe"))
//{
//    GameObject pipe = collision.transform.parent.gameObject;
//    Debug.Log("FinishPipe::Trigger " + pipe.name);
//    //GameObject.Destroy(pipe);
//    if ((pipe.transform.position - transform.position).magnitude < 10)
//    {
//        pipe.transform.Translate(Vector2.right * 20);
//    }
//}
