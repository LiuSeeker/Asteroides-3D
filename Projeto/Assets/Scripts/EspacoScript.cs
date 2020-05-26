using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspacoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int total_asteroides = 100;
    public float speed = 1.0f;

    public GameObject asteroide;
    void Start()
    {

        for (int i = 0; i < total_asteroides; i++)
        {
            float spawnPointX = Random.Range(-60.0f, 70.0f);
            float spawnPointY = Random.Range(-60.0f, 60.0f);
            float spawnPointZ = Random.Range(-90.0f, 30.0f);
            Vector3 spawnPos = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
            GameObject objeto = Instantiate(asteroide, spawnPos, Quaternion.identity);
            objeto.transform.parent = gameObject.transform;

            float spawnDirX = Random.Range(-1.0f, 1.0f);
            float spawnDirY = Random.Range(-1.0f, 1.0f);
            float spawnDirZ = Random.Range(-1.0f, 1.0f);
            float speed = Random.Range(-1.0f, 1.0f);
            Vector3 spawnDirection = new Vector3(spawnDirX, spawnDirY, spawnDirZ);
            objeto.GetComponent<Rigidbody>().velocity = speed * spawnDirection;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
