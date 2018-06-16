using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour , IPointerClickHandler, IPointerExitHandler , IPointerEnterHandler {

	public int id;
	private Renderer rend;
	private Collider col;
	private bool selected;
	
	void Awake(){
		rend = GetComponent<Renderer>();
		col = GetComponent<Collider>();
	}
	
	public void OnPointerClick (PointerEventData eventData){
		if(selected || TicTacToe.CPUTurn()) return;
		TicTacToe.PieceChosed(id);
	}
	
	public void OnPointerEnter (PointerEventData eventData){
		if(selected || TicTacToe.CPUTurn()) return;
		ApplyTexture(TicTacToe.GetTexture());
	}
	
	public void OnPointerExit (PointerEventData eventData){
		if(selected || TicTacToe.CPUTurn()) return;
		ApplyTexture(null);
	}
	
	public void ApplyTexture(Texture tex){
		rend.material.SetTexture("_MainTex",tex);
	}
	
	public void Selected(Texture tex){
		Off();
		ApplyTexture(tex);
	}
	
	public void Off(){
		selected = true;
		col.enabled = false;
	}
	
	
	
}
