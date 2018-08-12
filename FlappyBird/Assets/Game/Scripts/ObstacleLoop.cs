using UnityEngine;
using System.Collections;

public class ObstacleLoop : MonoBehaviour
{

    //边界常亮
    public const float LEFT = -8.4f;
    public const float CENTER = -1.68f;
    public const float RIGHT = 5.04f;

    //移动速度
    public float MoveSpeed = 1.5f;

    //是否可移动
    public bool IsMove = false;

    //后续是否显示管道
    public bool IsShowPipes = false;

    //管理的两个障碍物
    public GameObject[] Obstacles;


    //设置所有管道的可见性
    public void SetPipesVisible(bool visible)
    {
        foreach (GameObject go in Obstacles)
        {
            SetPipesVisibleForObstacle(go, visible);
        }
    }

    public void SetPipesVisibleForObstacle(GameObject obstacle, bool visible)
    {
        Transform obstacleTransform = obstacle.transform;

        foreach (Transform child in obstacleTransform)
        {
            if (child.name == "Pipe")
            {
                child.gameObject.SetActive(visible);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!IsMove)
            return;
        for (int i = 0; i < Obstacles.Length; i++)
        {
            //当前障碍物
            GameObject thisObstacle = Obstacles[i];

            // 另外障碍物
            GameObject anotherObstacle = Obstacles[i == 0 ? 1 : 0];

            //当前坐标
            Vector3 localPos = thisObstacle.transform.localPosition;
            localPos.x -= MoveSpeed * Time.deltaTime;

            //越界判断
            if (localPos.x <= LEFT)
            {
                //--------------------------------------------------------------thisObstacle
                //修正X
                localPos.x = RIGHT;

                //设置新位置
                thisObstacle.transform.localPosition = localPos;

                //控制管道显示
                SetPipesVisibleForObstacle(thisObstacle, IsShowPipes);

                //-------------------------------------------------------------anotherObstacle
                //修正另外障碍物的位置
                Vector3 anotherLocalPos = anotherObstacle.transform.localPosition;
                anotherLocalPos.x = CENTER;
                anotherObstacle.transform.localPosition = anotherLocalPos;

                //退出
                break;
            }
            else
            {
                thisObstacle.transform.localPosition = localPos;
            }
        }

    }
}
