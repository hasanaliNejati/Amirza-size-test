using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invoker : MonoBehaviour
{
    public InputField inputFieldCount;
    public UIManager manager;
    public void AddItem()
    {
        manager.AddItem(int.Parse(inputFieldCount.text));
    }

}
