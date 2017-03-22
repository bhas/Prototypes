using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    private const float VIEWPORT_WIDTH = 20f;
    private const float IMAGE_WIDTH = 20.48f;
    private const float BOUND = (VIEWPORT_WIDTH + IMAGE_WIDTH) / 2f;

    private const float playerSpeed = 2.5f;
    public Transform image1;
    public Transform image2;
    public float speedFactor = 1f;
	
	// Update is called once per frame
	void Update ()
	{
        // move whole layer
	    var dx = Input.GetAxis("Horizontal");
	    dx = 1;
        transform.Translate(-dx * speedFactor * playerSpeed * Time.deltaTime, 0, 0);

        // correct images
        CorrectImagePosition(image1);
        CorrectImagePosition(image2);

    }

    private void CorrectImagePosition(Transform image)
    {
        if (image.position.x < -BOUND)
            image.Translate(2 * IMAGE_WIDTH, 0, 0);
        else if (image.position.x > BOUND)
            image.Translate(-2 * IMAGE_WIDTH, 0, 0);
    }
}
