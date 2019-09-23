/*
 Author: Aaron Hines
 Edits By:
 Description:
 */

 namespace GameFramework.Actors
{
    /// <summary>
    ///     Denotes what kind of control is placed on this pawn
    /// </summary>
    public enum PawnControlType : int
    {
        None,
        PlayerController,
        AI,
        Network
    }
}
