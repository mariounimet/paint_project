using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    private Texture2D currentMask;
    private Texture2D newMask;
    public Texture2D[] backgroundImage;
    public int currentImage;
    private Renderer crenderer;
    private SpriteRenderer spriteRenderer;
    private float progressPercent;
    private float progressPerBlock;
    private Vector2Int currentSector; //0 for negative, 1 for positive
    public int xOffset;
    public int yOffset;
    [Range(1, 24)] public int completeSectorThreshold; // 20 is good
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
    public GameObject player;
    public TouchManagerScript touchManager;
    public Texture2D originalBackground;    

    public string textValue;
    public Text textElement;
    private float textPercentage;
    public AudioManagerScript AudioManager;
    public PauseMenu pauseMenu;


   

    public GridManagerScript grid;
    void Start()
    {
       
        crenderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentMask = (Texture2D) crenderer.material.GetTexture("_PaintMask");
        newMask = currentMask;
        setInitialProgress(this.grid.getMatrixDimensions(this.grid.canvasSize,this.grid.blockPixelSize));
        ResetCanvas();

    

        
    }

    // Update is called once per frame
    void Update()
    {
    //   if(Input.GetMouseButtonDown(0)){
    //         detectPaint(Camera.main.ScreenToWorldPoint);
    //   }
       

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

    public void detectPaint(Vector3 enemypos){

           
       
            // Vector2 realEnemyPos = Camera.main.ScreenToWorldPoint(enemypos);
            Vector2 pos = worldCoordsToImageCoords(enemypos.x, enemypos.y);
           

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
    public void resetProgressBar(){
        this.progressPercent = 0;
        this.currentPaintingRemainingIndexes.x = 0;
        this.currentPaintingRemainingIndexes.y = 0;
        setInitialProgress(this.grid.getMatrixDimensions(this.grid.canvasSize,this.grid.blockPixelSize));
        // Slider silderHealthBar = GameObject.Find("Health Bar").GetComponent<Slider>();
        // silderHealthBar.value = 0;
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
            // print("El progreso es: "+this.progressPercent.ToString()+"%"); // aqui es
            this.textPercentage = Mathf.Round(this.progressPercent);
            this.textValue = this.textPercentage.ToString()+"%";
            textElement.text = textValue;
            GameObject silderHealthBarGameObject = GameObject.Find("Health Bar");
            if (silderHealthBarGameObject) {
                Slider slider = silderHealthBarGameObject.GetComponent<Slider>();
                if (slider){
                slider.value = this.progressPercent/100;
            }
           
            }
           
        } 
        
    }

    public void TryMoveSector(float progressPercent, Vector2Int currentSector){
        if((progressPercent%25) >= completeSectorThreshold) {
            Vector2Int gridSize = this.grid.getMatrixDimensions(this.grid.canvasSize, this.grid.blockPixelSize);
           

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
            GameObject.Find("Spawner").GetComponent<Spawner>().canSpawnChange(false);
            GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>().changeStage();
        }
    }

    public void CompleteMoveSector(){
           if ((currentSector.x == 0) && (currentSector.y == 0)) {
                //this.mainCamera.transform.position = new Vector3(this.mainCamera.transform.position.x*-1,this.mainCamera.transform.position.y,this.mainCamera.transform.position.z);
               
                this.player.transform.position = new Vector3(this.player.transform.position.x+cameraStepX,this.player.transform.position.y,this.player.transform.position.z);
                this.camera.transform.position = new Vector3(this.camera.transform.position.x+cameraStepX,this.camera.transform.position.y,this.camera.transform.position.z);
                if (this.camera.transform.position.x> Mathf.Abs(this.initialCamaraCoords.x)) {
                    this.camera.transform.position = new Vector3(Mathf.Abs(this.initialCamaraCoords.x),this.camera.transform.position.y,this.camera.transform.position.z);
                    this.currentSector.x = 1;
                    this.isMovingCamera = false;
                    this.touchManager.setMiddleOfScreen(this.camera.transform.position.x);
                    this.AudioManager.PlayNextLayer();
                }
                
                
            } else if ((currentSector.x == 1) && (currentSector.y == 0)) {
                this.player.transform.position = new Vector3(this.player.transform.position.x,this.player.transform.position.y +cameraStepY,this.player.transform.position.z);
                this.camera.transform.position = new Vector3(this.camera.transform.position.x,this.camera.transform.position.y+cameraStepY,this.camera.transform.position.z);
                if (this.camera.transform.position.y> Mathf.Abs(this.initialCamaraCoords.y)) {
                    this.camera.transform.position = new Vector3(this.camera.transform.position.x,Mathf.Abs(this.initialCamaraCoords.y),this.camera.transform.position.z);
                    this.currentSector.y = 1;
                    this.isMovingCamera = false;
                    this.AudioManager.PlayNextLayer();

                }
            
            } else if ((currentSector.x == 1) && (currentSector.y == 1)) {
                this.player.transform.position = new Vector3(this.player.transform.position.x-cameraStepX,this.player.transform.position.y,this.player.transform.position.z);
                
                this.camera.transform.position = new Vector3(this.camera.transform.position.x-cameraStepX,this.camera.transform.position.y,this.camera.transform.position.z);
               
               if (this.camera.transform.position.x< this.initialCamaraCoords.x) {
                    this.camera.transform.position = new Vector3(this.initialCamaraCoords.x+0.18f,this.camera.transform.position.y,this.camera.transform.position.z);
                    this.currentSector.x = 0;
                    this.isMovingCamera = false;
                    this.touchManager.setMiddleOfScreen(this.camera.transform.position.x);
                    this.AudioManager.PlayNextLayer();
                }
                
                
            } else if ((currentSector.x == 0) && (currentSector.y == 1)) {
                  this.camera.transform.position = new Vector3(this.camera.transform.position.x+cameraStepX,this.camera.transform.position.y-cameraStepY,this.camera.transform.position.z);
                  if ( this.camera.transform.position.x > 0f) {
                    this.camera.transform.position = new Vector3(0,0-cameraStepY,this.camera.transform.position.z);
                    this.cameraStepX=0;
                    this.cameraStepY=0;
                  }
                  Camera mainCamera = this.camera.GetComponentInChildren<Camera>();
                  mainCamera.orthographicSize += 0.03f;
                  if (mainCamera.orthographicSize >= 8.9f) {
                     mainCamera.orthographicSize = 8.9f;
                     this.isMovingCamera= false;
                    
                     
                     //WIN LEVEL
                     this.player.GetComponent<Player>().resetPlayer(false);
                  }
            }
        GameObject.Find("Spawner").GetComponent<Spawner>().canSpawnChange(true);
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
        Color[] imagePixels = this.backgroundImage[currentImage].GetPixels(x, y, width, height, 0);
       
        this.newMask.SetPixels(x,y,width,height,imagePixels, 0);
 

        this.newMask.Apply();
        this.crenderer.material.mainTexture = newMask;
      
    
    }



    public void ResetCanvas(){
      
        Vector2 canvasSize = new Vector2(currentMask.width, currentMask.height);
        // Color[] cArray = new Color[(int)canvasSize.x*(int)canvasSize.y];
        // for(int i = 0; i < cArray.Length; i++) {
        //     cArray[i]=Color.black;
        // }
        Color[] cArray = this.originalBackground.GetPixels(0,0,2048,2048);

        this.newMask.SetPixels(0,0,(int)canvasSize.x,(int)canvasSize.y,cArray, 0);
        this.newMask.Apply();
        this.crenderer.material.mainTexture = newMask;
    }

    private void OnDestroy() {
        ResetCanvas();
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

// se borrara GetImageMousePositionOnImage
    // private Vector2 GetImageMousePositionOnImage(){
    //     Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     Sprite sprite = spriteRenderer.sprite;
    //     Rect rect =  sprite.textureRect;
    //     float x = pos.x-gameObject.transform.position.x;
    //     float y = pos.y-gameObject.transform.position.y;
    //     x *= sprite.pixelsPerUnit;
    //     y *= sprite.pixelsPerUnit;
    //     // x*= currentMask.width;
    //     // y*= currentMask.height;
    //     x+= rect.width/2;
    //     y+= rect.height/2;
    //     x += rect.x;
    //     y += rect.y;
    //     int realX = Mathf.FloorToInt(x);
    //     int realY = Mathf.FloorToInt(y);    
    //     return(new Vector2(x,y));
    // }

    // public Vector2Int getLowerLeftCoords(){
    //     Camera camera = Camera.main;
    //     Vector3 pos = camera.ViewportToWorldPoint(new Vector3(0,0,camera.nearClipPlane));
    //     Sprite sprite = spriteRenderer.sprite;
    //     Rect rect =  sprite.textureRect;
    //     float x = pos.x-gameObject.transform.position.x;
    //     float y = pos.y-gameObject.transform.position.y;
    //     x *= sprite.pixelsPerUnit;
    //     y *= sprite.pixelsPerUnit;
    //     x+= rect.width/2;
    //     y+= rect.height/2;
    //     x += rect.x;
    //     y += rect.y;
    //     int realX = Mathf.FloorToInt(x);
    //     int realY = Mathf.FloorToInt(y); 

    //     return new Vector2Int(realX, realY);
    // }

}
