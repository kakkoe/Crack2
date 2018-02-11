using System;
using System.Collections;
using UnityEngine;

public class Printer : MonoBehaviour
{
	public Projection[] prints;

	public LayerMask[] printLayers;

	public PrintSelection printMethod;

	private ProjectionPool pool;

	[SerializeField]
	private int poolID;

	public PrintParent parent;

	[SerializeField]
	protected PrinterOverlap[] overlaps = new PrinterOverlap[0];

	public FadeMethod fadeMethod;

	public float inDuration;

	public float fadeDelay;

	public float outDuration;

	public CullMethod cullMethod;

	public float cullDuration;

	public bool destroyOnPrint;

	public float frequencyTime;

	public float frequencyDistance;

	private float timeSincePrint = float.PositiveInfinity;

	private Vector3 lastPrintPos = Vector3.zero;

	private bool justSplatted;

	private float splatMod = 1f;

	public ProjectionPool Pool
	{
		get
		{
			if (this.pool == null)
			{
				this.pool = DynamicDecals.GetPool(this.poolID);
			}
			return this.pool;
		}
		set
		{
			this.poolID = value.ID;
		}
	}

	private void Update()
	{
		this.timeSincePrint += Time.deltaTime;
	}

	public bool Print(Vector3 Position, Quaternion Rotation, Transform Surface, int Layer = 0, float scale = 1f, bool splat = false)
	{
		if (this.prints != null && this.prints.Length >= 1)
		{
			if (this.justSplatted)
			{
				this.splatMod = 2.5f;
			}
			else
			{
				this.splatMod = 1f;
			}
			if (this.timeSincePrint >= this.frequencyTime && Vector3.Distance(Position, this.lastPrintPos) >= this.frequencyDistance * this.splatMod)
			{
				if (this.overlaps.Length > 0)
				{
					for (int i = 0; i < this.overlaps.Length; i++)
					{
						ProjectionPool projectionPool = DynamicDecals.GetPool(this.overlaps[i].poolId);
						if (projectionPool.ID == this.overlaps[i].poolId && projectionPool.CheckIntersecting(Position, this.overlaps[i].intersectionStrength))
						{
							if (this.destroyOnPrint)
							{
								UnityEngine.Object.Destroy(base.gameObject);
							}
							return true;
						}
					}
				}
				switch (this.printMethod)
				{
				case PrintSelection.LinkedSplats:
				{
					int num = UnityEngine.Random.Range(0, this.prints.Length / 2);
					if (splat)
					{
						num += this.prints.Length / 2;
					}
					this.PrintProjection(this.prints[num], Position, Rotation, Surface, scale);
					break;
				}
				case PrintSelection.Layer:
					if (this.printLayers == null || this.printLayers.Length == 0)
					{
						this.PrintProjection(this.prints[0], Position, Rotation, Surface, scale);
					}
					else
					{
						for (int k = 0; k < this.printLayers.Length; k++)
						{
							if ((int)this.printLayers[k] == ((int)this.printLayers[k] | 1 << Layer))
							{
								this.PrintProjection(this.prints[k], Position, Rotation, Surface, scale);
							}
						}
					}
					break;
				case PrintSelection.Random:
				{
					int num = UnityEngine.Random.Range(0, this.prints.Length);
					this.PrintProjection(this.prints[num], Position, Rotation, Surface, scale);
					break;
				}
				case PrintSelection.All:
				{
					Projection[] array = this.prints;
					foreach (Projection projection in array)
					{
						this.PrintProjection(projection, Position, Rotation, Surface, scale);
					}
					break;
				}
				}
				if (this.destroyOnPrint)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				this.timeSincePrint = 0f;
				this.lastPrintPos = Position;
				this.justSplatted = splat;
				return true;
			}
			return false;
		}
		Debug.LogError("No Projections to print. Please set at least one projection to print.");
		return false;
	}

	private void PrintProjection(Projection Projection, Vector3 Position, Quaternion Rotation, Transform Surface, float scale = 1f)
	{
		Projection projection = this.Pool.RequestCopy(Projection);
		projection.Fade(this.fadeMethod, this.inDuration, this.fadeDelay, this.outDuration);
		projection.Culled(this.cullMethod, this.cullDuration);
		projection.transform.position = Position;
		projection.transform.rotation = Rotation;
		if (this.parent == PrintParent.Surface)
		{
			Transform transform = null;
			IEnumerator enumerator = Surface.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform2 = (Transform)enumerator.Current;
					if (transform2.name == "Projections")
					{
						transform = transform2;
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			if ((UnityEngine.Object)transform == (UnityEngine.Object)null)
			{
				transform = new GameObject("Projections").transform;
				transform.SetParent(Surface);
			}
			projection.transform.SetParent(transform);
		}
	}
}
