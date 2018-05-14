using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class PatternPack : ScriptableObject {
	[SerializeField]
	public Sprite[] patterns;
}
