package md55e281bc3d2f52d00fa4ae6e6f884d455;


public class GuestInviteAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CountMeIn.Adapters.GuestInviteAdapterViewHolder, CountMeIn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GuestInviteAdapterViewHolder.class, __md_methods);
	}


	public GuestInviteAdapterViewHolder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GuestInviteAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("CountMeIn.Adapters.GuestInviteAdapterViewHolder, CountMeIn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
