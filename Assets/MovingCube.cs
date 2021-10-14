using UnityEngine;

public class MovingCube : MonoBehaviour
{

    Vector3 desPoint;   //목표지점
    Vector3 startPoint; //시작지점
    internal Vector3 pivot;

    float startTime;

    void Start()
    {
        startPoint = transform.position;
        desPoint = new Vector3(-startPoint.x, startPoint.y, -startPoint.z);
        startTime = Time.time;
    }

    void Update()
    {
        float elapsTime = Time.time - startTime; //흐른 시간
        float time = Mathf.Abs(elapsTime % 2 - 1f);
        Vector3 pos = Vector3.Lerp(desPoint, startPoint, time);
        transform.position = pos;
    }
}
