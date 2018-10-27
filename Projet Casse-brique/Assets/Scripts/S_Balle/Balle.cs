using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour {

    public Transform t;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Raquette[] raquettes;

    [SerializeField] private float BallSpeed;

    public Vector3 appliedForce;
    private Vector3 launchForce = Vector3.right;
    public int MirrorY = 1;
    public int MirrorX = 1;
    public bool checkNextCollision = true;


    // Use this for initialization
    void Start () {

        checkNextCollision = true;

        t = transform;
        rb = GetComponent<Rigidbody2D>();

        if(FindObjectsOfType(typeof(Balle)).Length == 1)
            AttachBallToPlayer(0);
	}


    // Update is called once per frame
    void FixedUpdate () {

        MoveBall();
	}







    private void MoveBall()
    {
        Vector3 force = new Vector3(appliedForce.x * MirrorX, appliedForce.y * MirrorY);
        t.Translate(force * BallSpeed * Time.deltaTime);
    }

    public void SwapX()
    {
        MirrorX = -MirrorX;
    }
    public void SwapY()
    {
        MirrorY = -MirrorY;
    }





    public void AttachBallToPlayer(int ID)
    {
        if(raquettes == null)
        {
            raquettes = (Raquette[])FindObjectsOfType(typeof(Raquette));
        }
        else
        {
            for (int i = 0; i < raquettes.Length; i++)
            {
                raquettes[i] = (Raquette)FindObjectsOfType(typeof(Raquette))[i];
            }
        }

        appliedForce = Vector3.zero;
        launchForce = (ID == 0) ? Vector3.right : Vector3.left;
        MirrorY = 0;
        t.parent = raquettes[ID].t.GetChild(2);
        t.localPosition = Vector3.zero;
        raquettes[ID].hasBall = true;
    }





    public IEnumerator CheckNextCollision()
    {
        yield return new WaitForSeconds(.05f);
        checkNextCollision = true;
    }





    public void DetachBallFromPlayer()
    {
        appliedForce = launchForce;
        t.parent = null;
    }




}
