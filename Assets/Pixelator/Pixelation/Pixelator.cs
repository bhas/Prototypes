using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixelator : MonoBehaviour
{
    public const float pixelSize = 0.125f;


	// Use this for initialization
	void Start ()
	{
	    var bounds = GetComponent<MeshFilter>().mesh.bounds;
	    var size = bounds.extents * 2;

	    for (var x = bounds.min.x; x < bounds.max.x; x += pixelSize)
	    {
            for (var y = bounds.min.y; y < bounds.max.y; y += pixelSize)
            {
                var z = bounds.min.z - 1;
                var ray = new Ray(new Vector3(x, y, z), Vector3.forward);
                var hits = Physics.RaycastAll(ray, size.z + 2);

                foreach (var hit in Physics.RaycastAll(ray, size.z + 2))
                {
                    if (hit.normal.z > 0)
                    {
                        // backside hit
                        print("back");
                    }
                    else
                    {
                        // front side hit
                        print("front");
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateMesh()
    {
        
    }

    private void AddFace(Vector3 position, Vector3 normal)
    {
        
    }
}
