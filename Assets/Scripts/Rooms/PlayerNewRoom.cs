using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNewRoom : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject player;

    // ROOM 1 WILL ALWAYS BE UP / LEFT
    [SerializeField] float room1UpBound;
    [SerializeField] float room1DownBound;
    [SerializeField] float room1LeftBound;
    [SerializeField] float room1RightBound;

    // ROOM 2 WILL ALWAYS BE DOWN / RIGHT
    [SerializeField] float room2UpBound;
    [SerializeField] float room2DownBound;
    [SerializeField] float room2LeftBound;
    [SerializeField] float room2RightBound;

    [SerializeField] bool usingLeftRight;

    [SerializeField] float room1RespawnX;
    [SerializeField] float room1RespawnY;

    [SerializeField] float room2RespawnX;
    [SerializeField] float room2RespawnY;

    private bool enterFromRoom1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Collided");
            if (usingLeftRight)
            {
                // ENTER FROM THE LEFT
                if (collision.transform.position.x < transform.position.x)
                {
                    enterFromRoom1 = true;
                }
                // ENTER FROM THE RIGHT
                else
                {
                    enterFromRoom1 = false;
                }
            }
            else
            {
                // ENTER FROM THE TOP
                if (collision.transform.position.y > transform.position.y)
                {
                    enterFromRoom1 = true;
                }
                // ENTER FROM THE DOWN
                else
                {
                    enterFromRoom1 = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player Left");
            if (usingLeftRight)
            {
                // EXIT TO THE LEFT
                if (collision.transform.position.x < transform.position.x && !enterFromRoom1)
                {
                    mainCamera.GetComponent<FollowPlayer>().setUpBound(room1UpBound);
                    mainCamera.GetComponent<FollowPlayer>().setDownBound(room1DownBound);
                    mainCamera.GetComponent<FollowPlayer>().setLeftBound(room1LeftBound);
                    mainCamera.GetComponent<FollowPlayer>().setRightBound(room1RightBound);

                    player.GetComponent<PlayerRespawn>().SetRespawnX(room1RespawnX);
                    player.GetComponent<PlayerRespawn>().SetRespawnY(room1RespawnY);
                }
                // EXIT TO THE RIGHT
                else if (collision.transform.position.x > transform.position.x && enterFromRoom1)
                {
                    mainCamera.GetComponent<FollowPlayer>().setUpBound(room2UpBound);
                    mainCamera.GetComponent<FollowPlayer>().setDownBound(room2DownBound);
                    mainCamera.GetComponent<FollowPlayer>().setLeftBound(room2LeftBound);
                    mainCamera.GetComponent<FollowPlayer>().setRightBound(room2RightBound);

                    player.GetComponent<PlayerRespawn>().SetRespawnX(room2RespawnX);
                    player.GetComponent<PlayerRespawn>().SetRespawnY(room2RespawnY);
                }
            }
            else
            {
                // EXIT TO THE TOP
                if (collision.transform.position.y > transform.position.y && !enterFromRoom1)
                {
                    mainCamera.GetComponent<FollowPlayer>().setUpBound(room1UpBound);
                    mainCamera.GetComponent<FollowPlayer>().setDownBound(room1DownBound);
                    mainCamera.GetComponent<FollowPlayer>().setLeftBound(room1LeftBound);
                    mainCamera.GetComponent<FollowPlayer>().setRightBound(room1RightBound);

                    player.GetComponent<PlayerRespawn>().SetRespawnX(room1RespawnX);
                    player.GetComponent<PlayerRespawn>().SetRespawnY(room1RespawnY);
                }
                // EXIT TO THE DOWN
                else if (collision.transform.position.y < transform.position.y && enterFromRoom1)
                {
                    mainCamera.GetComponent<FollowPlayer>().setUpBound(room2UpBound);
                    mainCamera.GetComponent<FollowPlayer>().setDownBound(room2DownBound);
                    mainCamera.GetComponent<FollowPlayer>().setLeftBound(room2LeftBound);
                    mainCamera.GetComponent<FollowPlayer>().setRightBound(room2RightBound);

                    player.GetComponent<PlayerRespawn>().SetRespawnX(room2RespawnX);
                    player.GetComponent<PlayerRespawn>().SetRespawnY(room2RespawnY);
                }
            }
            
        }

        
    }
}
