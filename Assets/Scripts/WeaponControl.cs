using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public Camera m_MainCamera = null;
    private bool direction = false;
    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
        direction = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Code used is from following this tutorial: https://www.youtube.com/watch?v=47xWM1RcY3Y

        // Rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5f;

        Vector3 objectPos = m_MainCamera.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle > 0f && angle < 100f || angle < 0f && angle > -90f)
        {
            if (direction == false)
            {
                direction = true;
                Flip();
            }
        }

        if (angle > 100f && angle < 180f || angle < -90f && angle > -180f)
        {
            if (direction == true)
            {
                direction = false;

                Flip();
            }
        }
    }

    public void Flip()
    {
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;

        Vector3 flipScaleParent = transform.parent.localScale;
        flipScaleParent.x *= -1;
        transform.parent.localScale = flipScaleParent;
    }
}
