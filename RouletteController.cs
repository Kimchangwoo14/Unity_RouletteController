using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float rotSpeed = 0;
    float tweenspeed = 0;
    public bool bStart = false;
    public GameObject arrow = null;
    public string stopname = "";
    public string rewardindex = "";
    // Update is called once per frame
    void Update()
    {
        if (bStart)
        {
            if (ProcessButton.instance.nowTab != 5)
            {
                StopRoulette();
            }

            transform.Rotate(0, 0, this.rotSpeed);
            if (this.rotSpeed > 0.5)
                this.rotSpeed *= 0.99f;//감속
            if ((int)this.rotSpeed == 7)
                this.tweenspeed = 0.2f;
            if ((int)this.rotSpeed == 5)
                this.tweenspeed = 0.5f;
            if ((int)this.rotSpeed == 3)
                this.tweenspeed = 0.8f;
            if ((int)this.rotSpeed == 1)
            {
                this.tweenspeed = 1.0f;
            }
            if (this.rotSpeed <= 0.5)
            {
                this.tweenspeed = 1.5f;
                if (!arrow)
                    StopRoulette();
                else
                {
                    if (!arrow.GetComponent<ColliderCheckController>().startCheck)
                    {
                        arrow.GetComponent<ColliderCheckController>().startCheck = true;
                    }
                }
            }

            if (arrow)
                arrow.GetComponent<TweenRotation>().duration = tweenspeed;

        }
    }

    public void StartRoulette()
    {
        if (this.bStart)
            return;

        this.rotSpeed = 10;
        this.tweenspeed = 0.1f;
        this.bStart = true;
        if (arrow)
        {
            arrow.GetComponent<ColliderCheckController>().startCheck = false;
            arrow.GetComponent<ColliderCheckController>().Check = false;
            arrow.GetComponent<ColliderCheckController>().stopcontroller = this;
            arrow.GetComponent<ColliderCheckController>().stopName = stopname;
            arrow.GetComponent<TweenRotation>().enabled = true;
            SoundManager.Instance.Play(SoundType.SE_ROULETTE);
        }
            
    }

    public void StopRoulette()
    {
        this.rotSpeed = 0;
        this.bStart = false;
        
        if (arrow)
        {
            arrow.GetComponent<ColliderCheckController>().startCheck = false;
            arrow.GetComponent<ColliderCheckController>().Check = false;
            arrow.GetComponent<ColliderCheckController>().stopcontroller = null;
            arrow.GetComponent<ColliderCheckController>().stopName = "";
            arrow.GetComponent<TweenRotation>().enabled = false;
            SoundManager.Instance.StopSE(SoundType.SE_ROULETTE);
            SoundManager.Instance.Play(SoundType.SE_Fedex);
        }
        int.TryParse(rewardindex, out int _rewardindex);
        string rt = GameDataManager.Zeventroulette[_rewardindex, (int)GameDataManager.ZeventRoulette_.rt];
        string rv = GameDataManager.Zeventroulette[_rewardindex, (int)GameDataManager.ZeventRoulette_.rv];
        ProcessButton.instance.ZEventReturnRouletteResultEffect(rt, rv);

    }
}
