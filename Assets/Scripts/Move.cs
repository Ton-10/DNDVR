using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3[] Targets;
    public float[] Speeds, Lifespans;
    public float CurrentTime;
    public int CurrentStep;
    public void SetValues(Vector3[] targets, float[] speeds, float[] times)
    {
        Targets = targets;
        Speeds = speeds;
        Lifespans = times;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTill();
    }

    public void moveTill()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime < Lifespans[CurrentStep])
        {
            transform.position = Vector3.Lerp(transform.position, Targets[CurrentStep], Speeds[CurrentStep]);
        }
        else if(CurrentTime > Lifespans[CurrentStep] && CurrentStep+1 < Targets.Length)
        {
            Debug.Log("Moved to next target :" + CurrentStep);
            CurrentTime = 0;
            CurrentStep++;
        }
        else
        {
            Debug.Log("Removed on step" + CurrentStep);
            Destroy(transform.gameObject);
            Destroy(this);
        }
    }
}
