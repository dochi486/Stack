using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube item;
    public int level; //블럭이 몇 개째인지 확인
    float cubeHeight;
    public float distance = 0.929f;


    // Start is called before the first frame update
    void Start()
    {
        cubeHeight = item.transform.localScale.y; //기존에 생성되어 있는 큐브의 높이
        item.gameObject.SetActive(false);
        CreateCube();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            CreateCube();
    }


    private void CreateCube()
    {

        level++;

        //레벨이 홀수일 때는 오른쪽? 짝수일 때는 왼쪽에 블럭 배치
        Vector3 startPos;
        if(level % 2 ==1) //홀수
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
    }
}
