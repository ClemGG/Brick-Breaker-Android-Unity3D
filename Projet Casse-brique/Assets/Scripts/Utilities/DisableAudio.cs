using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAudio : MonoBehaviour {

    [SerializeField] private AudioSource aud;

	// Use this for initialization
	void OnEnable () {
        StopCoroutine(Disable(aud.clip.length));
        StartCoroutine(Disable(aud.clip.length));
	}

    void OnDisable () {
        StopCoroutine(Disable(aud.clip.length));
	}

    private IEnumerator Disable(float length)
    {
        aud.Play();
        yield return new WaitForSecondsRealtime(length);
        gameObject.SetActive(false);
    }
}
