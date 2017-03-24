﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replacement : MonoBehaviour
{

    public Shader shader;

    // Use this for initialization
    void Start()
    {
        GetComponent<Camera>().SetReplacementShader(shader, "RenderType");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
