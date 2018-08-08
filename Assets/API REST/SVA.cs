using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SVA : MonoBehaviour {

	public Texture2D[] availableIcons;
	public RectTransform prefab;
	public Text countText;
	public ScrollRect scrollView;
	public RectTransform content;

	List<ExampleItemView> views = new List<ExampleItemView>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void UpdateItems () {
		int newCount = 0;
		int.TryParse(countText.text, out newCount);
		StartCoroutine(FetchItemModelsFromServer(newCount,results=> OnReceivedNewModels(results)));
	}
	void OnReceivedNewModels(ExampleItemModel[] models){
		foreach (Transform child in content)
			Destroy(child.gameObject);
		views.Clear();

		int i = 0;
		foreach(var model in models){
			var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
			instance.transform.SetParent(content, false);
			var view = InitializeItemView(instance,model);
			views.Add(view);
			++i;
		}
	}

	ExampleItemView InitializeItemView(GameObject viewGameObject, ExampleItemModel model){
		 ExampleItemView view = new ExampleItemView(viewGameObject.transform);
		 view.titleText.text = model.title;
		 view.icon1Image.texture = availableIcons[model.icon1Index];
		 return view;
	}

	IEnumerator FetchItemModelsFromServer(int count, Action<ExampleItemModel[]> onDone){
		yield return new WaitForSeconds(2f);
		var results = new ExampleItemModel[count];
		for (int i = 0; i < count; i++)
		{
			results[i] = new ExampleItemModel();
			results[i].title = "Item" + 1;
			results[i].icon1Index = UnityEngine.Random.Range(0,availableIcons.Length);
		}
		onDone(results);
	}

	public class ExampleItemView{ 
		public Text titleText;
		public RawImage icon1Image,icon2Image,icon3Image;

		public ExampleItemModel Model{
			set{
				if(value != null){
					titleText.text = value.title;
				}
			}
		}
		public ExampleItemView(Transform rootView){
			titleText = rootView.Find("TitlePanel/TitleText").GetComponent<Text>();
			icon1Image = rootView.Find("Icon1Image").GetComponent<RawImage>();
		}
	}
	public class ExampleItemModel{
		public string title;
		public int icon1Index, icon2Index, icon3Index;
	}
}
