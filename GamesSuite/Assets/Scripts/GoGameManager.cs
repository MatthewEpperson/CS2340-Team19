using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoGameManager : MonoBehaviour
{
    private GoUI goUI;
    private GoBoard goBoard;
    private bool newMove = false;
    private bool update = false;
    
    // Start is called before the first frame update
    void Start()
    {
        goUI = GameObject.Find("GoBoard").GetComponent<GoUI>();
        goBoard = GameObject.Find("goBoard").GetComponent<GoBoard>();
        // test add stone
        int turn = goBoard.getTurn();
        goBoard.makeMove(0,0,turn);
        goBoard.nextTurn();
        turn = goBoard.getTurn();
        goBoard.makeMove(0,1,turn);
        goBoard.clearMove(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input move
        int currMoveX = 0;
        int currMoveY = 0;
        if (Input.GetMouseButtonDown(0)){
            Vector3 mousePos = Input.mousePosition;
            (currMoveX,currMoveY) = mouseInputMove(mousePos.x, mousePos.y);
            newMove = true;
        }

        // check the validity of the new move if there is one
        if(newMove){
            // reset the newMove flag
            newMove = false;
            Debug.Log(currMoveX);
            Debug.Log(currMoveY);
            update = true;

            


            
        }


        // update the board
        if(update){
            int[,] currState = goBoard.getCurrState();
            goUI.printBoard(currState);
        }
    }

    public GoBoard getGoBoard(){
        return goBoard;
    }

    private (int, int) mouseInputMove(float mousePosX, float mousePosY){
        int x = 4;
        int y = 4;
        float gridSize = 108;
        float posX = mousePosX - 1920f/2;
        float posY = (1080-mousePosY) - 1080f/2;
        x += (int) Math.Round(posX/gridSize);
        y += (int) Math.Round(posY/gridSize);
        return (x,y);
    }

    // private void 
}
