using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class InputController : MonoBehaviour
{
    //是否触发Tab事件
    public bool CanTab { get; set; }

    //点击事件
    public event Action OnTab = null;

    void Update()
    {
        if (CanTab && Input.GetMouseButtonDown(0))
        {
            if (OnTab != null)
                OnTab();
        }
    }
}