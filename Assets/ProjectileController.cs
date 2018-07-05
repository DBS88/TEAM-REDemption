using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float bulletSpeed;

    Rigidbody2D myRB;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
        if (transform.localRotation.z>0)
        // adding impule force across the x,y axis and multipling it by the speed
        myRB.AddForce(new Vector2(-1, 0) * bulletSpeed, ForceMode2D.Impulse);
        else myRB.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);
        // This code works in coherence with fireBullet() in the PlayerController_RedHood script
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
