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
/// Modèle implémenté de l’article et Atkins et Mandryk en 2006 qui utilise la logique floue en passant par la valence arousal.
/// </summary>
public class AtkinsMandrykModel : Model
{
    /// <summary>
    /// Données des capteurs. Les différentes colonnes sont les différents capteurs, avec dans chaque ligne écarté de timeInterval le niveau d’activation du capteur à ce moment.
    /// </summary>
    private byte[][] sensorsStates
	{
		get;
		set;
	}

    /// <summary>
    /// Niveau de Valence et d'Arousal du joueur tous les timeInterval temps.
    /// </summary>
	private byte[][] valenceArousalStates
	{
		get;
		set;
	}

    /// <summary>
    /// Constructeur qui initialise framesArray selon le tableau de frames donné. L’intervalle représente le temps entre chaque ligne du tableau qui sera donné en sortie.
    /// </summary>
	public AtkinsMandrykModel(Bitalino.Frame[] frames, double interval, LiveSequencer liveSequencer)
	{
	}

    /// <summary>
    /// récupère les données des capteurs et met à jour les niveaux de chaque capteur dans sensorsStates (haut/moyen/faible…)
    /// </summary>
	private void DataToSensors(double[][] normalizedData)
	{
		throw new System.NotImplementedException();
	}

    /// <summary>
    /// Utilise les valeurs des capteurs pour mettre à jour le tableau valenceArousalStates.
    /// </summary>
	private void SensorsToValenceArousal()
	{
		throw new System.NotImplementedException();
	}

    /// <summary>
    /// Prend les shorts de la bitalino en effectuant une normalisation des données selon l'entièreté de l'échantillon.
    /// </summary>
	private double[][] NormalizeData()
	{
		throw new System.NotImplementedException();
	}

}

