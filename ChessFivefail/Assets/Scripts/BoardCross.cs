using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardCross : MonoBehaviour {
	
	//创建程序用的棋子
	public GameObject CrossPrefb;
	//棋子大小
	const float CrossSize = 40;
	//棋盘规格 15*15 15个交叉点
	public const int CrossBorad = 15;
	//棋盘大小
	const int Size =560;
	//像素
	public const int HaldSize = Size / 2;

	//存储棋子
	Dictionary<int,CrossSript> _crossMap = new Dictionary<int,CrossSript>();
	//制作key
	static int MakeKey(int x,int y){
		//x和y上的值不会互相冲突
		return x * 10000 + y;

	}
	// Use this for initialization
	//遍历15*15位置
	public	void Reset(){
		//把棋子上的字对象全部删除
		//父类transfrom删除子类transfrom

		foreach(Transform child in gameObject.transform){

			GameObject.Destroy (child.gameObject);

		}
		var mains = GetComponent<MainLoop> ();
		_crossMap.Clear ();



		//x轴 *y轴
		for(int x=0;x<BoardCross.CrossBorad;x++){

			for(int y=0;y<BoardCross.CrossBorad;y++){
				//生成棋子
				var corssObject = GameObject.Instantiate <GameObject>(CrossPrefb);
				//获取父级对象
				corssObject.transform.SetParent (gameObject.transform);
				//设置缩放大小在 1,1,1
				corssObject.transform.localScale = Vector3.one;
				//定义位置xyz,把棋子平均分布在棋盘上
				var pos =corssObject.transform.localPosition;
				pos.x = -BoardCross.HaldSize + x * CrossSize;
				pos.y = -BoardCross.HaldSize + y * CrossSize;
				pos.z = 1;
				//赋予坐标位置
				corssObject.transform.localPosition = pos;
				//获取棋子的脚本
				var crose =corssObject.GetComponent<CrossSript> ();
				crose.GridX = x;
				crose.GridY = y;
				crose.mainLoop = mains;

				//把棋子的x,y,棋子对象添加到字典上成为key
				_crossMap.Add (MakeKey (x, y), crose);

			}
		}

	}

	void Start () {


		Reset ();
	}
	
	public CrossSript GetCross(int gridX, int gridY){

		CrossSript cross;
		if(_crossMap.TryGetValue(MakeKey(gridX,gridX),out cross)){

			return cross;
		}

		return null;
	}

}
