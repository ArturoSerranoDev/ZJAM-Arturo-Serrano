using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum EnemyType { Melee, Shooter}

public class EnemyView : MonoBehaviour
{
    public GameObject enemyCenter;
    public List<Command> enemyCommands = new List<Command>();

    public EnemyLoopType enemyLoopType = EnemyLoopType.Reverse;
    public EnemyType enemyType = EnemyType.Melee;
    public int turn;
    public float animSpeed;

    public bool isAlive;
    public LineRenderer lineRenderer;
    //public Command GetNextCommandAction()
    //{
    //    //if end, reverse commands and execute
    //    if (turn >= enemyCommands.Count)
    //    {
    //        enemyCommands.Reverse();
    //        turn = 0;
    //    }

    //    return enemyCommands[turn];
    //}

    public IEnumerator ExecuteCommand()
    {
        if (!isAlive || LevelController.Instance.currentLevel == 5)
            yield break;

        bool shouldAttack = CheckAttackPlayer();

        if (shouldAttack)
        {
            // attack anim
            if (enemyType == EnemyType.Melee)
            {
                yield return transform.DORotate(new Vector3(0, 1080,0), 0.1f).SetLoops(4,LoopType.Incremental);

            }
            else
            {

            }

            // trigger death anim on player
            
            // set defeat

            yield break;
        }

        // If shooter, check if player is in range and do animation

        // set player to destroyed

        // break

        if(enemyType == EnemyType.Shooter)
        {
            UpdateLineRenderer();
        }


        Command turnCommand = enemyCommands[turn];

        Tween tweenAction;
        Debug.Log(turnCommand.GetCommandType());

        switch (turnCommand.GetCommandType())
        {
            case CommandType.MoveUp:
            case CommandType.MoveBack:
       
                Move moveCommand = turnCommand as Move;
                tweenAction = transform.DOMove(transform.position + (moveCommand.dir * transform.forward), 0.5f * animSpeed);

                yield return tweenAction.WaitForCompletion();
                break;
            case CommandType.RotateRigth:
            case CommandType.RotateLeft:
                Rotate rotateCommand = turnCommand as Rotate;
                tweenAction = transform.DORotate(transform.eulerAngles + new Vector3(0, 90 * rotateCommand.rotDir, 0), 0.5f * animSpeed);

                yield return tweenAction.WaitForCompletion();
                break;
        }


        turn++;
        //if end, reverse commands and execute
        if (turn >= enemyCommands.Count)
        {
            if (enemyLoopType == EnemyLoopType.Reverse)
                enemyCommands.Reverse();

            turn = 0;
        }

    }

    private void UpdateLineRenderer()
    {

        

            RaycastHit hit;


            int distance = 0;
            Debug.DrawRay(enemyCenter.transform.position, enemyCenter.transform.forward * 10, Color.blue, 10f);

            if (Physics.Raycast(lineRenderer.transform.position, lineRenderer.transform.forward * 10, out hit, 10f))
                if (hit.collider != null)
                {
                    lineRenderer.SetPosition(1, hit.collider.transform.position);
                }

            Debug.Log("Distance of" + distance);

        
    }

    bool CheckAttackPlayer()
    {
        if (enemyType == EnemyType.Melee)
        {

            RaycastHit hit;
            int layerMask = 1 << 8;

            if (Physics.Raycast(enemyCenter.transform.position, enemyCenter.transform.forward, out hit, 1.2f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PlayerController>())
                    {
                        Debug.Log("IsNextTileValidMove false");
                        return true;
                    }
                }
            }



            if (Physics.Raycast(enemyCenter.transform.position, -enemyCenter.transform.forward, out hit, 1.2f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PlayerController>())
                    {
                        Debug.Log("IsNextTileValidMove false");
                        return true;
                    }
                }
            }



            if (Physics.Raycast(enemyCenter.transform.position, enemyCenter.transform.right, out hit, 1.2f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PlayerController>())
                    {
                        Debug.Log("IsNextTileValidMove false");
                        return true;
                    }
                }
            }



            if (Physics.Raycast(enemyCenter.transform.position, -enemyCenter.transform.right, out hit, 1.2f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PlayerController>())
                    {
                        Debug.Log("IsNextTileValidMove false");
                        return true;
                    }
                }
            }



            return false;

        }

        else
        {
            RaycastHit hit;

            if (Physics.Raycast(enemyCenter.transform.position, enemyCenter.transform.forward, out hit, 10f))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.GetComponent<PlayerController>())
                    {
                        Debug.Log("IsNextTileValidMove false");
                        return true;
                    }
                }
            }

        }

        return false;
    }

    public void Destroy()
    {
        isAlive = false;
        gameObject.SetActive(false);
    }

    public void Reset(EnemyData enemyData)
    {
        isAlive = true;
        turn = 0;
        enemyCommands.Clear();
        enemyLoopType = enemyData.commandLoopType;
        enemyCommands = enemyData.enemyCommands;
    }
}
