using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    public static DontDestroyOnLoad instance;

	// Use this for initialization
	void Awake () {

        if (instance != null)
        {
            Debug.LogError("Erreur : Il y a plus d'un script DontDestroyOnLoad dans la scène.");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }


        DontDestroyOnLoad(gameObject);
	}

}
