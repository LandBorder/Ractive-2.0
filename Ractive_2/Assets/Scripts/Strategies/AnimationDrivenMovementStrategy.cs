using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

// TODO: Rework hard coded choreography path

// The classical Strategy Pattern is not able to use strategies that need different parameters.
// Therfore an extend version of the pattern was implemented using a Parameter class.
// "Extending the Strategy Pattern for parameterized Algorithms" https://hillside.net/plop/2010/papers/sobajic.pdf

public class AnimationDrivenMovementStrategy : MovementStrategy
{
    private ChoreographyHandler _choreographyHandler = new ChoreographyHandler();
    public AnimationDrivenMovementStrategy(NavMeshAgent agent, Camera camera, ThirdPersonCharacter character)
    {
        parameters = new Parameter[3];
        parameters[0] = new NavMeshAgentParameter("NavMesh Agent", agent);
        parameters[1] = new CameraParameter("Camera", camera);
        parameters[2] = new ThirdPersonCharacterParameter("Third Person Character", character);
    }

    NavMeshAgent navMeshAgent;
    Camera xrCamera;
    ThirdPersonCharacter thirdPersonCharacter;

    public override void Initialize()
    {
        navMeshAgent = ((NavMeshAgentParameter)parameters[0]).GetValue();

        navMeshAgent.updateRotation = false;
    }

    public override void Execute()
    {
        navMeshAgent = ((NavMeshAgentParameter)parameters[0]).GetValue();
        xrCamera = ((CameraParameter)parameters[1]).GetValue();
        thirdPersonCharacter = ((ThirdPersonCharacterParameter)parameters[2]).GetValue();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = xrCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _choreographyHandler.Load("tears_in_rain");
                _choreographyHandler.currentStoryBeat.previousPosition = navMeshAgent.transform.position;
                _choreographyHandler.currentStoryBeat.targetPosition = hit.point;
                _choreographyHandler.Save();
            }
        }

        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            thirdPersonCharacter.Move(navMeshAgent.desiredVelocity, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }
}
