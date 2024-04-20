using UnityEngine;

[System.Serializable]
public class JumpData: IJumps
{
    public virtual void OnHold()
    {
        
    }

    public virtual void OnPress()
    {
        if (PlayerManager.instance.grounded)
        {
            PlayerManager.instance.bodyAnimator.Play(PlayerManager.instance.moveInput.magnitude > 0 ? "JumpWhileRunning" : "Jump_Up");

            PlayerManager.instance.rb.velocity = new Vector3(PlayerManager.instance.rb.velocity.x, 0f, PlayerManager.instance.rb.velocity.z);

            Vector3 force = PlayerManager.instance.groundedNormal;

            force.y = 1f;

            PlayerManager.instance.rb.AddForce(force * GameManager.instance.currentWorldData.GetJumpForce(), ForceMode.Impulse);
        }
    }

    public virtual void OnRelease()
    {
        
    }
}