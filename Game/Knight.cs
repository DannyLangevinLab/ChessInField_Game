using UnityEngine;
using System.Collections;

public class Knight :Chessman {
	public override bool [,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];

		//EN HAUT A GAUCHE
		KnightMove(CurrentX-1, CurrentY+2, ref r);
		//EN HAUT A DROITE
		KnightMove(CurrentX+1, CurrentY+2, ref r);
		// A GAUCHE PUIS en HAUT
		KnightMove(CurrentX+2, CurrentY+1, ref r);
		// A DROITE puis en HAUT
		KnightMove(CurrentX+2, CurrentY-1, ref r);


		//EN BAS A GAUCHE
		KnightMove(CurrentX-1, CurrentY-2, ref r);
		//EN BAS A DROITE
		KnightMove(CurrentX+1, CurrentY-2, ref r);
		// A GAUCHE PUIS en BAS
		KnightMove(CurrentX-2, CurrentY+1, ref r);
		// A DROITE puis en BAS
		KnightMove(CurrentX-2, CurrentY-1, ref r);

		return r;
	}
	public void KnightMove(int x, int y, ref bool[,] r)
	{
		Chessman c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) 
		{
			c = BoardManager.Instance.Chessmans [x, y];
			if (c == null)
				r [x, y] = true;
			else if (isWhite != c.isWhite)
				r [x, y] = true;
		}
			
	}


}
