using UnityEngine;
using System.Collections;

public class attackSMB : StateMachineBehaviour {

	public hero mnBehaviour; 

	float duration;

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{	
		mnBehaviour.setCurrentAnim(stateInfo.fullPathHash);
		
		if(stateInfo.IsName("attack")){
			
			duration = stateInfo.length;

			mnBehaviour.setNextFire (duration);
		}
	}

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{	
		if(stateInfo.IsName("attack")){
			mnBehaviour.newAttack ();
		}
	}

}
