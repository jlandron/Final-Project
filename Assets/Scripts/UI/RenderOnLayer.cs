using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOnLayer : MonoBehaviour
{
    public string layer;

    private void Awake()
    {
        GetComponent<MeshRenderer>().sortingLayerName = layer;
        GetComponent<MeshRenderer>().sortingOrder = 0;
    }
}
