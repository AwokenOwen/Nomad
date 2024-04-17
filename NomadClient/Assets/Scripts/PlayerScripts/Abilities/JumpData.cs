using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JumpData: IJumps
{
    public float jumpForce;

    public JumpData(float jumpForce)
    {
        this.jumpForce = jumpForce;
    }

    public virtual void OnHold()
    {
        PlayerManager.instance.rb.velocity = new Vector3(PlayerManager.instance.rb.velocity.x, 0f, PlayerManager.instance.rb.velocity.z);

        PlayerManager.instance.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        Debug.Log(PlayerManager.instance.rb.velocity.y);
    }

    public virtual void OnPress()
    {
        
    }

    public virtual void OnRelease()
    {
        
    }
}
