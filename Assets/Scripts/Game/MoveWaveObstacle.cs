using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script simulate the start movement
public class MoveWaveObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    public float amplitude = 3;
    public float frequency = 0.5f;
    public float speed = 1;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
        amplitude = 3;
        frequency = 0.5f;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin((Time.time - startTime) * speed * Mathf.PI * 2f * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, offset, transform.position.z);

    }
}
