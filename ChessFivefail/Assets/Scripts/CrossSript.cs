using UnityEngine;
using UnityEngine.UI;
//此脚本是个字典的value值
public class CrossSript : MonoBehaviour {
	public int GridX;
	public int GridY;
	public  MainLoop mainLoop;
	// Use this for initialization
	void Start () {
		//棋子位置
		GetComponent<Button> ().onClick.AddListener (() => {
			//点击交叉点把数据传入到mainloop类里
			mainLoop.OnClick(this);


		});
	}
	

}
