using BlockGame.Enum;
using OpenTK.Mathematics;

namespace BlockGame.World;

public class Block {
    public readonly Dictionary<Faces, List<Vector3>> rawVertices = new Dictionary<Faces, List<Vector3>>() {
        {
            Faces.FRONT, new List<Vector3>() {
                new Vector3(-0.5f, 0.5f, 0.5f), 
                new Vector3(0.5f, 0.5f, 0.5f), 
                new Vector3(0.5f, -0.5f, 0.5f), 
                new Vector3(-0.5f, -0.5f, 0.5f), 
            }
        },
        {
            Faces.BACK, new List<Vector3>() {
                new Vector3(0.5f, 0.5f, -0.5f), 
                new Vector3(-0.5f, 0.5f, -0.5f), 
                new Vector3(-0.5f, -0.5f, -0.5f), 
                new Vector3(0.5f, -0.5f, -0.5f), 
            }
        },
        {
            Faces.LEFT, new List<Vector3>() {
                new Vector3(-0.5f, 0.5f, -0.5f), 
                new Vector3(-0.5f, 0.5f, 0.5f), 
                new Vector3(-0.5f, -0.5f, 0.5f), 
                new Vector3(-0.5f, -0.5f, -0.5f), 
            }
        },
        {
            Faces.RIGHT, new List<Vector3>() {
                new Vector3(0.5f, 0.5f, 0.5f), 
                new Vector3(0.5f, 0.5f, -0.5f), 
                new Vector3(0.5f, -0.5f, -0.5f), 
                new Vector3(0.5f, -0.5f, 0.5f), 
            }
        },
        {
            Faces.TOP, new List<Vector3>() {
                new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, 0.5f), 
                new Vector3(-0.5f, 0.5f, 0.5f), 
            }
        },
        {
            Faces.BOTTOM, new List<Vector3>() {
                new Vector3(-0.5f, -0.5f, 0.5f), 
                new Vector3(0.5f, -0.5f, 0.5f), 
                new Vector3(0.5f, -0.5f, -0.5f), 
                new Vector3(-0.5f, -0.5f, -0.5f), 
            }
        }
    };
}