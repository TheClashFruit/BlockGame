using BlockGame.Enum;
using BlockGame.Struct;
using OpenTK.Mathematics;

namespace BlockGame.World.Blocks;

public class Stone : Block {
    public Vector3 position;

    private Dictionary<Faces, FaceData> _faces;

    List<Vector2> _uv = new List<Vector2>() {
        (0f, 1f),
        (1f, 1f),
        (1f, 0f),
        (0f, 0f),
    };

    public Stone(Vector3 position) {
        this.position = position;

        _faces = new Dictionary<Faces, FaceData>() {
            {
                Faces.FRONT, new FaceData() {
                    vertices = AddTransformedVertices(rawVertices[Faces.FRONT]),
                    uv = _uv
                }
            },
            {
                Faces.BACK, new FaceData() {
                    vertices = AddTransformedVertices(rawVertices[Faces.BACK]),
                    uv = _uv
                }
            },
            {
                Faces.LEFT, new FaceData() {
                    vertices = AddTransformedVertices(rawVertices[Faces.LEFT]),
                    uv = _uv
                }
            },
            {
                Faces.RIGHT, new FaceData() {
                    vertices = AddTransformedVertices(rawVertices[Faces.RIGHT]),
                    uv = _uv
                }
            },
            {
                Faces.TOP, new FaceData() {
                    vertices = AddTransformedVertices(rawVertices[Faces.TOP]),
                    uv = _uv
                }
            },
            {
                Faces.BOTTOM, new FaceData() {
                    vertices = AddTransformedVertices(rawVertices[Faces.BOTTOM]),
                    uv = _uv
                }
            }
        };
    }

    public List<Vector3> AddTransformedVertices(List<Vector3> vertices) {
        List<Vector3> transformedVertices = new List<Vector3>();

        foreach (Vector3 vert in vertices) {
            transformedVertices.Add(vert + position);
        }

        return transformedVertices;
    }

    public FaceData GetFace(Faces face) {
        return _faces[face];
    }
}