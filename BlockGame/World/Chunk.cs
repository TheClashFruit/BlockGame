using BlockGame.Enum;
using BlockGame.Graphics;
using BlockGame.World.Blocks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BlockGame.World;

public class Chunk {
    private List<Vector3> _chunkVertices;
    private List<Vector2> _chunkUVs;
    private List<uint> _chunkIndices;

    const int size = 16;
    const int height = 256;

    public Vector3 position;

    private uint _iCount;

    private VAO _vao;
    private IBO _ibo;

    private VBO _vboVertex;
    private VBO _vboUV;

    Texture _texture;

    public Chunk(Vector3 pos) {
        position = pos;

        _chunkVertices = new List<Vector3>();
        _chunkUVs      = new List<Vector2>();
        _chunkIndices  = new List<uint>();

        GenerateChunk();
        Build();
    }

    public void GenerateWorld() {}

    public void GenerateChunk() {
        for (int x = 0; x <= 16; x++) {
            for (int y = 0; y <= 256; y++) {
                for (int z = 0; z <= 16; z++) {
                    Stone stone = new Stone(new Vector3(x, y, z));

                    int faceCount = 0;

                    if (x == 0) {
                        var leftFaceData = stone.GetFace(Faces.LEFT);

                        _chunkVertices.AddRange(leftFaceData.vertices);
                        _chunkUVs.AddRange(leftFaceData.uv);

                        faceCount++;
                    }

                    if (x == 16) {
                        var rightFaceData = stone.GetFace(Faces.RIGHT);

                        _chunkVertices.AddRange(rightFaceData.vertices);
                        _chunkUVs.AddRange(rightFaceData.uv);

                        faceCount++;
                    }

                    if (z == 0) {
                        var backFaceData = stone.GetFace(Faces.BACK);

                        _chunkVertices.AddRange(backFaceData.vertices);
                        _chunkUVs.AddRange(backFaceData.uv);

                        faceCount++;
                    }

                    if (z == 16) {
                        var frontFaceData = stone.GetFace(Faces.FRONT);

                        _chunkVertices.AddRange(frontFaceData.vertices);
                        _chunkUVs.AddRange(frontFaceData.uv);

                        faceCount++;
                    }

                    if (y == 0) {
                        var bottomFaceData = stone.GetFace(Faces.BOTTOM);

                        _chunkVertices.AddRange(bottomFaceData.vertices);
                        _chunkUVs.AddRange(bottomFaceData.uv);

                        faceCount++;
                    }

                    if (y == 256) {
                        var topFaceData = stone.GetFace(Faces.TOP);

                        _chunkVertices.AddRange(topFaceData.vertices);
                        _chunkUVs.AddRange(topFaceData.uv);

                        faceCount++;
                    }

                    AddIndices(faceCount);
                }
            }
        }
    }

    public void AddIndices(int amtFaces) {
        for (int i = 0; i < amtFaces; i++) {
            _chunkIndices.Add(0 + _iCount);
            _chunkIndices.Add(1 + _iCount);
            _chunkIndices.Add(2 + _iCount);
            _chunkIndices.Add(2 + _iCount);
            _chunkIndices.Add(3 + _iCount);
            _chunkIndices.Add(0 + _iCount);

            _iCount += 4;
        }
    }

    public void Build() {
        _vao       = new VAO();
        _vboVertex = new VBO(_chunkVertices);
        _vboUV     = new VBO(_chunkUVs);

        _vao.Bind();
        _vboVertex.Bind();
        _vboUV.Bind();

        _vao.LinkToVao(0, 3, _vboVertex);
        _vao.LinkToVao(1, 2, _vboUV);

        _ibo = new IBO(_chunkIndices);

        _texture = new Texture("Resources/textures/block/stone.png");
    }

    public void Render(Shader _shader) {
        _shader.Bind();

        _vao.Bind();
        _ibo.Bind();

        _texture.Bind();

        GL.DrawElements(PrimitiveType.Triangles, _chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
    }

    public void Dispose() {
        _vao.Dispose();
        _vboVertex.Dispose();
        _vboUV.Dispose();
        _ibo.Dispose();
        _texture.Dispose();
    }
}