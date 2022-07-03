using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBoard : MonoBehaviour
{
    private int[,] currState = new int[9,9];
    private List<int[,]> prevStates = new List<int[,]>();
    private int turn = 1;

    public void makeMove(int x, int y, int turn){
        currState[x,y] = turn;
    }

    public void clearMove(int x, int y){
        currState[x,y] = 0;
    }

    public int getTurn(){
        return turn;
    }
    public void nextTurn(){
        turn *= -1;
    }

    public int[,] getCurrState(){
        return currState;
    }

    public List<int[,]> getPrevStates(){
        return prevStates;
    }

    public void logCurrState(int[,] currState){
        prevStates.Add(currState);
    }
}