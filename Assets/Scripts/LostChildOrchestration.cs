using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LostChildOrchestration : MonoBehaviour
{
    public List<Material> materials;
    public string[] hats;

    [Range(1, 100)] public int percentageOfUniqueKids = 60;
    
    private Dictionary<Material, List<string>> uniqueChildrenCombinations = new Dictionary<Material, List<string>>();

    private void PopulateUniqueCombinations()
    {
        foreach (var material in materials)
        {
            uniqueChildrenCombinations.Add(material, new List<string>());       

            foreach (var hat in hats)
            {
                if (uniqueChildrenCombinations.ContainsKey(material))
                {
                    uniqueChildrenCombinations[material].Add(hat);
                }
            }
        }
    }

    public List<Child> Orchestrate(List<Child> children)
    {
        PopulateUniqueCombinations();
        
        // Take percentage of unique kids
        int amountOfUniqueChildren = (int)(children.Count * ((float)percentageOfUniqueKids / 100));

        List<Child> uniqueChildren = new List<Child>();
        
        for (int i = 0; i < amountOfUniqueChildren; i++)
        {
            Child child = children[Random.Range(0, children.Count)];
            uniqueChildren.Add(child);
            children.Remove(child);
        }
        
        // Apply unique combinations to them
        foreach (var child in uniqueChildren)
        {
            child.SetVisualCombination(GetRandomCombination());            
        }

        
        // Apply random shit to the others, as long as they are not one of the uniques

        foreach (var child in children)
        {
            child.SetVisualCombination(new KeyValuePair<Material, string>(materials[Random.Range(0, materials.Count)], ""));
        }
        
        return uniqueChildren;
    }

    private KeyValuePair<Material, string> GetRandomCombination()
    {
        Material randomMaterial = uniqueChildrenCombinations.Keys.ToList()[Random.Range(0, materials.Count)];
        
        if (uniqueChildrenCombinations.ContainsKey(randomMaterial))
        {
            List<string> hatsOfMaterial = uniqueChildrenCombinations[randomMaterial];
            string hat = hatsOfMaterial[Random.Range(0, hatsOfMaterial.Count)];
            hatsOfMaterial.Remove(hat);
            
            if (hatsOfMaterial.Count == 0)
            {
                uniqueChildrenCombinations.Remove(randomMaterial);
            }
            
            return new KeyValuePair<Material, string>(randomMaterial, hat);
        }

        return GetRandomCombination();
    }
}
