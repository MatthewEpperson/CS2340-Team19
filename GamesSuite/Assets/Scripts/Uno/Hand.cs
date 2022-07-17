using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private List<GameObject> cardsInHand;


    public GameObject getHand() {
        return hand;
    }

    public List<GameObject> getCardsInHand() {
        return cardsInHand;
    }

    public void drawCard(GameObject hand) {
        GameObject card = Deck.getCardFromDeck();
        card.transform.SetParent(hand.transform, false);
        applyCardPosition(card, cardsInHand, hand);
        hand.GetComponent<Hand>().cardsInHand.Add(card);
    }


    public void playCard(GameObject card, GameObject playArea) {
        cardsInHand.Remove(card);
        card.transform.SetParent(playArea.transform, true);
        StartCoroutine(moveToPlayArea(card, playArea));
        PlayAreaDeck.playAreaStack.Push(card);
        GameController.nextTurn();
    }


    /**********************************
    ************ HAND UI **************
    ***********************************/
    public static void applyCardPosition(GameObject card, List<GameObject> cardsInHand, GameObject hand) {
        float xOffset = 20f;
        float zOffset = 0.5f;

        try {
            GameObject prevCard = cardsInHand[cardsInHand.Count - 1];
            card.transform.position = hand.GetComponent<RectTransform>().anchoredPosition;
            card.transform.position = new Vector3(prevCard.transform.position.x + xOffset, 
                                        hand.transform.position.y,
                                        prevCard.transform.position.z - zOffset);
        } catch (ArgumentOutOfRangeException) {
            card.transform.position = new Vector3(hand.transform.position.x, 
                                        hand.transform.position.y,
                                        card.transform.position.z);
        }
    }


    // Card animation to move card to play area
    IEnumerator moveToPlayArea(GameObject card, GameObject playArea) {
        Vector3 oldPos = card.transform.position;
        Vector3 endPos = PlayAreaDeck.getPlayArea().transform.position;
        endPos = new Vector3(endPos.x, endPos.y, PlayAreaDeck.getCardFromPlayArea().transform.position.z - 1.0f);

        Quaternion rotateEndPos = Quaternion.Euler(new Vector3(card.transform.rotation.x,
                                            card.transform.rotation.y,
                                            UnityEngine.Random.Range(-360f, 360f)));

        float rotateSpeed = 500f;
        float moveSpeed = 1000.0f;

        while (Vector3.Distance(card.transform.position, endPos) > 0.01f) {
            card.transform.position = Vector3.MoveTowards(card.transform.position,
                                                            endPos, moveSpeed * Time.deltaTime);
            card.transform.rotation = Quaternion.RotateTowards(card.transform.rotation, rotateEndPos, rotateSpeed * Time.deltaTime);
            yield return null;
        }


        card.transform.position = endPos;
        // GameController.nextTurn();
    }

}
