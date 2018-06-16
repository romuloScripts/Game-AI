using UnityEngine;
using System.Collections;

public static class TicTacToeScore{
	
	public static int getScoreState(string[,] matrix,string symbol){
		int symbolPossibilities=0;
		int opponentPossibilities=0;

		for (int i = 0; i < matrix.GetLength(0); i++) {
			int numSymbol=0,numOpponent=0;
			for (int j = 0; j < matrix.GetLength(1); j++) {
				if(matrix[i,j] == symbol){
					numSymbol++;
				}else if(!string.IsNullOrEmpty(matrix[i,j])){
					numOpponent++;
				}	
			}
			if(numSymbol >=3){
				return 10;
			}else if(numOpponent >=3){
				return -10;
			}
			SetPossibilities(numSymbol,numOpponent,ref symbolPossibilities,ref opponentPossibilities);
		}
		
		for (int i = 0; i < matrix.GetLength(1); i++){
			int numSymbol=0,numOpponent=0;
			for (int j = 0; j < matrix.GetLength(0); j++){
				if(matrix[j,i] == symbol){
					numSymbol++;
				}else if(!string.IsNullOrEmpty(matrix[j,i])){
					numOpponent++;
				}	
			}
			if(numSymbol >=3){
				return 10;
			}else if(numOpponent >=3){
				return -10;
			}
			SetPossibilities(numSymbol,numOpponent,ref symbolPossibilities,ref opponentPossibilities);
		}
		
		int numSymbol2=0,numOpponent2=0;
		for (int i = 0; i < matrix.GetLength(0); i++){
			if(matrix[i,i] == symbol){
				numSymbol2++;
			}else if(!string.IsNullOrEmpty(matrix[i,i])){
				numOpponent2++;
			}
		}
		if(numSymbol2 >=3){
			return 10;
		}else if(numOpponent2 >=3){
			return -10;
		}
		SetPossibilities(numSymbol2,numOpponent2,ref symbolPossibilities,ref opponentPossibilities);
		
		numSymbol2=numOpponent2=0;
		int coluna = matrix.GetLength(0)-1;
		for (int i = 0; i < matrix.GetLength(0); i++){
			if(matrix[i,coluna] == symbol){
				numSymbol2++;
			}else if(!string.IsNullOrEmpty(matrix[i,coluna])){
				numOpponent2++;
			}
			coluna--;
		}
		if(numSymbol2 >=3){
			return 10;
		}else if(numOpponent2 >=3){
			return -10;
		}
		SetPossibilities(numSymbol2,numOpponent2,ref symbolPossibilities,ref opponentPossibilities);
		return symbolPossibilities - opponentPossibilities;		
	}
	
	public static void SetPossibilities(int numLetters,int numOpponent,ref int symbolPossibilities,ref int opponentPossibilities){
		if(numLetters>0 && numOpponent==0){
			symbolPossibilities++;
		}else if(numOpponent>0 && numLetters==0){
			opponentPossibilities++;
		}else if(numLetters==0 && numOpponent==0){
			symbolPossibilities++;
			opponentPossibilities++;
		}
	}
	
	public static string[,] CovertToMatrix(string[] vector, int m) {
		string[,] result = new string[m,m];
		int indice=0;
		for (int i = 0; i < result.GetLength(0); i++) {
			for (int j = 0; j < result.GetLength(1); j++) {
				result[i,j] = vector[indice];
				indice++;
			}
		}
		return result;
	} 
}
