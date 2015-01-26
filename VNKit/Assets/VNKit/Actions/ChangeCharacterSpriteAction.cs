using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
	public class ChangeCharacterSpriteAction : Action
	{
		public Character Character;
		public Sprite DifferentSprite;
		
		public override void Trigger()
		{
			SpriteRenderer renderer = Character.GetComponent<SpriteRenderer> ();

			List<Sprite> characterSprites = Character.sprites;

			foreach(Sprite s in characterSprites)
			{
				if(s.name == DifferentSprite.name)
				{
					renderer.sprite = DifferentSprite;
					return;
				}
			}

			Debug.LogWarning ("Sprite not found in Character's sprite list; sprite not changed!");
		}
	}
}