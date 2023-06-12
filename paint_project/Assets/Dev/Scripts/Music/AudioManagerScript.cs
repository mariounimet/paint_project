using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] musicLayers; 
    private int layerIndex;
    private bool fadingIn = false;
    public float audioFadeInDelay;
    public float audioFadeInStep;
    private float audioTimer = 0;
    public float maxVolume;

    void Start()
    {
        this.layerIndex = 1;
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
    }

    public void PlayNextLayer(){
        if(this.layerIndex < this.musicLayers.Length) {
            this.musicLayers[this.layerIndex].mute = false;
            
            this.fadingIn = true;
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


}
