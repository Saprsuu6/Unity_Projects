using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;
    private float pipeTime;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Random.Range(2, 3);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pipeTime -= Time.deltaTime;
        if (pipeTime < 0)
        {
            pipeTime = spawnTime;
            SpawnTime();
            spawnTime = Random.Range(2, 3);
        }
    }

    private void SpawnTime()
    {
        Instantiate(pipe, transform.position + Vector3.up * Random.Range(-2f, 2f), Quaternion.identity);
    }
}
