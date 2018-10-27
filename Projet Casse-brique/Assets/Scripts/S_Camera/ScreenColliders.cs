using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenColliders : MonoBehaviour
{

    [SerializeField] private AudioSource aud;
    [SerializeField] private int ID;
    [SerializeField] private bool isOnYaxis;


    private void OnTriggerEnter2D(Collider2D c)
    {


        if (c.gameObject.tag == "Balle")
        {
            if (isOnYaxis)
            {
                if (aud != null)
                {
                    aud.Play();
                }

                c.gameObject.GetComponent<Balle>().SwapY();
                return;
            }


            Balle[] ballesEnJeu = (Balle[])FindObjectsOfType(typeof(Balle));



            if (ballesEnJeu.Length > 1)
            {
                c.gameObject.SetActive(false);
            }
            else if (ballesEnJeu.Length == 1)
            {
                ScoreManager.instance.DecreaseLives();
                ballesEnJeu[0].AttachBallToPlayer(ID);
                //print("Perdu une vie");
            }



        }
        if (c.gameObject.tag == "Projectile")
        {
            c.gameObject.SetActive(false);
        }
    }
}
