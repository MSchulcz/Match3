using UnityEngine;

namespace Match3
{
    public class GamePiece : MonoBehaviour
    {
        public int score;

        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set { if (IsMovable()) { _x = value; } }
        }

        public int Y
        {
            get => _y;
            set { if (IsMovable()) { _y = value; } }
        }

        private PieceType _type;

        public PieceType Type => _type;

        private GameGrid _gameGrid;

        public GameGrid GameGridRef => _gameGrid;

        private MovablePiece _movableComponent;

        public MovablePiece MovableComponent => _movableComponent;

        private ColorPiece _colorComponent;

        public ColorPiece ColorComponent => _colorComponent;

        private ClearablePiece _clearableComponent;

        public ClearablePiece ClearableComponent => _clearableComponent;

        private Vector3 _dragStartPos;

        private void Awake()
        {
            _movableComponent = GetComponent<MovablePiece>();
            _colorComponent = GetComponent<ColorPiece>();
            _clearableComponent = GetComponent<ClearablePiece>();
        }

        public void Init(int x, int y, GameGrid gameGrid, PieceType type)
        {
            _x = x;
            _y = y;
            _gameGrid = gameGrid;
            _type = type;
        }

        private void OnMouseEnter() => _gameGrid.EnterPiece(this);

        private void OnMouseDown()
        {
            _dragStartPos = transform.position;
            _gameGrid.PressPiece(this);
        }

        private void OnMouseUp() => _gameGrid.ReleasePiece();

        private void OnMouseDrag()
        {
            if (_gameGrid.IsFilling) return;

            // Convert mouse position to world space
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane; // Set z to near clip plane for screen to world conversion
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0; // Assuming 2D game, set z to 0

            // Calculate drag delta
            Vector3 dragDelta = worldPos - _dragStartPos;

            // Determine primary drag direction (horizontal or vertical)
            bool isHorizontal = Mathf.Abs(dragDelta.x) > Mathf.Abs(dragDelta.y);

            // Restrict movement to the primary axis
            if (isHorizontal)
            {
                worldPos.y = _dragStartPos.y; // Lock Y to start position
            }
            else
            {
                worldPos.x = _dragStartPos.x; // Lock X to start position
            }

            // Get grid bounds
            Vector2 gridMin = _gameGrid.GetWorldPosition(0, _gameGrid.yDim - 1);
            Vector2 gridMax = _gameGrid.GetWorldPosition(_gameGrid.xDim - 1, 0);

            // Clamp position within grid bounds, but allow slight overhang for adjacent pieces
            float cellSize = 1.0f; // Assuming cell size is 1 unit
            worldPos.x = Mathf.Clamp(worldPos.x, gridMin.x - cellSize, gridMax.x + cellSize);
            worldPos.y = Mathf.Clamp(worldPos.y, gridMin.y - cellSize, gridMax.y + cellSize);

            // Move piece to clamped position
            transform.position = worldPos;
        }

        public bool IsMovable() => _movableComponent != null;

        public bool IsColored() => _colorComponent != null;

        public bool IsClearable() => _clearableComponent != null;
    }
}
