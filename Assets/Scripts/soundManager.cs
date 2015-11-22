using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour
{
    /*public AudioClip[] blueCrystal;
    public AudioClip[] blueCrystal;
    public AudioClip[] blueCrystal;
    public AudioClip[] blueCrystal;
    public AudioClip[] blueCrystalImpact;
    public AudioClip[] redCrystalImpact;
    public AudioClip[] yellowCrystalImpact;
    public AudioClip[] greenCrystalImpact;*/
    /*public AudioClip[] openDoor;
    public AudioClip[] closeDoor;
    public AudioClip[] grab;*/
    public AudioClip[] impactJoueur;
    public AudioClip[] wooshJoueur;

    public AudioClip[] grabObject;
    public AudioClip[] dropObject;
    public AudioClip[] pushObject;

    public AudioClip[] inhale;
    public AudioClip[] exhale;



    AudioSource impactPlayerAudioSrc;
    AudioSource wooshAudioSrc;
    AudioSource grabAudioSrc;
    AudioSource breathAudioSrc;


    public enum soundTypes
    {
        impactPlayer,
        grabObject,
        dropObject,
        pushObject,
        wooshPlayer,
        exhale,
        inhale,
    }

    //variables to handle breath
    bool inhaling = false;
    float breathInterval = 0;
    public float breathMaxInterval;
    public float breathMinInterval;
    float breathTimePassed = 0;


    // Use this for initialization
    void Start()
    {
        impactPlayerAudioSrc = GameObject.Find("impactPlayerAudioSource").GetComponent<AudioSource>();
        wooshAudioSrc = GameObject.Find("wooshAudioSource").GetComponent<AudioSource>();
        grabAudioSrc = GameObject.Find("grabAudioSource").GetComponent<AudioSource>();
        breathAudioSrc = GameObject.Find("breathAudioSource").GetComponent<AudioSource>();
    }

    private void playAlea(AudioClip[] audioClips, AudioSource audioSource)
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    void Update()
    {
        handleBreath();
    }

    void handleBreath()
    {
        breathTimePassed += Time.deltaTime;
        if( !breathAudioSrc.isPlaying && breathTimePassed > breathInterval)
        {     
            if (!inhaling)
            {
                Play(soundTypes.inhale);
                inhaling = true;
            }
            else
            {
                Play(soundTypes.exhale);
                inhaling = false;

                breathTimePassed = 0;
                breathInterval = Random.Range(breathMinInterval, breathMaxInterval);
            }
        }
    }

    public void Play(soundTypes soundToPlay, float delay = 0)
    {
        switch (soundToPlay)
        {
            case soundTypes.impactPlayer:
                this.playAlea(impactJoueur, impactPlayerAudioSrc);
                break;
            case soundTypes.wooshPlayer:
                this.playAlea(wooshJoueur, wooshAudioSrc);
                break;
            case soundTypes.grabObject:
                this.playAlea(grabObject, grabAudioSrc);
                break;
            case soundTypes.dropObject:
                this.playAlea(dropObject, grabAudioSrc);
                break;
            case soundTypes.pushObject:
                this.playAlea(pushObject, grabAudioSrc);
                break;
            case soundTypes.inhale:
                this.playAlea(inhale, breathAudioSrc);
                break;
            case soundTypes.exhale:
                this.playAlea(exhale, breathAudioSrc);
                break;
            default:
                break;
        }
    }
}