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

public class LiveSequencer
{
	public virtual object startTimer : DateTime
	{
		get;
		set;
	}

	public virtual object endTimer : DateTime
	{
		get;
		set;
	}

	public virtual string nomenclature
	{
		get;
		set;
	}

	public virtual double timeInterval
	{
		get;
		set;
	}

	public virtual int modelChosen
	{
		get;
		set;
	}

	public virtual BITalino bitalino
	{
		get;
		set;
	}

	public virtual Model Model
	{
		get;
		set;
	}

	public virtual DataWriter dataWriter
	{
		get;
		set;
	}

	public virtual MainForm mainForm
	{
		get;
		set;
	}

	public LiveSequencer(string nomenclature, double interval, int idmodel)
	{
	}

	public virtual void EndSession()
	{
		throw new System.NotImplementedException();
	}

	public virtual void StartSession()
	{
		throw new System.NotImplementedException();
	}

}

