using UnityEngine;

public class MainLooptext : MonoBehaviour {
	//白棋和黑棋的模板

	public GameObject WhitePrefab;
	public GameObject BlackPrefb;
	//结果窗口
	//public ResultWindow ResultWindow;

	//描述谁先走的旋转状态
	enum State{
		BlackGo,//黑方(玩家)走
		WhiteGo,//白方（电脑）走
		Over,//结束


	}
	//当前状态
	State _state;
	//棋盘显示
	BoardCross _board;
	//棋盘数据
	BoardModel _model;

	//人工智能
	AI _ai;

	bool CanPlace(int gridX,int gridY){

		//如果这个地方可以下棋子
		return _model.Get(gridX,gridY)==  ChessType.None;



	}
	//放白棋还是黑棋
	bool PlaceChess(CrossSript cross,bool isblack){
		//如果就下棋
		if(cross==null)
		return false;

		//创建棋子 创建白棋还是黑棋
		var newChess = GameObject.Instantiate<GameObject>(isblack ? BlackPrefb : WhitePrefab);


		//找到此对象
		newChess.transform.SetParent (cross.gameObject.transform,false);
		//设置数据
		_model.Set(cross.GridX,cross.GridY,isblack ? ChessType.Black:ChessType.White);
		//设置颜色
		var ctype = isblack ? ChessType.Black:ChessType.White;
		//把八个方向的最大值赋给linkcount 判断是否胜利
		var linkCount = _model.CheckLink (cross.GridX,cross.GridY,ctype);
	    //五子连珠胜利
		return linkCount >= BoardModel.WinChessCount;


	}
	//重新开始 数据重置
	public void Restart(){
		_state = State.BlackGo;
		_model = new BoardModel ();
		_ai = new AI ();
		_board.Reset();

	}
	//记录每次点击，不能让游戏规则错乱
	public void Onclick(CrossSript cross){

		if (_state != State.BlackGo)
			return;
		//不能再已经放置的棋子上放置
		if(CanPlace(cross.GridX,cross.GridY)){

			_lastPlayerX = cross.GridX;
			_lastPlayerY = cross.GridY;


		}
		//放棋子
		if (PlaceChess (cross, true)) {

			//已经胜利
			_state = State.Over;
			//ShowResult (ChessType.Black);

		} else {
		//换电脑走
			_state = State.WhiteGo;
		
		}
	


		

	}

	// Use this for initialization
	void Start () {

		_board = GetComponent<BoardCross> ();
		Restart ();
		
	}
	//记录上次放置的位置
	int _lastPlayerX,_lastPlayerY;
	//返回结果
//	void ShowResult(ChessType winside){
//
//		ResultWindow.gameObjetc.SetActive (true);
//		ResultWindow.Show (winside);
//
//
//	}

	// Update is called once per frame
	void Update () {
		//谁走就执行谁的方法
		switch(_state){
		//白方走（电脑）
		case State.WhiteGo:
			{
				//计算电脑下的位置
				int gridX, gridY;
				_ai.ComputerDo (_lastPlayerX, _lastPlayerY, out gridX, out gridY);
				//如果电脑胜利
				if (PlaceChess (_board.GetCross (gridX, gridY), false)) {

					_state = State.Over;
//					ShowResult (ChessType.White);


				}
				//玩家走
				else {

					_state = State.BlackGo;

				}



			}
			break;



		}


		
	}
}
