using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCamera;
    private Texture2D currentMask;
    private Texture2D newMask;
    public Texture2D backgroundImage;
    private Renderer crenderer;
    private SpriteRenderer spriteRenderer;
    private float progressPercent;
    private float progressPerBlock;
    private Vector2Int currentSector; //0 for negative, 1 for positive
    public int xOffset;
    public int yOffset;
    [Range(5, 24)] public int completeSectorThreshold; // 20 is good
    private bool isMovingCamera = false;
    private bool isPainting = false;
    private int[] PaintReaminingIndexes = new int[4]; // [startX, startY, endX, endY]
    private Vector2Int currentPaintingRemainingIndexes;
    private float Painttimer = 0;
    private float CameraTimer = 0;
    public float Paintdelay;
    public float CameraMoveDelay;
    public float CameraTransitionMaxDuration;
    private float cameraStepX;
    private float cameraStepY;
    public Vector2 initialCamaraCoords;

    public string textValue;
    public Text textElement;


   

    public GridManagerScript grid;
    void Start()
    {
        crenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentMask = (Texture2D) crenderer.material.GetTexture("_PaintMask");
        newMask = currentMask;
        mainCamera = Camera.main;
        setInitialProgress(this.grid.getMatrixDimensions(this.grid.canvasSize,this.grid.blockPixelSize));
        ResetCanvas();

    

        
    }

    // Update is called once per frame
    void Update()
    {
      
       detectPaint();

        CameraTimer += Time.deltaTime;
       if (isMovingCamera && (CameraTimer > CameraMoveDelay)) {
            CompleteMoveSector();
            CameraTimer = 0;
       }

        Painttimer += Time.deltaTime;
       
       if (isPainting && (Painttimer > Paintdelay)) {
            PaintRemainingInSector();
            Painttimer = 0;
       }
    }

    public void detectPaint(float xDie, float yDie){
        //TODO enemy defeated
        if(Input.GetMouseButtonDown(0)) {

            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = worldCoordsToImageCoords(xDie, yDie);
           

            int xIndex = closestDownMultiple((pos.x-xOffset),this.grid.blockPixelSize, true);
            int yIndex = closestDownMultiple((pos.y-yOffset),this.grid.blockPixelSize, false);
          
           // string msg = "la posicion en y es "+pos.y.ToString()+" y su yIndex es "+yIndex.ToString();
           // print(msg);
            Vector2Int maxMatrixSize = this.grid.getMatrixDimensions(this.grid.canvasSize, this.grid.blockPixelSize);
            bool areIndexesValid = (xIndex >= 0) && (yIndex >= 0) && (xIndex <maxMatrixSize.x) && (yIndex < maxMatrixSize.y);
            if(areIndexesValid) {

                TryPaintGridSquare(xIndex, yIndex);

                
            } else {
                
                print("Se intentó pintar fuera del grid en x="+pos.x.ToString() + " y=" +pos.y.ToString());
            }
           
           
           
        }
    }

    public void TryPaintGridSquare(int xIndex, int yIndex){
        if(this.grid.getIsPaintedMatrix()[yIndex,xIndex] == 0) {
            Vector2Int finalPos =  mapIndexToCoord(xIndex,yIndex);
            PaintMask(finalPos.x,finalPos.y,this.grid.blockPixelSize,this.grid.blockPixelSize );
            this.grid.updateIsPaintedMatrix(xIndex,yIndex, 1);
            this.progressPercent += this.progressPerBlock;
            if (!this.isPainting) {
                TryMoveSector(this.progressPercent, this.currentSector);
            }
         
            // print(progressPerBlock.ToString());
            print("El progreso es: "+this.progressPercent.ToString()+"%"); // aqui es
            this.textValue = this.progressPercent.ToString()+"%";
            this.textElement.text = textValue;
        } 
        
    }

    public void TryMoveSector(float progressPercent, Vector2Int currentSector){
        if((progressPercent%25) >= completeSectorThreshold) {
            Vector2Int gridSize = this.grid.getMatrixDimensions(this.grid.canvasSize, this.grid.blockPixelSize);
           
            // if ((currentSector.x == 0) && (currentSector.y == 0)) {
            //     this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x*-1,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
            //     this.currentSector.x = 1;
                
                
            // } else if ((currentSector.x == 1) && (currentSector.y == 0)) {
            //     this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x,this.mainCamera.transform.position.y*-1,this.mainCamera.transform.position.z);
            //     this.currentSector.y = 1;
            // } else if ((currentSector.x == 1) && (currentSector.y == 1)) {
            //     this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x*-1,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
            //     this.currentSector.x = 0;
            // }

            if (currentSector.x==0) {
                this.PaintReaminingIndexes[0] = 0 ;  //[startX, startY, endX, endY]
                this.PaintReaminingIndexes[2] = (gridSize.x/2)-1; 
            } else {
                this.PaintReaminingIndexes[0] = (gridSize.x/2) ;  //[startX, startY, endX, endY]
                this.PaintReaminingIndexes[2] = gridSize.x-1; 
            }
            if (currentSector.y==0) {
                this.PaintReaminingIndexes[1] = (gridSize.y/2);  //[startX, startY, endX, endY]
                this.PaintReaminingIndexes[3] = (gridSize.y)-1;
            } else {
                this.PaintReaminingIndexes[1] =  0;  //[startX, startY, endX, endY]
                this.PaintReaminingIndexes[3] = (gridSize.y/2)-1;
            }

            this.currentPaintingRemainingIndexes = new Vector2Int(this.PaintReaminingIndexes[0], this.PaintReaminingIndexes[1]);
            this.isPainting = true;
           
        }
    }

    public void CompleteMoveSector(){
           if ((currentSector.x == 0) && (currentSector.y == 0)) {
                //this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x*-1,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
               
                this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x+cameraStepX,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
                if (this.mainCamera.transform.position.x> Mathf.Abs(this.initialCamaraCoords.x)) {
                    this.mainCamera.transform.position = new Vector3(Mathf.Abs(this.initialCamaraCoords.x),this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
                    this.currentSector.x = 1;
                    this.isMovingCamera = false;
                }
                
                
            } else if ((currentSector.x == 1) && (currentSector.y == 0)) {
                this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x,this.mainCamera.transform.position.y+cameraStepY,this.mainCamera.transform.position.z);
                if (this.mainCamera.transform.position.y> Mathf.Abs(this.initialCamaraCoords.y)) {
                    this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x,Mathf.Abs(this.initialCamaraCoords.y),this.mainCamera.transform.position.z);
                    this.currentSector.y = 1;
                    this.isMovingCamera = false;
                }
            
            } else if ((currentSector.x == 1) && (currentSector.y == 1)) {
                this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x-cameraStepX,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
               
               if (this.mainCamera.transform.position.x< this.initialCamaraCoords.x) {
                    this.mainCamera.transform.position = new Vector3(this.initialCamaraCoords.x,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
                    this.currentSector.x = 0;
                    this.isMovingCamera = false;
                }
                
                
            } else if ((currentSector.x == 0) && (currentSector.y == 1)) {
                  this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x+cameraStepX,this.mainCamera.transform.position.y-cameraStepY,this.mainCamera.transform.position.z);
                  if ( this.mainCamera.transform.position.x > 0f) {
                    this.mainCamera.transform.position = new Vector3(0,0-cameraStepY,this.mainCamera.transform.position.z);
                    this.cameraStepX=0;
                    this.cameraStepY=0;
                  }

                  this.mainCamera.orthographicSize += 0.01f;
                  if (this.mainCamera.orthographicSize >= 8.9f) {
                     this.mainCamera.orthographicSize = 8.9f;
                     this.isMovingCamera= false;
                  }
            }
    }

    public void PaintRemainingInSector(){
        TryPaintGridSquare(this.currentPaintingRemainingIndexes.x, this.currentPaintingRemainingIndexes.y);
        this.currentPaintingRemainingIndexes.x += 1;
        if (this.currentPaintingRemainingIndexes.x > this.PaintReaminingIndexes[2]) { //endX
            this.currentPaintingRemainingIndexes.x = this.PaintReaminingIndexes[0]; //startx
           
            this.currentPaintingRemainingIndexes.y +=1;
            if (this.currentPaintingRemainingIndexes.y > this.PaintReaminingIndexes[3]) {
                this.isPainting = false;
                this.isMovingCamera = true;
            }
        }
    }

    public void PaintMask(int x, int y, int width, int height){
        // int xCentered = x;
        // int yCentered = y;
        // xCentered -= splashTextures[0].width/2;
        // yCentered-= splashTextures[0].height/2;
        //Color[] cArray = new Color[width*height];
        //Color[] currentPixels = this.newMask.GetPixels(xCentered, yCentered, width, height, 0);
        Color[] imagePixels = this.backgroundImage.GetPixels(x, y, width, height, 0);
        //Color[] splash = this.splashTextures[Random.Range(0,this.splashTextures.Length)].GetPixels();
        // for(int i = 0; i < cArray.Length; i++) {
 
        //     if(splash[i].r >= 0.1) {
        //         cArray[i]=imagePixels[i];
        //     } else {
               
        //         cArray[i] = currentPixels[i];
        //     }
          
        //  }
        this.newMask.SetPixels(x,y,width,height,imagePixels, 0);
 

        this.newMask.Apply();
        this.crenderer.material.mainTexture = newMask;
      
    
    }



    public void ResetCanvas(){
      
        Vector2 canvasSize = new Vector2(currentMask.width, currentMask.height);
        Color[] cArray = new Color[(int)canvasSize.x*(int)canvasSize.y];
        for(int i = 0; i < cArray.Length; i++) {
            cArray[i]=Color.black;
        }
        this.newMask.SetPixels(0,0,(int)canvasSize.x,(int)canvasSize.y,cArray, 0);
        this.newMask.Apply();
        this.crenderer.material.mainTexture = newMask;
    }

    private void OnDestroy() {
        ResetCanvas();
    }

