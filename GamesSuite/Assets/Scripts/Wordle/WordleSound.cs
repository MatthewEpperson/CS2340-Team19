using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordleSound : MonoBehaviour
{

    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioSource errorSound;
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource loseSound;

    // Update is called once per frame
    public void playTypeSound() {
        if (WordlePlayer.wordInputField.text != "" || Input.GetKeyDown("backspace")) {
            sound.Play();
        }
    }

    public void playBlockFlipSound() {
        sound.Play();
    }

    public void playErrorSound() {
        errorSound.Play();
    }

    public void playVictorySound() {
        victorySound.Play();
    }

    public void playLoseSound() {
        loseSound.Play();
    }

}
