using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBlock : MonoBehaviour
{
    public int x, y;
    public GPCtrlr GP;
    public Block block;
    void Awake()
    {
        block = gameObject.GetComponent<Block>();
        block.addable = true;
        GP = GameObject.Find("Script").GetComponent<GPCtrlr>();
        x = GP.lx;
        y = GP.ly;
        transform.SetPositionAndRotation(new Vector3((float)(-1.5 + x), (float)(-1.56 + y), 0f), new Quaternion());
        gameObject.name = x.ToString() + y.ToString();
        GP.blocks[x, y] = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = GP.sprites[GP.blockspritenum[x, y]];
    }
}
