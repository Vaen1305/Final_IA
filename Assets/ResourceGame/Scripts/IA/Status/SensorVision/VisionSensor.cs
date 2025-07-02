using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class DataViewBase
{
    #region RangeView


    [Header("----- RangeView -----")]
    [Range(0, 180)]
    public float angle = 30f;
    public float height = 1.0f;
    public float distance = 0f;
    public Color meshSightIn = Color.red;
    public Color meshSightOut = Color.red;
    public Mesh mesh;

    [Header("----- Owner ----- ")]
    public Health Owner;

    public bool InSight=false;
    #endregion
    [Header("----- DrawGizmo ----- ")]
    public bool IsDrawGizmo = false;

    [Header("----- Occlusionlayers ----- ")]
    public LayerMask Occlusionlayers;

    [Header("----- Occlusionlayers ----- ")]
    public bool InsideObject = false;
    
    public DataViewBase()
    { }
    
    public virtual void CreateMesh()
    {
        mesh = CreateMeshVision();
    }
    
    public virtual bool IsInSight(Transform enemyAimOffset)
    {
        this.InSight = false;

        if (enemyAimOffset == null) return this.InSight;

        Vector3 origin = this.Owner.AimOffset.position;
        Vector3 dest = enemyAimOffset.position;
        Vector3 direcction = dest - origin;

        if(direcction.magnitude>distance)
            return this.InSight;

        if (dest.y < -(this.height + this.Owner.transform.position.y) || dest.y > (this.height + this.Owner.transform.position.y))
        {
            return this.InSight;
        }

        direcction.y = 0;

        float deltaAngle = Vector3.Angle(direcction.normalized, this.Owner.transform.forward);

        if (deltaAngle > this.angle)
        {
            return this.InSight;
        }



        if (Physics.Linecast(origin, dest, this.Occlusionlayers) && this.InsideObject)
        {
            return this.InSight;
        }

        this.InSight = true;
        return this.InSight;
    }
    
    Mesh CreateMeshVision()
    {
        Mesh mesh = new Mesh();
        int segments = 10;
        int numTriangles = (segments * 4) + 4;
        int numVertices = numTriangles * 3;
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;
            // top 
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
            // bottom 
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;

        }


        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;

    }
    public virtual void OnDrawGizmos()
    {
        if (!IsDrawGizmo) return;

        if (mesh != null && Owner != null)
        {
            if(InSight)
                Gizmos.color = meshSightIn;
            else
                Gizmos.color = meshSightOut;

            Gizmos.DrawMesh(mesh, Owner.transform.position, Owner.transform.rotation);
        }
    }
}

public class VisionSensor : MonoBehaviour
{
    [Header("Main Vision")]
    public DataViewBase MainVision = new DataViewBase();
    //[Header("Vision Near")]
    //public DataViewBase NearVision = new DataViewBase();
    [Space(20)]
    [Header("Enemy View")]
    public Health EnemyView;

    [Header("Allied View")]
    public Health AlliedView;


    [Header("Onwer Health")]
    public Health health;

    [Space(20)]
    [Header("Scan Layer Mask")]
    public LayerMask ScanLayerMask;
    [Space(20)]
    [Header("Coun Enemy View")]
    public int CountEnemyView=0;
    [Space(20)]
    [Header("Frame Rate")]
    #region Rate
    protected int index = 0;
    protected float[] arrayRate;
    protected int bufferSize = 10;
    public float randomWaitScandMin = 1;
    public float randomWaitScandMax = 1;


    protected float Framerate = 0;
    #endregion
    private void Start()
    {
        LoadComponent();
    }
    public virtual void LoadComponent()
    {

        MainVision.Owner = GetComponent<Health>();

        Framerate = 0;
        index = 0;
        arrayRate = new float[bufferSize];
        for (int i = 0; i < arrayRate.Length; i++)
        {
            arrayRate[i] = (float)UnityEngine.Random.Range(randomWaitScandMin, randomWaitScandMax);
        }

        health = GetComponent<Health>();
    }
    public virtual void UpdateScand()
    {
        if (Framerate > arrayRate[index])
        {
            index++;
            index = index % arrayRate.Length;
            Scan();
            Framerate = 0;
        }
        Framerate += Time.deltaTime;
    }
    public virtual void Scan()
    {

    }
    public void ExtractViewEnemy(ref float min_dist, Health _health)
    {
        if (!IsAllies(_health))
        {
            float dist = (transform.position - _health.transform.position).magnitude;
            if (min_dist > dist)
            {
                EnemyView = _health;
                min_dist = dist;
            }

        }
        else
        {
            AlliedView = _health;
        }

    }
    public void CreateMesh()
    {
        MainVision.CreateMesh();
        
    }
    public virtual bool IsAllies(Health heatlhScan)
    {
        for (int j = 0; (health != null && j < health.typeAgentAllies.Length); j++)
        {
            if (health.typeAgentAllies[j] == heatlhScan.typeAgent)
            {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsNotIsThis( GameObject obj2)
    {
        return (gameObject.GetInstanceID() != obj2.GetInstanceID());
    }
    public void DrawGizmos()
    {

        MainVision.OnDrawGizmos();
        

        Gizmos.color = Color.red;
        if(EnemyView!=null)
        {
            Gizmos.DrawLine(MainVision.Owner.AimOffset.position,EnemyView.AimOffset.position);
        }
        
    }
}
