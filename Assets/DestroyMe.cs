using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    public float aliveTime;

	// Use this for initialization
	void Awake () {
        Destroy(gameObject, aliveTime); // Whatever game object is attached will be destroyed by the float aliveTime
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
