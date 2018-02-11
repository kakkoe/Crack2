using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectableMenuManager
{
	private static Vector3 v3;

	private static float smx;

	private static float smy;

	public static void animate(List<GameObject> objects, GameObject container, float objectSize, float containerY = -25f, float extraPadding = 0f)
	{
		SelectableMenuManager.v3 = Vector3.zero;
		Vector3 vector = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(Game.gameInstance.UI.transform.Find("bottomright").position);
		float x = vector.x;
		Vector3 vector2 = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(Game.gameInstance.UI.transform.Find("topleft").position);
		float num = Math.Abs(x - vector2.x);
		Vector3 vector3 = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(Game.gameInstance.UI.transform.Find("bottomright").position);
		float y = vector3.y;
		Vector3 vector4 = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(Game.gameInstance.UI.transform.Find("topleft").position);
		float num2 = Math.Abs(y - vector4.y);
		num /= Game.gameInstance.UI.GetComponent<Canvas>().scaleFactor;
		num2 /= Game.gameInstance.UI.GetComponent<Canvas>().scaleFactor;
		float num3 = objectSize * (float)(objects.Count - 1);
		float num4 = objectSize + extraPadding;
		float num5 = num3 + num4 * 2f - num;
		if (num5 < 0f)
		{
			num5 = 0f;
		}
		SelectableMenuManager.smx += (Game.gameInstance.mX * num - SelectableMenuManager.smx) * 0.7f;
		SelectableMenuManager.smy += (Game.gameInstance.mY * num2 - SelectableMenuManager.smy) * 0.7f;
		float num6 = SelectableMenuManager.cap((SelectableMenuManager.smx - num / 2f) / (num - num4), -0.5f, 0.5f);
		SelectableMenuManager.v3.x = 0f - num3 / 2f - num6 * num5;
		SelectableMenuManager.v3.y = containerY;
		container.transform.localPosition = SelectableMenuManager.v3;
		for (int i = 0; i < objects.Count; i++)
		{
			SelectableMenuManager.v3 = objects[i].transform.localPosition;
			SelectableMenuManager.v3.z = 0f;
			objects[i].transform.localPosition = SelectableMenuManager.v3;
			SelectableMenuManager.v3 = Vector3.one;
			objects[i].transform.localScale = SelectableMenuManager.v3;
			SelectableMenuManager.v3 = Vector3.zero;
			objects[i].transform.localRotation = Quaternion.Euler(SelectableMenuManager.v3);
			Vector3 vector5 = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(objects[i].transform.position);
			float val = Math.Abs(vector5.x - SelectableMenuManager.smx) / ((float)Screen.width * 0.75f);
			Vector3 vector6 = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(objects[i].transform.position);
			float val2 = (vector6.x - SelectableMenuManager.smx) / ((float)Screen.width * 0.75f);
			Vector3 vector7 = GameObject.Find("UICam").GetComponent<Camera>().WorldToScreenPoint(objects[i].transform.position);
			float val3 = (vector7.y - SelectableMenuManager.smy) / ((float)Screen.height * 0.75f);
			SelectableMenuManager.v3 = objects[i].transform.localPosition;
			SelectableMenuManager.v3.z = Mathf.Pow(SelectableMenuManager.cap(val, 0f, 1f) * 2f, 2f) * 450f;
			SelectableMenuManager.v3.z -= objects[i].GetComponent<SelectableMenuItem>().hoverAmount;
			objects[i].transform.localPosition = SelectableMenuManager.v3;
			SelectableMenuManager.v3 = Vector3.zero;
			SelectableMenuManager.v3.x = SelectableMenuManager.cap(val3, -1f, 1f) * 25f;
			SelectableMenuManager.v3.y = SelectableMenuManager.cap(val2, -1f, 1f) * 65f;
			objects[i].transform.localRotation = Quaternion.Euler(SelectableMenuManager.v3);
			SelectableMenuManager.v3 = Vector3.one;
			SelectableMenuManager.v3 *= 0.9f + objects[i].GetComponent<SelectableMenuItem>().hoverAmount * 0.3f;
			objects[i].transform.localScale = SelectableMenuManager.v3;
		}
		SelectableMenuManager.zSort(objects);
	}

	public static void zSort(List<GameObject> things)
	{
		List<GameObject> list = things.OrderByDescending(delegate(GameObject o)
		{
			Vector3 position = o.transform.position;
			return position.z;
		}).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			list[i].transform.SetSiblingIndex(i);
		}
	}

	public static float cap(float val, float min = 0f, float max = 1f)
	{
		if (val < min)
		{
			val = min;
		}
		if (val > max)
		{
			val = max;
		}
		return val;
	}
}
