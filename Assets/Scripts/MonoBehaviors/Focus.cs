using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    public enum FocusState
    {
        Default,
        Process,
        Finish,
    };

    public int counter = 0;
    public Player player;
    public FocusState fState = FocusState.Default;
    private int fsHash;
    private int ffHash;
    public Animator playerAN;
    public float healtphase = 32f;
    public float animationDelayer = 0.5f;

    void Start()
    {
        player = GetComponent<Player>();
        playerAN = GetComponent<Animator>();
        fsHash = Animator.StringToHash("FocusStart");
        ffHash = Animator.StringToHash("FocusFinish");
        healtphase /= animationDelayer;
    }

    public void focus() 
    {
        
        switch (fState)
        {
            case FocusState.Default:
                if (player.playerController.inputFocusDown && player.soul > 0f)
                {
                    playerAN.SetTrigger(fsHash);
                    fState = FocusState.Process;
                }
                break;
            case FocusState.Process:
                
                if (player.playerController.inputFocus && player.soul > 0f)
                {
                    //Debug.Log("AAA");
                    player.decreaseSoul(1 * animationDelayer);
                    counter++;
                    if (counter > healtphase)
                    {
                        player.heal(1);
                        counter = 0;
                        fState = FocusState.Finish;
                    }
                }
                else 
                {
                    //counter++;
                    
                    
                    fState = FocusState.Finish;
                    counter = 0;
                }
                break;
            case FocusState.Finish:
                playerAN.SetTrigger(ffHash);
                fState = FocusState.Default;
                break;
        }
    }
}