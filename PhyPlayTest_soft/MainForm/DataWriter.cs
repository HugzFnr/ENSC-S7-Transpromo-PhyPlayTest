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
/// <summary>
/// Crée le fichier de log et écrit dedans. Ses méthodes sont appelées par le LiveSequencer.
/// </summary>
public class DataWriter
{
    /// <summary>
    /// Chemin construit de création du fichier de logs, fini par le nom du fichier.
    /// </summary>
    public string path
    {
        get;
        set;
    }

    /// <summary>
    /// Intervalle de temps issu du LiveSequencer.
    /// </summary>
    public double timeInterval
    {
        get;
        set;
    }

    public LiveSequencer liveSequencer
    {
        get;
        set;
    }

    /// <summary>
    /// Construit le path à partir de la nomenclature donnée.
    /// </summary>
    public DataWriter(string nomenclature, double timeInterval, LiveSequencer liveSequencer)
    {
    }

    /// <summary>
    /// Ouvre le fichier correspondant au path.
    /// </summary>
	public void OpenFile()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Écrit les données ligne par ligne en nommant les colonnes accordément aux paramètres en récréant l'échelle des temps en fonction du timeInterval.
    /// A donc besoin du gros tableau de données timedStates.
    /// </summary>
	public void WriteData(byte[][] timedStates, string[] statesNames)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Ferme le fichier correspondant au path.
    /// </summary>
    public void CloseFile()
    {
        throw new System.NotImplementedException();
    }

}

