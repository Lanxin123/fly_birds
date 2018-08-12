using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
    void Start()
    {
        iTween.MoveBy(
            gameObject,
            iTween.Hash(
                "y", 10,
                "easeType", iTween.EaseType.linear,
                "loopType", iTween.LoopType.pingPong,
                "time", .5f)
            );
    }
}