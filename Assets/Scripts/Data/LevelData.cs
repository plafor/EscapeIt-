using System;
using System.Collections;

[System.Serializable]
public class LevelData
{
	public String sceneName;
	public int nbObjectsToFind;
	public Element[] elements;
	public SolutionObject[] solutionObjects;
	public PlacedDecor[] placedDecors;
	public Hideout[] hideouts;
	public String difficulty;
}

[System.Serializable]
public class PlacedDecor
{
	public String name;
	public Element[] elements;
	public Hideout[] hideouts;
}

[System.Serializable]
public class Hideout
{
	public String name;
	public String pos;
	public Element[] hiddenElements;
}