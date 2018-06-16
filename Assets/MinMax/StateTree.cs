using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node{
	public List<Node> childs = new List<Node>();
	public Node parent;
	public Node result;
	public string[] matrix = new string[9];
	public int score;
}

public class StateTree: MonoBehaviour {

	public int depthOfSearch=3;
	public int DepthOfSearch{
		get{ return depthOfSearch;}
		set{depthOfSearch = value;}
	}			
	public const int victoryScore=10;
	
	public void FillStates(Node node,bool x,string player, int numSearch=0,bool min=false){	
		node.score = TicTacToeScore.getScoreState(TicTacToeScore.CovertToMatrix(node.matrix,3),player);
		if((depthOfSearch >0 && numSearch > depthOfSearch) || Mathf.Abs(node.score)== victoryScore){
			return;
		}
		bool fillLeft=false;
		for (int i = 0; i < node.matrix.Length; i++) {
			if(string.IsNullOrEmpty(node.matrix[i])){

				Node newNode = new Node();
				node.matrix.CopyTo(newNode.matrix,0);
				newNode.matrix[i] = x?"X":"O";
				node.childs.Add(newNode);
				newNode.parent = node; 
				FillStates(newNode,!x,player,numSearch+1,!min);
				if(!fillLeft){
					fillLeft = true;
					node.score = newNode.score;
					node.result = newNode;
					if(min && CutMinScore(node.score)){
						return;
					}else if(!min && CutMaxScore(node.score)) {
						return;
					}
				}else{
					if(min && AlphaBetaMin(node,newNode)){
						return;
					}else if(!min && AlphaBetaMax(node,newNode)){
						return;
					}
				}
				
			}
		}
	}
	
	public bool AlphaBetaMin(Node node,Node novo){
		if(novo.score < node.score){
			node.score = novo.score;
			node.result = novo;
			if(node.score < node.parent.score)
				return true;
			return false;	
		}
		return false;
	}
	
	public bool AlphaBetaMax(Node node,Node novo){
		if(novo.score > node.score){
			node.score = novo.score;
			node.result = novo;
		}
		if(CutMaxScore(node.score)){
			return true;
		}
		return false;
	}
	
	public bool CutMaxScore(int score){
		return score == victoryScore;
	}
	
	public bool CutMinScore(int score){
		return score == victoryScore*-1;
	}
}
