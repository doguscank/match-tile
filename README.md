# Match Tile

Match Tile is a case study given by Trifles Games. In this game, user tries to match 3 tiles with same color on the tile bar. Tile bar has 7 slots and when all slots are full with no matches, user loses the level. User wins the level by making all matches and removing all tiles on grid. There is haptic feedback upon tapping on a tile.

## Tiles
There are 10 different tiles. Each tile has different color. A tile locks itself whenever there is a layer on the upper layer. Layer is simply set using z-axis. Lower layer means higher tile.

## Power-ups
User can use 3 power-ups:
* Redo
* Shuffle
* Helper

Redo power-up simply rewinds latest move. Shuffle power-up is shuffling the tiles by random. Helper power-up makes a match with tiles on tile bar. If there is no tile on the tile bar, it will make a random match.

## Levels
There are 5 levels available. Player wins 100 coins and 1 star upon completion of a level. After completing all, a reset button appears on the main menu and levels can be restarted. Coins and stars are not restarted.

## Level Editor
Another feature is the level editor. Level editor can be activated from code. `editor_mode` flag in Game Manager will activate the level editor. There is an out-of-camera debug tile that will help you to see which tile you selected. You can switch between tiles by pressing 9 and 0. Also you can change layers by pressing up arrow and down arrow buttons.

Created levels can be saved using `Save Level` button which activates only in editor mode. Levels are stored in JSON format.
