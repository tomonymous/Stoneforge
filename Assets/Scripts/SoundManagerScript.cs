//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SoundManagerScript : MonoBehaviour
//{
//    public static AudioClip matchSound, fourMatchSound, fiveMatchSound, blackSpawnSound, timerWarnSound, swapSound, goldSound, endWarnSound, switchSound, grenadeSound, startSound, slowSound, timeStopSound, blackStopSound;
//    static AudioSource audioSrc;

//    void Start()
//    {
//        matchSound = Resources.Load<AudioClip>("Short Ding");
//        fourMatchSound = Resources.Load<AudioClip>("Long Ding");
//        fiveMatchSound = Resources.Load<AudioClip>("Crystal Ding");
//        blackSpawnSound = Resources.Load<AudioClip>("Gong");
//        timerWarnSound = Resources.Load<AudioClip>("Beep Tone");
//        swapSound = Resources.Load<AudioClip>("Marble Roll");
//        goldSound = Resources.Load<AudioClip>("Gold Ding");
//        endWarnSound = Resources.Load<AudioClip>("Sad Ding");
//        switchSound = Resources.Load<AudioClip>("Switch17");
//        grenadeSound = Resources.Load<AudioClip>("Grenade");
//        startSound = Resources.Load<AudioClip>("StartTune");
//        slowSound = Resources.Load<AudioClip>("Slow Down");
//        timeStopSound = Resources.Load<AudioClip>("Time Stop");
//        blackStopSound = Resources.Load<AudioClip>("BlackStopWave");

//        audioSrc = GetComponent<AudioSource>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public static void PlayerSound (string clip)
//    {
//        switch (clip)
//        {
//            case "match":
//                audioSrc.PlayOneShot(matchSound);
//                break;
//            case "four":
//                audioSrc.PlayOneShot(fourMatchSound);
//                break;
//            case "five":
//                audioSrc.PlayOneShot(fiveMatchSound);
//                break;
//            case "black":
//                audioSrc.PlayOneShot(blackSpawnSound);
//                break;
//            case "beep":
//                audioSrc.PlayOneShot(timerWarnSound);
//                break;
//            case "swap":
//                audioSrc.PlayOneShot(swapSound);
//                break;
//            case "gold":
//                audioSrc.PlayOneShot(goldSound);
//                break;
//            case "endWarning":
//                audioSrc.PlayOneShot(endWarnSound);
//                break;
//            case "switch":
//                audioSrc.PlayOneShot(switchSound);
//                break;
//            case "bomb":
//                audioSrc.PlayOneShot(grenadeSound);
//                break;
//            case "start":
//                audioSrc.PlayOneShot(startSound);
//                break;
//            case "slow":
//                audioSrc.PlayOneShot(slowSound);
//                break;
//            case "timeStop":
//                audioSrc.PlayOneShot(timeStopSound);
//                break;
//            case "blackStop":
//                audioSrc.PlayOneShot(blackStopSound);
//                break;


//        }
//    }
//}
