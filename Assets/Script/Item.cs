using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject letter;
    public int count = 0;
    public void Init(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(letter, transform);

        }
        this.count = count;
    }

    public void SetSize(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }

}
