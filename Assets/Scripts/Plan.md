# Plan for Click Activation of Special Pieces

## Information Gathered
- Special pieces: RowClear and ColumnClear (use ClearLinePiece component, activate ClearRow/ClearColumn), Rainbow (uses ClearColorPiece, activates ClearColor with ColorType.Any).
- Current behavior: 
  - In GameGrid.SwapPieces: If swapping creates/ involves special, auto-activates Rainbow by setting color and calling ClearPiece; auto-clears RowClear/ColumnClear after match processing.
  - In ClearAllValidMatches: Matches are cleared, specials spawned, but specials can be included in future matches since they have ColorComponent and IsClearable().
  - ClearLinePiece.isRow is not set during spawning (potential bug; defaults to false, always clears column).
  - Input handling in GamePiece.OnMouseDown calls PressPiece for drag; no click-only logic.
  - Specials have MovablePiece, so draggable, but we want click to activate instead of drag.
- Dependencies: GameGrid manages grid/matches/swaps; GamePiece handles input/components; ClearLinePiece/ClearColorPiece handle effects via Clear().
- No external deps; all in Scripts folder.

## Plan
### File-level Updates
1. **ClearLinePiece.cs**:
   - Change 'internal class' to 'public class' for access from GamePiece.
   - Keep added Activate() method (does effect + ClearPiece self).
   - Clear() remains for fallback (if matched somehow).

2. **ClearColorPiece.cs**:
   - Keep added Activate() method (does ClearColor + ClearPiece self).
   - Clear() remains for fallback.

3. **GamePiece.cs**:
   - In OnMouseDown: Check if Type is special (RowClear, ColumnClear, Rainbow). If yes, call new ActivateSpecial() method. Else, proceed with drag (set _dragStartPos, PressPiece).
   - Add private void ActivateSpecial(): Get ClearLinePiece or ClearColorPiece component, call Activate() if present. Fallback to ClearableComponent.Clear() if needed. This activates effect and removes piece.

4. **GameGrid.cs**:
   - Add private bool IsSpecial(PieceType type) { return type == PieceType.RowClear || type == PieceType.ColumnClear || type == PieceType.Rainbow; }
   - In ClearAllValidMatches loop: Before GetMatch, add if (IsSpecial(_pieces[x, y].Type)) continue; to skip specials in matching.
   - In SwapPieces:
     - Remove the two if blocks for piece1/2.Type == PieceType.Rainbow (no auto-set color and ClearPiece).
     - Remove the two if blocks for piece1/2.Type == PieceType.RowClear || ColumnClear (no auto ClearPiece).
   - In ClearAllValidMatches spawning for match.Count == 4:
     - After SpawnNewPiece, add: ClearLinePiece clp = newPiece.GetComponent<ClearLinePiece>(); if (clp) clp.isRow = (specialPieceType == PieceType.RowClear);
     - Move the color set inside if for lines.
   - For Rainbow spawning: Already sets ColorType.Any.

### Dependent Files to be Edited
- ClearLinePiece.cs (make public, already has Activate).
- ClearColorPiece.cs (already has Activate).
- GamePiece.cs (add click logic and ActivateSpecial).
- GameGrid.cs (add IsSpecial, skip in matches, remove auto-activations, set isRow on spawn).

No new files needed (ActivatablePiece created but optional; can use direct component calls).

## Followup Steps
- [ ] Install no new packages (Unity built-in).
- [ ] Edit files as per plan.
- [ ] Test: Run game, create special piece via 4+ match, verify no auto-activation, drag normal pieces, click special to activate effect and remove it.
- [ ] Verify no regressions: Normal matches work, filling, hints.
- [ ] If issues, debug with Unity console/breakpoints.
- [ ] Update TODO.md with completion.

This plan ensures specials are movable but activate on click (single tap, no drag). Confirms with user before edits.
