using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollisionScript : MonoBehaviour
{
    SpriteRenderer spr;
    public CustomCollisionScript other;

    public bool isPixelCollision = false;
    private bool isColliding = false;
    public bool debugMode = false;

    public GameObject square;

	// Use this for initialization
	void Start ()
    {
        this.spr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        DetectCollision();
        DebugRenderer();
    }

    void DetectCollision()
    {
        if(!isPixelCollision)
        {
            isColliding = this.spr.bounds.Intersects(other.spr.bounds);
        }
        else
        {
            if(this.spr.bounds.Intersects(other.spr.bounds))
            {
                //Rect 1
                Bounds2D r1 = new Bounds2D()
                {
                    minX = this.spr.bounds.min.x,
                    maxX = this.spr.bounds.max.x,
                    minY = this.spr.bounds.min.y,
                    maxY = this.spr.bounds.max.y
                };

                //Debug.Log(string.Format("{0}, {1}, {2}, {3}", r1.minX, r1.maxX, r1.minY, r1.maxY));

                //Rect 2
                Bounds2D r2 = new Bounds2D()
                {
                    minX = other.spr.bounds.min.x,
                    maxX = other.spr.bounds.max.x,
                    minY = other.spr.bounds.min.y,
                    maxY = other.spr.bounds.max.y
                };

                //Intersection points (Largest from Mins, Smallest from Maxs)
                float x1 = Mathf.Max(r1.minX, r2.minX);
                float x2 = Mathf.Min(r1.maxX, r2.maxX);
                float y1 = Mathf.Max(r1.minY, r2.minY);
                float y2 = Mathf.Min(r1.maxY, r2.maxY);

                // Get section gobal coordinate and size
                Rect section = new Rect
                (
                    Mathf.Min(x1 , x2),
                    Mathf.Min(y1 , y2),
                    Mathf.Abs(x1 - x2),
                    Mathf.Abs(y1 - y2)
                );

                //Convert section to Local texture space
                Rect r1Local = new Rect
                (
                    section.x - r1.minX,
                    section.y - r1.minY,
                    section.width,
                    section.height
                );

//                if(debugMode)
//                DebugDrawRect(r1Local, Color.red, (Vector3)section.position);

                //Debug.Log(string.Format("{0}, {1}", r1Local.x, r1Local.y));

                Rect r2Local = new Rect
                (
                    section.x - r2.minX,
                    section.y - r2.minY,
                    section.width,
                    section.height
                );

//                if(debugMode)
//                DebugDrawRect(r2Local, Color.blue, (Vector3)section.position);

                //Get color information within local section
                Color[] r1Colors = this.spr.sprite.texture.GetPixels
                (
                    (int)(r1Local.min.x),
                    (int)(r1Local.min.y),
                    (int)(r1Local.width  * this.spr.sprite.pixelsPerUnit),
                    (int)(r1Local.height * this.spr.sprite.pixelsPerUnit)
                );

                Color[] r2Colors = other.spr.sprite.texture.GetPixels
                (
                    (int)(r2Local.min.x),
                    (int)(r2Local.min.y),
                    (int)(r2Local.width  * other.spr.sprite.pixelsPerUnit),
                    (int)(r2Local.height * other.spr.sprite.pixelsPerUnit)
                );

                //Compare both sprite color information
                int i = 0;
                int j = 0;
                while(i < r1Colors.Length && j < r2Colors.Length)
                {
                    if(r1Colors[i].a == 1.0f && r2Colors[j].a == 1.0f)
                    {
                        //Return colliding if both pixel are NOT TRANSPARENT, i.e color.a == 1f
                        isColliding = true;
                        return;
                    }

                    i++;
                    j++;
                }

                isColliding = false;
            }
            else
            {
                isColliding = false;
            }
        }
    }

    //Render sprite red if collison is detected
    void DebugRenderer()
    {
        if(debugMode)
        {
            if(isColliding)
            {
                spr.color = Color.red;
            }
            else
            {
                spr.color = Color.white;
            }
        }
    }

    void DebugDrawRect(Rect rect, Color color, Vector3 offset)
    {
        Debug.DrawLine(new Vector3(rect.min.x, rect.min.y, 0) + offset, new Vector3(rect.max.x, rect.min.y, 0) + offset, color);
        Debug.DrawLine(new Vector3(rect.min.x, rect.min.y, 0) + offset, new Vector3(rect.min.x, rect.max.y, 0) + offset, color);
        Debug.DrawLine(new Vector3(rect.max.x, rect.max.y, 0) + offset, new Vector3(rect.max.x, rect.min.y, 0) + offset, color);
        Debug.DrawLine(new Vector3(rect.max.x, rect.max.y, 0) + offset, new Vector3(rect.min.x, rect.max.y, 0) + offset, color);
    }
}
