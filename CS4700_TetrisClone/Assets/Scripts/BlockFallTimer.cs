using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFallTimer : MonoBehaviour
{

    public int level;                   //set level based on # of line clears
    private float timer;                //set timer delay based on level
    private float alarm;                //set to timer every time it hits 0

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        level = 9;
    }

    void FixedUpdate()
    {
        if (Application.targetFrameRate != 60)
        {
            Application.targetFrameRate = 60;
        }
        Debug.Log((int)1 / Time.deltaTime);
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

}
