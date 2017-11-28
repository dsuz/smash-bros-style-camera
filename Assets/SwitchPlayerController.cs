using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerController : MonoBehaviour {
    [SerializeField] UnityStandardAssets.Cameras.AbstractTargetFollower m_camera;

	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                PlayerController pc = hit.collider.gameObject.GetComponent<PlayerController>();
                if (pc) SwitchActivePlayer(pc);
            }
        }
	}

    void SwitchActivePlayer(PlayerController player)
    {
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Player");
        foreach (var go in goArray) go.tag = "Untagged";
        player.gameObject.tag = "Player";
        m_camera.FindAndTargetPlayer();
    }
}
