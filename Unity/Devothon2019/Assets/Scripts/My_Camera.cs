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

        camera.orthographicSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Debug.Log("Cannot find player");
            return;
        }

        float cameraOffsetSize = camera.orthographicSize / 2;

        Vector3 camPos = target.position + new Vector3(0, 0, -10);

        if(leftSide != null && rightSide != null)
            camPos.x = Mathf.Clamp(camPos.x, leftSide.transform.position.x + cameraOffsetSize, rightSide.transform.position.x - cameraOffsetSize);
        if (topSide != null && bottomSide != null)
            camPos.y = Mathf.Clamp(camPos.y, topSide.transform.position.y - cameraOffsetSize, bottomSide.transform.position.y + cameraOffsetSize);


        transform.position = target.position + new Vector3(0, 0, -10);
    }

}
