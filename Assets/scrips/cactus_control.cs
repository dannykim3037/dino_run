using System.Collections;
using UnityEngine;

public class cactus_control : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // ������ ������Ʈ���� ������ �迭
    public float minSpawnTime = 2f;     // �ּ� ���� �ð� ����
    public float maxSpawnTime = 5f;     // �ִ� ���� �ð� ����
    public Vector2 spawnPosition = new Vector2(12, -1.1f); // ���� ��ġ
    public float objectSpeed = 5f;      // ������ ������Ʈ�� �̵� �ӵ�
    public float destroyXPosition = -15f; // ������Ʈ�� ������ X ��ǥ

    void Start()
    {
        StartCoroutine(SpawnObject()); // �ڷ�ƾ�� ���� ������Ʈ ���� ����
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            // 2~5�� ������ ���� �ð� ���
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // ������Ʈ �迭���� �������� �ϳ��� ����
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject selectedObject = objectsToSpawn[randomIndex];

            // ���õ� ������Ʈ�� ������ ��ġ�� ����
            GameObject newObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity);

            // ������ ������Ʈ�� �̵� �� ���� ������ �߰�
            StartCoroutine(MoveAndDestroyObject(newObject));
        }
    }

    IEnumerator MoveAndDestroyObject(GameObject obj)
    {
        // ������Ʈ�� �������� �̵�
        while (obj != null && obj.transform.position.x > destroyXPosition)
        {
            obj.transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);
            yield return null; // ���� �����ӱ��� ���
        }

        // ������Ʈ�� ȭ�� ������ ������ ����
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            // ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
