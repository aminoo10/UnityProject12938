using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidUnit : MonoBehaviour
{
    [SerializeField] private float smoothDamp;
    private float speed;
    private Vector3 currentVelocity;
    public Transform target;
    public Transform myTransform { get; set; }
    private BoidManager assignedFlock;
    //private List<GameObject> cohesionNeighbors  = new List<GameObject>();
    private List<BoidUnit> avoidanceNeighbors = new List<BoidUnit>();
    //private List<GameObject> alignmentNeighbors = new List<GameObject>();

    private void Awake() {
        myTransform = transform;
        target = GameObject.Find("Target").transform;
        // floor = GameObject.Find("Plane").transform;
    }
    public void AssignFlock(BoidManager flock) {
        assignedFlock = flock;
    }
    public void InitializeSpeed(float speed) {
        this.speed = speed;
    }
    public void MoveUnit() {
        
        // FindNeighbors();
        //Debug.Log("hello");
        // CalculateSpeed();
        // var avoidanceVector = CalculateAvoidanceVector();
        var toPlayerVector = CalculateToPlayerVector();
        var moveVector = toPlayerVector;

        moveVector = Vector3.SmoothDamp(myTransform.forward, moveVector, ref currentVelocity, smoothDamp);
        moveVector = moveVector.normalized * speed;
        if (moveVector == Vector3.zero)
            moveVector = transform.forward;
        
        myTransform.forward = moveVector;
        myTransform.position += moveVector * Time.deltaTime;
        
    }
    private void CalculateSpeed() {

        speed = Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed);

    }
    private void FindNeighbors() {
        // cohesionNeighbors.Clear();
        avoidanceNeighbors.Clear();
        // alignmentNeighbors.Clear();
        var allUnits = assignedFlock.allUnits;
        for (int i=0; i < allUnits.Count; i++) {
            var currentUnit = allUnits[i];

            if (currentUnit != this) {
                float currentNeighborDistanceSqr = Vector2.SqrMagnitude(currentUnit.myTransform.position - myTransform.position);

                if(currentNeighborDistanceSqr <= assignedFlock.avoidanceDistance * assignedFlock.avoidanceDistance){
                    avoidanceNeighbors.Add(currentUnit);
                    Debug.Log(avoidanceNeighbors.Count);
                }
            }
            
        }
    }
    private Vector3 CalculateAvoidanceVector() {
        var avoidanceVector = Vector3.zero;
        if (avoidanceNeighbors.Count == 0)
            return Vector3.zero;
        int neighborsInFOV = 0;
        for (int i=0; i < avoidanceNeighbors.Count; i++) {
            neighborsInFOV++;
            avoidanceVector += (myTransform.position - avoidanceNeighbors[i].myTransform.position);
 
        }
        avoidanceVector /= neighborsInFOV;
        avoidanceVector = avoidanceVector.normalized;
        
        return avoidanceVector;
    }
    private Vector3 CalculateToPlayerVector() {
        var toPlayerVector = Vector3.zero;
        Vector3 direction =  target.position - myTransform.position;
        toPlayerVector += direction;
        toPlayerVector = toPlayerVector.normalized;

        return toPlayerVector;
    }

}
