using Entities;
using UnityEngine;

public class FoodProducer : MonoBehaviour
{
    private float _interval = 3f;
    [SerializeField] private GameObject fruitPrefab;

    private float _elapsed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _interval)
        {
            _elapsed = 0;
            Vector3 randomSpawnPoint = transform.position + Random.onUnitSphere + (Vector3.up * 2f);
            var fruit = Instantiate(fruitPrefab);
            fruit.transform.SetPositionAndRotation(randomSpawnPoint, transform.rotation);
            fruit.GetComponent<Rigidbody>().AddForce(Random.insideUnitCircle.normalized);
            fruit.GetComponent<FoodSource>().TotalOwned = Random.Range(
                fruit.GetComponent<FoodSource>().TotalOwned / 3, fruit.GetComponent<FoodSource>().TotalOwned
            );
            fruit.transform.localScale *= Random.Range(0.5f, 1f);
        }
    }
}