using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject TopPipe;
    public GameObject BottomPipe;
    float gap { get; set; }
    public bool isScored { get; set; } = false;

    public void SetGap(float gap)
    {
        this.gap = gap;
        TopPipe.transform.position = new Vector3(TopPipe.transform.position.x, TopPipe.transform.position.y + gap / 2f, TopPipe.transform.position.z);
        BottomPipe.transform.position = new Vector3(BottomPipe.transform.position.x, BottomPipe.transform.position.y - gap / 2f, BottomPipe.transform.position.z);
    }
}
