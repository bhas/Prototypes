using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Element : MonoBehaviour
{
    public int ElementID;
    public Color32 color;

	// Use this for initialization
	void Start ()
	{
	    var mesh = GetComponent<MeshFilter>().mesh;
	    var vertexCount = mesh.vertices.Length;
	    mesh.colors32 = Enumerable.Repeat(color, vertexCount).ToArray();
	    mesh.uv = Enumerable.Repeat(new Vector2(ElementID, 0), vertexCount).ToArray();
	}
}
