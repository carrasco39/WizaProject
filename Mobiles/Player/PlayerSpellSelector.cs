using BeheaderTavern.Scripts.Interfaces;
using System.Collections.Generic;
using BeheaderTavern.Scripts.Spells;
using UnityEngine;

namespace BeheaderTavern.Scripts.Mobiles.Player
{
    [System.Serializable]
    public class PlayerSpellSelector : IPlayerComponent
    {
        public SpellBase selectedSpell; 
        public SpellBookmarkUI spellBookmark;
        private List<SpellBase> _spells; //TODO: create spellbook class
        private int _selectedIndex;

        public void Start(PlayerActor actor)
        {
            _spells = CreateSpells();
            _selectedIndex = 0;
             spellBookmark = GameObject.FindObjectOfType<SpellBookmarkUI>();
            spellBookmark.InitializeComponents(GetSpellIcons(), _selectedIndex);

        }

        Sprite[] GetSpellIcons()
        {
            var sprites = new Sprite[_spells.Count];
            for(int i =0; i < _spells.Count; i++)
            {
                sprites[i] = _spells[i].SpellProps.Icon;
            }

            return sprites;

        }

        List<SpellBase> CreateSpells()
        {
            var spellList = new List<SpellBase>();
            spellList.Add(new FireballSpell());
            spellList.Add(new MinorHealSpell());
            spellList.Add(new FireWallSpell());

            return spellList;
        }
        public void Update()
        {
            selectedSpell = _spells[spellBookmark.GetCurrentSpellIndex()];
            UpdateCooldowns();
        }

        public void Prior()
        {
            spellBookmark.Prior();
        }

        public void Next()
        {
            spellBookmark.Next();
        }

        void UpdateCooldowns()
        {
            for(int i =0; i < _spells.Count; i++)
            {
                var spell = _spells[i];
                if(spell.SpellProps.CoolDownTimer > 0)
                {
                    spell.SpellProps.CoolDownTimer -= Time.deltaTime;

                }
                spellBookmark.UpdateCooldownIcon(i, spell.SpellProps.CoolDownTimer, spell.SpellProps.CoolDownDuration);
            }
        }

    }
}

