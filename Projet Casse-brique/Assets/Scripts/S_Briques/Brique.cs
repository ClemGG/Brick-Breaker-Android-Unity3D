
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brique : MonoBehaviour {

    [SerializeField] private Transform t;

    [SerializeField] private bool isDestructible;
    [SerializeField] private bool givePowerUp;
    [SerializeField] private int nbOfHitsBeforeDestroy;
    [SerializeField] private int scorePoints;


    [SerializeField] private ObjectPooler pool;
    [SerializeField] private string[] tagsPowerUp;
    [SerializeField] private Sprite[] powerupSprites;
    [SerializeField] private string[] tagsHit;
    [SerializeField] private string[] tagsDeath;
    

    private Vector2 relativePosition;



    
    private void OnDisable()
    {
        if(isDestructible)
            ScoreManager.instance.briques.Remove(this);
    }





    // Use this for initialization
    void Start () {
        t = transform;
        pool = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();

        if(isDestructible)
            ScoreManager.instance.briques.Add(this);
    }




    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag == "Balle")
        {

            Balle b = c.gameObject.GetComponent<Balle>();

            if (b.checkNextCollision)
            {
                b.checkNextCollision = false;
                relativePosition = b.t.position;

                //print(b.MirrorX + ", " + b.MirrorY);
                ChangeDirection(relativePosition, ref b);
                //print(b.MirrorX + ", " + b.MirrorY);

                //StopCoroutine(b.CheckNextCollision());
                b.checkNextCollision = true;
                //StartCoroutine(b.CheckNextCollision());

            }


            if (isDestructible)
            {
                if (nbOfHitsBeforeDestroy == 0)
                {
                    SpawnPrefabsOnDeath();
                    AddPowerUp(b);
                    gameObject.SetActive(false);
                    ScoreManager.instance.AddScore(scorePoints);
                }
                else
                {
                    nbOfHitsBeforeDestroy--;
                    SpawnPrefabsOnHit();
                }
            }
            else
            {
                SpawnPrefabsOnHit();
            }
        }

        else if(c.gameObject.tag == "Projectile")
        {

            if (isDestructible)
            {
                if (nbOfHitsBeforeDestroy == 0)
                {
                    SpawnPrefabsOnDeath();
                    gameObject.SetActive(false);
                    ScoreManager.instance.AddScore(scorePoints);
                }
                else
                {
                    nbOfHitsBeforeDestroy--;
                    SpawnPrefabsOnHit();
                }

            }
            else
            {
                SpawnPrefabsOnHit();
            }

            c.gameObject.SetActive(false);
        }
    }





    private void ChangeDirection(Vector2 relativePosition, ref Balle b)
    {
        Bounds bounds = GetComponent<Collider2D>().bounds;
        //if the right side of the collider is hit
        if (relativePosition.x > 0f && relativePosition.y <= bounds.max.y && relativePosition.y >= bounds.min.y)
        {
            //print ("Collided with the right side");
            b.SwapX();
        }
        //if the left side of the collider is hit
        else if (relativePosition.x < 0 && relativePosition.y <= bounds.max.y && relativePosition.y >= bounds.min.y)
        {
            //print ("Collided with the left side");
            b.SwapX();
        }

        //if the upper side of the collider is hit
        else if (relativePosition.y > 0 && relativePosition.x <= bounds.max.x && relativePosition.x >= bounds.min.x)
        {
            //print ("Collided with the upper side");
            b.SwapY();
        }
        //if the lower side of the collider is hit
        else if(relativePosition.y < 0 && relativePosition.x <= bounds.max.x && relativePosition.x >= bounds.min.x)
        {
            //print ("Collided with the lower side");
            b.SwapY();
        }
        else
        {
            b.SwapX();
        }


    }


    //private void OnDrawGizmos()
    //{
    //    Bounds bounds = GetComponent<Collider2D>().bounds;
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(bounds.min, .1f);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawSphere(bounds.max, .1f);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(new Vector3(bounds.min.x, bounds.max.y), .1f);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(new Vector3(bounds.max.x, bounds.min.y), .1f);

    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawSphere(relativePosition, .1f);


    //}


    private void AddPowerUp(Balle b)
    {


        if (!givePowerUp)
            return;


        int alea = UnityEngine.Random.Range(1, 6);
        SpawnPrefabsOnPowerUp(alea);

        switch (alea)
        {
            case 1:
                AgrandirRaquettes();
                break;
            //case 2:
            //    RetrecirRaquettes();
            //    break;
            case 3:
                SpawnBalles(b);
                break;
            case 4:
                EnableProjectiles();
                break;
            case 5:
                AddExtraLife();
                break;
            default:
                break;
        }
    }



    private void AgrandirRaquettes()
    {
        Raquette[] raquettes = (Raquette[])FindObjectsOfType(typeof(Raquette));
        for (int i = 0; i < raquettes.Length; i++)
        {
            //print(raquettes[i].name);
            raquettes[i].StopCoroutine("Agrandir");
            raquettes[i].t.localScale = raquettes[i].startScale;
            raquettes[i].estAgrandi = false;
            raquettes[i].StartCoroutine("Agrandir");
        }
    }

    private void RetrecirRaquettes()
    {
        Raquette[] raquettes = (Raquette[])FindObjectsOfType(typeof(Raquette));
        for (int i = 0; i < raquettes.Length; i++)
        {
            raquettes[i].StopCoroutine("Retrecir");
            raquettes[i].t.localScale = raquettes[i].startScale;
            raquettes[i].estReduit = false;
            raquettes[i].StartCoroutine("Retrecir");
        }
    }


    private void SpawnBalles(Balle b)
    {
        Vector3 v = b.t.position;
        v.y += .5f;
        GameObject g1 = pool.SpawnFromPool("extra_balle", v, Quaternion.identity);
        v.y -= 1f;
        GameObject g2 = pool.SpawnFromPool("extra_balle", v, Quaternion.identity);

        Balle b1 = g1.GetComponent<Balle>();
        Balle b2 = g2.GetComponent<Balle>();

        b1.MirrorY = 1;
        b2.MirrorY = -1;

        b1.appliedForce = b.appliedForce;
        b2.appliedForce = b.appliedForce;
    }




    private void EnableProjectiles()
    {
        Raquette[] raquettes = (Raquette[])FindObjectsOfType(typeof(Raquette));
        for (int i = 0; i < raquettes.Length; i++)
        {
            raquettes[i].StopCoroutine("EnableProjectiles");
            raquettes[i].StartCoroutine("EnableProjectiles");
        }
    }




    private void AddExtraLife()
    {
        //print("Gagné une vie");
        ScoreManager.instance.UpdateLivesText(1);
    }




    private void SpawnPrefabsOnPowerUp(int alea)
    {
        for (int i = 0; i < tagsPowerUp.Length; i++)
        {
            GameObject go = pool.SpawnFromPool(tagsPowerUp[i], t.position, Quaternion.identity);
            if (go.GetComponent<SpriteRenderer>())
            {
                if(powerupSprites[alea - 1] != null)
                go.GetComponent<SpriteRenderer>().sprite = powerupSprites[alea-1];
            }
        }
    }

    private void SpawnPrefabsOnDeath()
    {
        for (int i = 0; i < tagsDeath.Length; i++)
        {
            pool.SpawnFromPool(tagsDeath[i], t.position, Quaternion.identity);
        }
    }

    private void SpawnPrefabsOnHit()
    {
        for (int i = 0; i < tagsHit.Length; i++)
        {
            pool.SpawnFromPool(tagsHit[i], t.position, Quaternion.identity);
        }
    }
}
