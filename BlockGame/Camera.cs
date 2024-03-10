using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame;

public class Camera {
    private float _speed = 8.0f;
    private float _sensitivity = 180.0f;

    private float _screenWidth;
    private float _screenHeight;

    public Vector3 position;

    Vector3 up    = Vector3.UnitY;
    Vector3 front = -Vector3.UnitZ;
    Vector3 right = Vector3.UnitX;

    private float _pitch;
    private float _yaw = -90.0f;

    private bool _firstMove = true;

    public Vector2 lastPos;

    public Camera(float screenWidth, float screenHeight, Vector3 pos) {
        _screenWidth  = screenWidth;
        _screenHeight = screenHeight;

        position = pos;
    }

    public Matrix4 GetViewMatrix() {
        return Matrix4.LookAt(position, position + front, up);
    }

    public Matrix4 GetProjectionMatrix() {
        return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), _screenWidth / _screenHeight, 0.1f, 100.0f);
    }

    private void UpdateVectors() {
        if (_pitch > 89.0f) {
            _pitch = 89.0f;
        }

        if (_pitch < -89.0f) {
            _pitch = -89.0f;
        }

        front.X = MathF.Cos(MathHelper.DegreesToRadians(_pitch)) * MathF.Cos(MathHelper.DegreesToRadians(_yaw));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(_pitch));
        front.Z = MathF.Cos(MathHelper.DegreesToRadians(_pitch)) * MathF.Sin(MathHelper.DegreesToRadians(_yaw));

        front = Vector3.Normalize(front);

        right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
        up = Vector3.Normalize(Vector3.Cross(right, front));
    }

    public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e) {

        if (input.IsKeyDown(Keys.W)) {
            position += front * _speed * (float)e.Time;
        }

        if (input.IsKeyDown(Keys.A)) {
            position -= right * _speed * (float)e.Time;
        }

        if (input.IsKeyDown(Keys.S)) {
            position -= front * _speed * (float)e.Time;
        }

        if (input.IsKeyDown(Keys.D)) {
            position += right * _speed * (float)e.Time;
        }

        if (input.IsKeyDown(Keys.Space)) {
            position.Y += _speed * (float)e.Time;
        }

        if (input.IsKeyDown(Keys.LeftShift)) {
            position.Y -= _speed * (float)e.Time;
        }

        if (_firstMove) {
            lastPos = new Vector2(mouse.X, mouse.Y);

            _firstMove = false;
        } else {
            var deltaX = mouse.X - lastPos.X;
            var deltaY = mouse.Y - lastPos.Y;

            lastPos = new Vector2(mouse.X, mouse.Y);

            _yaw   += deltaX * _sensitivity * (float)e.Time;
            _pitch -= deltaY * _sensitivity * (float)e.Time;
        }

        UpdateVectors();
    }

    public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e) {
        InputController(input, mouse, e);
    }
}