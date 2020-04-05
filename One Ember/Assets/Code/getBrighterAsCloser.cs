using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script checks the distance between itself and the player and sets its brightness and scale accordingly.
/// </summary>
public class getBrighterAsCloser : MonoBehaviour
{

    [SerializeField]
    private Transform player;

    [SerializeField]
    private SpriteRenderer spriteRend;

    [SerializeField]
    private MainLevelManager levelMan;

    private bool calledYet;

    private float increasing;

    public bool victory;

    public getBrighterAsCloser pulseObj;

    private bool pulsing;

    private float pulseNumber;

    [SerializeField]
    private bool needsToPulse;

    [SerializeField]
    private bool hasPulsedYet;
    // Start is called before the first frame update
    void Start()
    {
        //  pulse();

        if (needsToPulse == true && hasPulsedYet == false)
        {
            spriteRend.enabled = false;  
        }
    }

    void Update()
    {
      

        if (needsToPulse == true && hasPulsedYet == false) return;
        float distance = Vector3.Distance(player.position, transform.position);
        if (calledYet == false)
        {
            Color mainCol = Color.Lerp(Color.white, Color.black, distance / 25);
            spriteRend.color = mainCol;
        }
        else
        {
            if(increasing < 25) increasing += 1.5f;
            else
            {
                spriteRend.enabled = false;
            }
            transform.localScale = new Vector3(10 + increasing,10 + increasing, 10 + increasing);
            Color mainCol = Color.Lerp(Color.white, Color.black, increasing / 30);
            if (increasing / 30 < 1f)
            {
                spriteRend.color = mainCol;
            }

        }
        if(calledYet == false && distance < 5)
        {
            calledYet = true;
            if (victory)
            {
                
                StartCoroutine(waitToLeave2());
                Debug.Log("game won");
            }
            else
            {
                // show next point.
                pulseObj.pulse();
            }
        }
    }

 
    public IEnumerator waitToLeave2()
    {
        levelMan.hasWon = true;
        yield return new WaitForSeconds(2.5f);
        Debug.Log("game won leave");
        levelMan.nextLevel();
    }


    IEnumerator waitToFinishPulse()
    {
        yield return new WaitForSeconds(1f);
        iTween.ValueTo(gameObject, iTween.Hash(
        "from", 1,
        "to", 0,
        "time", 1f,
        "onupdatetarget", gameObject,
        "onupdate", "callBackPulse",
        "easetype", iTween.EaseType.easeOutQuad)
        );
    }


    /// <summary>
    ///  briefly reveal this lights location so the player can move towords it.
    /// </summary>
    public void pulse()
    {
        Debug.Log("pulse");
        hasPulsedYet = true;
        spriteRend.enabled = true;
        iTween.ValueTo(gameObject, iTween.Hash(
        "from", 0,
        "to", 1,
        "time", 1f,
        "onupdatetarget", gameObject,
        "onupdate", "callBackPulse",
        "easetype", iTween.EaseType.easeOutQuad)
        );
        StartCoroutine(waitToFinishPulse());
    }

    public void callBackPulse (float value)
    {
        Color mainCol = Color.Lerp(Color.black, Color.white, value) ;
        spriteRend.color = mainCol;
    }


}
