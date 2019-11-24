using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movable
{
    public class ShiftHeadlamp : MonoBehaviour
    {
        float originalZ = 270;

        [SerializeField]
        private float angle;
        void Update()
        {
            //Vector2 startPoint = gameObject.transform.position;
            //Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 direction = (endPoint - startPoint);
            //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //if(angle > 90)
            //{
            //    transform.rotation = Quaternion.AngleAxis(90 + (angle / 10), Vector3.forward);
            //}
            //else if(angle < -90)
            //{
            //    transform.rotation = Quaternion.AngleAxis( 90 - Mathf.Abs(angle / 10), Vector3.forward);
            //}
            //else
            //{
            //    transform.rotation = Quaternion.AngleAxis(originalZ + (angle / 10), Vector3.forward);
            //}

            //perform slight shift up or down based on mouse position
        }
    }
}