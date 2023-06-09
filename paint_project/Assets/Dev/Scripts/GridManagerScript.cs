using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2Int[,] coordsMatrix;
    private int[,] isPaintedMatrix;//0 not painted, 1 painted
    public Vector2Int canvasSize;
    public int blockPixelSize;//potencia de 2
    private SpriteRenderer spriteRenderer;
    public GameObject camera;
    public float ratioThreshold;
    public int resolutionOffset;
    public Vector3 gridInicial;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.coordsMatrix = initializeCoordsMatrix(this.canvasSize, this.blockPixelSize);
        this.isPaintedMatrix = initializeIsPaintedMatrix();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetIsPaintedMatrix(){
        this.isPaintedMatrix = initializeIsPaintedMatrix();
    }

    public Vector2Int[,] initializeCoordsMatrix(Vector2Int canvasSize, int blockPixelSize){
        Camera camera = Camera.main;
      

        Vector2Int size = getMatrixDimensions(canvasSize, blockPixelSize);
        // y determina las filas, x las columnas
        Vector2Int[,] auxCoordsMatrix = new Vector2Int[size.y,size.x];
        Vector2Int originalCoords = getLowerLeftCoords();

        float ratio = (float) Screen.width/Screen.height;
        if (ratio < this.ratioThreshold) {
            originalCoords.x -= this.resolutionOffset;
        }


        Vector2Int currentCoords = originalCoords;
        for(int i = size.y-1; i >=0; i--){
            for(int j = 0; j < size.x; j++){
                auxCoordsMatrix[i,j] = currentCoords;
                currentCoords.x += blockPixelSize;
                // string msg = "fila: "+i.ToString()+" columna: " +j.ToString()+ " x="+ currentCoords.x.ToString()+" y="+currentCoords.y.ToString();
                // print(msg);
            }
            currentCoords.y += blockPixelSize;
            currentCoords.x = originalCoords.x;
        }
        //per step son 128 pixeles

        return auxCoordsMatrix;
    }

    public int[,] initializeIsPaintedMatrix(){
        Vector2Int size = getMatrixDimensions(this.canvasSize, this.blockPixelSize);
        return new int[size.y,size.x];
    }

    public Vector2Int getLowerLeftCoords(){
       //TODO revisar para dimensiones distintas a 9:16
        Vector3 pos = this.gridInicial;
        Sprite sprite = spriteRenderer.sprite;
        Rect rect =  sprite.textureRect;
        float x = pos.x-gameObject.transform.position.x;
        float y = pos.y-gameObject.transform.position.y;
        x *= sprite.pixelsPerUnit;
        y *= sprite.pixelsPerUnit;
        x+= rect.width/2;
        y+= rect.height/2;
        x += rect.x;
        y += rect.y;
        int realX = Mathf.FloorToInt(x);
        int realY = Mathf.FloorToInt(y); 

        return new Vector2Int(realX, realY);
    }

    public Vector2Int getMatrixDimensions(Vector2Int canvasSize, int blockPixelSize){
        int unitsInX = canvasSize.x / blockPixelSize;
        int unitsInY = canvasSize.y / blockPixelSize;
       
        return new Vector2Int(unitsInX,unitsInY);
    }

    public Vector2Int[,] getCoordsMatrix(){
        return this.coordsMatrix;
    }
    public int[,] getIsPaintedMatrix(){
        return this.isPaintedMatrix;
    }

    public void updateIsPaintedMatrix(int xindex, int yindex, int isPainted) {
        this.isPaintedMatrix[yindex,xindex] = isPainted;
    }

 
}
