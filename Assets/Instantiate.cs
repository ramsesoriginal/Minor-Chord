using UnityEngine;
using System.Collections;

[ExecuteInEditMode]  
public class Instantiate : MonoBehaviour {

	public GameObject[] children;
	public GameObject[] endPieces;
	public int levelSize;
	public bool generateInEditor = false;
	private Instantiate parent;
	public GameObject light;

	private GameObject[] recursiveChildren {
		get  {
			if (children!=null && children.Length>0)
				return children;
			return parent.recursiveChildren;
		}
	}

	private GameObject[] recursiveEndPieces {
		get  {
			if (endPieces!=null && endPieces.Length>0)
				return endPieces;
			return parent.recursiveEndPieces;
		}
	}
	
	private int recursiveLevelSize {
		get  {
			if (parent==null)
				return levelSize;
			return parent.recursiveLevelSize;
		}
	}

	private bool recursiveGenerateInEditor {
		get  {
			if (parent==null)
				return generateInEditor;
			return parent.recursiveGenerateInEditor;
		}
	}
	
	private int TreeSize {
		get  {
			if (parent!=null)
				return parent.TreeSize;
			return ((Instantiate[]) transform.GetComponentsInChildren<Instantiate>()).Length;
		}
	}

	// Update is called once per frame
	void Update () {
		if (recursiveGenerateInEditor) {
			Generate ();
		}
	}

	void OnTriggerEnter(Collider other) {
		Generate ();
	}


	void Generate() {
		Debug.Log(recursiveChildren);
		Debug.Log(recursiveChildren.Length);
		if (transform.childCount==0 && recursiveChildren!= null && recursiveChildren.Length>0) {
			Random.seed = (int)System.DateTime.Now.Ticks;
			var randumber = Random.Range(0,recursiveChildren.Length);
			var child = recursiveChildren[randumber];
			if (child != null) {
				var existingChild = (GameObject)Instantiate (child, new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), transform.rotation);
				existingChild.transform.parent=transform;
				gameObject.renderer.enabled = false;
				Destroy(light);
				var childSpawners=existingChild.GetComponentsInChildren<Instantiate>();
				foreach (var Instantiated in childSpawners) {
					Debug.Log(TreeSize.ToString() + " Size, should do at most" + recursiveLevelSize.ToString());
					if (TreeSize>=recursiveLevelSize)
					{
						Instantiated.children = recursiveEndPieces;
					}
					Instantiated.parent = this;
				}
				foreach (var Instantiated in childSpawners) {
					if (recursiveGenerateInEditor) {
						Debug.Log("Child, do something!");
						Instantiated.Generate();
					}
				}
			}
		}
	}
}
