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
        if (PlayerManager.instance.grounded)
        {
            PlayerManager.instance.rb.velocity = new Vector3(PlayerManager.instance.rb.velocity.x, 0f, PlayerManager.instance.rb.velocity.z);

            PlayerManager.instance.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public virtual void OnPress()
    {
        
    }

    public virtual void OnRelease()
    {
        
    }
}
