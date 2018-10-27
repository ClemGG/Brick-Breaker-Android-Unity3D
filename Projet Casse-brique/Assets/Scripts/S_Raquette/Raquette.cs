using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Clement.Utilities.Maths;

public class Raquette : MonoBehaviour {


    [Space(10)]
    [Header("Scripts & Components : ")]
    [Space(10)]

    public Transform t;


    [Space(10)]
    [Header("Touch Inputs : ")]
    [Space(10)]
    
    private Touch touchInput;
    private int ID = -1;
    [HideInInspector] public float forceAddedToBall;

    [SerializeField] private Vector3 upperLimit;
    [SerializeField] private Vector3 lowerLimit;
    private Vector3 startPos;
    [HideInInspector] public Vector3 startScale;


    [Space(10)]
    [Header("Values : ")]
    [Space(10)]
    
    [SerializeField] private float detectionZoneX;
    [SerializeField] private float detectionZoneY;


    public bool hasBall;

    private Vector3 newPos;
    private Vector3 v3;


    [Space(10)]
    [Header("PowerUps : ")]
    [Space(10)]

    [SerializeField] private Transform[] projectilesSpawnPoints;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private float coefSize = 2f;
    [HideInInspector] public bool estReduit;
    [HideInInspector] public bool estAgrandi;


    [Space(10)]
    [Header("Gizmos : ")]
    [Space(10)]
    
    [SerializeField] private Color gizmoBoundsColor;
    [SerializeField] private bool showGizmos = true;





    // Use this for initialization
    void Start () {
        Input.multiTouchEnabled = true; //enabled Multitouch
        t = transform;
        startPos = t.position;
        startScale = t.localScale;
	}



	
	// Update is called once per frame
	void Update () {
        GetInput();
	}



    private void FixedUpdate()
    {
        Move();
    }



    private void GetInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                //print(i);

                Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

                //print(v3);

                if ((v3.x > startPos.x - detectionZoneX / 2 && v3.x < startPos.x + detectionZoneX / 2) && 
                    (v3.y > startPos.y - detectionZoneY / 2 && v3.y < startPos.y + detectionZoneY / 2))
                {
                    this.v3 = v3;


                    //print("True pour " + name);
                    if (ID == -1)
                    {
                        ID = i;
                    }


                    if (ID != -1)
                    {
                        if(Input.touchCount <= ID)
                        {
                            ID--;
                        }
                        if (Input.touchCount >= ID)
                        {
                            touchInput = Input.GetTouch(ID);

                            if (touchInput.tapCount == 2 && hasBall)
                            {
                                hasBall = false;
                                //print(GameObject.FindGameObjectWithTag("Balle").GetComponent<Balle>());
                                GameObject.FindGameObjectWithTag("Balle").GetComponent<Balle>().DetachBallFromPlayer();
                            }
                        }
                    }




                }
                else
                {
                    //print("False pour " + name);
                    ID = -1;
                }
                
            }


        }
        else
        {
            ID = -1;
        }

        //print(name + " ; index : " + ID);



    }




    private void Move()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(touchInput.position);
        newPos.x = t.position.x;
        newPos.z = 0f;

        this.newPos = newPos;
        
        forceAddedToBall = touchInput.deltaPosition.y / 3f /* * ((newPos.y > t.position.y) ? 1 : (newPos.y < t.position.y) ? -1 : 0)*/;
        forceAddedToBall = Mathf.Clamp(forceAddedToBall, -2f, 2f);
        //print(forceAddedToBall);

        if (newPos.y < upperLimit.y && newPos.y > lowerLimit.y)
        {
            t.position = newPos;
        }

    }





    public IEnumerator EnableProjectiles()
    {
        for (int i = 0; i < projectilesSpawnPoints.Length; i++)
        {
            projectilesSpawnPoints[i].gameObject.SetActive(true);
        }
            yield return new WaitForSeconds(powerUpDuration);

        for (int i = 0; i < projectilesSpawnPoints.Length; i++)
        {
            projectilesSpawnPoints[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator Retrecir()
    {
        if (!estReduit)
        {
            //print("estreduit : " + estReduit);

            //Vector3 scale = t.localScale;
            //scale.y /= coefSize;
            //t.localScale = scale;

            t.localScale /= coefSize;
            estReduit = true;
        }
        yield return new WaitForSeconds(powerUpDuration * 2f);
        if (estReduit)
        {
            //print("estreduit : " + estReduit);

            //Vector3 scale = t.localScale;
            //scale.y *= coefSize;
            //t.localScale = scale;

            t.localScale *= coefSize;
            estReduit = false;
        }
    }

    public IEnumerator Agrandir()
    {
        if (!estAgrandi)
        {
            //print("estAgrandi : " + estAgrandi);

            //Vector3 scale = t.localScale;
            //scale.y *= coefSize;
            //t.localScale = scale;

            t.localScale *= coefSize;
            estAgrandi = true;
        }

        yield return new WaitForSeconds(powerUpDuration * 2f);

        if (estAgrandi)
        {
            //print("estAgrandi : " + estAgrandi);

            //Vector3 scale = t.localScale;
            //scale.y /= coefSize;
            //t.localScale = scale;

            t.localScale /= coefSize;
            estAgrandi = false;
        }
    }




    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        Gizmos.color = gizmoBoundsColor;
        Gizmos.DrawSphere(upperLimit, .2f);
        Gizmos.DrawSphere(lowerLimit, .2f);

        if (startPos != Vector3.zero)
            Gizmos.DrawCube(startPos, new Vector3(detectionZoneX, detectionZoneY));
        else
            Gizmos.DrawCube(t.position, new Vector3(detectionZoneX, detectionZoneY));

        if (Input.touchCount > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(newPos, .1f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(v3, .1f);
        }

    }
}
