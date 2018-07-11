using System;
using System.Collections;

[System.Serializable]
public class SolutionObject
{
	public String pos;
	public String name;
	public String acceptedElements;
	public String targetedSkill;
	public Element[] elements;
	public SolutionArea[] solutionAreas;
	public int nbElem2Find;

	public int getElem2Find() {
		return nbElem2Find;
	}

	public Boolean requiredImage() 
	{
		return targetedSkill.Equals ("B4") || targetedSkill.Equals ("B8");
	}

	public Boolean requiredObject() 
	{
		return targetedSkill.Equals ("B3");
	}
}

[System.Serializable]
public class SolutionArea
{
	public String acceptedElement;
	public String pos;

}

