using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float distanceToCamera;
    void Update()
    {
        Vector3 v3 = new Vector3(
            Mathf.Clamp(Input.mousePosition.x, 0, Camera.main.pixelWidth), 
            Mathf.Clamp(Input.mousePosition.y, 0, Camera.main.pixelHeight), 
            Camera.main.transform.position.z + distanceToCamera);
        transform.position = Camera.main.ScreenToWorldPoint(v3);
    }
}
