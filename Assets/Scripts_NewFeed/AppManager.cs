using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class AppManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		feedURl = "http://ryotar.wpengine.com/wp-json/wp/v2/ar_experience?page=1";

		StartCoroutine (LoadData());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string feedURl;


	public GameObject imageObject;
	public GameObject imageObjectPrefab;
	public GameObject grid_List;
	public GameObject LoadingIndicator;

	public IEnumerator LoadData()
	{
		LoadingIndicator.SetActive (true);
		WWW response = new WWW (feedURl);
		yield return response;

		Debug.Log ("Response is "+response.text);

		if (response.error == null) {
			Debug.Log ("Hello data is ");

			var jsonResponse = JSONNode.Parse (response.text);
			for(int i =0; i< jsonResponse.Count ; i++)
			{

				Debug.Log ("Hello data is " +jsonResponse[i]["id"] );

				Debug.Log ("type "+jsonResponse[i]["type"]);

				Debug.Log ("title "+jsonResponse[i]["title"]["rendered"]);

				Debug.Log ("link "+jsonResponse[i]["link"]);

				Debug.Log ("post_title "+jsonResponse[i]["cover_image_url"]["post_title"]);

				Debug.Log ("post_name "+jsonResponse[i]["cover_image_url"]["post_name"]);

				Debug.Log ("image url guid "+jsonResponse[i]["cover_image_url"]["guid"]);

				Debug.Log ("image url guid "+jsonResponse[i]["description"]);

				Debug.Log ("image url guid "+jsonResponse[i]["ar_experience_type"]);

				Debug.Log ("image url guid "+jsonResponse[i]["ar_video_source"]);

				Debug.Log ("image url guid "+jsonResponse[i]["ar_experience_type"]["post_title"]);

				Debug.Log ("image url guid "+jsonResponse[i]["ar_experience_type"]["guid"]);

				Debug.Log ("image url guid "+jsonResponse[i]["ar_vuforia_database_native_dat_file"]["guid"]);


				Debug.Log ("image url guid "+jsonResponse[i]["ar_vuforia_database_native_xml_file"]["guid"]);


				WWW downloadFile = new WWW (""+jsonResponse[i]["cover_image_url"]["guid"]);

				yield return downloadFile;

				if (downloadFile.error == null) {

					var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
					texture.LoadImage (downloadFile.bytes);
					Sprite imageSprite = Sprite.Create (texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f));
					imageObjectPrefab = Instantiate (imageObject)as GameObject;
					imageObjectPrefab.GetComponent<Image> ().overrideSprite = imageSprite;
					imageObjectPrefab.transform.FindChild ("Text_title").GetComponent<Text> ().text = ""+jsonResponse[i]["cover_image_url"]["post_title"];
					imageObjectPrefab.transform.SetParent (grid_List.transform,false);
				
				} else {

					Debug.Log ("Error is "+ downloadFile.error);
				
				}




			}


			LoadingIndicator.SetActive (false);
		
		} else {

			LoadingIndicator.SetActive (false);
			Debug.Log ("Error is "+response.error );
		}
	}
}
