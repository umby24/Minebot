# C# Minebot

C# Minebot is the complete, ground up rewrite and successor to VB Minebot. 

C# Minebot utilizes the Wrapped socket library for connections with the server, and to handle all 
encrypted communications as well.

Currently supports Minecraft 1.4.7. does **NOT** support snapshots, and likely never will.

This project is only worked on in my free time, and thus is not in active development most of the time.

## Current Features

### IRC Proxy

C# Minebot can be used as a chat proxy between minecraft servers and IRC Channels. You can find the settings for what IRC server,
channel, and nickname you would like the bot to use in the settings form. Settings will save as you type them.

The IRC proxy has 3 different functional modes. 1, 2, and 3.

Mode 1 - Chat will only flow from IRC channel to the minecraft server.

Mode 2 - Chat will only flow from Minecraft server to the IRC channel.

Mode 3 - Chat will flow in both directions;

If there is a message you don't want the bot to send to the irc channel, simply prefix it with =.

The proxy will accept a limited number of commands from the IRC channel. These commands are =say, =ssay, and =help.

=say will cause the bot to say the message you provide in irc

=ssay (server say) will cause the bot to say the message you provide in the minecraft server.

=help will print a message giving version information, and the 3 acceptable commands.


### Inventory Viewing

C# Minebot can now show you all of the inventory items that you have in game, as well as the ammount you have of each of these items.

You can use commands in game to drop the entire stack of whatever item you may have. 

Simply preform the +drop command, with the arugument of the slot number for the item. (slot numbers are the numbers before the name of each item in the list)

To access the list of items, click on the menu item "Option", and then click "Inventory".

### Other Commands

This is the full list of commands that C# Minebot will accept from in game, or from the bot's console if prefixed with a +.

Note that all commands listed below to activate must be prefixed by a '+' by default, which can be changed from the settings menu.

	say [message] -- Make the bot repeat [message]
	irc [mode] -- [mode] may be 0,1,2,3. 0 is off, 1,2,3 are described above under IRC Proxy
	hold [0-8] -- Will make the bot change which item is selected on the hot bar.
	mute -- Will mute all chat messages the bot sends.
	yaw [number] -- Will change the bot's yaw (body position) to [number] (Range is 0-360)
	pitch [number] -- Will change the bot's pitch (head position) to [number] (range is 0-360) (Try 180 for an upside down head!)
	move [x|y|z] [-10-10] -- Will make the bot move in the specified direction by the ammount given (Max of 10 blocks in any direction at one time)
	drop [number] -- Will make the bot drop the item contained in your inventory at slot [number].
	pos -- make's the bot print it's current position.



## Developers and Credits

Current Developer is Umby24.

Those whose work has assisted include SinZ (Github: SinZ167) and SirCmpwn

The socket libary used in C# Minebot (Wrapped) is a port of SinZ' own project, SinZationalSockets to VB.NET.

Encryption support in this bot as required to communicate with minecraft servers was achieved thanks to SirCmpwn's project SMProxy.