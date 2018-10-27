using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromPool : MonoBehaviour {

    private Transform t;
    private AudioSource aud;
    private ObjectPooler pool;
    [SerializeField] private string tagToSpawn;
    [SerializeField] private float delayBetweenSpawns;
    private float timer;


	// Use this for initialization
	void Start () {
        t = transform;
        aud = GetComponent<AudioSource>();
        pool = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
    }


    private void Update()
    {
        if(timer < delayBetweenSpawns)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            aud.Play();
            pool.SpawnFromPool(tagToSpawn, t.position, Quaternion.identity);
        }

    }

}
