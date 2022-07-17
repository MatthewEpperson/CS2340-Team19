using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public Hand hand;
    [SerializeField] private List<GameObject> cardsInHand;

    public UIController uIController;

    void Start() {
        cardsInHand = hand.getCardsInHand();
    }

    public void onCardClick(GameObject card) {
        Card cardInfo = card.GetComponent<Card>();
        Card cardOnPlayArea = PlayAreaDeck.getCardFromPlayArea().GetComponent<Card>();

        if (cardInfo.getColor() == cardOnPlayArea.getColor()) {
                hand.playCard(card, PlayAreaDeck.getPlayArea());
        } else if (cardInfo.GetType() == typeof(NumberCard) &&
                    cardOnPlayArea.GetType() == typeof(NumberCard)) {
            if (((NumberCard)cardInfo).getNumber() == ((NumberCard)cardOnPlayArea).getNumber()) {
                hand.playCard(card, PlayAreaDeck.getPlayArea());
            }
        } else if (cardInfo.GetType() == typeof(WildCard)) {
            hand.playCard(card, PlayAreaDeck.getPlayArea());
            uIController.activatePickColorUI(cardInfo);
        }
    }
}
