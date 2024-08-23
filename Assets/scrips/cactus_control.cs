using System.Collections;
using UnityEngine;

public class cactus_control : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // 생성할 오브젝트들을 저장할 배열
    public float minSpawnTime = 2f;     // 최소 생성 시간 간격
    public float maxSpawnTime = 5f;     // 최대 생성 시간 간격
    public Vector2 spawnPosition = new Vector2(12, -1.1f); // 생성 위치
    public float objectSpeed = 5f;      // 생성된 오브젝트의 이동 속도
    public float destroyXPosition = -15f; // 오브젝트가 삭제될 X 좌표

    void Start()
    {
        StartCoroutine(SpawnObject()); // 코루틴을 통해 오브젝트 생성 시작
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            // 2~5초 사이의 랜덤 시간 대기
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // 오브젝트 배열에서 랜덤으로 하나를 선택
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject selectedObject = objectsToSpawn[randomIndex];

            // 선택된 오브젝트를 지정된 위치에 생성
            GameObject newObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity);

            // 생성된 오브젝트에 이동 및 삭제 로직을 추가
            StartCoroutine(MoveAndDestroyObject(newObject));
        }
    }

    IEnumerator MoveAndDestroyObject(GameObject obj)
    {
        // 오브젝트가 왼쪽으로 이동
        while (obj != null && obj.transform.position.x > destroyXPosition)
        {
            obj.transform.Translate(Vector3.left * objectSpeed * Time.deltaTime);
            yield return null; // 다음 프레임까지 대기
        }

        // 오브젝트가 화면 밖으로 나가면 삭제
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            // 오브젝트 삭제
            Destroy(gameObject);
        }
    }
}
