using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
	public enum animalState {Wander, FindFood, FindWater, Eat, Drink, Sleep, FindMate, Mate};

	public int hunger;
	public int thirst;
	public int tiredness;
	public animalState state = animalState.Wander;

	private void Start() {
		UpdateState();
	}

	public void UpdateState() {
		//Wander
		if (state == animalState.Wander) {
			print("X");
		}
	}
}
