# TODO: Implement Click Activation for Special Pieces

## Overview
Modify special pieces (RowClear, ColumnClear, Rainbow) to activate on click instead of automatically during matches. Create a new component for handling activation.

## Tasks
- [ ] Create ActivatablePiece.cs component for click handling
- [ ] Modify GamePiece.cs to handle clicks for special pieces
- [ ] Update ClearLinePiece.cs to add Activate() method
- [ ] Update ClearColorPiece.cs to add Activate() method
- [ ] Modify GameGrid.cs SwapPieces() to remove auto-clear for RowClear/ColumnClear
- [ ] Test click activation functionality
