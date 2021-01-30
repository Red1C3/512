using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
{
    public int x, y;
    public GPCtrlr GP;
    public Block block;
    void Awake()
    {
        transform.localScale = new Vector3(0, 0);
        block = gameObject.GetComponent<Block>();
        block.addable = true;
        GP = GameObject.Find("Script").GetComponent<GPCtrlr>();
        x = Random.Range(0, 4);
        y = Random.Range(0, 4);
        if (GP.blocks[x, y] != false)
        {
            Awake();
        }

        transform.SetPositionAndRotation(new Vector3((float)(-1.5 + x), (float)(-1.56 + y), 0f), new Quaternion());
        gameObject.name = x.ToString() + y.ToString();
        GP.blocks[x, y] = true;
        int twoorfour = Random.Range(0, 2);
        GP.blockspritenum[x, y] = twoorfour;
        gameObject.GetComponent<SpriteRenderer>().sprite = GP.sprites[GP.blockspritenum[x, y]];
    }
    private void Update()
    {
        if (transform.localScale.x < 1f)
        {
            transform.localScale += new Vector3(0.1f, 0.1f);
        }
    }


}
