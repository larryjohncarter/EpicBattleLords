using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SingletonBehaviour : MonoBehaviour
{
    protected static List<SingletonBehaviour> singletons = new List<SingletonBehaviour>( 8 );
}
public abstract class SingletonBehaviour<T> : SingletonBehaviour where T : SingletonBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if( Instance == null )
        {
            Instance = (T) this;
            singletons.Add( this );
        }
        else if( this != Instance )
            Destroy( this );
    }

    protected virtual void OnDestroy()
    {
        singletons.Remove( this );
    }
}
