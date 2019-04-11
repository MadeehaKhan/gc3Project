using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DragController))]
public class TangramsSupervisor : MonoBehaviour {

    public List<GameObject> puzzlePrefabs;
    public Fader star;
    DragController dragController;

    static TangramsSupervisor instance;
    int currentPuzzleIndex = 0;
    GameObject currentPuzzle;
    

    void Awake(){
        instance = this;
        PlayerPrefs.DeleteAll();
    }

    public static TangramsSupervisor GetInstance(){
        return instance;
    }

    public DragController DragController {
        get {
            return dragController;
        }
    }

	void Start () {
        dragController = GetComponent<DragController>();
		LoadPuzzle(0);
	}               
		
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.OverlapPoint(worldPoint);
            if (hit != null)
            {                              
                if (hit.name == "next-arrow")
                {
                    if (currentPuzzleIndex + 1 < puzzlePrefabs.Count)
                    {
                        currentPuzzleIndex++;
                        LoadPuzzle(currentPuzzleIndex);

                    }
					else if (currentPuzzleIndex + 1 == puzzlePrefabs.Count)
					{
						SceneManager.LoadScene(0);
					}

					
                } 
                else if (hit.name == "back-arrow")
                {
                    if (currentPuzzleIndex > 0)
                    {
                        currentPuzzleIndex--;
                        LoadPuzzle(currentPuzzleIndex);

                    }
					else if (currentPuzzleIndex == 0)
					{
						SceneManager.LoadScene(0);
					}
                }

            }
        }
	}

    void LoadPuzzle(int index){                
        Destroy(currentPuzzle);
        currentPuzzle = null;
        var prefab = puzzlePrefabs [index];
        currentPuzzle = Instantiate(prefab);
        var puzzle = currentPuzzle.GetComponentInChildren<Puzzle>();
        SetSolved(puzzle);
	
    }       

    public void SetSolved(Puzzle puzzle){
        var data = Data.GetInstance();
        var wasEverSolved = data.GetSolved(puzzle.PuzzleName);
		star.IsVisible = wasEverSolved;
       
    }

}
