using UnityEngine;
using System.Collections;

public class FireCollision : MonoBehaviour {

	public ParticleSystem part;
	public ParticleCollisionEvent[] collisionEvents;
	void Start() {
		part = GetComponent<ParticleSystem>();
		collisionEvents = new ParticleCollisionEvent[16];
	}
	void OnParticleCollision(GameObject other) {
		int safeLength = part.GetSafeCollisionEventSize();
		if (collisionEvents.Length < safeLength)
			collisionEvents = new ParticleCollisionEvent[safeLength];
		
		int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
		Rigidbody rb = other.GetComponent<Rigidbody>();
		int i = 0;
		while (i < numCollisionEvents) {
			if (rb) {
				Vector3 pos = collisionEvents[i].intersection;
				Vector3 force = collisionEvents[i].velocity * 10;
				rb.AddForce(force);
			}
			i++;
		}
	}
}
