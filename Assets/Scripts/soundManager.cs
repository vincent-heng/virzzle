using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour
{
    public enum lockColor
    {
        blue,
        red,
        green,
        yellow
    }


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

    public AudioSource blueUnlock;
    public AudioSource redUnlock;
    public AudioSource yellowUnlock;
    public AudioSource greenUnlock;

    public AudioSource blueMusic;
    public AudioSource redMusic;
    public AudioSource greenMusic;
    public AudioSource yellowMusic;


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

    public void handleCrystalUnlock( int[] order )
    {
        int lastUnlocked = -1;
        for( int i =3; i >=0; i--)
        {
            if (lastUnlocked == -1 && order[i] != -1)
                lastUnlocked = i;

            int j = 3 - i;
            int first = 3 - lastUnlocked;

            switch (order[i])
            {
                case (int)lockColor.blue:
                    blueUnlock.PlayDelayed(0.75f*(j-first));
                    break;
                case (int)lockColor.red:
                    redUnlock.PlayDelayed(0.75f* (j - first));
                    break;
                case (int)lockColor.green:
                    greenUnlock.PlayDelayed(0.75f* (j - first));
                    break;
                case (int)lockColor.yellow:
                    yellowUnlock.PlayDelayed(0.75f* (j - first));
                    break;
            }
        }
        launchMusic(lastUnlocked);
    }

    void launchMusic(int i)
    {
        switch (i)
        {
            case (int)lockColor.blue:
                blueMusic.Play();
                break;
            case (int)lockColor.red:
                redMusic.Play();
                break;
            case (int)lockColor.green:
                greenMusic.Play();
                break;
            case (int)lockColor.yellow:
                yellowMusic.Play();
                break;
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