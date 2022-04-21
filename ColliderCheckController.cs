using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheckController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string stopName = "";
    public bool startCheck = false;
    public bool Check = false;
    public RouletteController stopcontroller = null;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other.gameObject.name);
        if (other.gameObject.name == stopName && startCheck)
        {
            other.gameObject.transform.parent.transform.Find("RouletteBG").GetComponent<UITexture>().color = Util.rouletteSet;

            Check = true;
            stopcontroller.StopRoulette();
        }
    }
}
