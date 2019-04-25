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
		StartCoroutine(UpdateState());
	}

	private void Update() {
		targetPos = new Vector3(Mathf.Clamp(targetPos.x, -8, 8), Mathf.Clamp(targetPos.y, -5, 5), 0);
		transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

		if (transform.position == targetPos && state == animalState.Wander) {
			StartCoroutine(UpdateState());
		}
	}

	void OnTriggerStay(Collider collider) {
		if (collider.gameObject.tag == "Water" && state == animalState.FindWater) {
			state = animalState.Drink;
			targetPos = transform.position;
			StartCoroutine(UpdateState());
		}

		if (collider.gameObject.tag == "Food" && state == animalState.FindFood) {
			state = animalState.Eat;
			targetPos = transform.position;
			StartCoroutine(UpdateState());
		}
	}

	public IEnumerator UpdateState() {
		//Wander
		if (state == animalState.Wander) {
			hunger += Random.Range(1,10);
			thirst += Random.Range(1, 10);
			tiredness += Random.Range(1, 10);

			if (Random.value*100 < thirst/2) {
				state = animalState.FindWater;
				StartCoroutine(UpdateState());
			}
			if (Random.value * 100 < hunger / 2) {
				state = animalState.FindFood;
				StartCoroutine(UpdateState());
			} else {
				targetPos = new Vector3(Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange), 
										Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange), 0);
			}
		}

		//FindWater
		if (state == animalState.FindWater) {
			GameObject closest = GameObject.FindGameObjectsWithTag("Water")[0];
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("Water")) {
				float dist = Vector3.Distance(go.transform.position, transform.position);
				if (dist <= Vector3.Distance(closest.transform.position, transform.position)) {
					closest = go;
				}
			}

			targetPos = closest.transform.position;
		}

		//Drink
		if (state == animalState.Drink) {
			yield return new WaitForSeconds(3);
			thirst = 0;
			state = animalState.Wander;
			StartCoroutine(UpdateState());
		}

		//FindFood
		if (state == animalState.FindFood) {
			GameObject closest = GameObject.FindGameObjectsWithTag("Food")[0];
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("Food")) {
				float dist = Vector3.Distance(go.transform.position, transform.position);
				if (dist <= Vector3.Distance(closest.transform.position, transform.position)) {
					closest = go;
				}
			}

			targetPos = closest.transform.position;
		}

		//Eat
		if (state == animalState.Eat) {
			yield return new WaitForSeconds(3);
			hunger = 0;
			state = animalState.Wander;
			StartCoroutine(UpdateState());
		}
	}
}
