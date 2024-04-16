using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject SpiderPrefab;

    // The number of frames to wait before spawning another Ball
    public int SpawnTime = 120;

    // We will use this to count how many frames have elapsed since the last Ball creation
    private int counter = 0;

    void Start()
    {

    }

    void FixedUpdate()
    {
        // If we can spawn the Ball
        if (counter > SpawnTime)
        {
            // We instantiate a new Ball at the generated axis while keeping the Y and Z axes constant
            var spider = Instantiate(SpiderPrefab, transform.position + new Vector3(25.0f, 0.1f, 0.0f), Quaternion.Euler(0, 90, 0));

            // We make sure that the Ball is properly scaled
            spider.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // We tell the program to destroy the Ball after 10 seconds have passed and reset the counter
            Destroy(spider, 5);
            counter = 0;
        }
        else
        {
            counter++;
        }

    }
}
