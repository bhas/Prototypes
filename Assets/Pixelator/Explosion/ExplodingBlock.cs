using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBlock : MonoBehaviour
{
    public bool rotate = true;
    public GameObject blockPrefab;
    public float explosivePower = 1f;

    void Start()
    {
        if (rotate)
            GetComponent<Rigidbody>().angularVelocity = Random.onUnitSphere * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Explode();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Player"))
            Explode();
    }

    private void Explode()
    {
        var mainBody = GetComponent<Rigidbody>();
        var blockSize = blockPrefab.transform.localScale.x;
        var mx = (int)(transform.localScale.x / blockSize);
        var my = (int)(transform.localScale.y / blockSize);
        var mz = (int)(transform.localScale.z / blockSize);
        var start = -transform.lossyScale / 2;
        transform.localScale = Vector3.one;
        for (var x = 0; x < mx; x++)
        {
            for (var y = 0; y < my; y++)
            {
                for (var z = 0; z < mz; z++)
                {
                    if ((x + y + z) % 3 == 0)
                        continue;

                    if (!IsEdge(x, y, z, mx, my, mz))
                        continue;

                    var obj = Instantiate(blockPrefab, transform.parent);
                    obj.transform.rotation = transform.rotation;
                    obj.transform.position = transform.TransformPoint(start + new Vector3(x, y, z) * blockSize);
                    obj.GetComponent<Rigidbody>().velocity = Random.onUnitSphere * explosivePower;
                    obj.GetComponent<MeshRenderer>().sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;
                }
            }
        }

        Destroy(gameObject);
    }

    private bool IsEdge(int x, int y, int z, int mx, int my, int mz)
    {
        return x == 0 || x == mx - 1 ||
               y == 0 || y == my - 1 ||
               z == 0 || z == mz - 1;
    }
}
