
using UnityEngine;

public   enum ChessType{
	None =0,
	Black =1,
	White =2,


}
public class BoardModel {
	//棋子颜色白棋和黑棋，没有棋子
	//枚举类型用逗号连接

	//五子棋连接五个子可以赢
	public const int WinChessCount =5;
	//下棋的位置 15*15
	ChessType[,] _data = new ChessType[BoardCross.CrossBorad,BoardCross.CrossBorad];
	//获取棋盘数据,访问棋盘坐标
	public ChessType Get(int x,int y){

		//如果超出x,y边界 棋子颜色无
		if(x<0||x>=BoardCross.CrossBorad){

			return ChessType.None;

		}
		if(y<0||y>=BoardCross.CrossBorad){

			return ChessType.None;

		}

		//没有超出边界就获取棋子位置
		return _data[x,y];


	}
	//设置棋盘数据
	public bool Set(int x,int y,ChessType type){

		if(x<0||x>=BoardCross.CrossBorad){

			return false;
		}
		if(y<0||y>=BoardCross.CrossBorad){

			return false;
		}
		//把获取到的xy赋给颜色枚举
		_data [x, y] = type;
		return true;

	}
	#region 检查棋子周边连接的八个方向的情况，并且遍历它们
	//检查垂直方向连接情况
	int CheckVerticalINK(int px,int py,ChessType type){

		//算上自己
		int linkCount =1;
		//朝上

		for(int y= py+1;y<BoardCross.CrossBorad;y++){
			//检查到我下了五子连接，返回五子
			if (Get (px, y) == type) {

				linkCount++;
				if (linkCount >= WinChessCount) {

					return linkCount;

				}
			} else {
			
			//当它和周围没有连接时跳出此循环
				break;
			}

		}

		//朝下检查
		for(int y=py-1;y>=0;y--){

			if (Get (px, y) == type) {

				linkCount++;
				if (linkCount >= WinChessCount) {

					return linkCount;
				}


			} else {
				 break;
			
			}

		}
		return linkCount;

	}
	//检查水平位置连接
	int CheckHorizentalLink(int px,int py,ChessType type){


		int LinkCount = 1;
		//朝右边检查+
		for(int x=px+1;x<BoardCross.CrossBorad;x++){
			//检查到我下了五子连接，返回五子
			if (Get (x, py) == type) {

				LinkCount++;
				if (LinkCount >= WinChessCount) {
					return LinkCount;
				}
			} else {
			
				break;
			}


		}
		//朝左
		for(int x=px-1;x>=0;x--){

			if (Get (x, py) == type) {

				LinkCount++;
				if (LinkCount >= WinChessCount) {

					return LinkCount;

				}

			} else {
			
				break;
			}

		}
		return LinkCount;
		
	}
	//检查斜边情况
	int CheckBiasLink(int px,int py,ChessType type){

		int ret = 0;
		int linkCount = 1;
		//左下
		for(int x=px-1,y=py-1;x>=0&&y>=0;x--,y--){

			if (Get (x, y) == type) {

				linkCount++;
				if (linkCount >= WinChessCount) {

					return linkCount;
				}
			} else {
			
				break;
			}
		}
		//右上
		for(int x=px+1,y=py+1;x<BoardCross.CrossBorad&&y<BoardCross.CrossBorad;x++,y++){

			if (Get (x, y) == type) {
				linkCount++;
				if (linkCount >= WinChessCount) {

					return linkCount;
				}

			} else {
				break;
			}

		}
		//合并左上和右下
		ret = linkCount;
		linkCount = 1;

		for (int x=px-1,y=py+1;x>=0&&y<BoardCross.CrossBorad;x--,y++){

			if (Get (x, y) == type) {

				linkCount++;
				if (linkCount >= WinChessCount) {

					return linkCount;
				}
			} else {
			
				break;
			}
		}

		//右下
		for (int x=px+1,y=py-1;x<BoardCross.CrossBorad&&y>=0;x++,y--){

			if (Get (x, y) == type) {

				linkCount++;
				if (linkCount >= WinChessCount) {
					return linkCount;

				}
			} else {
			
				break;
			}


		}
		//取它们的最大值返回
		return Mathf.Max (ret,linkCount);

	}

	//检查给定周边的最大连接情况，取三种情况的最大值
	public int CheckLink(int px,int py,ChessType type){

		int linkCount = 0;
		linkCount = Mathf.Max (CheckHorizentalLink(px,py,type),linkCount);
		linkCount = Mathf.Max (CheckVerticalINK(px,py,type),linkCount);
		linkCount = Mathf.Max (CheckBiasLink(px,py,type),linkCount);
		return linkCount;

	}

	#endregion
}

