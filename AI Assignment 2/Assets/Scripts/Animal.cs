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
	private Vector3 targetPos;
	private float speed = 0.5f;
	private float wanderRange = 2;

	private void Start() {
		targetPos = transform.position;
		UpdateState();
	}

	private void Update() {
		targetPos = new Vector3(Mathf.Clamp(targetPos.x, -8, 8), Mathf.Clamp(targetPos.y, -5, 5), 0);
		transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
		if (transform.position == targetPos && state == animalState.Wander) {
			UpdateState();
		}
	}

	public void UpdateState() {
		//Wander
		if (state == animalState.Wander) {
			hunger += Random.Range(1,10);
			thirst += Random.Range(1, 10);
			tiredness += Random.Range(1, 10);

			if (Random.value*100 < thirst) {
				state = animalState.FindWater;
				UpdateState();
			} else {
				targetPos = new Vector3(Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange), 
										Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange), 0);
			}
		}

		//FindWater
		if (state == animalState.FindWater) {
			print("C");
		}
	}
}
