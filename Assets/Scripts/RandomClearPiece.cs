using UnityEngine;

namespace Match3
{
    public class RandomClearPiece : ActivatablePiece
    {
        public override void Activate()
        {
            Debug.Log("RandomClearPiece активирован!");
            // Clear a random piece on the grid
            var allPieces = piece.GameGridRef.GetPiecesOfType(PieceType.Normal);
            Debug.Log($"Найдено {allPieces.Count} обычных кусочков");
            if (allPieces.Count > 0)
            {
                int randomIndex = Random.Range(0, allPieces.Count);
                GamePiece randomPiece = allPieces[randomIndex];
                Debug.Log($"Очищаю случайный кусочек в ({randomPiece.X},{randomPiece.Y})");
                piece.GameGridRef.ClearPiece(randomPiece.X, randomPiece.Y);
            }

            // Clear self
            Debug.Log("Очищаю себя");
            piece.ClearableComponent.Clear();
        }
    }
}
