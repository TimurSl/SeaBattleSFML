using SeaBattleSFML.Core;
using SeaBattleSFML.Core.Types;
using SeaBattleSFML.Input;
using SeaBattleSFML.Objects;


GameLaunchParams gameLaunchParams = new GameLaunchParams ();

gameLaunchParams.Players = new List<Player> ();

gameLaunchParams.Players.Add (new Player ("Player 1", new PlayerInput ()));
gameLaunchParams.Players.Add (new Player ("Player 2", new BotInput ()));

Game game = new Game (gameLaunchParams);
game.Run ();