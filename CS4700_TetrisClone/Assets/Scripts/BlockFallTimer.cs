using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFallTimer : MonoBehaviour
{

    private int level;                  //set level based on # of line clears
    private float timer;                //set timer delay based on level
    public static float alarm;          //set to timer every time it hits 0
    public int startLevel;              //what level the player starts at

    void Awake()
    {
        level = startLevel;
        SetTimer();
        alarm = timer;
    }

    void FixedUpdate()
    {
        SetTimer();
        FallSpeed();
    }

    void SetTimer()
    {
        if(level == 0) {
            timer = 0.8f;
        } else if (level > 0 && level < 9){
            timer = 0.8f - ((5f/60f)*level);
        } else if (level == 9){
            timer = 0.1f;
        } else if (level >= 10 && level < 13){
            timer = 5f / 60f;
        } else if (level >= 13 && level < 16){
            timer = 4f / 60f;
        } else if (level >= 16 && level < 19){
            timer = 3f / 60f;
        } else if (level >= 19 && level < 29){
            timer = 2f / 60f;
        } else if (level >= 29){
            timer = 1f / 60f;
        }
    }

    void FallSpeed()
    {
        alarm -= Time.deltaTime;
        if(alarm <= 0f)
        {
            Spawner.activeBlock.transform.position = new Vector3(Spawner.activeBlock.transform.position.x, Spawner.activeBlock.transform.position.y - 1, Spawner.activeBlock.transform.position.z);
            alarm = timer;
        }
       
    }

    //formula for going to the next level:
    //Mathf.Min(startLevel * 10 + 10, Mathf.Max(100, (startLevel * 10 - 50)));
    //this will give you a number which is the number of lines that have to be cleared to go to the next level
    //from then on, advance the level by 10 line clears
    //e.g. starting at level 9 means clearing 100 lines to go to level 10, then 10 lines to go to level 11
    //pros start at level 18 for this reason so they can clear 100 lines at that speed
}
