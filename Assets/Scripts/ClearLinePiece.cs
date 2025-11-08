namespace Match3
{
    internal class ClearLinePiece : ClearablePiece
    {
        public bool isRow;

        public override void Clear()
        {
            base.Clear();

            if (isRow)
            {            
                piece.GameGridRef.ClearRow(piece.Y);
            }
            else
            {            
                piece.GameGridRef.ClearColumn(piece.X);
            }
        }

        public void Activate()
        {
            if (isRow)
            {            
                piece.GameGridRef.ClearRow(piece.Y);
            }
            else
            {            
                piece.GameGridRef.ClearColumn(piece.X);
            }
            // Clear the piece itself after activation
            piece.GameGridRef.ClearPiece(piece.X, piece.Y);
        }
    }
}
