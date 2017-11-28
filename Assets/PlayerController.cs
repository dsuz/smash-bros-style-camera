using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController m_charCtrl;
    [SerializeField] float m_moveSpeed = 1.0f;
    [SerializeField] float m_rotateSpeed = 1.0f;

	void Start ()
    {
        m_charCtrl = GetComponent<CharacterController>();
	}
	
	void Update ()
    {
        if (tag != "Player") return;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v != 0) m_charCtrl.SimpleMove(v * transform.forward * m_moveSpeed);
        if (h != 0)
        {
            if (v >= 0) transform.Rotate(0, h * m_rotateSpeed, 0);
            else transform.Rotate(0, -1 * h * m_rotateSpeed, 0);
        }
	}
}
