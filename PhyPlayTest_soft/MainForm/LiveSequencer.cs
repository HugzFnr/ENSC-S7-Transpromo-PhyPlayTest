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
	public DateTime startTimer
	{
		get;
		set;
	}

	public DateTime endTimer
	{
		get;
		set;
	}

	public string nomenclature
	{
		get;
		set;
	}

	public double timeInterval
	{
		get;
		set;
	}

	public int modelChosen
	{
		get;
		set;
	}

	public Bitalino bitalino
	{
		get;
		set;
	}

	public Model Model
	{
		get;
		set;
	}

	public virtual DataWriter dataWriter
	{
		get;
		set;
	}

	public MainForm.MainForm mainForm
	{
		get;
		set;
	}

	public LiveSequencer(string nomenclature, double interval, int idmodel)
	{
	}

	public void EndSession()
	{
		throw new System.NotImplementedException();
	}

	public void StartSession()
	{
		throw new System.NotImplementedException();
	}

}

