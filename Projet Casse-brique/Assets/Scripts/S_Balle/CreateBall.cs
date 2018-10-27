using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBall : MonoBehaviour {

    private ObjectPooler pool;

	// Use this for initialization
	void Start () {
        pool = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
        pool.SpawnFromPool("extra_balle", transform.position, Quaternion.identity);
    }
	
}
