using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube item;
    public int level; //블럭이 몇 개째인지 확인
    float cubeHeight;
    public float distance = 0.929f;
    public Color nextColor;
    public Transform baseCube;

    void Start()
    {
        cubeHeight = item.transform.localScale.y; //기존에 생성되어 있는 큐브의 높이
        item.gameObject.SetActive(false);
        nextColor = item.GetComponent<Renderer>().sharedMaterial.GetColor("_ColorTop");

        CreateCube();
    }

    Transform topCubeTr;
    Transform brokenCubeTr;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            BreakCube(); //큐브 부수기
            CreateCube();
        }
    }

    MovingCube previousCube;

    private void BreakCube()
    {
        if (previousCube == null)
            return;
        //previousCube //지정 영역을 벗어나면 큐브가 부서지게한다

        //스케일, 포지션을 구해서 부순다?
        Vector3 newCubeScale, newCubePos;

        newCubeScale = new Vector3(topCubeTr.localScale.x - Mathf.Abs(brokenCubeTr.localPosition.x - topCubeTr.localRotation.x),
            brokenCubeTr.localScale.y, 
            topCubeTr.localScale.z - Math.Abs(brokenCubeTr.localPosition.z - topCubeTr.localRotation.z));

        newCubePos = Vector3.Lerp(brokenCubeTr.position, topCubeTr.position, 0.5f) + Vector3.up * cubeHeight;
        //newCubeScale.
        brokenCubeTr.localScale = newCubeScale;
        brokenCubeTr.position = newCubePos;
        brokenCubeTr.GetComponent<MovingCube>().enabled = false;
        brokenCubeTr.name = "깨진 큐브";
        //var currentCube = previousCube;
    }


    public float h;

    private void CreateCube()
    {
        level++;
        //레벨이 홀수일 때는 오른쪽, 짝수일 때는 왼쪽에 블럭 배치
        Vector3 startPos;
        if (level % 2 == 1) //홀수
        {
            //오른쪽은 음수
            startPos = new Vector3(distance, level * cubeHeight, distance); //z축은 항상 양수?
        }
        else
        {
            startPos = new Vector3(-distance, level * cubeHeight, distance);
        }
        var newCube = Instantiate(item, startPos, item.transform.rotation);

        newCube.transform.parent = item.transform.parent;

        if (brokenCubeTr != null)
            newCube.pivot = brokenCubeTr.transform.position;

        newCube.gameObject.SetActive(true);

        //매테리얼 색 타입 HSV로 바꾸고 Hue(채도) 레벨마다 변경
        Color.RGBToHSV(nextColor, out h, out float s, out float v);

        nextColor = Color.HSVToRGB(h + 1f / 256 * colorChangeStep, s, v);
        newCube.GetComponent<Renderer>().material.SetColor("_ColorTop", nextColor);
        newCube.GetComponent<Renderer>().material.SetColor("_ColorBottom", nextColor);

        Camera.main.transform.Translate(0, cubeHeight, 0, Space.World);

        topCubeTr = brokenCubeTr;
        brokenCubeTr = newCube.transform;
    }
    public float colorChangeStep = 2f; //색 변하는 단계
}
