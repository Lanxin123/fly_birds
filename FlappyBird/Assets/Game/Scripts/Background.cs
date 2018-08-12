//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Background : MonoBehaviour
{
    //图片数组
    public Sprite[] Images; //2

    //随机显示
    public void RandomShow()
    {
        //随机索引
        int randomIndex = Random.Range(0, Images.Length);
        //随机图片
        Sprite image = Images[randomIndex];
        //获取组件
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //设置图片
        renderer.sprite = image;
    }
}