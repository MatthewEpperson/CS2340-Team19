using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateWord
{
    private static string[] words = System.IO.File.ReadAllLines("Assets/Scripts/words.csv"); // Parses words.csv into an array

    // Retrieves random word from words
    public static string getWord() {
        int randomInd = Random.Range(0, words.Length);
        return words[randomInd];
    }

    // Checks if the word guessed by player is a possible choice
    public static bool wordsContains(string playerGuess) { 
        return ((IList)words).Contains(playerGuess);
    }
}
