using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using XRL;
using XRL.Core;
using UnityEngine;

[HasGameBasedStaticCache]
public class IndividualTileScaling
{
    public static int TileHeight => tileHeight;
	
	private static int tileHeight = 16;
	
    public static void Reset()
    {
		GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager)
        {
			tileHeight = gameManager.tileHeight;
            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < 25; j++)
                {
					var conchar = gameManager.ConsoleCharacter[i, j];
                    if (conchar != null)
                    {
						var sts = conchar.gameObject.GetComponent<SingleTileScaler>();
						if(!sts)
							sts = conchar.gameObject.AddComponent<SingleTileScaler>();                        
                    }
                }
            }
        }
        else
            Debug.Log("TileScaling: Where is the GameManager?");
    }
}

public class SingleTileScaler : MonoBehaviour
{	
    private ex3DSprite2 spriteRenderer;
	
    private void Start()
    {
        spriteRenderer = GetComponent<ex3DSprite2>();		
    }

    private void Update()
    {
		//Object textures have no names	
        if (!spriteRenderer.textureInfo.texture.name.Contains("Dynamic") && spriteRenderer.textureInfo.texture.height != IndividualTileScaling.TileHeight)
        {			
			//Hack - decreasing object scale creates a weird dark overlay over tile. Having negative scale and rotation object 180 degrees fixes the issue.			
			spriteRenderer.scale = -(float)IndividualTileScaling.TileHeight / (float)spriteRenderer.textureInfo.texture.height;
			spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
		else if (spriteRenderer.transform.rotation != Quaternion.identity)
		{
			spriteRenderer.transform.rotation = Quaternion.identity;
		}
    }
}