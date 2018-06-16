using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TicTacToe: MonoBehaviour {

	public StateTree tree;
	public bool playerX;
	public bool PlayerX{
		get{return playerX;}
		set{playerX = value;}
	}
	public bool start;
	public bool Start{
		get{return start;}
		set{start = value;}
	}
	public Piece[] board = new Piece[9];
	public Texture circle,x;
	public UnityEvent onVictory;
	public UnityEvent onLose;
	public UnityEvent onTie;
	
	private Node currentMove = new Node();
	private bool CpuPlaying =false;
	private static TicTacToe singleton;
	
	void Awake(){
		singleton = this;
	}
	
	public void StartGame(){
		if(!start){
			CpuPlaying = true;
			CPUplay();
		}
	}
	
	public static void PieceChosed(int num){
		if(!singleton) return;
		singleton.PieceChosed2(num);
	}
	
	public static Texture GetTexture(){
		if(!singleton) return null;
		return singleton.playerX?singleton.x:singleton.circle;
	}
	
	public static bool CPUTurn(){
		return singleton.CpuPlaying;
	}
	
	public void PieceChosed2(int num){
		if(CpuPlaying) return;
		CpuPlaying = true;
		board[num].Selected(playerX?x:circle);
		currentMove.matrix[num] = playerX?"X":"O";
		Invoke("CPUplay",0.5f);
	}
	
	public void CPUplay(){
		tree.FillStates(currentMove,!playerX,!playerX?"X":"O",0,false);
		FillBoard();
		CpuPlaying = false;
		StartCoroutine(CheckResult());
	}
	
	public void FillBoard(){
		if(currentMove.result != null)
			currentMove.result.matrix.CopyTo(currentMove.matrix,0);
		currentMove.childs.Clear();
		currentMove.parent = null;
		currentMove.result = null;
		for (int i = 0; i < board.Length; i++) {
			if(currentMove.matrix[i] == "X"){
				board[i].Selected(x);
			}else if(currentMove.matrix[i] == "O"){
				board[i].Selected(circle);
			}
		}
	}
	
	IEnumerator CheckResult(){
		int score = TicTacToeScore.getScoreState(TicTacToeScore.CovertToMatrix(currentMove.matrix,3),playerX?"X":"O");
		if(score == StateTree.victoryScore){
			TurnOffButtons();
			yield return new WaitForSeconds(1);
			onVictory.Invoke();
		}else if(score == StateTree.victoryScore*-1){
			TurnOffButtons();
			yield return new WaitForSeconds(1);
			onLose.Invoke();
		}else if(Tie()){
			TurnOffButtons();
			yield return new WaitForSeconds(1);
			onTie.Invoke();
		}
	}
	
	bool Tie(){
		for (int i = 0; i < currentMove.matrix.Length; i++){
			if(string.IsNullOrEmpty(currentMove.matrix[i])){
				return false;
			}
		}
		return true;
	}
	
	void TurnOffButtons(){
		for (int i = 0; i < board.Length; i++){
			board[i].Off();
		}
	}
	
	public void ResetGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
