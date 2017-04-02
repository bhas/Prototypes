using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{

    private CharacterController controller;

	// Use this for initialization
	void Start ()
	{
	    controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var dx = Input.GetAxis("Horizontal");
	    var dz = Input.GetAxis("Vertical");

	    controller.SimpleMove(new Vector3(dx, 0, dz));
	}
}
