using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitFootsteps : MonoBehaviour
{
    [SerializeField]
    GameObject m_FootstepPrefab;

    [SerializeField]
    float m_SpawnDist = 10.0f;

    [SerializeField]
    Transform m_LeftSpawnPos;
    [SerializeField]
    Transform m_RightSpawnPos;

    Vector3 m_LastPos;

    int m_counter = 0;

    void Awake()
    {
        m_LastPos = transform.position;
        StartCoroutine(FootstepSpawn());
    }

    void SpawnFootstep()
    {
        m_counter++;
        Transform front = m_counter % 2 == 0 ? m_LeftSpawnPos : m_RightSpawnPos;
        Transform back = m_counter % 2 == 1 ? m_LeftSpawnPos : m_RightSpawnPos;

        Instantiate(m_FootstepPrefab, back.position, transform.rotation);
        Instantiate(m_FootstepPrefab, front.position + new Vector3(0,0,1), transform.rotation);
    }

    IEnumerator FootstepSpawn()
    {
        while (true)
        {
            //Wait .5sec and check distance
            float diff = Vector3.Distance(transform.position, m_LastPos);

            if (Mathf.Abs(diff) > m_SpawnDist)
            {
                SpawnFootstep();
                m_LastPos = transform.position;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}