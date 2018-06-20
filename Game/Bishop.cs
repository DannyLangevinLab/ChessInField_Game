using UnityEngine;
using System.Collections;

public class Bishop : Chessman 
{
	public override bool [,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];
		Chessman c;
		int i, j;

		//EN HAUT GAUCHE
		i=CurrentX;
		j = CurrentY;
		while(true)
		{
			i--;
			j++;
			if (i < 0 || j >= 8)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null)
				r [i, j] = true;
			else
			{
				if (isWhite != c.isWhite)
					r [i, j] = true;

				break;
					
			}
		}


		//EN HAUT DROITE
		i=CurrentX;
		j = CurrentY;
		while(true)
		{
			i++;
			j++;
			if (i >=8 || j >= 8)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null)
				r [i, j] = true;
			else
			{
				if (isWhite != c.isWhite)
					r [i, j] = true;

				break;

			}
		}

		//EN BAS GAUCHE
		i=CurrentX;
		j = CurrentY;
		while(true)
		{
			i--;
			j--;
			if (i <0 || j < 0)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null)
				r [i, j] = true;
			else
			{
				if (isWhite != c.isWhite)
					r [i, j] = true;

				break;

			}
		}

		//EN BAS DROITE
		i=CurrentX;
		j = CurrentY;
		while(true)
		{
			i++;
			j--;
			if (i >8 || j <0)
				break;
			c = BoardManager.Instance.Chessmans [i, j];
			if (c == null)
				r [i, j] = true;
			else
			{
				if (isWhite != c.isWhite)
					r [i, j] = true;

				break;

			}
		}


		return r;


}
}
