using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{

    Vector3 desPoint;   //목표지점
    public Vector3 startPoint; //시작지점

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        desPoint = new Vector3(-startPoint.x, startPoint.y, -startPoint.z);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsTime = Time.time - startTime; //흐른 시간
        float time = Mathf.Abs(elapsTime % 2 - 1f);
        Vector3 pos = Vector3.Lerp(desPoint, startPoint, time);
        transform.position = pos;
    }
}
