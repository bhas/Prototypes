using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParticle : MonoBehaviour
{
    private const float fadeDuration = 0.2f;
    private const float minDelay = 2.5f;
    private const float maxDelay = 2.8f;

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(Fade());
	}

    private IEnumerator Fade()
    {
        // initial delay
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

        // fade animation
        var mat = GetComponent<MeshRenderer>().material;
        var color = mat.color;
        var t = 0f;
        while (t < 1)
        {
            color.a = 1 - t;
            mat.color = color;
            t += Time.deltaTime / fadeDuration;
            yield return null;
        }

        Destroy(gameObject);
    }
}
