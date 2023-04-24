using UnityEngine;

public class AfterBoom : MonoBehaviour
{
    [SerializeField]
    private GameObject bird;
    private GameObject oldBird;

    private GameObject[] tubesForRemove;

    // Start is called before the first frame update
    void Start()
    {
        //oldBird = GameObject.FindGameObjectWithTag("Bird");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Border"))
        {
            tubesForRemove = GameObject.FindGameObjectsWithTag("Tube");
            foreach (var item in tubesForRemove)
            {
                Destroy(item);
            }

            Instantiate(bird, new Vector3(-5, 0, 0), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Bird"));
            //Destroy(oldBird);
        }
    }
}
