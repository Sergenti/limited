﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{

	private float orthoOrg;
	private float orthoCurr;
	private Vector3 scaleOrg;
	private Vector3 posOrg;

	public static TileSelector instance;
	public Tilemap environmentTilemap;
	private bool locked = false;
	private Vector3 lastPosition;

	public void Enabled(bool value)
	{
		gameObject.SetActive(value);
	}

	public void LockPosition(bool value)
	{
		locked = value;
	}

	void Start()
	{
		lastPosition = transform.position;

		// used to make it so that this object doesn't scale with the zoom
		// stays scaled with tilemap
		orthoOrg = Camera.main.orthographicSize;
		orthoCurr = orthoOrg;
		scaleOrg = transform.localScale;
		posOrg = Camera.main.WorldToViewportPoint(transform.position);
	}

	void Update()
	{
		Vector3 newPos = lastPosition;

		if (!locked)
		{
			Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int tilePos = environmentTilemap.WorldToCell(point);
			newPos = new Vector3(tilePos.x + 0.5f, tilePos.y + 0.5f, tilePos.z);
		}

		if (transform.position != newPos)
		{
			transform.position = newPos;
			lastPosition = newPos;
		}

		// don't scale
		var osize = Camera.main.orthographicSize;
		if (orthoCurr != osize)
		{
			transform.localScale = scaleOrg * orthoOrg / osize;
			orthoCurr = osize;
			transform.position = Camera.main.ViewportToWorldPoint(posOrg);
		}

	}
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
}
