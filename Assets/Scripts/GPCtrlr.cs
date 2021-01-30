using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPCtrlr : MonoBehaviour
{
    public bool[,] blocks = new bool[4,4];
    public int[,] blockspritenum = new int[4, 4];
    public Sprite[] sprites=new Sprite[23];
    public GameObject defaultblock,loadedblock;
    public int lx, ly,ls;
    public int[] blockstate = new int[16];
    public bool instiate,over;
    public int Score,HighScore,SavedScore;
    public Text scoretxt,highscoretxt,gameovertxt;
    byte goalpha;
    public Button resetbutton;
    Vector2 startvec, endvec, movevec;
    public GameObject barrier;
    void Start()
    {
        goalpha = 0;
        over = false;
        SavedScore = 0;
        Score = 0;
        HighScore = 0;
        Loadstate();
        resetbutton.onClick.AddListener(ClearState);
        if (FirstTime() == true)
        {
            Instantiate(defaultblock);
            Instantiate(defaultblock);
            Instantiate(defaultblock);

        }
    }
    
    public void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            Instantiate(defaultblock);

        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startvec = Input.GetTouch(0).position;
            }
            if(startvec.y< barrier.GetComponent<Transform>().position.y) { 
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    endvec = Input.GetTouch(0).position;
                    movevec = endvec - startvec;
                    if (Mathf.Abs(movevec.x) > Mathf.Abs(movevec.y))
                    {
                        if (movevec.x > 0)
                        {
                            Moveright();
                        }
                        else if (movevec.x < 0) { Moveleft(); }
                    }
                    else if (Mathf.Abs(movevec.x) < Mathf.Abs(movevec.y))
                    {
                        if (movevec.y > 0)
                        {
                            Moveup();
                        }
                        else if (movevec.y < 0)
                        {
                            Movedown();
                        }

                    }
                }
            }
        }
        if (Input.GetKeyDown("a"))
        {
            Moveleft();
        }
        if (Input.GetKeyDown("d"))
        {
            Moveright();
        }
        if (Input.GetKeyDown("s"))
        {
            Movedown();
        }
        if (Input.GetKeyDown("w"))
        {
            Moveup();
        }
        RefreshAddable();
        if (Input.GetKeyDown("m"))
        {
            Loadstate();
        }
        if (Input.GetKeyDown("l"))
        {
            Savestate();
        }
        if (Input.GetKeyDown("n"))
        {
            ClearState();
        }
        if (BlocksState() == true)
        {
            if (CheckMoveablility() == false)
            {
                over = true;
            }
        }
        scoretxt.text = Score.ToString();
        highscoretxt.text = HighScore.ToString();
        gameovertxt.color = new Color32(255, 0, 11, (byte)goalpha);
        if (over==true)
        {
            if (goalpha < 255)
            {
                goalpha += 15;
            }
        }
        
    }
    
    bool CheckMoveablility()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                if (x != 0)
                {
                    if (blockspritenum[x-1, y] == blockspritenum[x, y])
                    {
                        return true;
                    }
                }
                if (x != 3)
                {
                    if (blockspritenum[x + 1, y] == blockspritenum[x, y])
                    {
                        return true;
                    }
                }
                if (y != 0)
                {
                    if (blockspritenum[x, y-1] == blockspritenum[x, y])
                    {
                        return true;
                    }
                }
                if (y != 3)
                {
                    if (blockspritenum[x, y + 1] == blockspritenum[x, y])
                    {
                        return true;
                    }
                }
                x += 1;
            }
            x = 0;
            y += 1;
        }
        return false;
    }
    bool BlocksState()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                if (blocks[x, y] == false)
                {
                    return false;
                }
                x += 1;
            }
            x = 0;
            y += 1;
        }
        return true;
    }
    bool FirstTime()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                if (blocks[x, y] == true)
                {
                    return false;
                }
                x += 1;
            }
            x = 0;
            y += 1;
        }
        return true;
    }
   
    void ClearState()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                blocks[x, y] = false;
                x += 1;
            }   
                x = 0;
                y += 1;
        }
        SavedScore = 0;
        Score = 0;
        Instantiate(defaultblock);
        Instantiate(defaultblock);
        Instantiate(defaultblock);
        over = false;
        goalpha = 0;
    }
    void Loadstate()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                int state;
                state = 0;
                state=PlayerPrefs.GetInt(x.ToString() + y.ToString(), state);
                if (state == 1)
                {
                    lx = x;
                    ly = y;
                    blockspritenum[lx, ly] = PlayerPrefs.GetInt("c" + x.ToString() + y.ToString(), blockspritenum[x, y]);
                    blocks[lx, ly] = true;
                    Instantiate(loadedblock);
                }
                
                x += 1;
            }
            x = 0;
            y += 1;
        }
        SavedScore = PlayerPrefs.GetInt("savedscore", SavedScore);
        if (SavedScore != 0)
        {
            Score = SavedScore;
        }
        else { Score = 0; }
        HighScore = PlayerPrefs.GetInt("highscore", HighScore);

    }
    void Savestate()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                int state;
                if (blocks[x, y] == false) { state = 0; } else {
                    state = 1;
                    PlayerPrefs.SetInt("c" + x.ToString() + y.ToString(), blockspritenum[x, y]);
                }
                PlayerPrefs.SetInt(x.ToString() + y.ToString(), state);
                x += 1;
            }
            x = 0;
            y += 1;
        }
        SavedScore = Score;
        PlayerPrefs.SetInt("savedscore", SavedScore);
        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("highscore", HighScore);
        }
        PlayerPrefs.Save();

    }
    void Moveleft()
    {
        
            instiate = false;
            int x, y;
            x = 0;
            y = 0;
            while (y < 4)
            {
                while (x < 4)
                {
                    if (GameObject.Find(x.ToString() + y.ToString()) != null)
                    {
                        GameObject.Find(x.ToString() + y.ToString()).GetComponent<Block>().Moveleft();
                    }
                    x += 1;
                }
                x = 0;
                y += 1;
            }
            if (instiate == true)
            {
                Instantiate(defaultblock);
            }
            Savestate();

        
    }
    void Moveright()
    {
        
            instiate = false;
            int x, y;
            x = 3;
            y = 0;
            while (y < 4)
            {
                while (x > -1)
                {
                    if (GameObject.Find(x.ToString() + y.ToString()) != null)
                    {
                        GameObject.Find(x.ToString() + y.ToString()).GetComponent<Block>().Moveright();
                    }
                    x -= 1;
                }
                x = 3;
                y += 1;
            }
            if (instiate == true)
            {
                Instantiate(defaultblock);
            }
            Savestate();
        
    }
    void Movedown()
    {
        
      
            instiate = false;
            int x, y;
            x = 0;
            y = 0;
            while (x < 4)
            {
                while (y < 4)
                {
                    if (GameObject.Find(x.ToString() + y.ToString()) != null)
                    {
                        GameObject.Find(x.ToString() + y.ToString()).GetComponent<Block>().Movedown();
                    }
                    y += 1;
                }
                y = 0;
                x += 1;
            }
            if (instiate == true)
            {
                Instantiate(defaultblock);
            }
            Savestate();
        
    }
    void Moveup()
    {
        
            instiate = false;
            int x, y;
            y = 3;
            x = 0;
            while (x < 4)
            {
                while (y > -1)
                {
                    if (GameObject.Find(x.ToString() + y.ToString()) != null)
                    {
                        GameObject.Find(x.ToString() + y.ToString()).GetComponent<Block>().Moveup();
                    }
                    y -= 1;
                }
                y = 3;
                x += 1;
            }
            if (instiate == true)
            {
                Instantiate(defaultblock);
            }
            Savestate();
        
    }
    void RefreshAddable()
    {
        int x, y;
        x = 0;
        y = 0;
        while (y < 4)
        {
            while (x < 4)
            {
                if (GameObject.Find(x.ToString() + y.ToString()) != null)
                {
                    GameObject.Find(x.ToString() + y.ToString()).GetComponent<Block>().addable = true;
                }
                x += 1;
            }
            x = 0;
            y += 1;
        }
    }
    
}
