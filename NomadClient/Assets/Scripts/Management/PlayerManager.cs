using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField]
    Vector3 gravity;

    Vector2 moveVec;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        moveVec = new Vector2();
    }

    public void SetMoveVec(Vector2 moveVec)
    {
        this.moveVec = moveVec;
    }

    public Vector2 GetMoveVec()
    {
        return moveVec;
    }

    public Vector3 getGravity() 
    { 
        return gravity; 
    }
}
