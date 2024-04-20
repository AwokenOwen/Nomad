using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraScript : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(PlayerManager.instance.transform.position.x, transform.position.y, PlayerManager.instance.transform.position.z);

        transform.rotation = Quaternion.Euler(90, 0, -PlayerManager.instance.bodyAnimator.transform.rotation.eulerAngles.y);
    }
}
