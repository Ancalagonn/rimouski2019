using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Camera : MonoBehaviour
{
    private Camera camera;
    private Transform target;

    public GameObject topSide;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject bottomSide;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        target = FindObjectOfType<Player_Stat>().transform;

        camera.orthographicSize = 15;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            return;
        }

        float cameraOffsetSizeY = camera.orthographicSize;
        float cameraOffsetX = cameraOffsetSizeY * 16f/9f;

        Vector3 camPos = target.position + new Vector3(0, 0, -10);

        if(leftSide != null && rightSide != null)
            camPos.x = Mathf.Clamp(camPos.x, leftSide.transform.position.x + cameraOffsetX, rightSide.transform.position.x - cameraOffsetX);

        if (topSide != null && bottomSide != null)
            camPos.y = Mathf.Clamp(camPos.y, bottomSide.transform.position.y + cameraOffsetSizeY, topSide.transform.position.y - cameraOffsetSizeY);


        transform.position = camPos;
    }

}
