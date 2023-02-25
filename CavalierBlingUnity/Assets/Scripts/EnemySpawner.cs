using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_EnemyPrefab;
    [SerializeField]
    private GameObject m_TargetPrefab;
    [SerializeField]
    private Transform m_PivotTransform;
    [SerializeField]
    private int m_nbEnemy;
    [SerializeField]
    private float m_DeltaTime;
    [SerializeField]
    private float m_SpawnAngle = 45.0f;
    [SerializeField]
    private int m_nbSpawnedPosition = 5;


    void Start()
    {
        StartCoroutine(SurroundStepAnimated());
    }

    IEnumerator SurroundStepAnimated()
    {
        yield return new WaitForSeconds(m_DeltaTime);

        float AngleStep = m_SpawnAngle * 2.0f / (float)m_nbSpawnedPosition;

        for (int i = 1; i < m_nbEnemy; i++)
        {
            int position = Random.Range(0, m_nbSpawnedPosition);
            GameObject newSurrounderObject = Instantiate(m_EnemyPrefab, m_PivotTransform.position, Quaternion.identity, transform);

            float angle = (AngleStep * position) - (m_SpawnAngle / 2.0f);

            newSurrounderObject.transform.RotateAround(transform.position, Vector3.up, angle);
            newSurrounderObject.transform.rotation = Quaternion.identity;
            EnemyMove enemy = newSurrounderObject.GetComponent<EnemyMove>();
            enemy.m_Target = m_TargetPrefab;

            yield return new WaitForSeconds(m_DeltaTime);
        }
    }




    /*
    // Start is called before the first frame update
    void Start()
    {
        m_SpawnPosition = this.transform.position;

        EnemyMove enemy = m_EnemyPrefab.GetComponent<EnemyMove>();
        enemy.m_Target = m_TargetPrefab;

        StartCoroutine(RequestSpawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    IEnumerator RequestSpawn()
    {
        for (int i = 0; i < m_nbEnemy; i++)
        {
            yield return StartCoroutine(SpawnEnemy());

            if (i == m_nbEnemy - 1)
            {
                m_nbEnemy = 0;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(m_DeltaTime);

        Instantiate(m_EnemyPrefab, m_SpawnPosition, Quaternion.identity);
    }*/
}
