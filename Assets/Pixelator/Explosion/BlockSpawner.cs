using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public float interval = 4f;
    public float explosivePower = 1f;
    public Material material;
    public GameObject BlockPrefab;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            var block = Instantiate(BlockPrefab, transform.position, Quaternion.identity);
            var x = Random.Range(2, 5) * 0.25f;
            var y = Random.Range(2, 5) * 0.25f;
            var z = Random.Range(2, 5) * 0.25f;
            block.transform.localScale = new Vector3(x, y, z);
            block.GetComponent<ExplodingBlock>().explosivePower = explosivePower;
            block.GetComponent<MeshRenderer>().sharedMaterial = material;
            yield return new WaitForSeconds(interval);
        }
    }
}
