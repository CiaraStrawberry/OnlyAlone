using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTextureManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer mainTex;

    [SerializeField]
    private Sprite originalSprite;

    private Sprite originalSpriteNewInstance;
    // Start is called before the first frame update
    void Start()
    {
        originalSpriteNewInstance = Instantiate(originalSprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// use the position of a pixel within a texture to get the world space so it can be determined if the light raycast hit it.
    /// </summary>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <returns></returns>
    public Vector2[] getWorldSpaceLocationsOfTextures(float height, float width)
    {
        Sprite currentTexture = mainTex.sprite;

        Vector2 bottomLeft = getBottomLeftOfTexture(mainTex);
        Vector2 topLeft = getTopLeftOfTexture(mainTex);
        Vector2 bottomRight = getBottomRightOfTexture(mainTex);

        float distancePerPixelHeight = (topLeft.y - bottomLeft.y) / height;
        float distancePerPixelWidth = (bottomRight.x - bottomLeft.x) / width;

        Debug.Log(distancePerPixelHeight * height);
        List<Vector2> output = new List<Vector2>();
        int counter = 0;

        for (float a = bottomLeft.y; a <= topLeft.y;)
        {
            counter = 0;
            a += distancePerPixelHeight;
            for (float b = bottomLeft.x; b <= bottomRight.x;)
            {
                b += distancePerPixelWidth;
                counter++;
                if (counter < 301) output.Add(new Vector2(b, a));
                //   if(counter % 650 == 0)  Debug.DrawLine(new Vector3(0,0,0),new Vector3(a,b,0),Color.white,10f);
                // Debug.Log(counter);
            }
        }
        Debug.Log(counter);
        return output.ToArray();
        /*
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(getBottomLeftOfTexture(mainTex));
        Vector2 topLeft = Camera.main.ViewportToWorldPoint(getTopLeftOfTexture(mainTex));
        Vector2 bottomRight = Camera.main.ViewportToWorldPoint(getBottomRightOfTexture(mainTex));



        float heighInPixels = topLeft.y - bottomLeft.y;
        float widthInPixels = bottomRight.x - bottomLeft.x;
        float distancePerPixelHeight = heighInPixels / currentTexture.height;
        float distancePerPixelWidth = widthInPixels / currentTexture.width;
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(bottomLeft.x, bottomLeft.y, 0), Color.white, 10f);
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(topLeft.x, topLeft.y, 0), Color.white, 10f);
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(bottomRight.x, bottomRight.y, 0), Color.white, 10f);

        List<Vector2> output = new List<Vector2>();
        for (float a = bottomLeft.y; a < topLeft.y;)
        {
            a += distancePerPixelHeight;
            for (float b = bottomLeft.x; b < bottomRight.x;)
            {
                b += distancePerPixelWidth;
                output.Add(new Vector2(b, a));
                //   Debug.DrawLine(new Vector3(0,0,0),new Vector3(b,a,0),Color.white,10f);
            }
        }

        return output.ToArray();

        */


    }


    //keeping the memory location permanently asigned for GC reasons.
    // This function gets the distance of every pixel from the center.
    private Vector3 _tempPos;
    public float[] allPixelDistancesFromCenter (Vector2[] input)
    {
        float[] output = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            _tempPos = new Vector3(input[i].x,input[i].y,0);
            output[i] = Vector3.Distance(new Vector3(0,0,0),_tempPos);
        }
        return output;
    }


    private Vector2 getBottomLeftOfTexture(SpriteRenderer img)
    {
        float width = img.sprite.bounds.size.x * img.transform.localScale.x;
        float height = img.sprite.bounds.size.y * img.transform.localScale.y;
        return new Vector2(img.transform.position.x - (width / 2), img.transform.position.y - (height / 2));
    }

    private Vector2 getTopLeftOfTexture(SpriteRenderer img)
    {
        float width = img.sprite.bounds.size.x * img.transform.localScale.x;
        float height = img.sprite.bounds.size.y * img.transform.localScale.y;
        return new Vector2(img.transform.position.x - (width / 2), img.transform.position.y + (height / 2));
    }

    private Vector2 getBottomRightOfTexture(SpriteRenderer img)
    {
        float width = img.sprite.bounds.size.x * img.transform.localScale.x;
        float height = img.sprite.bounds.size.y * img.transform.localScale.y;
        return new Vector2(img.transform.position.x + (width / 2), img.transform.position.y - (height / 2));
    }

    public Texture getTexture ()
    {
        return mainTex.sprite.texture;
    }


    private Vector3 _direction;
    private int _angle;
    private RaycastHit2D _hitData;
    private float _distanceFromCenter;
    public bool[] arePixelsVisible(RaycastHit2D[] hitData,float[] hitDataHitDistances,Vector2[] pixelLocationData,float[] pixelDistanceData)
    {
        Vector3 _up = Vector3.up;
        bool[] output = new bool[pixelLocationData.Length];
        for (int i = 0; i < pixelLocationData.Length;i++)
        {
            _direction = new Vector3(pixelLocationData[i].x, pixelLocationData[i].y, 0) - new Vector3(0,0,0);
            _angle = Mathf.RoundToInt(CalculateAngle(_direction, _up, _up));
            if(_angle < 360 && _angle > -1)  _hitData = hitData[_angle];
            else
            {
                _hitData = hitData[0];
                _angle = 0;
            }
            if (_hitData.collider == null) output[i] = true;
            else
            {
                if (pixelDistanceData[i] > hitDataHitDistances[_angle]) output[i] = false;
                else output[i] = true;
            }
        //  if (i % 23 == 0 && output[i] == true) Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(pixelLocationData[i].x, pixelLocationData[i].y, 0));
        }

        return output;
    }

    public static float CalculateAngle(Vector3 from, Vector3 to,Vector3 up)
    {
        return Quaternion.FromToRotation(up, from - to).eulerAngles.z;
    }

    // reset each pixel in the texture to either on or off depending on it it has been found to be lit by the raycaster.
    public void repaintTexture (bool[] allPixelStatus)
    {
        Texture2D tex2D =  Instantiate((Texture2D)originalSpriteNewInstance.texture);
        Color[] pixels = tex2D.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (allPixelStatus[i] == false)
            {
                pixels[i] = Color.black;
            }
        }
        tex2D.SetPixels(pixels);
        tex2D.Apply();
        mainTex.sprite = Sprite.Create(tex2D, new Rect(0.0f, 0.0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 60f);

    }
}
