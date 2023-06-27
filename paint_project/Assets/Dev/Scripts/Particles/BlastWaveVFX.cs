using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWaveVFX : MonoBehaviour
{
    [SerializeField] int pointsCount;
    [SerializeField] float maxRadius;
    [SerializeField] float speed;
    [SerializeField] float startWidth;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = pointsCount + 1;
        
    }
    private IEnumerator Blast()
    {
        float currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
    }

    private void Damage(float currentRadius)
    {
        Vector2 pointInSpace = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hittingObjects = Physics2D.OverlapCircleAll(pointInSpace, currentRadius-2);
       
        for (int i = 0; i < hittingObjects.Length; i++)
        {
            GameObject enemy = hittingObjects[i].gameObject;
           
           // "7" es la layer enemies
            bool isValid = (enemy.layer.ToString() == "7");
            if (!enemy || !isValid)
                continue;

            if (enemy.CompareTag("Follower")){
                enemy.GetComponent<FollowerScript>().Die(false);
            } else if(enemy.CompareTag("ShooterScript")) {
                enemy.GetComponent<ShooterScript>().Die(false);
            } else if(enemy.CompareTag("Kamikaze")) {
                enemy.GetComponent<KamikazeScript>().Die(false);
            } else if(enemy.CompareTag("Tank")) {
                enemy.GetComponent<TankScript>().Die(false);
            } else if(enemy.CompareTag("Dasher")) {
                enemy.GetComponent<DasherScript>().Die(false);
            }
            //Vector3 direction = (hittingObjects[i].transform.position - transform.position).normalized;

          
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360f / pointsCount;
        
        for(int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;

            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }


    public void createWave(Color waveColor){
        this.lineRenderer.startColor = waveColor;
        this.lineRenderer.endColor = waveColor;
        StartCoroutine(Blast());
    }
}