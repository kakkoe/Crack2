using UnityEngine;

public class PoolItem
{
	private ProjectionPool pool;

	private GameObject gameObject;

	private Projection projection;

	private FadeMethod fadeMethod;

	private float delay;

	private float inDuration;

	private float outDuration;

	private CullMethod cullMethod;

	private float cullDuration;

	private float timeElapsed;

	private float timeSinceSeen;

	public ProjectionPool Pool
	{
		get
		{
			return this.pool;
		}
	}

	public GameObject GameObject
	{
		get
		{
			return this.gameObject;
		}
	}

	public Projection Projection
	{
		get
		{
			return this.projection;
		}
	}

	public PoolItem(ProjectionPool Pool)
	{
		this.pool = Pool;
	}

	public void Reset(ProjectionType Type)
	{
		if ((Object)this.gameObject == (Object)null)
		{
			this.gameObject = new GameObject("Projection");
		}
		this.gameObject.transform.SetParent(this.pool.Parent);
		this.gameObject.SetActive(true);
		this.timeElapsed = 0f;
		this.fadeMethod = FadeMethod.None;
		this.inDuration = 0f;
		this.delay = 0f;
		this.outDuration = 0f;
		this.cullMethod = CullMethod.None;
		this.timeSinceSeen = 0f;
		this.cullDuration = 0f;
		Eraser eraser = this.gameObject.GetComponent<Eraser>();
		OmniDecal omniDecal = this.gameObject.GetComponent<OmniDecal>();
		Decal decal = this.gameObject.GetComponent<Decal>();
		switch (Type)
		{
		case ProjectionType.Decal:
			if ((Object)eraser != (Object)null)
			{
				eraser.enabled = false;
			}
			if ((Object)omniDecal != (Object)null)
			{
				omniDecal.enabled = false;
			}
			if ((Object)decal == (Object)null)
			{
				decal = this.gameObject.AddComponent<Decal>();
			}
			this.projection = decal;
			break;
		case ProjectionType.Eraser:
			if ((Object)decal != (Object)null)
			{
				decal.enabled = false;
			}
			if ((Object)omniDecal != (Object)null)
			{
				omniDecal.enabled = false;
			}
			if ((Object)eraser == (Object)null)
			{
				eraser = this.gameObject.AddComponent<Eraser>();
			}
			this.projection = eraser;
			break;
		case ProjectionType.OmniDecal:
			if ((Object)decal != (Object)null)
			{
				decal.enabled = false;
			}
			if ((Object)eraser != (Object)null)
			{
				eraser.enabled = false;
			}
			if ((Object)omniDecal == (Object)null)
			{
				omniDecal = this.gameObject.AddComponent<OmniDecal>();
			}
			this.projection = omniDecal;
			break;
		}
		this.projection.PoolItem = this;
		this.projection.enabled = true;
		this.projection.AlphaModifier = 1f;
		this.projection.ScaleModifier = 1f;
	}

	public void Update(float deltaTime)
	{
		if (this.fadeMethod != 0)
		{
			float num = 1f;
			this.timeElapsed += deltaTime;
			if (this.timeElapsed < this.inDuration)
			{
				num = 1f - (this.inDuration - this.timeElapsed) / this.inDuration;
			}
			if (this.timeElapsed > this.inDuration + this.delay)
			{
				num = (this.inDuration + this.delay + this.outDuration - this.timeElapsed) / this.outDuration;
			}
			if (this.fadeMethod == FadeMethod.Alpha || this.fadeMethod == FadeMethod.Both)
			{
				this.projection.AlphaModifier = num;
			}
			if (this.fadeMethod == FadeMethod.Scale || this.fadeMethod == FadeMethod.Both)
			{
				this.projection.ScaleModifier = num;
			}
			if (this.timeElapsed >= this.inDuration + this.delay + this.outDuration)
			{
				this.pool.Return(this);
			}
		}
		else
		{
			this.projection.AlphaModifier = 1f;
			this.projection.ScaleModifier = 1f;
		}
		if (this.cullMethod != 0)
		{
			if (this.projection.Visible)
			{
				this.timeSinceSeen = 0f;
			}
			else
			{
				this.timeSinceSeen += deltaTime;
			}
			if (this.timeSinceSeen > this.cullDuration)
			{
				this.pool.Return(this);
			}
		}
	}

	public void Fade(FadeMethod Method, float InDuration, float Delay, float OutDuration)
	{
		this.fadeMethod = Method;
		this.delay = Delay;
		this.inDuration = InDuration;
		this.outDuration = OutDuration;
	}

	public void Culled(CullMethod Method, float Duration)
	{
		this.cullMethod = Method;
		this.cullDuration = Duration;
	}

	public void Return()
	{
		this.pool.Return(this);
	}
}
