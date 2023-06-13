using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] musicLayers; 
    public AudioSource menuAudioSource;
    public AudioSource enemyDeadAudioSource;
    private int layerIndex;
    private bool fadingIn = false;
    private bool fadeOut = false;
    public float audioFadeInDelay;
    public float audioFadeInStep;
    private float audioTimer = 0;
    private float audioFadeOutTime = 0;
    public float maxVolume;
    public AudioClip[] enemyDieSound;
    public AudioClip failSound;

    void Start()
    {
        this.layerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (fadingIn) {
            audioTimer += Time.deltaTime;
            if ((audioTimer > audioFadeInDelay)){
                  FadeInMusic();
                  audioTimer = 0;
            }
        }

        if(fadeOut){
            audioFadeOutTime+= Time.deltaTime;
            if ((audioFadeOutTime > audioFadeInDelay)){
                  FadeOutMenuMusic();
                  audioFadeOutTime = 0;
            }
        }
    }

    public void PlayNextLayer(){
        if(this.layerIndex < this.musicLayers.Length) {
            this.musicLayers[this.layerIndex].mute = false;
            
            this.fadingIn = true;
        }
      
        
    }

    public void FadeOutMenuMusic(){

        float auxVolume = this.menuAudioSource.volume - audioFadeInStep;
        auxVolume = (auxVolume <= 0) ? 0 : auxVolume;
        this.menuAudioSource.volume = auxVolume;

        if (this.menuAudioSource.volume == 0) {
                this.fadeOut = false;
                PlayNextLayer();
            }
    }

    public void FadeInMusic(){
        float auxVolume = this.musicLayers[this.layerIndex].volume+ audioFadeInStep;
        auxVolume = (auxVolume >= maxVolume) ? maxVolume : auxVolume;
        this.musicLayers[this.layerIndex].volume = auxVolume;

        if (this.musicLayers[this.layerIndex].volume == maxVolume) {
                
                this.fadingIn = false;
                this.layerIndex++;
            }
    }

    public void PlayenemyDieSound(int index){
        this.enemyDeadAudioSource.PlayOneShot(this.enemyDieSound[index]);
    }

    public void StartFadingOutMenuMusic(){
        this.fadeOut = true;
    }

    public void resetMusic(){
        //TODO
        foreach (var layer in musicLayers)
        {
          layer.mute = true;
        }
    }

    public void playFailSound(){
        this.enemyDeadAudioSource.PlayOneShot(this.failSound);
    }


}
