using UnityEngine;
using System.Collections;

public class playerAbilities : MonoBehaviour {
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html This is where you go to find all of these


    public float m_teleportDistance = 6.0f;

    public float m_startTime = 0.2f;
    public float m_startDistance = 1.0f;

    public float m_endTime = 0.1f;
    public float m_endDistance = 1.0f;

    public float m_cooldownTime = 1.0f;

    [HideInInspector()]
    public float m_curCooldown = 0.0f;

    private int m_substate = 1;
    private float m_time = 0.0f;

    private Transform m_visual = null;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
    
    // Called every fixed framerate frame
    private void FixedUpdate()
    {

    }

    public void Teleport()
    {
        if (m_substate == 1) // Teleport Init
        {
            //GameObject obj = GameObject.Instantiate(ResourceModule.Instance.m_effects.m_prefTeleportStart, transform.position, transform.rotation) as GameObject;
            //obj.transform.SetParent(transform);

            m_substate = 2;
        }

        if (m_substate == 2) // Teleport Start
        {
            float speed = (m_startTime > 0 ? m_startDistance / m_startTime : 0.0f);
            transform.position += transform.forward * speed * Time.deltaTime;

            float t = m_time / m_startTime;
            m_visual.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0, 0, 1.0f), t);

            if (m_time > m_startTime)
                m_substate = 3;
        }
        else if (m_substate == 3) // Teleport Move
        {
            transform.position += transform.forward * m_teleportDistance;

            m_time = 0.0f;
            m_substate = 4;
        }
        else if (m_substate == 4) // Teleport End
        {
            float speed = (m_startTime > 0 ? m_endDistance / m_endTime : 0.0f);
            transform.position += transform.forward * speed * Time.deltaTime;

            float t = m_time / m_endTime;
            m_visual.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0.0f), new Vector3(1, 1, 1), t);

            if (m_time > m_endTime)
            {
                m_visual.transform.localScale = Vector3.one;
                m_curCooldown = m_cooldownTime;
                m_substate = 0;
            }
        }
      

    }

}
