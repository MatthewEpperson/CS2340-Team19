using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject opponentHand1;
    [SerializeField] private GameObject opponentHand2;
    [SerializeField] private GameObject opponentHand3;
    [SerializeField] private GameObject playerHand;

    public static string[] players = {"Player", "Opponent 1", "Opponent 2", "Opponent 3"};
    public static string currTurn;

    private List<GameObject> hands = new List<GameObject>();

    void Start()
    {

        currTurn = players[0]; // Game always starts with player

        hands.Add(playerHand);
        hands.Add(opponentHand1);
        hands.Add(opponentHand2);
        hands.Add(opponentHand3);

        StartCoroutine(dealStartCard(PlayAreaDeck.getPlayArea(), PlayAreaDeck.playAreaStack));

        foreach (GameObject hand in hands) {
            Hand currHand = hand.GetComponent<Hand>();
            StartCoroutine(dealInitialCards(currHand.getCardsInHand(), currHand.getHand()));
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currTurn);
    }

    IEnumerator dealInitialCards(List<GameObject> cardsInhand, GameObject hand) {
        yield return new WaitForSeconds(0.3f); // wait 0.3 seconds to allow deck to be shuffled before dealing cards
        for (int i = 0; i < 7; i++) {
            GameObject card = Deck.getCardFromDeck();
            card.transform.SetParent(hand.transform, false);
            Hand.applyCardPosition(card, cardsInhand, hand);
            cardsInhand.Add(card);
        }
    }

    IEnumerator dealStartCard(GameObject playAreaDeck, Stack<GameObject> playAreaStack) {
        yield return new WaitForSeconds(0.3f);
        GameObject card = Deck.getCardFromDeck();
        card.transform.SetParent(playAreaDeck.transform, false);
        card.transform.position = playAreaDeck.transform.position;
        playAreaStack.Push(card);
    }



    public static bool isReversed = false;

    // THIS SETS THE NEXT TURN
    public static void nextTurn() {
        int indexOfTurn = Array.IndexOf(players, currTurn);
        
        if (isReversed == false) {
            if (indexOfTurn >= players.Length - 1) {
                indexOfTurn = 0;
            } else {
                indexOfTurn++;
            }
        } else {
            if (indexOfTurn - 1 < 0) {
                indexOfTurn = players.Length - 1;
            } else {
                indexOfTurn--;
            }
        }

        if (indexOfTurn != 0) {
            CountdownController.generateAITimer();
        }
        currTurn = players[indexOfTurn];
        CountdownController.resetTimer();
    }


    // THIS ONLY CHECKS WHAT THE NEXT TURN IS, IT DOES NOT SET THE NEXT TURN
    public static string checkNextTurn() {
        int indexOfTurn = Array.IndexOf(players, currTurn);
        if (isReversed == false) {
            if (indexOfTurn >= players.Length - 1) {
                indexOfTurn = 0;
            } else {
                indexOfTurn++;
            }
        } else {
            if (indexOfTurn < 0) {
                indexOfTurn = players.Length - 1;
            } else {
                indexOfTurn--;
            }
        }

        return players[indexOfTurn];
    }

}
