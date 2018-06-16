using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UtilMethods : MonoBehaviour {

	public static GameObject activeMenu;
	public void LoadScene(string scene){
		SceneManager.LoadScene(scene);
	}
	
	public void LoadScene(int scene){
		SceneManager.LoadScene(scene);
	}
	
	public void ToggleAudio(){
		AudioListener.volume = AudioListener.volume == 1?0:1;
	}
	
	public void MoveTarget(Transform target){
		StopCoroutine("Move");
		StartCoroutine(Move(transform,target));
	}
	
	public IEnumerator Move(Transform trans,Transform target){
		float num=0;
		while (num <= 1) 
		{
			trans.position = Vector3.Lerp(trans.position,target.position,num);
			num+= Time.unscaledDeltaTime;
			yield return null;
		}
		trans.position = target.position;
	}
	
	public void ActivateMenu(GameObject menu){
		if(activeMenu)activeMenu.SetActive(false);
		menu.SetActive(true);
		activeMenu = menu;
	}
	
	public void Teleport(Transform t){
		transform.position = t.position;
	}
	
	public void CopyAndParent(Transform trans){
		transform.parent = null;
		if(!trans)return;
		transform.position = trans.position;
		transform.rotation= trans.rotation;
		transform.localScale = trans.lossyScale;
		transform.parent = trans;
	}
	
}
