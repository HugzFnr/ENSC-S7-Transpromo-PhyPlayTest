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
    /// <summary>
    /// Tableau de frames récupéré du Bitalino.
    /// </summary>
    protected virtual Bitalino.Frame[] framesArray
	{
		get;
		set;
	}

    /// <summary>
    /// Tableau de données de chaque capteur qui sera normalisé (dans le modèle spécifique).
    /// </summary>
	protected virtual short[][] dataArray
	{
		get;
		set;
	}

    /// <summary>
    /// Propre à chaque modèle spécialisé.
    /// </summary>
	public virtual byte idModel
	{
		get;
		set;
	}

	public virtual LiveSequencer liveSequencer
	{
		get;
		set;
	}

    /// <summary>
    /// Tableau contenant le niveau d'activation de chaque état.
    /// </summary>
	public virtual string[] StatesNames()
	{
		throw new System.NotImplementedException();
	}

    /// <summary>
    /// Tableau contenant le nom des différents états du modèle (ex null to veryhigh) pour chaque moment.
    /// L'écart temportel correspondant entre les lignes du tableau est défini par LiveSequencer.timeInterval.  
    /// </summary>
	public virtual byte[][] TimedStates()
	{
		throw new System.NotImplementedException();
	}

    /// <summary>
    /// Transforme les données provenant de Bitalino.Frame en données exploitables.
    /// </summary>
	protected virtual void FramesToData()
	{
		throw new System.NotImplementedException();
	}

}

