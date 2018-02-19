using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraController : MonoBehaviour
{
	public Transform camera;
	[Range(0.005f, 0.2f)]
	public float movementSpeed;

	private MouseLook mouseLook;

	// Use this for initialization
	void Start () {
		mouseLook = new MouseLook();
		mouseLook.Init(transform, camera);
	}
	
	// Update is called once per frame
	void Update ()
	{
		var dx = Input.GetAxisRaw("Horizontal");
		var dy = Input.GetAxisRaw("Vertical");
		var dir = (dx * transform.right + dy * transform.forward);
		dir = new Vector3(dir.x, 0, dir.z).normalized;
		transform.Translate(dir * movementSpeed, Space.World);

		mouseLook.LookRotation(transform, camera);
	}
}
