﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil
//     Les modifications apportées à ce fichier seront perdues si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Model
{
	private Bitalino.Frame[] framesArray
	{
		get;
		set;
	}

	private double[][] dataArray
	{
		get;
		set;
	}

	public virtual double timeInterval
	{
		get;
		set;
	}

	public virtual LiveSequencer liveSequencer
	{
		get;
		set;
	}

	public virtual string[] StatesNames()
	{
		throw new System.NotImplementedException();
	}

	public virtual byte[][] TimedStates()
	{
		throw new System.NotImplementedException();
	}

	public virtual void FramesToData()
	{
		throw new System.NotImplementedException();
	}

}