// se borrara GetImageMousePositionOnImage
    private Vector2 GetImageMousePositionOnImage(){
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Sprite sprite = spriteRenderer.sprite;
        Rect rect =  sprite.textureRect;
        float x = pos.x-gameObject.transform.position.x;
        float y = pos.y-gameObject.transform.position.y;
        x *= sprite.pixelsPerUnit;
        y *= sprite.pixelsPerUnit;
        // x*= currentMask.width;
        // y*= currentMask.height;
        x+= rect.width/2;
        y+= rect.height/2;
        x += rect.x;
        y += rect.y;
        int realX = Mathf.FloorToInt(x);
        int realY = Mathf.FloorToInt(y);    
        return(new Vector2(x,y));
    }

    public Vector2Int getLowerLeftCoords(){
        Camera camera = Camera.main;
        Vector3 pos = camera.ViewportToWorldPoint(new Vector3(0,0,camera.nearClipPlane));
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

    public Vector2 worldCoordsToImageCoords(float worldX,float worldY) {
        Sprite sprite = spriteRenderer.sprite;
        Rect rect =  sprite.textureRect;
        float x = worldX-gameObject.transform.position.x;
        float y = worldY-gameObject.transform.position.y;
        x *= sprite.pixelsPerUnit;
        y *= sprite.pixelsPerUnit;
        // x*= currentMask.width;
        // y*= currentMask.height;
        x+= rect.width/2;
        y+= rect.height/2;
        x += rect.x;
        y += rect.y;
        int realX = Mathf.FloorToInt(x);
        int realY = Mathf.FloorToInt(y);    
        return(new Vector2(x,y));
    }

    public int closestDownMultiple(float num, int blockPixelSize, bool isX){
        // returns the index of the closest rounded down blockPixelSize multiple
        int previousMultiple = 0;
        int nextMultiple = 0;
        int maxIteration = (grid.canvasSize.y /blockPixelSize)-1;
        for(int i = 0; i<= maxIteration;i++){
            previousMultiple = nextMultiple;
            nextMultiple = blockPixelSize*(i+1);
            if((previousMultiple <= num)&&(num < nextMultiple)){
                if (isX){
                    return i;
                } else {
                    
                    return (i==0) ? maxIteration: maxIteration-i;
                }
               
            }
           
        }
        return -1;
    }

    public Vector2Int mapIndexToCoord(int Xindex, int YIndex){
        return this.grid.getCoordsMatrix()[YIndex, Xindex];
    }

    public void setInitialProgress(Vector2Int matrixSize){
        this.progressPercent = 0;
        this.progressPerBlock = (100f/(matrixSize.x*matrixSize.y))+0.001f;
        this.currentSector = new Vector2Int(0,0);
        this.cameraStepX = Mathf.Abs(this.initialCamaraCoords.x*2)/(this.CameraTransitionMaxDuration/this.CameraMoveDelay);
        this.cameraStepY = Mathf.Abs(this.initialCamaraCoords.y*2)/(this.CameraTransitionMaxDuration/this.CameraMoveDelay);
       // print((100f/(matrixSize.x*matrixSize.y)).ToString());
    }



}
