namespace Match3
{
    public class ClearColorPiece : ClearablePiece
    {
        public ColorType Color { get; set; }

        public override void Clear()
        {
            base.Clear();

            piece.GameGridRef.ClearColor(Color);
        }

        public void Activate()
        {
            piece.GameGridRef.ClearColor(Color);
            // Clear the piece itself after activation
            piece.GameGridRef.ClearPiece(piece.X, piece.Y);
        }
    }
}
