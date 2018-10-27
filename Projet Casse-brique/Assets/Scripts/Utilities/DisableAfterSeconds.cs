using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour {

    [SerializeField] private float delay;

	// Use this for initialization
	void Start () {
        StartCoroutine(Disable());
        StopCoroutine(Disable());
    }


    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }



    //private void OnDisable()
    //{
    //    StopCoroutine(Disable());
    //}



    private void OnEnable()
    {
        StopCoroutine(Disable());
        StartCoroutine(Disable());
    }
}
