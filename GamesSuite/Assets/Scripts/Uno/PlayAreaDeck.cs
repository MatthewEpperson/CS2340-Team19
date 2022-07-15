using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaDeck : MonoBehaviour
{
    [SerializeField] private static GameObject playArea;
    public static Stack<GameObject> playAreaStack = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playArea = GameObject.Find("Played Area");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject getPlayArea() {
        return playArea;
    }

    public static GameObject getCardFromPlayArea() {
        return playAreaStack.Peek();
    }
}
