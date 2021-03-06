# MonsterApp
This is a console application game that I created that allows the user to create a character to fight off monsters in a battle simulation.
The user can choose between four different classes each with their own stats. The user can name their hero and then are presented with a random
monster generated from an api. A battle starts between the monster and the player. The player can choose to make a basic attack, use an ability, 
use a health potion, or quit and save their game. The player must win five increasingly difficult battles to win, or be defeated by the monster and lose.

To run this program, the only requirement is an internet connection to be able to access the api https://api.open5e.com/monsters.

Features included in this program:

- A master loop where users can repeated input commands/actions and includes an option to quit and save the user's game.
- Classes that inherit one or more property from its parent
- Create a dictionary, populate it with values, retrieve its values, and use it in the program
- Implement a log that records invalid inputs
- Read data from an external JSON file
- Connect to an external/3rd party API and read data to the app
- Use a LINQ query to retrieve information from a data structure
