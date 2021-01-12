using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [Header("Detection Distances")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionDistance;
    public float cohesionDistance { get {return _cohesionDistance; } }
    [Range(0, 10)]
    [SerializeField] private float _avoidanceDistance;
    public float avoidanceDistance { get {return _avoidanceDistance; } }
    [Range(0, 10)]
    [SerializeField] private float _alignmentDistance;
    public float alignmentDistance { get {return _alignmentDistance; } }

    [Header("Speed Setup")]
    [Range(0, 30)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }
    [Range(0, 30)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed; } }
    
    [Header("Spawn Setup")]
    [SerializeField] private BoidUnit boidPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    public List<BoidUnit> allUnits = new List<BoidUnit>();
    
    private void Start() {
        GenerateUnits();
    }
    private void Update() {
        for (int i=0; i < allUnits.Count; i++) {
        allUnits[i].MoveUnit();
        }
    }
    private void GenerateUnits() {
        for (int i=0; i < 12; i++) {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            BoidUnit thisBoid = Instantiate(boidPrefab, spawnPosition, rotation) as BoidUnit;
            allUnits.Add(thisBoid);
            thisBoid.name = "Unit " + i;
        }
    }

    /*
    
    public GameObject boidUnit;
    public Transform target;
 
    public int startingCount = 100;
    const float boidDensity = 0.08f;

    void Start() {
        for (int i=0; i < startingCount; i++)  {
            Instantiate(
                agent,
                Random.insideUnitCircle * startingCount * boidDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            agent.name = "Agent " + i;
            // agent.Initialize(this);
            agents.Add(agent);
        }
    }
    */
}
