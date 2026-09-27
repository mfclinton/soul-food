# Soul Food

Your customers are hungry and the menu is monsters. Summon them with spell combos, then fight them and cook them into what was ordered.

- Play: [itch.io](https://unitedfailures.itch.io/soul-food)
- Made: April 2024 for Ludum Dare 55
- Team: [@mfclinton](https://github.com/mfclinton) (programming), [@DavidKoleczek](https://github.com/DavidKoleczek) (programming), [@antoinerosselli](https://github.com/antoinerosselli) (programming), [Blue](https://sovereignblue.artstation.com/) (art), [@MrAozora](https://github.com/MrAozora) (music)
- Engine: Unity, C#

This is an export of a private repo with only the code we wrote. Art, audio, the Unity project files, and third party plugins aren't included. The history is squashed into one commit.

## What I built

- I built the kitchen as a set of stations wired together in the editor, where each station lists which direction or action leads to the next one. The same keys do different things at each station. WASD walks you around the kitchen but enters spell combos at the spell station, and space attacks in a fight and cooks at the cooking station. The camera glides between stations, and props fade, animate, or appear as you move.
- Spells and recipes are both ScriptableObjects that check for a match and then run their effect. A spell matches your directional combo and summons a monster, and a recipe matches the monsters you've defeated and serves the dish. They all load when the game starts, so adding a spell or a dish is just making a new asset.
- Orders come in at random times, up to three at once, each with its own timer, reward, and penalty.
- In a fight, the monsters you summoned line up and each hit lands on the one in front with a red flash, a shake, and a draining health bar. Clearing them all ends the fight.
