using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionPool
{
	public static ProjectionPool defaultPool;

	private PoolInstance instance;

	private Transform parent;

	private List<PoolItem> activePool;

	private List<PoolItem> inactivePool;

	public static ProjectionPool Default
	{
		get
		{
			if (ProjectionPool.defaultPool == null)
			{
				ProjectionPool.defaultPool = DynamicDecals.PoolFromInstance(DynamicDecals.Settings.pools[0]);
			}
			return ProjectionPool.defaultPool;
		}
	}

	public string Title
	{
		get
		{
			return this.instance.title;
		}
	}

	public int ID
	{
		get
		{
			return this.instance.id;
		}
	}

	private int Limit
	{
		get
		{
			return this.instance.limits[QualitySettings.GetQualityLevel()] * (int)(0.05 + (double)(UserSettings.data.mod_cumLife * UserSettings.data.mod_cumLife) / 100.0);
		}
	}

	internal Transform Parent
	{
		get
		{
			if ((UnityEngine.Object)this.parent == (UnityEngine.Object)null)
			{
				this.parent = new GameObject(this.instance.title + " Pool").transform;
			}
			return this.parent;
		}
	}

	public ProjectionPool(PoolInstance Instance)
	{
		this.instance = Instance;
	}

	public static ProjectionPool GetPool(string Title)
	{
		return DynamicDecals.GetPool(Title);
	}

	public static ProjectionPool GetPool(int ID)
	{
		return DynamicDecals.GetPool(ID);
	}

	internal void Update(float DeltaTime)
	{
		if (this.activePool != null && this.activePool.Count > 0)
		{
			for (int num = this.activePool.Count - 1; num >= 0; num--)
			{
				if ((UnityEngine.Object)this.activePool[num].GameObject == (UnityEngine.Object)null)
				{
					this.activePool.RemoveAt(num);
				}
				else
				{
					this.activePool[num].Update(DeltaTime);
				}
			}
		}
	}

	public bool CheckIntersecting(Vector3 Point, float intersectionStrength)
	{
		if (this.activePool != null && this.activePool.Count > 0)
		{
			for (int i = 0; i < this.activePool.Count; i++)
			{
				if (this.activePool[i].Projection.CheckIntersecting(Point) > intersectionStrength)
				{
					return true;
				}
			}
		}
		return false;
	}

	public Projection Request(ProjectionType Type)
	{
		if (this.activePool == null)
		{
			this.activePool = new List<PoolItem>();
		}
		this.RemoveOverflow();
		if (this.activePool.Count >= this.Limit)
		{
			PoolItem poolItem = this.activePool[0];
			this.activePool.RemoveAt(0);
			this.activePool.Add(poolItem);
			poolItem.Reset(Type);
			return poolItem.Projection;
		}
		if (this.inactivePool != null && this.inactivePool.Count > 0)
		{
			PoolItem poolItem2 = this.inactivePool[0];
			this.inactivePool.RemoveAt(0);
			this.activePool.Add(poolItem2);
			poolItem2.Reset(Type);
			return poolItem2.Projection;
		}
		PoolItem poolItem3 = this.NewPoolItem();
		poolItem3.Reset(Type);
		this.activePool.Add(poolItem3);
		return poolItem3.Projection;
	}

	public Projection RequestCopy(Projection Projection)
	{
		if ((UnityEngine.Object)Projection == (UnityEngine.Object)null)
		{
			return null;
		}
		if (Projection.GetType() == typeof(Decal))
		{
			Decal decal = (Decal)this.Request(ProjectionType.Decal);
			decal.CopyAllProperties((Decal)Projection, true);
			return decal;
		}
		if (Projection.GetType() == typeof(Eraser))
		{
			Eraser eraser = (Eraser)this.Request(ProjectionType.Eraser);
			eraser.CopyAllProperties((Eraser)Projection);
			return eraser;
		}
		if (Projection.GetType() == typeof(OmniDecal))
		{
			OmniDecal omniDecal = (OmniDecal)this.Request(ProjectionType.OmniDecal);
			omniDecal.CopyAllProperties((OmniDecal)Projection, true);
			return omniDecal;
		}
		throw new NotImplementedException("Projection Type not recognized, If your implementing your own projection types, you need to implement a copy method like the projection types above");
	}

	public void Return(Projection Projection)
	{
		if (Projection.PoolItem != null)
		{
			this.Return(Projection.PoolItem);
		}
	}

	internal void Return(PoolItem Item)
	{
		if (this.inactivePool == null)
		{
			this.inactivePool = new List<PoolItem>();
		}
		this.activePool.Remove(Item);
		if ((UnityEngine.Object)Item.GameObject != (UnityEngine.Object)null)
		{
			Item.GameObject.SetActive(false);
		}
		this.inactivePool.Add(Item);
	}

	public void RemoveOverflow()
	{
		if (ProjectionPool.defaultPool == null)
		{
			ProjectionPool.defaultPool = this;
		}
		for (int num = this.activePool.Count - 1; num >= this.Limit; num--)
		{
			this.Return(this.activePool[num]);
		}
	}

	public void RemoveAll()
	{
		for (int num = this.activePool.Count - 1; num >= 0; num--)
		{
			this.Return(this.activePool[num]);
		}
	}

	public PoolItem NewPoolItem()
	{
		return new PoolItem(this);
	}
}
