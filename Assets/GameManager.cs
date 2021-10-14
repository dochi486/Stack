using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube item;
    public int level; //블럭이 몇 개째인지 확인
    float cubeHeight;
    public float distance = 0.929f;
    public Color nextColor;

    void Start()
    {
        cubeHeight = item.transform.localScale.y; //기존에 생성되어 있는 큐브의 높이
        item.gameObject.SetActive(false);
        nextColor = item.GetComponent<Renderer>().sharedMaterial.GetColor("_ColorTop");
        CreateCube();
    }

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
        var currentCube = previousCube;
        
    }

    public float h;

    private void CreateCube()
    {
        level++;
        //레벨이 홀수일 때는 오른쪽? 짝수일 때는 왼쪽에 블럭 배치
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
        newCube.gameObject.SetActive(true);

        //매테리얼 색 타입 HSV로 바꾸고 Hue(채도) 레벨마다 변경
        Color.RGBToHSV(nextColor, out h, out float s, out float v);

        nextColor = Color.HSVToRGB(h + 1f/256 * colorChangeStep, s, v);
        newCube.GetComponent<Renderer>().material.SetColor("_ColorTop", nextColor);
        newCube.GetComponent<Renderer>().material.SetColor("_ColorBottom", nextColor);

        Camera.main.transform.Translate(0, cubeHeight, 0, Space.World);

        previousCube = newCube;
    }
    public float colorChangeStep = 2f; //색 변하는 단계
}
