/*
 Author: Aaron Hines
 Edits By: 
 Description: interface for defining the function of a phase
 */

 namespace GameFramework.Phases
{
    public interface IPhase 
    {
        PhaseIdentifier identifier { get; set; }
        PhaseManager phaseManager { get; set; }
        void EnterPhase();
        void RunPhase(float deltaTime);
        void ExitPhase();
    }
}
