public class ScriptStructure
{
    /*
     
    Player: 
        PlayerNav: Controls movement, can enable it or disable it from other scripts and has the animator. Uses the camManager and the dialogueManager
        ***Have to set a point to go to if i click the puzzle, so the character doesnt block vision

        ItemPickup: To save the item that is going to be picked up with a click, detects the click and saves it to collect to the inventoryManager when gets close

        Player_Interact: Selects the item from the inventory, so it can be used on a puzzlePiece

        PlayerDoor: Saved the door on click that is going to cross, and cross it when approaches. Uses the playerNav and the camManager

        PlayerPuzzle: Detects the click on a puzzle to zoom in and out, and trigger the dialogue if needed. Uses the camManager and the dialogue

        PlayerDialogue: To disable movement from playerNav when a dialogue is running. (could be changed of script)

    GameManager:
        GameManager: Pause menu and quit to menu button.

        RoomManager: Only for loading screen, for now useless.

    Camera: 
        CamManager: To change the camera of cinemachine. Also for the camShake

    Puzzles
    ------
    Pieces:
        PuzzleManager: Checks if all the parts are complete

        PuzzlePiece: Needs a correct item. Can be accessed to check the player's item and then checks in the puzzleManager if all are complete.

        PuzzleDetector: Only saves the index of the camera to focus on the puzzle.

        ItemController: On the item to be picked from the scenary. Only saves the scriptable object item, so it can be added to the inventory

        Highlight: In everything that needs a highlight, to change color when hovering on it.

    ElectricityPipes:
        RayOrigin: When mouse clicks, activates a temporary ray to activate the connected pipes.

        RayReceiver: If the origin is on (only for a moment when clicked), if it gets ray input, outputs a ray to the top and bottom for the next receiver that does the same.

        Turner: If a object with this script is clicked, changes the rotation 90 grades, the initial rotation has to be set in the inspector.

    CablePlug:
        Cable: If there is no cable selected, selects this cable and holds it to follow the mouse and changes the layer to ignore further clicks while holding it.
                If right click, deselects it and leaves it there. Also on selecting it, unplugs it from the socket if it was plugged.
        
        CableSocket: If there is no cable already socketed and the player is holding a cable, on clicking the socket, the cable gets plugged in and released it from holding.
                    If a cable was socketed, it checks if all the sockets are correct.

        PlayerCable: Updates the position of the object, so it moves the cable when holding one. Needs to be placed on a empty "hand"
    ------

    Doors:
        Door: Has an index related to the camera to go to, and a position to go to after crossing it.

    Inventory:
        InventoryManager_v2: Takes an instance of an inventory_v2. Clears the inventory_v2 at awake. Adds and removes items from Inventory_v2. Opens the inventory and generates the
                                icons depending on the inventory items (maybe should block other scripts like movement or clicking when inventory is open)

        Inventory_v2: holds a list of items to be accessed from different places.

        Item: has the item info like the sprite and name, and has a function to select for puzzle usage.

    */
}
