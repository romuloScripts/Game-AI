using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Auction : MonoBehaviour {

	public float epsilon=0.2f;
	public Negotiator[] negotiators;
	public AuctionObject[] auctionObjs;
	public bool onStart;

	public void Start(){
		if(onStart)
			ResetAndAuction();
	}

	[ContextMenu("Calc")]
	public void ResetAndAuction(){
		Reset();
		ToAuction();
	}

	public void ToAuction (){
		while (!Completed()) {
			for (int i = 0; i < negotiators.Length; i++) {
				if (negotiators [i].purchased == null) {
					 Offer (negotiators [i]);
				}
			}
		}
	}

	[ContextMenu("Reset")]
	public void Reset(){
		for (int i = 0; i < negotiators.Length; i++) {
			negotiators[i].purchased = null;
		}
		for (int i = 0; i < auctionObjs.Length; i++) {
			auctionObjs[i].price = 0;
		}
	}

	void Remove(AuctionObject o){
		for (int i = 0; i < negotiators.Length; i++) {
			if(negotiators[i].purchased && negotiators[i].purchased == o){
				negotiators[i].purchased = null;
				return;
			}
		}	
	}

	bool Completed(){
		for (int i = 0; i < negotiators.Length; i++) {
			if(!negotiators[i].purchased){
				return false;
			}
		}	
		return true;
	}

	public void Offer(Negotiator negotiator){

		for (int i = 0; i < negotiator.profits.Length; i++) {
			Profit b = negotiator.profits[i];
			b.gain = b.benefit - b.auctionObj.price;
		}

		float max1 = float.MinValue, max2 = float.MinValue; 
		Profit pMax1 = null , pMax2 = null;

		for (int i = 0; i < auctionObjs.Length; i++) {
			Profit b = negotiator.profits[i];
			if(b.gain > max1){
				pMax1 = b;
				max1 = b.gain;
			}
		}

		for (int i = 0; i < auctionObjs.Length; i++) {
			Profit b = negotiator.profits[i];
			if(b.gain > max2 && b != pMax1){
				pMax2 = b;
				max2 = b.gain;
			}
		}

		pMax1.auctionObj.price += pMax1.gain - pMax2.gain + epsilon;
		Remove (pMax1.auctionObj);
		negotiator.purchased = pMax1.auctionObj;
	}
}
