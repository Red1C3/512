using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block:MonoBehaviour
{
    public int x, y;
    public GPCtrlr GP;
    public StartBlock startBlock;
    public LoadBlock loadBlock;
    public bool addable,mup,mdown,mleft,mright;
    void Start()
    {
        startBlock = gameObject.GetComponent<StartBlock>();
        GP = GameObject.Find("Script").GetComponent<GPCtrlr>();
        if (gameObject.GetComponent<StartBlock>() != null)
        {
            startBlock = gameObject.GetComponent<StartBlock>();
            x = startBlock.x;
            y = startBlock.y;
        }
        else
        {
            loadBlock = gameObject.GetComponent<LoadBlock>();
            x = loadBlock.x;
            y = loadBlock.y;
        }
    }
    private void Update()
    {
        if (GameObject.Find(x.ToString() + y.ToString()) != null)
        {
            GameObject game = GameObject.Find(x.ToString() + y.ToString());
            if (game != gameObject)
            {
                DestroyImmediate(game);
            }
        }
        
        if (GP.blocks[x, y] == false)
            {
                Destroy(gameObject);
            }
        if (mleft == true)
        {
            if (transform.position.x > -1.5 + x +0.5f)
            {
                transform.Translate(new Vector3(-0.5f, 0));
            }
            else
            {
                transform.SetPositionAndRotation(new Vector3((float)(-1.5 + x), (float)(-1.56 + y), 0f), new Quaternion());
                gameObject.GetComponent<SpriteRenderer>().sprite = GP.sprites[GP.blockspritenum[x, y]];
                
                mleft = false;
                
            }
        }
        if (mright == true)
        {
            if (transform.position.x < -1.5 + x - 0.5f)
            {
                transform.Translate(new Vector3(0.5f, 0));
            }
            else
            {
                transform.SetPositionAndRotation(new Vector3((float)(-1.5 + x), (float)(-1.56 + y), 0f), new Quaternion());
                gameObject.GetComponent<SpriteRenderer>().sprite = GP.sprites[GP.blockspritenum[x, y]];

                mright = false;

            }
        }
        if (mup == true)
        {
            if (transform.position.y < -1.56 + y - 0.5f)
            {
                transform.Translate(new Vector3(0, 0.5f));
            }
            else
            {
                transform.SetPositionAndRotation(new Vector3((float)(-1.5 + x), (float)(-1.56 + y), 0f), new Quaternion());
                gameObject.GetComponent<SpriteRenderer>().sprite = GP.sprites[GP.blockspritenum[x, y]];

                mup = false;

            }
        }
        if (mdown == true)
        {
            if (transform.position.y > -1.56 + y + 0.5f)
            {
                transform.Translate(new Vector3(0, -0.5f));
            }
            else
            {
                transform.SetPositionAndRotation(new Vector3((float)(-1.5 + x), (float)(-1.56 + y), 0f), new Quaternion());
                gameObject.GetComponent<SpriteRenderer>().sprite = GP.sprites[GP.blockspritenum[x, y]];

                mdown = false;

            }
        }
    }
    public void Moveleft()
    {
        if (x > 0)
        {
            if (GP.blocks[x - 1, y] == false)
            {
                GP.instiate = true;
                GP.blocks[x - 1, y] = true;
                GP.blockspritenum[x - 1, y] = GP.blockspritenum[x, y];
                x -= 1;
                mleft = true;
                gameObject.name = x.ToString() + y.ToString();
                GP.blocks[x + 1, y] = false;
                DestroyImmediate(GameObject.Find((x + 1).ToString() + y.ToString()));
                Moveleft();
            }
            else if (GP.blockspritenum[x - 1, y] == GP.blockspritenum[x, y] && addable==true && GameObject.Find((x - 1).ToString() + y.ToString()).GetComponent<Block>().addable==true)
            {
                GP.blocks[x - 1, y] = false;
                DestroyImmediate(GameObject.Find((x-1).ToString() + (y).ToString()));
                GP.blockspritenum[x, y] += 1;
                GP.Score += (int)Mathf.Pow(2, GP.blockspritenum[x, y] + 1);
                addable = false;
                Moveleft();
            }
        }
    }
    public void Moveright()
    {
        if (x < 3)
        {
            if (GP.blocks[x + 1, y] == false)
            {
                GP.instiate = true;
                GP.blocks[x + 1, y] = true;
                GP.blockspritenum[x + 1, y] = GP.blockspritenum[x, y];
                x += 1;
                mright = true;
                gameObject.name = x.ToString() + y.ToString();
                GP.blocks[x - 1, y] = false;
                DestroyImmediate(GameObject.Find((x - 1).ToString() + y.ToString()));
                Moveright();
            }
            else if (GP.blockspritenum[x + 1, y] == GP.blockspritenum[x, y] && addable == true && GameObject.Find((x + 1).ToString() + y.ToString()).GetComponent<Block>().addable == true)
            {
                GP.blocks[x + 1, y] = false;
                DestroyImmediate(GameObject.Find((x+1).ToString() + (y).ToString()));
                GP.blockspritenum[x, y] += 1;
                GP.Score += (int)Mathf.Pow(2, GP.blockspritenum[x, y] + 1);
                addable = false;
                Moveright();
            }



        }
    }
    public void Movedown()
    {
        if (y > 0)
        {
            if (GP.blocks[x , y-1] == false)
            {
                GP.instiate = true;
                GP.blocks[x , y-1] = true;
                GP.blockspritenum[x , y-1] = GP.blockspritenum[x, y];
                y -= 1;
                mdown = true;
                gameObject.name = x.ToString() + y.ToString();
                GP.blocks[x , y+1] = false;
                DestroyImmediate(GameObject.Find((x ).ToString() + (y+1).ToString()));
                Movedown();
            }
            else if (GP.blockspritenum[x , y-1] == GP.blockspritenum[x, y] && addable == true && GameObject.Find((x ).ToString() + (y-1).ToString()).GetComponent<Block>().addable == true)
            {

                GP.blocks[x , y-1] = false;
                DestroyImmediate(GameObject.Find((x).ToString() + (y - 1).ToString()));
                GP.blockspritenum[x, y] += 1;
                GP.Score += (int)Mathf.Pow(2, GP.blockspritenum[x, y] + 1);
                addable = false;
                Movedown();
            }



        }
    }
    public void Moveup()
    {
        if (y < 3)
        {
            if (GP.blocks[x , y+1] == false)
            {
                mup = true;

                GP.instiate = true;
                GP.blocks[x , y+1] = true;
                GP.blockspritenum[x , y+1] = GP.blockspritenum[x, y];
                y += 1;
                gameObject.name = x.ToString() + y.ToString();
                GP.blocks[x , y-1] = false;
                DestroyImmediate(GameObject.Find((x ).ToString() + (y-1).ToString()));
                Moveup();
            }
            else if (GP.blockspritenum[x , y+1] == GP.blockspritenum[x, y] && addable == true && GameObject.Find((x).ToString() + (y+1).ToString()).GetComponent<Block>().addable == true)
            {
                GP.blocks[x , y+1] = false;
                DestroyImmediate(GameObject.Find((x ).ToString() + (y+1).ToString()));
                GP.blockspritenum[x, y] += 1;
                GP.Score += (int)Mathf.Pow(2, GP.blockspritenum[x, y] + 1);
                addable = false;
                Moveup();
            }



        }
    }
   
}
