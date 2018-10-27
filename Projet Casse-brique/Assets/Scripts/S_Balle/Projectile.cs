using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Transform t;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 forceToApply;


    private void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move();	
	}

    private void Move()
    {
        t.Translate(forceToApply * speed * Time.deltaTime);
    }

}
