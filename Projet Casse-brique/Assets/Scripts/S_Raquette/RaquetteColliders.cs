using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Clement.Utilities.Maths;

public class RaquetteColliders : MonoBehaviour {
    
    [SerializeField] private AudioSource aud;
    [SerializeField] private float addedForce;
    [SerializeField] private Raquette raquette;


    private void OnTriggerEnter2D(Collider2D c)
    {
        //print(name);

        if(c.gameObject.tag == "Balle")
        {
            if(aud != null)
                aud.Play();

            Balle b = c.gameObject.GetComponent<Balle>();
            b.SwapX();

            //if(Maths.FastApproximate(b.MirrorY, 0f, .1f) && name != "Main")
            //{
            //    b.appliedForce = new Vector3(b.appliedForce.x, b.appliedForce.y + addedForce);
            //}
            //else
            //{

            

            float f = Mathf.Abs(raquette.forceAddedToBall);
            if(f > .2f)
            {
                if(b.MirrorY != (int)Mathf.Sign(raquette.forceAddedToBall))
                    b.MirrorY = (int)Mathf.Sign(raquette.forceAddedToBall);
            }
            else
            {
                if (b.MirrorY == 0)
                    b.MirrorY = (int)Mathf.Sign(raquette.forceAddedToBall);

                f = 1f;
            }
            b.appliedForce = new Vector3(b.appliedForce.x, f);


            //}

        }
    }
}
