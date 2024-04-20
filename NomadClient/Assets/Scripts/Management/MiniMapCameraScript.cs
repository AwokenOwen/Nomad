using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapCameraScript : MonoBehaviour
{
    public Image MiniMap;

    public Image northImage;
    public float radius;

    public GameObject objectiveMarkerPrefab;

    private void Update()
    {
        transform.position = new Vector3(PlayerManager.instance.transform.position.x, transform.position.y, PlayerManager.instance.transform.position.z);

        float angle = -PlayerManager.instance.bodyAnimator.transform.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Euler(90, 0, angle);

        Vector3 dir = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle), 0);

        dir.Normalize();

        northImage.rectTransform.localPosition = (dir * radius);


    }


    //This is for objective markers and other markers
    void UpdateMarkers()
    {
        /*Vector3 directionToDestinationRelativeToPlayer = Destination.transform.position - PlayerManager.instance.transform.position;

        float convertToMapSize = GetComponent<Camera>().orthographicSize;

        directionToDestinationRelativeToPlayer.y = 0;

        float mapMag = directionToDestinationRelativeToPlayer.magnitude / convertToMapSpace > 1f ? radius : directionToDestinationRelativeToPlayer.magnitude / convertToMapSpace * radius;

        float destinationAngleInMapSpace = Vector3.Angle(Vector3.forward, directionToDestinationRelativeToPlayer) * Mathf.Sign(directionToDestinationRelativeToPlayer.x);

        Vector3 directionInMapSpace = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (destinationAngleInMapSpace + angle)), Mathf.Cos(Mathf.Deg2Rad * (destinationAngleInMapSpace + angle)), 0);

        destination.rectTransform.position = MiniMap.rectTransform.position + (directionInMapSpace * mapMag);*/
    }
}
