using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleControl : MonoBehaviour
{
    public Camera m_MainCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
